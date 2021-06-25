using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EspaLib
{
    public class SingleEspaRecord : ICloneable
    {
        #region member data        
        private int recordid;
        private String recorddata;
        #endregion

        #region properties       
        public int RecordID
        {
            get { return recordid; }
            set { recordid = value; }
        }

        public String RecordData
        {
            get { return recorddata; }
            set { recorddata = value; }
        }
        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        
        public SingleEspaRecord()
        {

        }
    }
}
