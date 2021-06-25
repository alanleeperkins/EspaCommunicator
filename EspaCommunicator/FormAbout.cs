using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EspaCommunicator
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            GetSoftwareInfo();
            ShowSoftwareInfo();

            Text = String.Format("{0} - About",Program.GlobalVars.DisplayApplicationName);
        }

        public void GetSoftwareInfo()
        {

            // AssemblyVersion
            string AssemblyVersion = Application.ProductVersion;
            Program.GlobalVars.BuildVersion = AssemblyVersion;

            // copyright
            string AssemblyCopyright = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute), false)).Copyright;
            Program.GlobalVars.CopyrightInformation = AssemblyCopyright;

            // Developer
            string Developer = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company;
            Program.GlobalVars.BuildDeveloper = Developer;

            // Build Time (AssemblyDescription)
            string AssemblyDescription = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyDescriptionAttribute), false)).Description;
            Program.GlobalVars.BuildTime = AssemblyDescription;
        }

        public void ShowSoftwareInfo()
        {
            lbSoftwareName.Text = "Software: " + Program.GlobalVars.DisplayApplicationName; //OK
            lbDeveloper.Text = "Developer: " + Program.GlobalVars.BuildDeveloper; //OK
            lbCopyright.Text = Program.GlobalVars.CopyrightInformation; //OK
            lbBuildTime.Text = "Build-Time: " + Program.GlobalVars.BuildTime; //OK
            lbBuildType.Text = "Build-Type: " + Program.GlobalVars.BuildType; //Ok
            lbVersion.Text = "Version: " + Program.GlobalVars.BuildVersion;  //OK
        }
    }
}
