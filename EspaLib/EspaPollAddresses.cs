using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaLib
{
    public class EspaPollAddresses
    {
        #region member data
        protected bool address1;
        protected bool address2;
        protected bool address3;
        protected bool address4;
        protected bool address5;
        protected bool address6;
        protected bool address7;
        protected bool address8;
        protected bool address9;
        #endregion 

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="addr1"></param>
        /// <param name="addr2"></param>
        /// <param name="addr3"></param>
        /// <param name="addr4"></param>
        /// <param name="addr5"></param>
        /// <param name="addr6"></param>
        /// <param name="addr7"></param>
        /// <param name="addr8"></param>
        /// <param name="addr9"></param>
        public EspaPollAddresses(bool addr1 = false, bool addr2 = true, bool addr3 = false, bool addr4 = false, bool addr5 = false, bool addr6 = false, bool addr7 = false, bool addr8 = false, bool addr9 = false)
        {
            SetDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr1"></param>
        /// <param name="addr2"></param>
        /// <param name="addr3"></param>
        /// <param name="addr4"></param>
        /// <param name="addr5"></param>
        /// <param name="addr6"></param>
        /// <param name="addr7"></param>
        /// <param name="addr8"></param>
        /// <param name="addr9"></param>
        public void SetDefault(bool addr1 = false, bool addr2 = true, bool addr3 = false, bool addr4 = false, bool addr5 = false, bool addr6 = false, bool addr7 = false, bool addr8 = false, bool addr9 = false)
        {
            address1 = addr1;
            address2 = addr2;
            address3 = addr3;
            address4 = addr4;
            address5 = addr5;
            address6 = addr6;
            address7 = addr7;
            address8 = addr8;
            address9 = addr9;
        }

        /// <summary>
        /// returns the poll enabledstate of the ESPA Address 1-9
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public bool CanPollAddress(char Address)
        {
            switch (Address)
            {
                case '1': return address1;
                case '2': return address2;
                case '3': return address3;
                case '4': return address4;
                case '5': return address5;
                case '6': return address6;
                case '7': return address7;
                case '8': return address8;
                default: return false;
            }
        }

    };
}
