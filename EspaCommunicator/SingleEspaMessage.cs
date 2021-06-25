using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaCommunicator
{
    public class SingleEspaMessage
    {
        private List<SingleEspaRecord> records = new List<SingleEspaRecord>();

        #region properties
        public List<SingleEspaRecord> Records
        {
            get { return records; }
        }

        public int CountRecords
        {
            get { return Records.Count; }
        }      
        #endregion

        /// <summary>
        /// constructor
        /// </summary>
        public SingleEspaMessage()
        {

        }

        /// <summary>
        /// removes all records
        /// </summary>
        public void ClearMessage()
        {
            records.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool AddRecord(SingleEspaRecord record)
        {
            // message structure already filled up?
            if (records.Count >= Constants.ESPA_MAX_RECORDS_PER_STRUCTURE) return false;

            records.Add(record);

            return true; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool RemoveRecord(SingleEspaRecord record)
        {
            return records.Remove(record);
        }
    }
}
