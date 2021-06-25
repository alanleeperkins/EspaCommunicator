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
    public partial class FormErrorMessage : Form
    {
        public FormErrorMessage(String Title, String Message)
        {
            InitializeComponent();
            Text = String.Format("{0} - Error Message", Program.GlobalVars.DisplayApplicationName);

            lbTitle.Text = Title;
            lbMessage.Text = Message;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
