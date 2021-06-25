using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LicensingLib;


namespace EspaLib
{
    public partial class ESPA
    {
        #region events
        public event EspaDataReceivedHandler EspaDataReceivedEvent;
        public event UpdateStatusCommunicationLineHandler UpdateStatusCommunicationLineEvent;
        public event UpdateTrafficLogHandler UpdateTrafficLogEvent;
        #endregion events

        #region member data
        private EspaDeviceStatus espadevicestatus = new EspaDeviceStatus();
        private EspaDeviceConfiguration espadeviceconfiguration = new EspaDeviceConfiguration();
        private Licensing licensing = new Licensing();
        private DateTime lastvalidtransactiontimestamp = new DateTime();
        private SingleEspaRequest activerequest = null;
        private SerialCommunicator SerialCom = new SerialCommunicator();
 
        private bool KeepAliveSerialSendThread;
        private Thread SerialSendThread = null;
   
        private bool KeepAliveAnalyerThread;
        private Thread AnalyerThread = null;    

        private object EspaDataOutputQueueLocker = new object();
        private object EspaDataInputQueueLocker = new object();
        private bool CanSendNewEspaDataOutputQueueContent = false;

        private bool KeepAliveControlStationThread;
        private Thread ControlStationThread = null; 
        private bool KeepAliveStationThread;
        private Thread StationThread = null;
        #endregion

        #region properties
        public Licensing Licensing
        {
            get { return licensing; }
            set { licensing = value; }
        }

        /// <summary>
        /// logs the date of the last legal transaction in the espa communication line
        /// </summary>
        protected DateTime LastValidTransactionTimestamp
        {
            get { return lastvalidtransactiontimestamp; }
            set { lastvalidtransactiontimestamp = value; }
        }

        public String SerialComPortName
        {
            get { return SerialCom.ComPort.PortName; }
        }

        public EspaDeviceStatus DeviceStatus
        {
            get { return espadevicestatus; }
            set { espadevicestatus = value; }
        }

        public EspaDeviceConfiguration DeviceConfiguration
        {
            get { return espadeviceconfiguration; }
            set { espadeviceconfiguration = value; }
        }

        #endregion

        #region input array
        Queue<byte[]> SerialInputQueue = new Queue<byte[]>(); 
        private object InputArrayLocker = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        void AddIntoInputArray(byte[] data)
        {
            if (Monitor.TryEnter(InputArrayLocker, new TimeSpan(0, 0, 15)))
            {
                try
                {
                    SerialInputQueue.Enqueue(data);
                }
                finally
                {
                    Monitor.Exit(InputArrayLocker);                
                }
            }       
        }

        void ResetInputArray()
        {
            if (Monitor.TryEnter(InputArrayLocker, new TimeSpan(0, 0, 15)))
            {
                try
                {
                    SerialInputQueue.Clear();
                }
                finally
                {
                    Monitor.Exit(InputArrayLocker);
                }
            }       
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        bool GetNextDataInInputArray(out byte[] data)
        {
            bool Success = false;

            data = null;
            if (Monitor.TryEnter(InputArrayLocker, new TimeSpan(0, 0, 15)))
            {
                try
                {
                    if (SerialInputQueue.Count>0)
                    {
                        data = SerialInputQueue.Dequeue();
                        Success = true;
                    }
                }
                catch(Exception exc)
                {
                    Success = false;
                    Console.WriteLine(exc.Message);
                }                       
                finally
                {
                    Monitor.Exit(InputArrayLocker);
                }
            }
            return Success;
        }
        #endregion

        #region serial port communication 
        /// <summary>
        ///  builds up a serial connection to another epsa device (network)
        /// </summary>
        /// <param name="ConnPortName"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public eReturnValue Connect(String ConnPortName, int baud, Parity parity, int databits, StopBits stopbits, eEspaDeviceType DeviceSimulationType)
        {
            //check the license first
            if (IsSingleLicenseValid(DeviceSimulationType) == false) return eReturnValue.LicenseError;

            bool Successful = SerialCom.Connect(ConnPortName, baud, parity, databits);

            DeviceConfiguration.EspaLocalDeviceType = DeviceSimulationType;

            if (Successful)
            {
                AnalyerThread = new Thread(() => ReceivedDataAnalyzer());
                AnalyerThread.Start();

                SerialCom.ComPort.StopBits = stopbits;
                SerialCom.SerialDataReceivedEvent += SerialCom_SerialDataReceivedEvent;

                SerialSendThread = new Thread(() => SerialPortSend());
                SerialSendThread.Start();

                switch (DeviceSimulationType)
                {
                    case eEspaDeviceType.None:
                        break;

                    case eEspaDeviceType.Station:
                        StationThread = new Thread(() => SimulateStation());
                        StationThread.Start();    
                        break;

                    case eEspaDeviceType.ControlStation:
                        ControlStationThread = new Thread(() => SimulateControlStation());
                        ControlStationThread.Start();                   
                        break;
                    default:
                        break;
                }
            }
            else
            {
                return eReturnValue.SerialComError;
            }

            return eReturnValue.Ok;
        }

        /// <summary>
        /// returns the serial connection state 
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            if (SerialCom == null) return false;
            return SerialCom.IsConnected;
        }

        /// <summary>
        /// disconnects from the other espa device (network)
        /// </summary>
        public void Disconnect()
        {
            KeepAliveSerialSendThread = false;
            if (SerialSendThread!=null) SerialSendThread.Abort();

            KeepAliveAnalyerThread = false;
            if (AnalyerThread != null) AnalyerThread.Abort();

            switch (DeviceConfiguration.EspaLocalDeviceType)
            {
                case eEspaDeviceType.Station:
                    KeepAliveStationThread = false;
                    if (StationThread != null) StationThread.Abort();
                    break;

                case eEspaDeviceType.ControlStation:
                    KeepAliveControlStationThread = false;
                    if (ControlStationThread != null) ControlStationThread.Abort();
                    break;
            }

            if (IsConnected())
            {
                SerialCom.SerialDataReceivedEvent -= SerialCom_SerialDataReceivedEvent;
            }
            SerialCom.Disconnect();
        }

