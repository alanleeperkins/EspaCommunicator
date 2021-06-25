using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EspaCommunicator
{
    static class Program
    {

        public static GlobalData GlobalVars = new GlobalData();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GlobalVars.DisplayApplicationName = "E.S.P.A. Communicator";
#if (DEBUG)
            GlobalVars.BuildType = "Debug";
#else
            GlobalVars.BuildType = "Release";
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormTrafficOverview());
        }
    }
}
