using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using EspaLib;

namespace EspaCommunicator
{
    public class PropertyEspaDeviceConfiguration 
    { 
        #region private data
        private eEspaDeviceType espalocaldevicetype;
        private char espamylocaladdress;
        private bool isactive_rejectpollrequest;
        private bool isactive_rejectselectrequest;
        private bool isactive_rejectreceiveddatablock;
        private eTraCtrlPrefix datareceiverejectreasontype;
        private int maxdatablockrestransmit;
        private PropertyEspaPollAddresses polladdresses = new PropertyEspaPollAddresses();
        private bool isactive_polling;
        private int polling_interval;
        private bool isactive_sendeotafterlastrecordinqueue;
        #endregion private data

        #region properties
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ESPA Device Type")]
        [Description("The local device type ('Central Station' or 'Station')"), Category("Local Device Configuration")]
        [ReadOnly(true)]
        public eEspaDeviceType EspaLocalDeviceType
        {
            get { return espalocaldevicetype; }
            set { espalocaldevicetype = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ESPA Address")]
        [Description("The ESPA Address of this local device. Legal Values are between '1' and '9' where the 'Central Station' usually gets '1'"), Category("Local Device Configuration")]
        [ReadOnly(false)]
        public char EspaMyLocalAddress
        {
            get { return espamylocaladdress; }
            set { espamylocaladdress = value; }
        }
    
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Reject Poll Request")]
        [Description("Auto reject the next poll request from the 'Central Station' to become 'Temporary Master Station'"), Category("Simulation")] 
        [ReadOnly(false)]
        public bool IsActiveRejectPollRequest
        {
            get { return isactive_rejectpollrequest; }
            set { isactive_rejectpollrequest = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Reject Select Request")]
        [Description("Auto reject the next select request from the 'Temporary Master Station' to receive data from"), Category("Simulation")] 
        [ReadOnly(false)]
        public bool IsActiveRejectSelectRequest
        {
            get { return isactive_rejectselectrequest; }
            set { isactive_rejectselectrequest = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Reject Received Data Block")]
        [Description("Auto reject the next received data block coming from the 'Temporary Master Station', with a defined prefix"), Category("Simulation")]
        [ReadOnly(false)]
        public bool IsActiveRejectReceivedDataBlock
        {
            get { return isactive_rejectreceiveddatablock; }
            set { isactive_rejectreceiveddatablock = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Reject Received Data - Reason")]
        [Description("The reason for rejecting the received data"), Category("Simulation")]
        [ReadOnly(false)]
        public eTraCtrlPrefix DataReceivedRejectReasonType
        {
            get { return datareceiverejectreasontype; }
            set { datareceiverejectreasontype = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Max Record Data Retransmits")]
        [Description("The number of retransmits the 'Temporary Master Station' will try before terminating the transmission by sending <EOT>. (The sending of a data block failes when the 'Slave Station' answers with n<NAK>)"), Category("Simulation")]
        [ReadOnly(false)]
        public int MaxDataBlockReTransmit
        {
            get { return maxdatablockrestransmit; }
            set { maxdatablockrestransmit = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Send <EOT> after last record")]
        [Description("Send and End-of-transmission (<EOT>) after sending the last content of the message-out queue"), Category("Message Queue (Send)")]
        [ReadOnly(true)]
        public bool SendEotAfterLastRecordInQueue
        {
            get { return isactive_sendeotafterlastrecordinqueue; }
            set { isactive_sendeotafterlastrecordinqueue = value; }
        }

        #region non-browsable
        /// <summary>
        /// 
        /// </summary>
        [RefreshProperties(RefreshProperties.Repaint),Browsable(false)]
        [DisplayName("Poll Addresses")]
        [Description("The ESPA Addresses the 'Central Station' shall poll"), Category("Simulation (Control Station)")]
        [ReadOnly(false)]
        public PropertyEspaPollAddresses PollingAddresses
        {
            get { return polladdresses; }
            set { polladdresses = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Polling"),Browsable(false)]
        [Description("The 'Control Station' polls a device. On receipt of this sequence, the polling device becomes 'Temporary Master Station'\n\nSequence: <EOT>'address'<ENQ>"), Category("Simulation (Control Station)")]
        [ReadOnly(false)]
        public bool IsActivePolling
        {
            get { return isactive_polling; }
            set { isactive_polling = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Polling Interval"), Browsable(false)]
        [Description("The interval (seconds) in which the 'Control Station' shall poll the next device.\n\nMin: 1 second / Max: 30 seconds"), Category("Simulation (Control Station)")]
        [ReadOnly(false)]
        public int PollingInterval
        {
            get { return polling_interval; }
            set { polling_interval = value; }
        }
        #endregion non-browsable

        #endregion properties

        public PropertyEspaDeviceConfiguration()
        {
            SetDefault();
        }

        public void SetDefault()
        {
            EspaLocalDeviceType = eEspaDeviceType.ControlStation;
            EspaMyLocalAddress = (char)eEspaStandards.AddressControlStation;

            DataReceivedRejectReasonType = eTraCtrlPrefix.TransmissionError;
            IsActiveRejectPollRequest = false;
            IsActiveRejectSelectRequest = false;
            maxdatablockrestransmit = 2;
            isactive_polling = true;
            polling_interval = (int)eEspaConstants.DefaultPollingInterval;
            isactive_sendeotafterlastrecordinqueue = true;
            polladdresses.SetDefault();
        }
    }
}
