using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EspaCommunicator
{
    public partial class FormConfiguration : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public FormConfiguration()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Text = String.Format("{0} - Configuration", Program.GlobalVars.DisplayApplicationName);
            LoadSerialComVariations();

            BuildUI();

            ReadSettings();
            ShowSerialSettingsOnUI();
        }

        private void BuildUI()
        {
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ReadSettings()
        {

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool WriteSettings()
        {
            if (CheckSettingsAreLegal() == false) return false;
 
            Program.SettingsMng.RegSettings.RegistryVariables.SerialComSettings.Baudrate = (int)coboBaudrate.SelectedItem;
            Program.SettingsMng.RegSettings.RegistryVariables.SerialComSettings.Parity = (System.IO.Ports.Parity)coboParity.SelectedItem;
            Program.SettingsMng.RegSettings.RegistryVariables.SerialComSettings.SerialDataBits = (int)coboDataBits.SelectedItem;
            Program.SettingsMng.RegSettings.RegistryVariables.SerialComSettings.StopBits = (System.IO.Ports.StopBits)coboStopBits.SelectedItem;

            return Program.SettingsMng.RegSettings.WriteRegistrySettings();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckSettingsAreLegal()
        {
            // ESPA Address must be one single sign (char)

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {
            if (WriteSettings() == false)
            {
                MessageBox.Show("ERROR: Please check your settings!","ERROR",MessageBoxButtons.OK);
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadSerialComVariations()
        {
            //coboBaudrate
            coboBaudrate.Items.Clear();
            foreach(var item in EspaLib.Constants.SerialBaudrates)
            {
                coboBaudrate.Items.Add(item);
            }

            //coboParity
            coboParity.Items.Clear();
            var values = Enum.GetValues(typeof(System.IO.Ports.Parity));
            foreach (var item in values)
            {
                coboParity.Items.Add(item);
            }

            // coboDataBits
            coboDataBits.Items.Clear();
            foreach (var item in EspaLib.Constants.SerialDataBits)
            {
                coboDataBits.Items.Add(item);
            }

            //coboStopBits
            coboStopBits.Items.Clear();
            var sbvalues = Enum.GetValues(typeof(System.IO.Ports.StopBits));
            foreach (var item in sbvalues)
            {
                coboStopBits.Items.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowSerialSettingsOnUI()
        {
            coboBaudrate.SelectedItem = Program.SettingsMng.ActiveSettings.SerialComSettings.Baudrate;
            coboParity.SelectedItem = Program.SettingsMng.ActiveSettings.SerialComSettings.Parity;
            coboDataBits.SelectedItem = Program.SettingsMng.ActiveSettings.SerialComSettings.SerialDataBits;
            coboStopBits.SelectedItem = Program.SettingsMng.ActiveSettings.SerialComSettings.StopBits;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSetDefaultEspaSerialSettings_Click(object sender, EventArgs e)
        {
            // reset something
            Program.SettingsMng.ResetToDefaultSerialComSettings();

            ShowSerialSettingsOnUI();
        }
    }
}
