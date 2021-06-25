using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensingLib
{
    public enum eSingleLicense {

        /// <summary>
        /// no legal license key
        /// </summary>
        None = -1, 

        /// <summary>
        /// Slave (central system)
        /// </summary>
        ESPA_ControlStation = 1,

        /// <summary>
        /// Master (paging installation)
        /// </summary>
        ESPA_Station = 2
    };
}
