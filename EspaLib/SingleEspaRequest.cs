using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaLib
{
    public class SingleEspaRequest : ICloneable
    {
        #region member data
        private eEspaRequestType esparequesttype;
        private object data;      
        private bool sendendoftransmissionaftersuccess;

        public char HeaderIdentifier;
        public byte ReceiverAddress;
        private List<SingleEspaRecord> records = new List<SingleEspaRecord>();
        #endregion

        #region properties
        public eEspaRequestType Type
        {
            get { return esparequesttype; }
            set { esparequesttype = value; }
        }

        public object Data
        {
            get { return data; }
            set { data = value; }
        }

        public List<SingleEspaRecord> Records
        {
            get { return records; }
            set { records = value; }
        }

        public int CountRecords
        {
            get { return Records.Count; }
        }      

        public object Clone()
        {
            var copy = (SingleEspaRequest)MemberwiseClone();

            for (int i = 0; i < copy.records.Count; i++)
            {
                copy.records[i] = (SingleEspaRecord)records[i].Clone();
            }

            return copy;
        } 

        /// <summary>
        /// after and <ACK> received, send an <EOT> to the 'Temporary Master Station'
        /// </summary>
        public bool SendEndOfTransmissionAfterSuccess
        {
            get { return sendendoftransmissionaftersuccess; }
            set { sendendoftransmissionaftersuccess = value; }
        }
        #endregion      
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_data"></param>
        public SingleEspaRequest(eEspaRequestType _type, object _data, bool _sendEOTaftersuccess = false)
        {
            esparequesttype = _type;
            data = _data;
            sendendoftransmissionaftersuccess = _sendEOTaftersuccess;
        }
    }
}
