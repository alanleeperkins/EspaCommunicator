using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensingLib
{
    /// <summary>
    /// a single license info 
    /// </summary>
    public class SingleLicenseInfo
    {
        private string description;

        public object oType;

        public eSingleLicense Type;
        public String Description
        {
            set { description = value; }
            get {
                if (Constants.LicenseDescriptions.ContainsKey(Type))
                {
                    return Constants.LicenseDescriptions[Type];
                }
                else
                {
                    return Constants.LicenseDescriptions[eSingleLicense.None];
                }           
            }
        }
        public DateTime ActivationDate;
        public DateTime ExpireDate;

        public SingleLicenseInfo()
        {
            Type = eSingleLicense.None;
            Description = "no legal license";
            ActivationDate = DateTime.Now;
            ExpireDate = DateTime.Now;
        }
    }
}