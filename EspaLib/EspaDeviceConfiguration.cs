using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using EspaLib;

namespace EspaLib
{
    public class EspaDeviceConfiguration : PropertyBase 
    {
        #region private data
        private eEspaDeviceType espalocaldevicetype;
        private char espamylocaladdress;
        private bool isactive_rejectpollrequest;
        private bool isactive_rejectselectrequest;
        private bool isactive_rejectreceiveddatablock;
        private eTraCtrlPrefix datareceiverejectreasontype;
        private int maxdatablockrestransmit;
        private EspaPollAddresses polladdresses = new EspaPollAddresses();
        private bool isactive_polling;
        private int polling_interval;
        private bool isactive_sendeotafterlastrecordinqueue;
        #endregion private data

        #region properties
        /// <summary>
        /// 
        /// </summary>
        public eEspaDeviceType EspaLocalDeviceType
        {
            get { return espalocaldevicetype; }
            set { SetValue(ref espalocaldevicetype, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public char EspaMyLocalAddress
        {
            get { return espamylocaladdress; }
            set { SetValue(ref espamylocaladdress, value); }
        }
    
        /// <summary>
        /// 
        /// </summary>
        public bool IsActiveRejectPollRequest
        {
            get { return isactive_rejectpollrequest; }
            set { SetValue(ref isactive_rejectpollrequest, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActiveRejectSelectRequest
        {
            get { return isactive_rejectselectrequest; }
            set { SetValue(ref isactive_rejectselectrequest, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActiveRejectReceivedDataBlock
        {
            get { return isactive_rejectreceiveddatablock; }
            set { SetValue(ref isactive_rejectreceiveddatablock, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public eTraCtrlPrefix DataReceivedRejectReasonType
        {
            get { return datareceiverejectreasontype; }
            set { SetValue(ref datareceiverejectreasontype, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public int MaxDataBlockReTransmit
        {
            get { return maxdatablockrestransmit; }
            set { SetValue(ref maxdatablockrestransmit, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool SendEotAfterLastRecordInQueue
        {
            get { return isactive_sendeotafterlastrecordinqueue; }
            set { SetValue(ref isactive_sendeotafterlastrecordinqueue, value); }
        }

        #region non-browsable
        /// <summary>
        /// 
        /// </summary>
        public EspaPollAddresses PollingAddresses
        {
            get { return polladdresses; }
            set { polladdresses = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActivePolling
        {
            get { return isactive_polling; }
            set { isactive_polling = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PollingInterval
        {
            get { return polling_interval; }
            set { polling_interval = value; }
        }
        #endregion non-browsable

        #endregion properties

        public EspaDeviceConfiguration()
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
