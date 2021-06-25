using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using EspaLib;

namespace EspaCommunicator
{  
    public class PropertyGridObjectSimulationControlStation
    {
        #region private data
        private bool isactive_polling;
        private int polling_interval;
        private PropertyEspaPollAddresses polladdresses = new PropertyEspaPollAddresses();
        #endregion private data

        #region properties    
        /// <summary>
        /// 
        /// </summary>
        [RefreshProperties(RefreshProperties.Repaint)]
        [DisplayName("Poll Addresses")]
        [Description("The ESPA Addresses the 'Central Station' shall poll"), Category("Simulation (Control Station)")]
        [ReadOnly(false)]
        public PropertyEspaPollAddresses PollingAddresses
        {
            get { return polladdresses; }
            set { polladdresses = value; }
        }
   
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Polling")]
        [Description("The 'Control Station' polls a device. On receipt of this sequence, the polling device becomes 'Temporary Master Station'\n\nSequence: <EOT>'address'<ENQ>"), Category("Simulation (Control Station)")]
        [ReadOnly(false)]
        public bool IsActivePolling
        {
            get { return isactive_polling; }
            set { isactive_polling = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Polling Interval")]
        [Description("The interval (seconds) in which the 'Control Station' shall poll the next device.\n\nMin: 1 second / Max: 30 seconds"), Category("Simulation (Control Station)")]
        [ReadOnly(false)]
        public int PollingInterval
        {
            get { return polling_interval; }
            set { polling_interval = value; }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public PropertyGridObjectSimulationControlStation()
        {
            SetDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDefault()
        {
            isactive_polling = true;
            polling_interval = (int)eEspaConstants.DefaultPollingInterval;
        }
    }
}
