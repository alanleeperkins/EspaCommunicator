using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EspaLib
{
    /// <summary>
    /// a data block can contain several records but all with the same receiver address and header
    /// </summary>
    public class SingleEspaDataBlock : ICloneable
    {
        #region member data        
        public byte ReceiverAddress;
        public eEspaDataBlockType DataBlockType;
        public char HeaderIdentifier;
        private List<SingleEspaRecord> records = new List<SingleEspaRecord>();
        #endregion

        #region properties
        public List<SingleEspaRecord> Records
        {
            get { return records; }
            set { records = value; }
        }

        public int CountRecords
        {
            get { return Records.Count; }
        }      
        #endregion

        #region cloning
        public object Clone()
        {
            var copy = (SingleEspaDataBlock)MemberwiseClone();

            for (int i = 0; i < copy.records.Count; i++)
            {
                copy.records[i] = (SingleEspaRecord)records[i].Clone();
            }
  
            return copy;
        }
        #endregion


        /// <summary>
        /// retuns the full record data block as a byte data stream
        /// </summary>
        public static bool GetDataStream(SingleEspaDataBlock DataBlockPackage, out byte[] cStructure, out int iStructurePointer)
        {
            // TODO: check the connection state
            cStructure = Enumerable.Repeat((byte)0, (int)eEspaConstants.MaxSizeStructure).ToArray();
            iStructurePointer = 0;

            ///////////////////////////////////////////////////////////////////
            // insert head 1*
            cStructure[iStructurePointer++] = (byte)eAsciiCtrl.SOH;
            cStructure[iStructurePointer++] = (byte)DataBlockPackage.HeaderIdentifier;
            cStructure[iStructurePointer++] = (byte)eAsciiCtrl.STX;

            // send the records
            foreach (var item in DataBlockPackage.Records)
            {
                // DataIdentifier
                cStructure[iStructurePointer++] = Convert.ToByte(Convert.ToChar(item.RecordID.ToString()));
                // UnitSeperator
                cStructure[iStructurePointer++] = (byte)eAsciiCtrl.US;
                // Data
                for (int i = 0; i < item.RecordData.Length; i++)
                {
                    cStructure[iStructurePointer++] = (byte)item.RecordData[i];
                }
                // RecordSeperator
                cStructure[iStructurePointer++] = (byte)eAsciiCtrl.RS;
            }

            ///////////////////////////////////////////////////////////////////
            // send tail 1*
            byte cCalcBCC = (byte)0;
            cStructure[iStructurePointer++] = (byte)eAsciiCtrl.ETX;
            cCalcBCC = (byte)ESPA.CalculateBCC(cStructure, (int)eEspaConstants.MaxSizeStructure);
            cStructure[iStructurePointer++] = cCalcBCC;

            ///////////////////////////////////////////////////////////////////
            //check the data structure
            if (ESPA.CheckBCC(cStructure, (int)eEspaConstants.MaxSizeStructure) == 1)
            {
                Console.WriteLine("bcc OK");
            }
            else
            {
                Console.WriteLine("bcc ERROR");
            }

            ///////////////////////////////////////////////////////////////////
            // finish the data structure
            cStructure[iStructurePointer] = (byte)0;

            return true;
        }

        /// <summary>
        /// constructor
        /// </summary>
        public SingleEspaDataBlock(eEspaDataBlockType datablockType=eEspaDataBlockType.Records)
        {
            DataBlockType = datablockType;
        }

        /// <summary>
        /// removes all records
        /// </summary>
        public void ClearDataBlock()
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
            // DataBlock structure already filled up?
            if (records.Count >= (int)eEspaConstants.MaxSizeStructure) return false;

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
