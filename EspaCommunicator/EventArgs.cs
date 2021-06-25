using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EspaLib;

namespace EspaCommunicator
{

    #region SendEspaDataArgs
    public delegate void SendEspaDataBlockHandler(object sender, SendEspaDataBlockArgs e);
    
    public class SendEspaDataBlockArgs : EventArgs
    {
        private eEspaDataBlockType datablocktype;
        private byte[] rawdatablock;
        private SingleEspaDataBlock recorddata;

        #region properties
        public eEspaDataBlockType DataBlockType
        {
            get { return datablocktype; }
            set { datablocktype = value; }
        }

        public byte[] RawDataBlock
        {
            get { return rawdatablock; }
            set { rawdatablock = value; }
        }

        public SingleEspaDataBlock RecordDataBlock
        {
            get { return recorddata; }
            set { recorddata = value; }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rawdata"></param>
        /// <param name="recorddata"></param>
        public SendEspaDataBlockArgs(eEspaDataBlockType type, byte[] rawdata = null, SingleEspaDataBlock recordData = null)
        {
            datablocktype = type;
            rawdatablock = rawdata;
            recorddata = recordData;
        }       
    }
    #endregion

    #region SendDataIntoEspaDataBusArgs
    public delegate void SendDataIntoEspaDataBusHandler(object sender, SendDataIntoEspaDataBusArgs e);
 
    public class SendDataIntoEspaDataBusArgs : EventArgs
    {
        private eEspaRequestType type;
        public eEspaRequestType Type
        {
            get { return type; }
            set { type = value; }
        }

        private object content;
        public object Content
        {
            get { return content; }
            set { content = value; }
        }        
        
        /// <summary>
        /// 
        /// </summary>
        public SendDataIntoEspaDataBusArgs(eEspaRequestType _type, object _content)
        {
            type = _type;
            content = _content;
        }
    }
    #endregion
}
