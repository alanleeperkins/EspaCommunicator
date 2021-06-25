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


    public partial class FormAddLicenseKey : Form
    {
        public FormAddLicenseKey()
        {
            InitializeComponent();
            Text = String.Format("{0} - Add License Key", Program.GlobalVars.DisplayApplicationName);


        }

        

        private bool IsLicenseLegal()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {
            if (IsLicenseLegal())
            {
                Close();
            }
            else
            {
                MessageBox.Show("This License-Key is not valid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
