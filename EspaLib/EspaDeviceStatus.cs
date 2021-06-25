using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaLib
{
    public class EspaDeviceStatus : PropertyBase 
    {
        #region private data
        private eEspaDeviceState devicestate; 
        private bool isactive_waitingfortransactioncomplete;
        private bool isactive_waitingforacknowledge;
        private char espatemporarymasterastationddress;
        private char espaslavestationaddress;
        private int espalastdatasentretrycount;
        private char pendingrecorddatareceiveraddress;
        private EspaTransactionsQueue espadatainputqueue;
        private EspaTransactionsQueue espamessageoutputqueue;  
        #endregion private data
 

        #region properties  
        public eEspaDeviceState DeviceState
        {
            get { return devicestate; }
            set { SetValue(ref devicestate, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public char TemporaryMasterStationAddress
        {
            get { return espatemporarymasterastationddress; }
            set { SetValue(ref espatemporarymasterastationddress, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public char SlaveStationAddress
        {
            get { return espaslavestationaddress; }
            set { SetValue(ref espaslavestationaddress, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public char PendingRecordDataReceiverAddress
        {
            get { return pendingrecorddatareceiveraddress; }
            set { SetValue(ref pendingrecorddatareceiveraddress, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActiveWaitingForTransactionsComplete
        {
            get { return isactive_waitingfortransactioncomplete; }
            set { SetValue(ref isactive_waitingfortransactioncomplete, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActiveWaitingForAcknowledge
        {
            get { return isactive_waitingforacknowledge; }
            set { SetValue(ref isactive_waitingforacknowledge, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int InputQueueSize
        {
            get { return espadatainputqueue.Transactions.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int OutputQueueSize
        {
            get { return espamessageoutputqueue.Transactions.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LastDataSentRetryCount
        {
            get { return espalastdatasentretrycount; }
            set { SetValue(ref espalastdatasentretrycount, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public EspaTransactionsQueue EspaDataInputQueue
        {
            get { return espadatainputqueue; }
            set { SetValue(ref espadatainputqueue, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public EspaTransactionsQueue EspaDataOutputQueue
        {
            get { return espamessageoutputqueue; }
            set { SetValue(ref espamessageoutputqueue, value); }
        }
        
        #endregion properties

        public EspaDeviceStatus()
        {
            espadatainputqueue = new EspaTransactionsQueue();
            espamessageoutputqueue = new EspaTransactionsQueue();

            DeviceState = eEspaDeviceState.Idle;
            IsActiveWaitingForTransactionsComplete = false;
            IsActiveWaitingForAcknowledge = false;        
            TemporaryMasterStationAddress = (char)0;
            SlaveStationAddress = (char)0;
            PendingRecordDataReceiverAddress = (char)0;
        }

    }
}
