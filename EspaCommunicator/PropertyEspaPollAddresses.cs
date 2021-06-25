using EspaLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaCommunicator
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PropertyEspaPollAddresses : EspaPollAddresses
    {
        #region properties
        [DisplayName("ESPA Address '1' ")]
        [Description("The ESPA Address '1' is usually used for the 'Control Station'")]
        public bool Address1 { get { return address1; } set { address1 = value; } }

        [DisplayName("ESPA Address '2' ")]
        public bool Address2 { get { return address2; } set { address2 = value; } }

        [DisplayName("ESPA Address '3' ")]
        public bool Address3 { get { return address3; } set { address3 = value; } }

        [DisplayName("ESPA Address '4' ")]
        public bool Address4 { get { return address4; } set { address4 = value; } }

        [DisplayName("ESPA Address '5' ")]
        public bool Address5 { get { return address5; } set { address5 = value; } }

        [DisplayName("ESPA Address '6' ")]
        public bool Address6 { get { return address6; } set { address6 = value; } }

        [DisplayName("ESPA Address '7' ")]
        public bool Address7 { get { return address7; } set { address7 = value; } }

        [DisplayName("ESPA Address '8' ")]
        public bool Address8 { get { return address8; } set { address8 = value; } }

        [DisplayName("ESPA Address '9' ")]
        public bool Address9 { get { return address9; } set { address9 = value; } }
        #endregion

        public override string ToString()
        {
            return Address1 + "," + Address2 + "," + Address3 + "," + Address4 + "," + Address5 + "," + Address6 + "," + Address7 + "," + Address8;
        }
    }
}
