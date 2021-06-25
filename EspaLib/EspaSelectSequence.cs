using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EspaLib;

namespace EspaLib
{
    public class EspaSelectSequence
    {
        #region member data
        private char temporarymasteraddress;
        private char slavestationaddress;
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
                    sequencedata = new byte[] { (byte)eAsciiCtrl.EOT, (byte)temporarymasteraddress, (byte)eAsciiCtrl.ENQ, (byte)slavestationaddress, (byte)eAsciiCtrl.ENQ };
                }
            }
        }

        public char SlaveStationAddress
        {
            get { return slavestationaddress; }
            set
            {
                if ((char)value >= '1' || (char)value <= '9')
                {
                    slavestationaddress = value;
                    sequencedata = new byte[] { (byte)eAsciiCtrl.EOT, (byte)temporarymasteraddress, (byte)eAsciiCtrl.ENQ, (byte)slavestationaddress, (byte)eAsciiCtrl.ENQ };
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
        /// <param name="SlaveStationAddr"></param>
        public EspaSelectSequence(char TmpMasterAddr, char SlaveStationAddr)
        {
            TemporaryMasterAddress = TmpMasterAddr;
            SlaveStationAddress = SlaveStationAddr;
        }
    }
}
