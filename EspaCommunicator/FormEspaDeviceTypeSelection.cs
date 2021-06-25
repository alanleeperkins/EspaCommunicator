using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EspaLib;

namespace EspaCommunicator
{
    public partial class FormEspaDeviceTypeSelection : Form
    {
        public eEspaDeviceType ChoosenDeviceType;

        public FormEspaDeviceTypeSelection()
        {
            InitializeComponent();

            Text = String.Format("{0} - ESPA Type", Program.GlobalVars.DisplayApplicationName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btControlStation_Click(object sender, EventArgs e)
        {
            ChoosenDeviceType = eEspaDeviceType.ControlStation;
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btStation_Click(object sender, EventArgs e)
        {
            ChoosenDeviceType = eEspaDeviceType.Station;
            Close();
        }
    }
}
