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
    public partial class FormLicenseManager : Form
    {
        public FormLicenseManager()
        {
            InitializeComponent();

            Init();
        } 

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            Text = String.Format("{0} - License Manager", Program.GlobalVars.DisplayApplicationName);
            UpdateLicenseList();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateLicenseList()
        {
            lvLicenseInformation.Items.Clear();
          
            foreach (LicensingLib.SingleLicenseInfo single in Program.GlobalVars.EspaCom.Licensing.LicenseList)
            {
                bool IsExpired = false;
                if (single.ExpireDate < DateTime.Now)
                {
                    IsExpired = true;
                }

                string[] saLvwItem = new string[3];
                saLvwItem[0] = IsExpired ? "(EXPIRED) " + single.Description : single.Description;
                saLvwItem[1] = single.ActivationDate.ToShortDateString();
                saLvwItem[2] = single.ExpireDate.ToShortDateString();
    
                ListViewItem lvi = new ListViewItem(saLvwItem);

                if(IsExpired)
                {
                    lvi.ForeColor = Color.Red;
                }

                lvLicenseInformation.Items.Add(lvi);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void RemoveExpiredLicences()
        {
            Program.GlobalVars.EspaCom.Licensing.RemoveExpiredLicences();
            UpdateLicenseList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAddLicense_Click(object sender, EventArgs e)
        {
            FormAddLicenseKey dlg = new FormAddLicenseKey();
            dlg.ShowDialog();
            UpdateLicenseList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRemovedExpired_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove all expired Licenses?","Remove expired Licenses", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) return;
            RemoveExpiredLicences();
        }
    }
}
