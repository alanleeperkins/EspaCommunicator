using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicensingLib;

namespace EspaLib
{
    public partial class ESPA
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeviceSimulationType"></param>
        /// <returns></returns>
        private bool IsSingleLicenseValid(eEspaDeviceType DeviceSimulationType)
        {
            switch (DeviceSimulationType)
            {
                case eEspaDeviceType.None:
                    return false;

                case eEspaDeviceType.Station:
                    return Licensing.IsSingleLicenseValid(eSingleLicense.ESPA_Station);

                case eEspaDeviceType.ControlStation:
                    return Licensing.IsSingleLicenseValid(eSingleLicense.ESPA_ControlStation);

                default:
                    return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="License"></param>
        /// <returns></returns>
        public bool IsSingleLicenseValid(LicensingLib.eSingleLicense License)
        {
            return Licensing.IsSingleLicenseValid(License);
        }
    }
}
