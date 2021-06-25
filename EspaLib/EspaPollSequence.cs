using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EspaLib;

namespace EspaLib
{
    public class EspaPollSequence
    {
        #region member data
        private char temporarymasteraddress;
        private byte[] sequencedata;
        #endregion 

        #region properties
        public char TemporaryMasterAddress
        {
            get { return temporarymasteraddress; }
            set
            {
                if ((char)value >= '1' || (char)value <= '9')
                {
                    temporarymasteraddress = value;
                    sequencedata = new byte[] { (byte)eAsciiCtrl.EOT, (byte)temporarymasteraddress, (byte)eAsciiCtrl.ENQ };
                }
            }
        }

        public byte[] SequenceData
        {
            get { return sequencedata; }
        }          
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TmpMasterAddr"></param>
        public EspaPollSequence(char TmpMasterAddr)
        {
            TemporaryMasterAddress = TmpMasterAddr;
        }
    }
}
