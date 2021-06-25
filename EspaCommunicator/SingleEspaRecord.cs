using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaCommunicator
{
    public class SingleEspaRecord
    {
        private int recordid;
        private String recorddata;

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
        
        
        public SingleEspaRecord()
        {

        }
    }
}
