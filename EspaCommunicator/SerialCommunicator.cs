using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace EspaCommunicator
{
    public class SerialCommunicator
    {
        #region member variables
        private SerialPort communicationport;
        #endregion member variables

        #region events
        public event SerialDataReceivedHandler SerialDataReceivedEvent;
        #endregion events
    
        #region properties
        public SerialPort ComPort
        {
            get { return communicationport; }
            set { communicationport = value; }
        }
    
        public bool IsConnected
        {
            get {

                if (ComPort == null) return false;
                return ComPort.IsOpen;       
            }
        }
        #endregion properties

        #region constructor
        public SerialCommunicator()
        {
        
        }
        #endregion constructor


        /// <summary>
        /// 
        /// </summary>
        /// <param name="portname"></param>
        /// <param name="baudrate"></param>
        /// <param name="parity"></param>
        /// <param name="databits"></param>
        /// <returns></returns>
        public bool Connect(String portname, int baudrate, Parity parity, int databits)
        {
            try
            {
                ComPort = new SerialPort(portname, baudrate, parity, databits);
                ComPort.DataReceived += ComPort_DataReceived;
                ComPort.Open();
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR Connect");
                Console.WriteLine(exc.Message);
                return false;
            }

            return ComPort.IsOpen;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Disconnect()
        {
            if (ComPort == null) return;

            try
            {
                if (ComPort.IsOpen == false) return;
                ComPort.Close();
                ComPort.DataReceived -= ComPort_DataReceived;
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR Disconnect");
                Console.WriteLine(exc.Message);
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer
            Console.WriteLine(ComPort.ReadExisting());
            char[] Received = new char[1200];
            int ReceiveCount = ComPort.Read(Received,0,Received.Length);
            Raise_SerialDataReceivedEvent(new SerialDataReceivedArgs(Received,ReceiveCount, DateTime.Now));          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        public bool SendData(char[] data,int count)
        {
            if (ComPort == null) return false;
            if (ComPort.IsOpen == false) return false;

            try
            {
                ComPort.Write(data, 0, count);
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR SendData(char[] data,int count)");
                Console.WriteLine(exc.Message);
                return false;
            }

            return true;
        }

        #region Event handler methods

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Fires the TestFrameworkFormStartedEvent event.
        /// </summary>
        /// <param name="e"></param>
        public void Raise_SerialDataReceivedEvent(SerialDataReceivedArgs e)
        {
            if (SerialDataReceivedEvent != null)
            {
                SerialDataReceivedEvent(this, e);
            }
        }
        #endregion
    }
}
