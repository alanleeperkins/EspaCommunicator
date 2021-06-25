using EspaLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaCommunicator
{
    class PropertyEspaDeviceStatus
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
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Device State")]
        [Description("The state of the ESPA Device varies between 'Idle', 'Temporary Master Station' and 'Slave Station'"), Category("Status Device")]
        [ReadOnly(true)]
        public eEspaDeviceState DeviceState
        {
            get { return devicestate; }
            set { devicestate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Temporary Master Station Address")]
        [Description("The address of the active 'Temporary Master Station'"), Category("Status Communication Line")]
        [ReadOnly(true)]
        public char TemporaryMasterStationAddress
        {
            get { return espatemporarymasterastationddress; }
            set { espatemporarymasterastationddress = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Slave Station Address")]
        [Description("The address of the active 'Slave Station'"), Category("Status Communication Line")]
        [ReadOnly(true)]
        public char SlaveStationAddress
        {
            get { return espaslavestationaddress; }
            set { espaslavestationaddress = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Pending Record Data Receiver Address")]
        [Description("Do I have record data for a potential 'Slave Station' and am I waiting for a positive selecte response from that device?"), Category("Status Communication Line")]
        [ReadOnly(true)]
        public char PendingRecordDataReceiverAddress
        {
            get { return pendingrecorddatareceiveraddress; }
            set { pendingrecorddatareceiveraddress = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Wait for Transaction Complete")]
        [Description("Wait for Transaction Complete <EOT>"), Category("Status Device")] 
        [ReadOnly(true)]
        public bool IsActiveWaitingForTransactionsComplete
        {
            get { return isactive_waitingfortransactioncomplete; }
            set { isactive_waitingfortransactioncomplete = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Wait for Acknowledge")]
        [Description("Wait for Acknowledge <ACK>"), Category("Status Device")]
        [ReadOnly(true)]
        public bool IsActiveWaitingForAcknowledge
        {
            get { return isactive_waitingforacknowledge; }
            set { isactive_waitingforacknowledge = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Input Queue Size")]
        [Description("Size of the data input queue"), Category("Status Device")]
        [ReadOnly(true)]
        public int InputQueueSize
        {
            get { return espadatainputqueue.Transactions.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Output Queue Size")]
        [Description("Size of the data output queue"), Category("Status Device")]
        [ReadOnly(true)]
        public int OutputQueueSize
        {
            get { return espamessageoutputqueue.Transactions.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public int LastDataSentRetryCount
        {
            get { return espalastdatasentretrycount; }
            set { espalastdatasentretrycount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public EspaTransactionsQueue EspaDataInputQueue
        {
            get { return espadatainputqueue; }
            set { espadatainputqueue = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public EspaTransactionsQueue EspaDataOutputQueue
        {
            get { return espamessageoutputqueue; }
            set { espamessageoutputqueue = value; }
        }
        
        #endregion properties

        public PropertyEspaDeviceStatus()
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
