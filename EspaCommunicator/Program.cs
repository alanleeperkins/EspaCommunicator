using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EspaCommunicator
{
    static class Program
    {
        public static GlobalData GlobalVars = new GlobalData(); 
        public static SettingsManagement SettingsMng = new SettingsManagement();


        // //////////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////////
        // used for the console
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        [DllImport("kernel32", SetLastError = true)]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        // //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            GlobalVars.OutputWindowVisible = false;

            // check for arguments given by the user
            if (args.Length > 0)
            {
                foreach (String argument in args)
                {
                    if (argument == "--CONSOLE") GlobalVars.OutputWindowVisible = true;

                    if (Helper.IsLegalFile(argument))
                    {
                        GlobalVars.ArgumentToOpenFilePath = argument;
                    }
                }
            }

            if (Debugger.IsAttached || GlobalVars.OutputWindowVisible)
            {
                IntPtr ptr = GetForegroundWindow();
                int u;
                GetWindowThreadProcessId(ptr, out u);
                Process process = Process.GetProcessById(u);
                AllocConsole();
            }

            if (Program.GlobalVars.EspaCom.Licensing.LoadLicenses() == false)
            {
                Console.WriteLine("Error loading licenses\n");
            }

            Program.SettingsMng.SetRegistrySettingsActive();
            ApplySettingsOnUI();

            GlobalVars.DisplayApplicationName = "ESPA Communicator/Simulator";
#if (DEBUG)
            GlobalVars.BuildType = "Debug";
#else
            GlobalVars.BuildType = "Release";
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        /// <summary>
        /// take the Active and approved settings and show it on the UI 
        /// or user specified configurations
        /// </summary>
        public static void ApplySettingsOnUI()
        {

        }
    }
}
