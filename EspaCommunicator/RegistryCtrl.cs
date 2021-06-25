using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;

namespace EspaCommunicator
{
    public enum eSettingsType { All };
  
    public class RegistryCtrl
    {
        public SettingsVars standardSettings;   
        public static string mruRegKey = "SOFTWARE\\SesKion\\ESPA Communicator";
        public SettingsVars RegistryVariables = new SettingsVars();
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Settings"></param>
        public RegistryCtrl(ref SettingsVars Settings)
        {
            standardSettings = Settings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool WriteRegistrySettings(eSettingsType Type = eSettingsType.All)
        {
            RegistryKey AppKey = Registry.CurrentUser.OpenSubKey(mruRegKey, true);
      
            switch (Type)
            {
                case eSettingsType.All:
                    WriteRegistrySettings(AppKey);
                    break;

                default:
                    break;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WriteRegistrySettings(RegistryKey AppKey)
        {
            bool bRetVal = true;

            try
            {
                // SerialComSettings
                AppKey.SetValue("SerialComSettings.Baudrate", (int)RegistryVariables.SerialComSettings.Baudrate);
                AppKey.SetValue("SerialComSettings.Parity", (string)RegistryVariables.SerialComSettings.Parity.ToString());
                AppKey.SetValue("SerialComSettings.SerialDataBits", (int)RegistryVariables.SerialComSettings.SerialDataBits);
                AppKey.SetValue("SerialComSettings.StopBits", (string)RegistryVariables.SerialComSettings.StopBits.ToString());
            }
            catch
            {
                bRetVal = false;
            }

            return bRetVal;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool ReadRegistrySettings(eSettingsType Type = eSettingsType.All)
        {
            // AppKey
            RegistryKey AppKey = Registry.CurrentUser.OpenSubKey(mruRegKey, true);
            if (AppKey == null)
            {
                AppKey = Registry.CurrentUser.CreateSubKey(mruRegKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }

            switch (Type)
            {
                case eSettingsType.All:
                    return ReadRegistrySettings(AppKey);

                default:
                    break;
            }

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ReadRegistrySettings(RegistryKey AppKey)
        {
            bool bRetVal = true;

            try
            {            
                try
                {
                    // SerialComSettings.Baudrate 
                    RegistryVariables.SerialComSettings.Baudrate = (int)(AppKey.GetValue("SerialComSettings.Baudrate", standardSettings.SerialComSettings.Baudrate));
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);

                    RegistryVariables.SerialComSettings.Baudrate = standardSettings.SerialComSettings.Baudrate;
                }

                try
                {
                    // SerialComSettings.Parity 
                    String sParity = (string)(AppKey.GetValue("SerialComSettings.Parity", standardSettings.SerialComSettings.Parity));
                    RegistryVariables.SerialComSettings.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), sParity);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);

                    RegistryVariables.SerialComSettings.Parity = standardSettings.SerialComSettings.Parity;
                }

                try
                {
                    // SerialComSettings.SerialDataBits 
                    RegistryVariables.SerialComSettings.SerialDataBits = (int)(AppKey.GetValue("SerialComSettings.SerialDataBits", standardSettings.SerialComSettings.SerialDataBits));
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);

                    RegistryVariables.SerialComSettings.SerialDataBits = standardSettings.SerialComSettings.SerialDataBits;
                }

                try
                {
                    // SerialComSettings.StopBits 
                    String sStopBits = (string)(AppKey.GetValue("SerialComSettings.StopBits", standardSettings.SerialComSettings.StopBits));
                    RegistryVariables.SerialComSettings.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), sStopBits);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);

                    RegistryVariables.SerialComSettings.StopBits = standardSettings.SerialComSettings.StopBits;
                }
            }
            catch
            {
                return false;
            }

            return bRetVal;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetRegistrySettings()
        {
            // reload the subkey with the default settings
            ReadRegistrySettings();
        }
    }
}
