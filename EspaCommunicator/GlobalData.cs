using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EspaLib;
using System.Drawing;

namespace EspaCommunicator
{
    public class GlobalData
    {
        #region member data
        public ESPA EspaCom = new ESPA();
        public EspaDeviceStatus DeviceStatus = new EspaDeviceStatus();
        public PropertyEspaDeviceConfiguration DeviceConfiguration = new PropertyEspaDeviceConfiguration();
        public FileManagement FileManager = new FileManagement();
        public PropertyGridObjectSimulationControlStation PropGridObjectSimControlStation = new PropertyGridObjectSimulationControlStation();
        public PropertyGridObjectSimulationStation PropGridObjectSimStation = new PropertyGridObjectSimulationStation();

        public String DisplayApplicationName;
        public String BuildDeveloper;
        public String CopyrightInformation;
        public String BuildTime;
        public String BuildType;
        public String BuildVersion;

        public Rectangle FormMainRectangle;

        /// <summary>
        /// [For DEBUG]Enables OutputWindow, so the user can see Error/Warnings/Information Outputs
        /// </summary>
        public bool OutputWindowVisible;

        public bool IsSetArgumentToOpenFilePath
        {
            get { return (ArgumentToOpenFilePath!=null && ArgumentToOpenFilePath.Length > 0); }
        }
        public String ArgumentToOpenFilePath;
   
        /// <summary>
        /// the logging file managed by the operating system
        /// </summary>
        public WindowsLogging Logging = new WindowsLogging("AlanLeePerkins", ".", "ESPA Communicator");
        #endregion

        /// <summary>
        /// constructor
        /// </summary>
        public GlobalData()
        {
            PropGridObjectSimControlStation.PollingAddresses = DeviceConfiguration.PollingAddresses;
            PropGridObjectSimControlStation.IsActivePolling = DeviceConfiguration.IsActivePolling;
            PropGridObjectSimControlStation.PollingInterval = DeviceConfiguration.PollingInterval;

            EspaCom.DeviceConfiguration.PropertyChanged += DeviceConfiguration_PropertyChanged;
            EspaCom.DeviceStatus.PropertyChanged += DeviceStatus_PropertyChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DeviceStatus_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "DeviceState":
                    DeviceStatus.DeviceState = EspaCom.DeviceStatus.DeviceState;
                    break;
                case "TemporaryMasterStationAddress":
                    DeviceStatus.TemporaryMasterStationAddress = EspaCom.DeviceStatus.TemporaryMasterStationAddress;
                    break;
                case "SlaveStationAddress":
                    DeviceStatus.SlaveStationAddress = EspaCom.DeviceStatus.SlaveStationAddress;
                    break;
                case "PendingRecordDataReceiverAddress":
                    DeviceStatus.PendingRecordDataReceiverAddress = EspaCom.DeviceStatus.PendingRecordDataReceiverAddress;
                    break;
                case "IsActiveWaitingForTransactionsComplete":
                    DeviceStatus.IsActiveWaitingForTransactionsComplete = EspaCom.DeviceStatus.IsActiveWaitingForTransactionsComplete;
                    break;
                case "IsActiveWaitingForAcknowledge":
                    DeviceStatus.IsActiveWaitingForAcknowledge = EspaCom.DeviceStatus.IsActiveWaitingForAcknowledge;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DeviceConfiguration_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "EspaLocalDeviceType":
                    DeviceConfiguration.EspaLocalDeviceType = EspaCom.DeviceConfiguration.EspaLocalDeviceType;
                    break;

                case "EspaMyLocalAddress":
                    DeviceConfiguration.EspaMyLocalAddress = EspaCom.DeviceConfiguration.EspaMyLocalAddress;
                    break;

                case "IsActiveRejectPollRequest":
                    DeviceConfiguration.IsActiveRejectPollRequest = EspaCom.DeviceConfiguration.IsActiveRejectPollRequest;
                    break;

                case "IsActiveRejectSelectRequest":
                    DeviceConfiguration.IsActiveRejectSelectRequest = EspaCom.DeviceConfiguration.IsActiveRejectSelectRequest;
                    break;

                case "IsActiveRejectReceivedDataBlock":
                    DeviceConfiguration.IsActiveRejectReceivedDataBlock = EspaCom.DeviceConfiguration.IsActiveRejectReceivedDataBlock;
                    break;

                case "DataReceivedRejectReasonType":
                    DeviceConfiguration.DataReceivedRejectReasonType = EspaCom.DeviceConfiguration.DataReceivedRejectReasonType;
                    break;

                case "MaxDataBlockReTransmit":
                    DeviceConfiguration.MaxDataBlockReTransmit = EspaCom.DeviceConfiguration.MaxDataBlockReTransmit;
                    break;

                case "SendEotAfterLastRecordInQueue":
                    DeviceConfiguration.SendEotAfterLastRecordInQueue = EspaCom.DeviceConfiguration.SendEotAfterLastRecordInQueue;
                    break;
            }          
        }
    }

}
