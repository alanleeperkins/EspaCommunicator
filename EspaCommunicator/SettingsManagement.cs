using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaCommunicator
{
    class SettingsManagement
    {
        /// <summary>
        /// the standard settings
        /// </summary>
        public SettingsVars StandardSettings;

        /// <summary>
        /// the settings token from the windows registry
        /// </summary>
        public RegistryCtrl RegSettings;
   
        /// <summary>
        /// the settings currently be used by the user
        /// </summary>
        public SettingsVars ActiveSettings;

        /// <summary>
        /// 
        /// </summary>
        public SettingsManagement()
        {
            StandardSettings = new SettingsVars();
            SetStandardSettings(); // fill it with the standard settings, so we can go back to them whenever needed
            
            RegSettings = new RegistryCtrl(ref StandardSettings);
            ActiveSettings = new SettingsVars();
            SetRegistrySettingsActive();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetStandardSettings()
        {
            // standard serial-com settings
            StandardSettings.SerialComSettings.Baudrate = 9600;
            StandardSettings.SerialComSettings.Parity = System.IO.Ports.Parity.None;
            StandardSettings.SerialComSettings.SerialDataBits = 8;
            StandardSettings.SerialComSettings.StopBits = System.IO.Ports.StopBits.One;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetToDefaultSerialComSettings()
        {
            // standard serial-com settings
            ActiveSettings.SerialComSettings = StandardSettings.SerialComSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetRegistrySettingsActive()
        {
            RegSettings.ReadRegistrySettings();
     
            ActiveSettings.SerialComSettings = RegSettings.RegistryVariables.SerialComSettings;

            //
            ApplyLicenseOnSettings(ref ActiveSettings);            
        }

        /// <summary>
        /// 
        /// </summary>
        private void ApplyLicenseOnSettings(ref SettingsVars Settings)
        {
           
        }
    }
}
