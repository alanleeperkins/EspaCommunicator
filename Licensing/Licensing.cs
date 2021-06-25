using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensingLib
{
    public class Licensing
    {
        private List<SingleLicenseInfo> licenses;

        #region properties
        public  List<SingleLicenseInfo> LicenseList
        {
            get { return licenses; }
            set { value = licenses; }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public Licensing()
        {
            licenses = new List<SingleLicenseInfo>();

            Constants.LicenseDescriptions.Add(eSingleLicense.None, "No legal license");
            Constants.LicenseDescriptions.Add(eSingleLicense.ESPA_Station, "Simulation - ESPA Paging Installation / Station");
            Constants.LicenseDescriptions.Add(eSingleLicense.ESPA_ControlStation, "Simulation - ESPA Control System / Control Station");
        }

        /// <summary>
        /// checks if this license key is activated and still valid
        /// </summary>
        /// <param name="SingleLicense"></param>
        /// <returns></returns>
        public bool IsSingleLicenseValid(eSingleLicense SingleLicense)
        {
            foreach (var item in LicenseList)
            {
                if (item.Type == SingleLicense)
                {
                    DateTime Today = DateTime.Now;
                    return (Today < item.ExpireDate);
                }
            }

            return false;
        }

        public bool RemoveExpiredLicences()
        {
            List<SingleLicenseInfo> ToBeRemoved = new List<SingleLicenseInfo>();
            foreach (var item in LicenseList)
            {
                DateTime Today = DateTime.Now;
                if (Today > item.ExpireDate)
                {
                    ToBeRemoved.Add(item);
                }                
            }

            foreach (var item in ToBeRemoved)
            {
                LicenseList.Remove(item);
            }
            
            return true; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool LoadLicenses()
        {

            #region load dummy licenses
            licenses.Add(new SingleLicenseInfo
            {
                Type = eSingleLicense.ESPA_ControlStation,
                ActivationDate = new DateTime(2017, 03, 12),
                ExpireDate = new DateTime(2020, 03, 12)
            });
            
            licenses.Add(new SingleLicenseInfo
            {
                Type = eSingleLicense.ESPA_Station,
                ActivationDate = new DateTime(2017, 03, 12),
                ExpireDate = new DateTime(2020, 03, 12)
            });
            #endregion

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SaveLicenses()
        {
            return true;
        }       
    }
}