        /// <summary>
        /// returns the active connection settings in a string
        /// </summary>
        /// <returns></returns>
        public String ConnectionInfoString()
        {
            if (SerialCom == null) return "<no connection info available>";

            String ConnectionInfoString = "";

            ConnectionInfoString = String.Format("PRT={0} BDR={1} PTY={2} DTB={3} STB={4}", SerialCom.ComPort.PortName,
                                                                                              SerialCom.ComPort.BaudRate,
                                                                                              SerialCom.ComPort.Parity,
                                                                                              SerialCom.ComPort.DataBits,
                                                                                              SerialCom.ComPort.StopBits);
            return ConnectionInfoString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialCom_SerialDataReceivedEvent(object sender, SerialDataReceivedArgs e)
        {
            HandleReceivedData(e.ReceivedData, e.DataLength, e.Date);
        }

        /// <summary>
        /// INACTIVE: simulate the send data also as data received if we're in the simulation mode (the control station must read it's own out data as in data for polls and selects)
        /// </summary>
        /// <param name="RequestType"></param>
        /// <param name="data"></param>
        private void SimulateDataReceived(eEspaRequestType RequestType, object data)
        {
/*
            if (DeviceConfiguration.EspaLocalDeviceType != eEspaDeviceType.ControlStation) return;

            switch (RequestType)
            {
                case eEspaRequestType.Poll:
                    if (data != null && data is EspaPollSequence)
                    {
                        SimulateDataReceivedEvent(new SerialDataReceivedArgs(((EspaPollSequence)data).SequenceData, ((EspaPollSequence)data).SequenceData.Length, DateTime.Now));                      
                    }
                    break;

                case eEspaRequestType.Select:
                    if (data != null && data is EspaSelectSequence)
                    {
                        SimulateDataReceivedEvent(new SerialDataReceivedArgs(((EspaSelectSequence)data).SequenceData, ((EspaSelectSequence)data).SequenceData.Length, DateTime.Now));                        
                    }
                    break;

                case eEspaRequestType.RecordDataBlock:
                    if (data != null && data is SingleEspaDataBlock)
                    {
                        byte[] Structure;
                        int StructurePointer;
                        if (SingleEspaDataBlock.GetDataStream(data as SingleEspaDataBlock, out Structure, out StructurePointer)==false)
                        {

                        }
                        SimulateDataReceivedEvent(new SerialDataReceivedArgs(Structure as byte[], StructurePointer+1, DateTime.Now));                        
                    }
                    break;

                case eEspaRequestType.ControlSign:
                    if (data != null && data is byte[])
                    {
                        SimulateDataReceivedEvent(new SerialDataReceivedArgs(data as byte[], (data as byte[]).Length, DateTime.Now));                        
                    }
                    break;
            }
 */ 
        }

        /// <summary>
        /// simulates an SerialDataReceivedEvent so the Master Station can poll itself
        /// </summary>
        /// <param name="e"></param>
        private void SimulateDataReceivedEvent(SerialDataReceivedArgs e)
        {
            Logfile.LogDataStream(DeviceConfiguration.EspaLocalDeviceType, false, e.ReceivedData, "Simulate");
            HandleReceivedData(e.ReceivedData, e.DataLength, e.Date);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReceivedData"></param>
        /// <param name="DataLength"></param>
        private void LogReceivedData(byte[] ReceivedData, int DataLength)        
        {
/*
            if (false)
            {
                SingleEspaTrafficInfo LoggingInput = new SingleEspaTrafficInfo
                {
                    TrafficContent = ConvertToReadableEspa(ReceivedData, DataLength),
                    Direction = eTrafficDirection.Receive,
                    TrafficDate = DateTime.Now
                };
                Raise_UpdateTrafficLogEvent(new UpdateTrafficLogArgs(LoggingInput));
            }
 */ 
        }

        /// <summary>
        /// thread for analyzing of the incoming data stream
        /// </summary>
        private void ReceivedDataAnalyzer()
        {
            Console.WriteLine("START RDA");
            KeepAliveAnalyerThread = true;
            ResetInputArray();

            bool bWaitForBCC = false;
            byte[] AnalyzeArray = new byte[10000];
            int AnalyzeArrayPointer = 0;
            byte[] newdata;

            while (KeepAliveAnalyerThread)
            {
                if (GetNextDataInInputArray(out newdata) == false)
                {
                    Thread.Sleep(100);
                    continue;
                }

                foreach (var newByte in newdata)
                {
                    if (newByte == (byte)eAsciiCtrl.EOT)
                    {
                        HandleResponse_EOT();

                        // clear the analyze array
                        Array.Clear(AnalyzeArray, 0, AnalyzeArray.Length);
                        AnalyzeArrayPointer = 0;
                        continue;
                    }

                    switch (DeviceStatus.DeviceState)
                    {
                        case eEspaDeviceState.Idle:
                            {
                                ReceivedDataAnalyzer_DeviceState_Idle(newByte, ref AnalyzeArray, ref AnalyzeArrayPointer);
                            }
                            break;

                        case eEspaDeviceState.TemporaryMasterStation:
                            {
                                ReceivedDataAnalyzer_DeviceState_TemporaryMasterStation(newByte, ref AnalyzeArray, ref AnalyzeArrayPointer);
                            }
                            break;

                        case eEspaDeviceState.SlaveStation:
                            {
                                ReceivedDataAnalyzer_DeviceState_SlaveStation(newByte, ref AnalyzeArray, ref AnalyzeArrayPointer, ref bWaitForBCC);
                            }
                            break;
                    }
                }
            }

            Console.WriteLine("END   RDA");
        }

        /// <summary>
        /// analyzing of the incoming data stream in DeviceState: 'Idle'
        /// </summary>
        /// <param name="data"></param>
        /// <param name="AnalyzeData"></param>
        /// <param name="AnalyzeDataPointer"></param>
        private void ReceivedDataAnalyzer_DeviceState_Idle(byte data, ref byte[] AnalyzeData, ref int AnalyzeDataPointer)
        {
            switch (data)
            {
                case (byte)eAsciiCtrl.ACK:
                    HandleResponse_ACK();
                    break;

                case (byte)eAsciiCtrl.NAK:
                    HandleResponse_NAK();
                    break;

                case (byte)eTraCtrlPrefix.Busy:
                    AnalyzeData[AnalyzeDataPointer] = data;
                    AnalyzeData[AnalyzeDataPointer + 1] = 0;
                    AnalyzeDataPointer++;
                    break;

                case (byte)eTraCtrlPrefix.InvalidMessage:
                    AnalyzeData[AnalyzeDataPointer] = data;
                    AnalyzeData[AnalyzeDataPointer + 1] = 0;
                    AnalyzeDataPointer++;
                    break;

                case (byte)eTraCtrlPrefix.TransmissionError:
                    AnalyzeData[AnalyzeDataPointer] = data;
                    AnalyzeData[AnalyzeDataPointer + 1] = 0;
                    AnalyzeDataPointer++;
                    break;

                case (byte)eAsciiCtrl.ENQ:
                    {
                        AnalyzeData[AnalyzeDataPointer] = data;
                        AnalyzeData[AnalyzeDataPointer + 1] = 0;
                        AnalyzeDataPointer++;

                        if (IsSelectSequenceLegal(AnalyzeData, AnalyzeDataPointer))
                        {
                            if (IsSelectSequenceForMe(AnalyzeData, AnalyzeDataPointer, (byte)DeviceConfiguration.EspaMyLocalAddress) == true)
                            {
                                byte TempMasterStationAddr = 0;
                                byte SlaveStationAddr = 0;
                                GetSelectSequenceAddresses(AnalyzeData, AnalyzeDataPointer, out TempMasterStationAddr, out SlaveStationAddr);

                                if (DeviceConfiguration.IsActiveRejectSelectRequest)
                                {
                                    Logfile.WriteLog(DeviceConfiguration.EspaLocalDeviceType, "Select rejected");
                                    // Select rejected
                                    SendResponse(eAsciiCtrl.EOT);
                                }
                                else
                                {
                                    Logfile.WriteLog(DeviceConfiguration.EspaLocalDeviceType, "Select accepted");
                                    // Select accepted
                                    SendResponse(eAsciiCtrl.ACK);
                                    SetEspaDeviceStatus(eEspaDeviceState.SlaveStation, (char)TempMasterStationAddr, (char)SlaveStationAddr);

                                    // prepare for a new data package
                                    AnalyzeDataPointer = 0;
                                }
                            }
                        }

                        if (IsPollingLegal(AnalyzeData,AnalyzeDataPointer))
                        {
                            if (IsPollingForMe(AnalyzeData,AnalyzeDataPointer,(byte)DeviceConfiguration.EspaMyLocalAddress))
                            {
                                if (DeviceConfiguration.IsActiveRejectPollRequest)
                                {
                                    Logfile.WriteLog(DeviceConfiguration.EspaLocalDeviceType, "Poll reject");
                                    // okay we reject the sending of data so we sent an EOT
                                    // back to the central station
                                    SendResponse(eAsciiCtrl.EOT);
                                }
                                else
                                {
                                    // do the sending
                                    if (IsEspaAddressLegal((byte)DeviceStatus.PendingRecordDataReceiverAddress))
                                    {
                                        Logfile.WriteLog(DeviceConfiguration.EspaLocalDeviceType, "Poll accept send the select");
                                        SetEspaDeviceStatus(eEspaDeviceState.TemporaryMasterStation, DeviceConfiguration.EspaMyLocalAddress);

                                        // it seems as that we have some data to send
                                        //activerequest.ReceiverAddress    
                                        activerequest = new SingleEspaRequest(eEspaRequestType.Select, new EspaSelectSequence(DeviceConfiguration.EspaMyLocalAddress, (char)DeviceStatus.PendingRecordDataReceiverAddress));
                                        SendRequest(eEspaRequestType.Select, activerequest.Data);
                                    }
                                    else
                                    {
                                        Logfile.WriteLog(DeviceConfiguration.EspaLocalDeviceType, "Poll EOT");
                                        // we really have nothing to send so we send back an EOT
                                        SendResponse(eAsciiCtrl.EOT);
                                    }
                                }
                            }
                            else
                            {
                                // it's not for me, but we have a new 'Temporary Master Station' (till the next <EOT> comes)
                                byte TempMasterStationAddr = 0;
                                ESPA.GetPollSequenceAddress(AnalyzeData, AnalyzeDataPointer, out TempMasterStationAddr);
                                DeviceStatus.TemporaryMasterStationAddress = (char)TempMasterStationAddr;
                            }
                        }                    
                    }
                    break;

                default:
                    {
                        if ((data >= (byte)eEspaStandards.LowestAddress) && (data <= (byte)eEspaStandards.HighestAddress))
                        {
                            AnalyzeData[AnalyzeDataPointer] = data;
                            AnalyzeData[AnalyzeDataPointer + 1] = 0;
                            AnalyzeDataPointer++;
                        }
                    }
                    break;              
            }
        }

        /// <summary>
        /// analyzing of the incoming data stream in DeviceState: 'TemporaryMasterStation'
        /// </summary>
        /// <param name="data"></param>
        /// <param name="AnalyzeData"></param>
        /// <param name="AnalyzeDataPointer"></param>
        private void ReceivedDataAnalyzer_DeviceState_TemporaryMasterStation(byte data, ref byte[] AnalyzeData, ref int AnalyzeDataPointer)
        {
            switch (data)
            {
                case (byte)eAsciiCtrl.ACK:
                    HandleResponse_ACK();
                    break;

                case (byte)eAsciiCtrl.NAK:
                    HandleResponse_NAK();
                    break;

                case (byte)eTraCtrlPrefix.Busy:
                    AnalyzeData[AnalyzeDataPointer] = data;
                    AnalyzeData[AnalyzeDataPointer + 1] = 0;
                    AnalyzeDataPointer++;
                    break;

                case (byte)eTraCtrlPrefix.InvalidMessage:
                    AnalyzeData[AnalyzeDataPointer] = data;
                    AnalyzeData[AnalyzeDataPointer + 1] = 0;
                    AnalyzeDataPointer++;
                    break;

                case (byte)eTraCtrlPrefix.TransmissionError:
                    AnalyzeData[AnalyzeDataPointer] = data;
                    AnalyzeData[AnalyzeDataPointer + 1] = 0;
                    AnalyzeDataPointer++;
                    break;
            }
        }

        /// <summary>
        /// analyzing of the incoming data stream in DeviceState: 'SlaveStation'
        /// </summary>
        /// <param name="data"></param>
        /// <param name="AnalyzeData"></param>
        /// <param name="AnalyzeDataPointer"></param>
        /// <param name="bWaitForBCC"></param>
        private void ReceivedDataAnalyzer_DeviceState_SlaveStation(byte data, ref byte[] AnalyzeData, ref int AnalyzeDataPointer, ref bool bWaitForBCC)
        {
            switch (data)
            {
                case (byte)eAsciiCtrl.SOH:
                    AnalyzeData[AnalyzeDataPointer] = data;
                    AnalyzeData[AnalyzeDataPointer + 1] = 0;
                    AnalyzeDataPointer++;
                    break;

                case (byte)eAsciiCtrl.STX:
                    AnalyzeData[AnalyzeDataPointer] = data;
                    AnalyzeData[AnalyzeDataPointer + 1] = 0;
                    AnalyzeDataPointer++;
                    break;

                case (byte)eAsciiCtrl.US:
                    AnalyzeData[AnalyzeDataPointer] = data;
                    AnalyzeData[AnalyzeDataPointer + 1] = 0;
                    AnalyzeDataPointer++;
                    break;

                case (byte)eAsciiCtrl.RS:
                    AnalyzeData[AnalyzeDataPointer] = data;
                    AnalyzeData[AnalyzeDataPointer + 1] = 0;
                    AnalyzeDataPointer++;
                    break;

                case (byte)eAsciiCtrl.ETX:
                    AnalyzeData[AnalyzeDataPointer] = data;
                    AnalyzeData[AnalyzeDataPointer + 1] = 0;
                    bWaitForBCC = true;
                    AnalyzeDataPointer++;
                    break;

                default:
                    if (bWaitForBCC)
                    {
                        bWaitForBCC = false;

                        AnalyzeData[AnalyzeDataPointer] = data;
                        AnalyzeData[AnalyzeDataPointer + 1] = 0;

                        LogReceivedData(AnalyzeData, AnalyzeDataPointer + 1);

                        SingleEspaDataBlock DataBlock;
                        eReturnValue retval = GetEspaDataBlockFromDataStream(AnalyzeData, AnalyzeDataPointer, out DataBlock);

                        if (retval == eReturnValue.Ok)
                        {
                            AddIntoInputQueue(DataBlock);

                            if (DeviceConfiguration.IsActiveRejectReceivedDataBlock)
                            {
                                SendResponse(eAsciiCtrl.NAK, DeviceConfiguration.DataReceivedRejectReasonType);
                            }
                            else
                            {
                                SendResponse(eAsciiCtrl.ACK);
                            }
                        }
                        else
                        {
                            SendResponse(eAsciiCtrl.NAK);
                        }

                        // prepare for a new data package
                        AnalyzeDataPointer = 0;
                    }
                    else 
                    {
                        AnalyzeData[AnalyzeDataPointer] = data;
                        AnalyzeData[AnalyzeDataPointer + 1] = 0;
                        AnalyzeDataPointer++;
                    }
                    break;
            }
        }

        /// <summary>
        /// thread for sending data through the serial port
        /// </summary>
        private void SerialPortSend()
        {
            Console.WriteLine("SerialPortSend start");
            KeepAliveSerialSendThread = true;

            while (KeepAliveSerialSendThread)
            {
                try
                {                                    
                    if (DeviceStatus.EspaDataOutputQueue.IsEmpty()) continue;
                    DeviceStatus.PendingRecordDataReceiverAddress = (char)GetNextReceiverAddressFromOutputQueue();

                    if (DeviceStatus.TemporaryMasterStationAddress != DeviceConfiguration.EspaMyLocalAddress) continue;
                    if (DeviceStatus.SlaveStationAddress != DeviceStatus.PendingRecordDataReceiverAddress) continue;

                    if (CanSendNewEspaDataOutputQueueContent == true)
                    {
                        if (activerequest != null && activerequest.Data is SingleEspaDataBlock)
                        {
                            Console.WriteLine("Problem");
                        }
                        // get the next in the queue without yet removing it
                        CanSendNewEspaDataOutputQueueContent = false;

                        // let's check the ESPA-data-output queue for new data to send
                        activerequest = GetNextFromDataOutputQueue();
                    }

                    if (IsEspaAddressLegal((byte)DeviceConfiguration.EspaMyLocalAddress) == false) continue;
                    if (IsEspaAddressLegal(((SingleEspaDataBlock)activerequest.Data).ReceiverAddress) == false) continue;
            
                    DeviceStatus.PendingRecordDataReceiverAddress = (char)0;

                    // now send the data to the ESPA communication line

                    byte[] cStructure;
                    int iStructurePointer;
                    if (SingleEspaDataBlock.GetDataStream(activerequest.Data as SingleEspaDataBlock, out cStructure, out iStructurePointer) == false)
                    {
                        Console.WriteLine("problem");
                    }

                    SingleEspaTrafficInfo DataBusCommLogEntry;
                    if (SendData(cStructure, iStructurePointer, out DataBusCommLogEntry, true) == true)
                    {
                        Logfile.LogDataStream(DeviceConfiguration.EspaLocalDeviceType, true, cStructure, "SerialPortSend");

                        Raise_UpdateTrafficLogEvent(new UpdateTrafficLogArgs(DataBusCommLogEntry));

                        // now let's check if we have to simulate the response of the data
                        if (DeviceConfiguration.EspaLocalDeviceType == eEspaDeviceType.ControlStation)
                        {
                            if (activerequest != null) SimulateDataReceived(activerequest.Type, activerequest.Data);
                        }
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);           
                }
            }
            Console.WriteLine("SerialPortSend end");
        }

        #endregion serial port communication

        #region simulations
        /// <summary>
        /// simulates the behavior of a 'control station' / 'central station'
        /// </summary>
        private void SimulateControlStation()
        {
            Console.WriteLine("SimulateControlStation started");
            KeepAliveControlStationThread = true;

            // Poll for the next Temporary Master Station
            DateTime LastTemporaryMasterPollTimestamp = DateTime.Now;
            char ActiveTemporaryMasterPollAddress = (char)eEspaStandards.LowestAddress;

            // now let's jump into the control station simulation
            while(KeepAliveControlStationThread)
            {
                if (DeviceStatus.IsActiveWaitingForTransactionsComplete)
                {
                    if (Helper.GetTimeDiff(DateTime.Now, LastValidTransactionTimestamp) > (double)eEspaConstants.ControlStationRegainControlTimout)
                    {
                        Console.WriteLine("no valid transaction within {0} seconds -> terminate and regain control!", (int)eEspaConstants.ControlStationRegainControlTimout);
                        SendResponse(eAsciiCtrl.EOT);
                    }
                }


                if (DeviceConfiguration.IsActivePolling)
                {
                    if (Helper.GetTimeDiff(DateTime.Now,LastTemporaryMasterPollTimestamp) > (double)DeviceConfiguration.PollingInterval)
                    {
                        if (DeviceStatus.SlaveStationAddress <= 0 && DeviceStatus.TemporaryMasterStationAddress <= 0)
                        {
                            if (DeviceConfiguration.PollingAddresses.CanPollAddress(ActiveTemporaryMasterPollAddress))
                            {
                                SendRequest(eEspaRequestType.Poll, new EspaPollSequence(ActiveTemporaryMasterPollAddress));

                                LastTemporaryMasterPollTimestamp = DateTime.Now;

                                if (ActiveTemporaryMasterPollAddress == DeviceConfiguration.EspaMyLocalAddress)
                                {
                                    Console.WriteLine("just polled the 'central station' on address: '{0}'", DeviceConfiguration.EspaMyLocalAddress);
                                    if ((DeviceStatus.EspaDataOutputQueue.IsEmpty() == false) && (DeviceStatus.TemporaryMasterStationAddress != DeviceConfiguration.EspaMyLocalAddress))
                                    {
                                        // let's make me 'temporary master station'
                                        SetEspaDeviceStatus(eEspaDeviceState.TemporaryMasterStation, DeviceConfiguration.EspaMyLocalAddress);

                                        Console.WriteLine("'central station' on address: '{0}' wants to be 'temporary master station'!", DeviceConfiguration.EspaMyLocalAddress);
                                        SendRequest(eEspaRequestType.Select, new EspaSelectSequence(DeviceConfiguration.EspaMyLocalAddress,DeviceStatus.PendingRecordDataReceiverAddress));

                                    }
                                }
                            }

                            // take the next valid Address for polling
                            if (ActiveTemporaryMasterPollAddress == (char)eEspaStandards.HighestAddress)
                            {
                                ActiveTemporaryMasterPollAddress = (char)eEspaStandards.LowestAddress;
                            }
                            else 
                            {
                                ActiveTemporaryMasterPollAddress++;
                            }
                        }
                    }
                }
                           
                Thread.Sleep((int)eEspaConstants.SimulateControlStationInterval);
                                  
                UpdateStatusCommunicationLineArgs args = new UpdateStatusCommunicationLineArgs(DeviceStatus);
                Raise_UpdateStatusCommunicationLineEvent(args);
            }
            Console.WriteLine("SimulateControlStation ended");
        }

        /// <summary>
        /// simulates the behavior of a 'Station'
        /// </summary>
        private void SimulateStation()
        {
            Console.WriteLine("SimulateStation started");
            KeepAliveStationThread = true;

            while (KeepAliveStationThread)
            {

                Thread.Sleep((int)eEspaConstants.SimulateStationInterval);

                UpdateStatusCommunicationLineArgs args = new UpdateStatusCommunicationLineArgs(DeviceStatus);
                Raise_UpdateStatusCommunicationLineEvent(args);
            }
            Console.WriteLine("SimulateStation ended");
        }
        #endregion 

        #region event handler methods
        /// <summary>
        /// fires the 'EspaDataReceivedEvent' event when we received data from the espa communication line
        /// </summary>
        /// <param name="e"></param>
        public void Raise_EspaDataReceivedEvent(EspaDataReceivedArgs e)
        {
            if (EspaDataReceivedEvent != null)
            {
                EspaDataReceivedEvent(this, e);
            }
        }

        /// <summary>
        /// fires the 'UpdateStatusCommunicationLineEvent' event after an change on the espa communication line
        /// </summary>
        /// <param name="e"></param>
        public void Raise_UpdateStatusCommunicationLineEvent(UpdateStatusCommunicationLineArgs e)
        {
            if (UpdateStatusCommunicationLineEvent != null)
            {
                UpdateStatusCommunicationLineEvent(this, e);
            }
        }

        /// <summary>
        /// fires the 'UpdateTrafficLogEvent' event after an update in the espa communication line traffig-log
        /// </summary>
        /// <param name="e"></param>
        public void Raise_UpdateTrafficLogEvent(UpdateTrafficLogArgs e)
        {
            if (UpdateTrafficLogEvent != null)
            {
                UpdateTrafficLogEvent(this, e);
            }
        }   
        #endregion

        #region received data handling
        /// <summary>
        /// handle the last received data from espa data communication line and sends a response if necessary 
        /// </summary>
        /// <param name="ReceivedData"></param>
        /// <param name="Length"></param>
        /// <param name="Date"></param>
        void HandleReceivedData(byte[] ReceivedData, int DataLength, DateTime Date)
        {
            SingleEspaTrafficInfo LoggingInput = new SingleEspaTrafficInfo
            {
                TrafficContent = ConvertToReadableEspa(ReceivedData, DataLength),
                Direction = eTrafficDirection.Receive,
                TrafficDate = Date
            };
            Raise_UpdateTrafficLogEvent(new UpdateTrafficLogArgs(LoggingInput));

            AddIntoInputArray(ReceivedData);
        }
        #endregion 

        #region espa communication line status
        /// <summary>
        /// sets the status of the espa device (idle, temporary master station, slave station)
        /// </summary>
        /// <param name="Status"></param>
        public void SetEspaDeviceStatus(eEspaDeviceState State, char TemporaryMasterAddress = (char)0, char SlaveStationAddress = (char)0)
        {
            DeviceStatus.DeviceState = State;
            switch (State)
            {
                case eEspaDeviceState.Idle:
                    DeviceStatus.SlaveStationAddress = (char)0;
                    DeviceStatus.TemporaryMasterStationAddress = (char)0;
                    LastValidTransactionTimestamp = DateTime.Now;
                    break;
                case eEspaDeviceState.TemporaryMasterStation:
                    if (SlaveStationAddress != (char)0)
                    {
                        DeviceStatus.SlaveStationAddress = SlaveStationAddress;
                    }
                    DeviceStatus.TemporaryMasterStationAddress = DeviceConfiguration.EspaMyLocalAddress;
                    DeviceStatus.IsActiveWaitingForTransactionsComplete = true;
                    LastValidTransactionTimestamp = DateTime.Now;
                    break;
                case eEspaDeviceState.SlaveStation:
                    DeviceStatus.SlaveStationAddress = SlaveStationAddress;
                    DeviceStatus.TemporaryMasterStationAddress = TemporaryMasterAddress;
                    DeviceStatus.IsActiveWaitingForTransactionsComplete = true;
                    LastValidTransactionTimestamp = DateTime.Now;
                    break;
                default:
                    break;
            }

            // Raise_EspaDeviceStatusUpdate(StatusUpdate);
        }
        #endregion

        #region response handling
        /// <summary>
        /// handle the received 'end of transmission' <EOT>
        /// </summary>
        private void HandleResponse_EOT()
        {
            switch (DeviceStatus.DeviceState)
            {
                case eEspaDeviceState.Idle:
                    SetEspaDeviceStatus(eEspaDeviceState.Idle);
                    break;
                case eEspaDeviceState.TemporaryMasterStation:
                    SetEspaDeviceStatus(eEspaDeviceState.Idle);
                    break;
                case eEspaDeviceState.SlaveStation:
                    SetEspaDeviceStatus(eEspaDeviceState.Idle);
                    break;
                default:
                    break;
            }

            DeviceStatus.IsActiveWaitingForTransactionsComplete = false;
            DeviceStatus.IsActiveWaitingForAcknowledge = false;
            DeviceStatus.LastDataSentRetryCount = 0;

            ResetInputArray();
        }

        /// <summary>
        /// handle the received 'acknowledge' <ACK>
        /// </summary>
        private void HandleResponse_ACK()
        {
            if (activerequest == null) return;
            if (activerequest.Type == eEspaRequestType.None) return;

            // we have to check what type of request has been acknowlegded
            Console.WriteLine("ACK for {0}", activerequest.Type);
            DeviceStatus.IsActiveWaitingForAcknowledge = false;
            DeviceStatus.LastDataSentRetryCount = 0;
            switch (activerequest.Type)
            {
                case eEspaRequestType.Poll:
                    if (activerequest.Data is EspaPollSequence)
                    {
                        Console.WriteLine("HandleResponse_ACK Poll");
                    }
                    break;

                case eEspaRequestType.Select:
                    if (activerequest.Data is EspaSelectSequence)
                    {
                        SetEspaDeviceStatus(eEspaDeviceState.TemporaryMasterStation, ((EspaSelectSequence)activerequest.Data).TemporaryMasterAddress, ((EspaSelectSequence)activerequest.Data).SlaveStationAddress);
                        CanSendNewEspaDataOutputQueueContent = true;
                        Console.WriteLine("HandleResponse_ACK Select");
                    }
                    LastValidTransactionTimestamp = DateTime.Now;
                    break;
                case eEspaRequestType.RecordDataBlock:
                    if (activerequest.Data is SingleEspaDataBlock)
                    {
                        Console.WriteLine("HandleResponse_ACK RecordDataBlock");
                        if (activerequest.SendEndOfTransmissionAfterSuccess)
                        {
                            SendResponse(eAsciiCtrl.EOT);
                        }
                        else
                        {
                            CanSendNewEspaDataOutputQueueContent = true;

                            // when your output queue is empty (just received the ACK of the last data set) send an <EOT>
                            if (DeviceStatus.EspaDataOutputQueue.IsEmpty())
                            {
                                SendResponse(eAsciiCtrl.EOT);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            
            activerequest = null;          
        }

        /// <summary>
        /// handle the received 'not acknowledge' <NAK>
        /// </summary>
        private void HandleResponse_NAK()
        {
            if (activerequest == null) return;
            if (activerequest.Type == eEspaRequestType.None) return;

            // we have to check what type of request has been rejected
            Console.WriteLine("NAK for {0}", activerequest.Type);

            if (activerequest.Data is SingleEspaDataBlock)
            {
                if (DeviceStatus.LastDataSentRetryCount < DeviceConfiguration.MaxDataBlockReTransmit)
                {
                    Console.WriteLine("'Temporary Master Station' retry sending the block Nr.{0} / {1}!", DeviceStatus.LastDataSentRetryCount, DeviceConfiguration.MaxDataBlockReTransmit);
                    DeviceStatus.LastDataSentRetryCount++;
                    SendRequest(activerequest.Type, activerequest.Data);
                }
                else
                {
                    CanSendNewEspaDataOutputQueueContent = true;
                    Console.WriteLine("'Temporary Master Station' Termination after {0} tries of sending the block!", DeviceStatus.LastDataSentRetryCount);
                    SendResponse(eAsciiCtrl.EOT);
                }
            }
            else
            {
                activerequest = null;
            }                         
        }

        #endregion response handling

        #region sending response
        /// <summary>
        /// sends the response into the espa data communication line
        /// </summary>
        /// <param name="Response"></param>
        /// <param name="RejectReason"></param>
        public void SendResponse(eAsciiCtrl Response, eTraCtrlPrefix RejectReason=eTraCtrlPrefix.Busy)
        {
            byte[] cAnswer = null;

            switch (Response)
            {
                case eAsciiCtrl.EOT:
                    cAnswer = new byte[] { (byte)EspaLib.eAsciiCtrl.EOT };
                    break;

                case eAsciiCtrl.ACK:
                    cAnswer = new byte[] { (byte)EspaLib.eAsciiCtrl.ACK };
                    break;

                case eAsciiCtrl.NAK:
                    cAnswer = new byte[] { (byte)RejectReason, (byte)EspaLib.eAsciiCtrl.NAK };
                    break;

                default:
                    return;
            }

            SingleEspaTrafficInfo LoggingOutput = new SingleEspaTrafficInfo();
            if (SendData(cAnswer, cAnswer.Length, out LoggingOutput) == true)
            {
                Logfile.LogDataStream(DeviceConfiguration.EspaLocalDeviceType, true, cAnswer, "SendResponse");

                LastValidTransactionTimestamp = DateTime.Now;
                Raise_UpdateTrafficLogEvent(new UpdateTrafficLogArgs(LoggingOutput));

                SimulateDataReceived(eEspaRequestType.ControlSign, cAnswer);

                switch (Response)
                {
                    case eAsciiCtrl.EOT:
                        HandleResponse_EOT();
                        break;

                    case eAsciiCtrl.ACK:
                        break;

                    case eAsciiCtrl.NAK:
                        break;
                }
            }
        }
        #endregion 

        #region serial data sending    
        /// <summary>
        /// sends an espa request and logs the event
        /// </summary>
        /// <param name="RequestType"></param>
        /// <returns></returns>
        public bool SendRequest(eEspaRequestType RequestType, object data, bool SendEotAfterSuccess = false)
        {
            bool DoLogging = false;
            SingleEspaTrafficInfo LoggingOutput = new SingleEspaTrafficInfo();

            switch (RequestType)
            {
                case eEspaRequestType.Poll:
                    if (data != null && data is EspaPollSequence)
                    {
                        if (SendData(((EspaPollSequence)data).SequenceData,((EspaPollSequence)data).SequenceData.Length, out LoggingOutput))
                        {
                            Logfile.LogDataStream(DeviceConfiguration.EspaLocalDeviceType, true, ((EspaPollSequence)data).SequenceData, "SendRequest");

                            DoLogging = true;
                            activerequest = new SingleEspaRequest(eEspaRequestType.Poll, data);
                        }
                    }
                    break;

                case eEspaRequestType.Select:
                    if (data != null && data is EspaSelectSequence)
                    {
                        if (SendData(((EspaSelectSequence)data).SequenceData, ((EspaSelectSequence)data).SequenceData.Length, out LoggingOutput))
                        {
                            Logfile.LogDataStream(DeviceConfiguration.EspaLocalDeviceType, true, ((EspaSelectSequence)data).SequenceData, "SendRequest");
                            DoLogging = true;
                            activerequest = new SingleEspaRequest(eEspaRequestType.Select, data);
                            DeviceStatus.IsActiveWaitingForAcknowledge = true;
                        }
                    }
                    break;

                case eEspaRequestType.RecordDataBlock:
                    if (data != null && data is SingleEspaDataBlock)
                    {
                        if (((SingleEspaDataBlock)data).Records.Count <= 0) break;
                        if (((SingleEspaDataBlock)data).HeaderIdentifier <= 0) break;

                        byte[] cStructure;
                        int iStructurePointer;
                        if (SingleEspaDataBlock.GetDataStream(data as SingleEspaDataBlock, out cStructure, out iStructurePointer) == false)
                        {
                            Console.WriteLine("problem");
                        }
                        if (SendData(cStructure,iStructurePointer, out LoggingOutput,true))
                        {
                            Logfile.LogDataStream(DeviceConfiguration.EspaLocalDeviceType, true, cStructure, "SendRequest");
                            DoLogging = true;
                            activerequest = new SingleEspaRequest(eEspaRequestType.RecordDataBlock, data, SendEotAfterSuccess);
                            DeviceStatus.IsActiveWaitingForAcknowledge = true;                       
                        }
                    }
                    break;
            }

            if (DoLogging)
            {
                LastValidTransactionTimestamp = DateTime.Now;
                Raise_UpdateTrafficLogEvent(new UpdateTrafficLogArgs(LoggingOutput));

                // now let's check if we have to simulate the response of the data
                if (DeviceConfiguration.EspaLocalDeviceType == eEspaDeviceType.ControlStation)
                {
                    SimulateDataReceived(RequestType, data);
                }
            }        

            return true; 
        }

        /// <summary>
        /// sends data via serial port (without logging)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool SendData(byte[] data, int count, bool EndsWithCRC = false)
        {
            bool SendSuccessful = false;

            try
            {
                SendSuccessful = SerialCom.SendData(data, count);
            }
            catch (Exception)
            {
                
            }

            return SendSuccessful;
        }

        /// <summary>
        /// sends data via serial port and logs the event
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        /// <param name="LoggingOutput"></param>
        /// <returns></returns>
        public bool SendData(byte[] data, int count,out SingleEspaTrafficInfo LoggingOutput,bool EndsWithCRC=false)
        {
            ///////////////////////////////////////////////////////////////////
            // now send it and wait for the answer
            bool SendSuccessful = SendData(data, count);

            if (SendSuccessful == true)
            {
                LoggingOutput = new SingleEspaTrafficInfo();
                LoggingOutput.TrafficContent = ConvertToReadableEspa(data, count, EndsWithCRC);
                LoggingOutput.Direction = eTrafficDirection.Send;
                LoggingOutput.TrafficDate = DateTime.Now;
            }
            else
            {
                LoggingOutput = null;
            }

            return SendSuccessful;
        }
        #endregion

        #region message out queue
        /// <summary>
        /// adds a new transaction into the output queue (FIFO)
        /// </summary>
        /// <param name="Transaction"></param>
        /// <returns></returns>
        public bool AddIntoOutputQueue(object Transaction)
        {
            if (Monitor.TryEnter(EspaDataOutputQueueLocker, new TimeSpan(0, 0, 15)))
            {
                try
                {
                    DeviceStatus.EspaDataOutputQueue.Add(Transaction);
                }
                finally
                {
                    Monitor.Exit(EspaDataOutputQueueLocker);
                }
            }

            Raise_UpdateStatusCommunicationLineEvent(null);

            return true;
        }

        /// <summary>
        /// gets a new transaction out of the FIFO
        /// </summary>
        /// <returns></returns>
        private SingleEspaRequest GetNextFromDataOutputQueue(bool RemoveIt = true)
        {
            SingleEspaRequest NextRequest = null;

            if (Monitor.TryEnter(EspaDataOutputQueueLocker, new TimeSpan(0, 0, 15)))
            {
                try
                {
                    NextRequest = DeviceStatus.EspaDataOutputQueue.GetNext(RemoveIt) as SingleEspaRequest;
                }
                finally
                {
                    Monitor.Exit(EspaDataOutputQueueLocker);
                }
            }

            return NextRequest;
        }
        /// <summary>
        /// delete the next in the output queue (FIFO)
        /// </summary>
        private void RemoveNextFromOutputQueue()
        {
            if (Monitor.TryEnter(EspaDataOutputQueueLocker, new TimeSpan(0, 0, 15)))
            {
                try
                {
                    DeviceStatus.EspaDataOutputQueue.GetNext();
                }
                finally
                {
                    Monitor.Exit(EspaDataOutputQueueLocker);
                }
            }
        }

        /// <summary>
        /// returns the receiver address of the next transaction
        /// </summary>
        /// <returns></returns>
        public char GetNextReceiverAddressFromOutputQueue()
        {
            if (Monitor.TryEnter(EspaDataOutputQueueLocker, new TimeSpan(0, 0, 15)))
            {
                try
                {
                    SingleEspaRequest NextRequest = DeviceStatus.EspaDataOutputQueue.GetNext(false) as SingleEspaRequest;

                    if (NextRequest != null)
                    {
                        var data = NextRequest.Data as SingleEspaDataBlock;
                        if (data != null)
                        {
                            return (char)data.ReceiverAddress;
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(EspaDataOutputQueueLocker);
                }
            }

            return (char)0;
        }

        #endregion message out queue

        #region message in queue
        /// <summary>
        /// adds a new transaction into the input queue (FIFO)
        /// </summary>
        /// <param name="Transaction"></param>
        /// <returns></returns>
        public bool AddIntoInputQueue(object Transaction)
        {
            if (Monitor.TryEnter(EspaDataInputQueueLocker, new TimeSpan(0, 0, 15)))
            {
                try
                {
                    DeviceStatus.EspaDataInputQueue.Add(Transaction);
                }
                finally
                {
                    Monitor.Exit(EspaDataInputQueueLocker);
                }
            }

            Raise_UpdateStatusCommunicationLineEvent(null);

            return true;
        }

        /// <summary>
        /// gets a new transaction out of the input queue FIFO
        /// </summary>
        /// <returns></returns>
        private SingleEspaRequest GetNextFromDataInputQueue(bool RemoveIt = true)
        {
            SingleEspaRequest NextRequest = null;

            if (Monitor.TryEnter(EspaDataInputQueueLocker, new TimeSpan(0, 0, 15)))
            {
                try
                {
                    NextRequest = DeviceStatus.EspaDataInputQueue.GetNext(RemoveIt) as SingleEspaRequest;
                }
                finally
                {
                    Monitor.Exit(EspaDataInputQueueLocker);
                }
            }

            return NextRequest;
        }
        #endregion message in queue

        #region static methods

        /// <summary>
        /// checks for a legal ESPA-Address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static bool IsEspaAddressLegal(byte Address)
        {
            return (Address >= (byte)eEspaStandards.LowestAddress && Address <= (byte)eEspaStandards.HighestAddress);
        }

        /// <summary>
        /// analyzes the received espa data from the communication line and answers confirming the espa protocol spec.
        /// </summary>
        /// <param name="DataStream"></param>
        /// <returns></returns>
        public static eReturnValue GetEspaDataBlockFromDataStream(byte[] DataStream, int MaxDataStreamSize, out SingleEspaDataBlock EspaDataBlock)
        {
            EspaDataBlock = new SingleEspaDataBlock();

            if (DataStream == null) return eReturnValue.EspaRecordReadError;
            if (DataStream.Length <= 0) return eReturnValue.EspaRecordReadError;

            int DataStreamPointer = 0;

            int RecordCounter = 0;
            int CallAddrCounter = 0;
            int DisplayMessageCounter = 0;
            int BeepCounter = 0;
            int CallTypeCounter = 0;
            int NoOfTransmissionsCounter = 0;
            int PriorityCounter = 0;
            int CallStatusCounter = 0;
            int SystemStatusCounter = 0;


            // it must start with start-of-header
            if ((char)eAsciiCtrl.SOH != DataStream[DataStreamPointer++]) return eReturnValue.EspaRecordReadError;
            if (IsHeaderIdLegal(DataStream[DataStreamPointer]) == false) return eReturnValue.EspaRecordReadError;

            // keep the header identifier
            EspaDataBlock.HeaderIdentifier = (char)DataStream[DataStreamPointer++];

            while ((char)eAsciiCtrl.ETX != DataStream[DataStreamPointer])
            {
                if ((char)eAsciiCtrl.US == DataStream[DataStreamPointer])
                {
                    if (RecordCounter >= (int)eEspaConstants.MaxSizeStructure)
                    {
                        // too many records found in that sequence
                        return eReturnValue.EspaRecordReadError;
                    }

                    char RecordIdentifier = (char)DataStream[DataStreamPointer - 1];
                    if (IsRecordIdLegal(RecordIdentifier) == true)
                    {
                        EspaDataBlock.AddRecord(new SingleEspaRecord
                        {
                            RecordData = "",
                            RecordID = 0
                        });
                        EspaDataBlock.Records[RecordCounter].RecordID = RecordIdentifier;
                        String Data;
                        if (GetDataBlockRecordContent(DataStream, DataStreamPointer, out Data) == false)
                        {
                            return eReturnValue.EspaRecordReadError;
                        }
                        EspaDataBlock.Records[RecordCounter].RecordData = Data;

                        switch (RecordIdentifier)
                        {
                            case (char)eEspaRecordTypes.CallAddress:
                                CallAddrCounter++;
                                break;

                            case (char)eEspaRecordTypes.DisplayMessage:
                                DisplayMessageCounter++;
                                break;

                            case (char)eEspaRecordTypes.BeepCoding:
                                BeepCounter++;
                                break;

                            case (char)eEspaRecordTypes.CallType:
                                CallTypeCounter++;
                                break;

                            case (char)eEspaRecordTypes.NumberOfTransmissions:
                                NoOfTransmissionsCounter++;
                                break;

                            case (char)eEspaRecordTypes.Priority:
                                PriorityCounter++;
                                break;

                            case (char)eEspaRecordTypes.CallStatus:
                                CallStatusCounter++;
                                break;

                            case (char)eEspaRecordTypes.SystemStatus:
                                SystemStatusCounter++;
                                break;
                        }
                        RecordCounter++;
                    }
                }
                DataStreamPointer++;
                if (MaxDataStreamSize-- <= 0)
                {
                    return eReturnValue.EspaRecordReadError;
                }
            }

            Console.WriteLine("Packet with {0} Records:\n", EspaDataBlock.CountRecords);
            Console.WriteLine("{0}  CallAddrCounter:\n", CallAddrCounter);
            Console.WriteLine("{0}  DisplayMessageCounter:\n", DisplayMessageCounter);
            Console.WriteLine("{0}  CallTypeCounter:\n", CallTypeCounter);
            Console.WriteLine("{0}  NoOfTransmissionsCounter:\n", NoOfTransmissionsCounter);
            Console.WriteLine("{0}  PriorityCounter:\n", PriorityCounter);
            Console.WriteLine("{0}  CallStatusCounter:\n", CallStatusCounter);
            Console.WriteLine("{0}  SystemStatusCounter:\n", SystemStatusCounter);

            return eReturnValue.Ok;
        }

        /// <summary>
        /// returns the content of the next record located in a data block
        /// </summary>
        /// <param name="DataStream"></param>
        /// <param name="DataPointer"></param>
        /// <param name="RecordContent"></param>
        /// <returns></returns>
        public static bool GetDataBlockRecordContent(byte[] DataStream, int DataPointer, out String RecordContent)
        {
            RecordContent = "";
            DataPointer++;
            // go through till we are at the end or get one of the seperator/end signs
            while ((DataStream[DataPointer] != 0) && (DataStream[DataPointer] != (char)eAsciiCtrl.RS) && (DataStream[DataPointer] != (char)eAsciiCtrl.ETX))
            {
                RecordContent += Convert.ToChar(DataStream[DataPointer++]);
            }

            return true;
        }

        /// <summary>
        /// calculates the block check character
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="maxPacketSize"></param>
        /// <returns></returns>
        public static int CalculateBCC(byte[] packet, int maxPacketSize)
        {
            int bcc = 0;
            int csum = 0;
            int packetpointer = 0;

            if (packet == null || packet.Length == 0) return 0;
            if ((byte)eAsciiCtrl.SOH != packet[packetpointer++]) return 0;

            while (packet[packetpointer] != (byte)eAsciiCtrl.ETX)
            {
                csum ^= packet[packetpointer++];
                if (maxPacketSize-- <= 0) return 0;
            }

            csum ^= packet[packetpointer++];	/* add the ETX */
            bcc = (csum & 0x7f);

            return bcc;
        }

        /// <summary>
        /// checks the block check character
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="maxPacketSize"></param>
        /// <returns></returns>
        public static int CheckBCC(byte[] packet, int maxPacketSize)
        {
            int res = 0;
            int packetpointer = 0;

            if ((byte)eAsciiCtrl.SOH != packet[packetpointer++]) return 0;

            while (packet[packetpointer] != (byte)eAsciiCtrl.ETX)
            {
                res ^= packet[packetpointer++];
                if (maxPacketSize-- <= 0) return 0;
            }

            res ^= packet[packetpointer++];	/* add the ETX */
            res ^= packet[packetpointer++];	/* add the BCC */
            return ((res & 0x7f) == 0) ? 1 : 0;
        }

        /// <summary>
        /// checks if the polling sequence is legal confirming the espa protocol spec (EOT'Address'ENQ)
        /// </summary>
        /// <param name="DataStream"></param>
        /// <param name="DataPointer"></param>
        /// <returns></returns>
        public static bool IsPollingLegal(byte[] DataStream, int DataPointer)
        {
            // 's'<ENQ>
            if (DataPointer < 1) return false;
            if (DataStream[1] != (byte)eAsciiCtrl.ENQ) return false;
            if (DataStream[0] < (byte)eEspaStandards.LowestAddress || DataStream[DataPointer - 1] > (byte)eEspaStandards.HighestAddress) return false;

            return true;
        }

        /// <summary>
        /// checks if the polling sequence is for me confirming the espa protocol spec (EOT'MyLocaEspaAddress'ENQ)
        /// </summary>
        /// <param name="DataStream"></param>
        /// <param name="DataPointer"></param>
        /// <param name="MyEspaAddress"></param>
        /// <returns></returns>
        public static bool IsPollingForMe(byte[] DataStream, int DataPointer, byte MyEspaAddress)
        {
            // 's'<ENQ>
            if (DataPointer < 1) return false;

            if (DataStream[1] != (byte)eAsciiCtrl.ENQ) return false;
            if (DataStream[0] != MyEspaAddress) return false;

            return true;
        }


        /// <summary>
        /// returns the 'TemporaryMasterStation' Address send in the poll sequence
        /// </summary>
        /// <param name="DataStream"></param>
        /// <param name="DataPointer"></param>
        /// <param name="TemporaryMasterAddress"></param>
        /// <returns></returns>
        public static bool GetPollSequenceAddress(byte[] DataStream, int DataPointer, out byte TemporaryMasterAddress)
        {
            TemporaryMasterAddress = 0;
            // 'm'<ENQ>
            if (DataPointer < 1) return false;
            TemporaryMasterAddress = DataStream[0];

            return true;
        }

        /// <summary>
        /// checks if the select sequence is legal confirming the espa protocol spec (EOT'TemporaryMasterStation'ENQ'SlaveStation'ENQ)
        /// </summary>
        /// <param name="DataStream"></param>
        /// <param name="DataPointer"></param>
        /// <returns></returns>
        public static bool IsSelectSequenceLegal(byte[] DataStream, int DataPointer)
        {
            // 'm'<ENQ>'s'<ENQ>
            if (DataPointer < 3) return false;

            if (DataStream[0] < (byte)eEspaStandards.LowestAddress || DataStream[DataPointer - 1] > (byte)eEspaStandards.HighestAddress) return false;
            if (DataStream[1] != (char)eAsciiCtrl.ENQ) return false;
            if (DataStream[2] < (byte)eEspaStandards.LowestAddress || DataStream[DataPointer - 1] > (byte)eEspaStandards.HighestAddress) return false;
            if (DataStream[3] != (byte)eAsciiCtrl.ENQ) return false;
            return true;
        }

        /// <summary>
        /// checks if the read select sequence has my local espa address as 'slave station' address
        /// </summary>
        /// <param name="DataStream"></param>
        /// <param name="DataPointer"></param>
        /// <param name="MyEspaAddress"></param>
        /// <returns></returns>
        public static bool IsSelectSequenceForMe(byte[] DataStream, int DataPointer, byte MyEspaAddress)
        {
            // 'm'<ENQ>'s'<ENQ>
            if (DataPointer < 3) return false;
            if (DataStream[1] != (char)eAsciiCtrl.ENQ) return false;
            if (DataStream[2] != MyEspaAddress) return false;
            if (DataStream[3] != (byte)eAsciiCtrl.ENQ) return false;

            return true;
        }

        /// <summary>
        /// returns the 'TemporaryMasterStation' and 'Slave Station' Addresses send in the select sequence
        /// </summary>
        /// <param name="DataStream"></param>
        /// <param name="DataPointer"></param>
        /// <param name="MyEspaAddress"></param>
        /// <returns></returns>
        public static bool GetSelectSequenceAddresses(byte[] DataStream, int DataPointer, out byte TemporaryMasterAddress, out byte SlaveStationAddress)
        {
            TemporaryMasterAddress = 0;
            SlaveStationAddress = 0;
            // 'm'<ENQ>'s'<ENQ>
            if (DataPointer < 3) return false;
            TemporaryMasterAddress = DataStream[0];
            SlaveStationAddress = DataStream[2];

            return true;
        }
      
        /// <summary>
        /// checks for a legal espa data record id
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static bool IsRecordIdLegal(char recordId)
        {
            if (recordId == (char)eEspaRecordTypes.CallAddress) return true;
            if (recordId == (char)eEspaRecordTypes.DisplayMessage) return true;
            if (recordId == (char)eEspaRecordTypes.BeepCoding) return true;
            if (recordId == (char)eEspaRecordTypes.CallType) return true;
            if (recordId == (char)eEspaRecordTypes.NumberOfTransmissions) return true;
            if (recordId == (char)eEspaRecordTypes.Priority) return true;
            if (recordId == (char)eEspaRecordTypes.CallStatus) return true;
            if (recordId == (char)eEspaRecordTypes.SystemStatus) return true;

            return false;
        }

        /// <summary>
        /// checks if the espa header id is legal
        /// </summary>
        /// <param name="headerId"></param>
        /// <returns></returns>
        public static bool IsHeaderIdLegal(byte headerId)
        {
            if (headerId == (byte)eEspaHeaderTypes.CallToPager) return true;
            if (headerId == (byte)eEspaHeaderTypes.StatusInformation) return true;
            if (headerId == (byte)eEspaHeaderTypes.StatusRequest) return true;
            if (headerId == (byte)eEspaHeaderTypes.CallToSubscriberLine) return true;
            if (headerId == (byte)eEspaHeaderTypes.OtherInformation) return true;

            return false;
        }

        /// <summary>
        /// returns the name of the record type id (char)
        /// </summary>
        /// <param name="RecordTypeID"></param>
        /// <returns></returns>
        public static String GetRecordTypeName(int RecordTypeID)
        {
            switch (RecordTypeID)
            {
                case 1: return "Call Address";
                case 2: return "Display Text";
                case 3: return "Beep Coding";
                case 4: return "Call Type";
                case 5: return "Number of Transmissions";
                case 6: return "Priority Level";
                case 7: return "Call Status";
                case 8: return "System Status";
                default: return "";
            }
        }

        /// <summary>
        /// converts the real received data into a stream of symbolic signs (ASCII) and marks the BCC
        /// </summary>
        /// <param name="rawespa"></param>
        /// <returns></returns>
        public static String ConvertToReadableEspa(byte[] rawespa, int Length, bool EndsWithCRC = false)
        {
            String ReadableEspa = "";

            if (rawespa == null || rawespa.Length == 0) return ReadableEspa;

            if (Length == -1) Length = rawespa.Length;

            foreach (var item in rawespa)
            {
                int itemnumeric = Convert.ToInt32(item);

                if (EndsWithCRC && Length == 1)
                {
                    // maybe it is the crc value!? and CRC is enabled
                    ReadableEspa += String.Format("<BCC=0x{0:X}>", itemnumeric);
                }
                else if (itemnumeric < Constants.AsciiTable.Length)
                {
                    ReadableEspa += Constants.AsciiTable[itemnumeric];
                }
                else
                {
                    ReadableEspa += String.Format("<{0:X}>", itemnumeric);
                }

                if (--Length <= 0) break;
            }

            return ReadableEspa;
        }
        #endregion

    }
}
