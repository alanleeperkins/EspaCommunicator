using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using EspaLib;
using System.Windows.Forms;

namespace EspaCommunicator
{
	public class Helper
	{
        /// <summary>
        /// check if the argument is a legal (*.tlog) and existing file
        /// </summary>
        /// <param name="Filepath"></param>
        /// <returns></returns>
        public static bool IsLegalFile(String Filepath)
        {
            bool Legal = true;

            try
            {
                if (File.Exists(Filepath) == false) Legal = false;

                if (Filepath.EndsWith(".tlog") == false) Legal = false;

            }
            catch (Exception)
            {
                Legal = false;            
            }

            return Legal;
        }

		/// <summary>
		/// returns all serial ports found on that system
		/// </summary>
		/// <returns></returns>
		public static string[] GetAllLocalSerialPorts()
		{
			try
			{
				// Get a list of serial port names.
				string[] ports = SerialPort.GetPortNames();
				return ports;
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// special formats the espa rawdata for a better look in the ListView Table
        /// exp. adds line breaks after important commands like RS EOT STX ENQ
		/// </summary>
		/// <param name="RawData"></param>
		/// <returns></returns>
		public static string [] ConvertEspaInfosForTable(string RawData)
		{
			List<string> Replacer = new List<string>();

			Replacer.Add("<RS>"); Replacer.Add("<EOT>"); Replacer.Add("<STX>"); Replacer.Add("<ENQ>");

			foreach (var item in Replacer)
			{
				RawData = RawData.Replace(item,item+"\n");
			}

			string[] subs = RawData.Split('\n');
			if (subs == null)
			{
				subs = new string[] {RawData}; 
			}

			return subs;
		}

		/// <summary>
		/// shows and special error for showing the user an no legal license problem
		/// </summary>
		public static void ShowErrorBox_NoLegalLicense()
		{
			ShowErrorBox("License-Error","No legal license found for this action.\nPlease check the License-Manager!");
		}

		/// <summary>
		/// shows an error box with user specified title and message
		/// </summary>
		/// <param name="Title"></param>
		/// <param name="Message"></param>
		private static void ShowErrorBox(String Title, String Message)
		{
			FormErrorMessage dlg = new FormErrorMessage(Title,Message);
			dlg.ShowDialog();
		}

        /// <summary>
        /// scroll to the last entry of the ListView
        /// </summary>
        public static void ScrollDownListView(ListView livw)
        {
            if (livw == null) return;
            if (livw.Items.Count <= 1) return;
            livw.EnsureVisible(livw.Items.Count - 1);
        }

        /// <summary>
        /// scroll to the top of the traffic log
        /// </summary>
        public static void ScrollUpListView(ListView livw)
        {
            if (livw == null) return;
            if (livw.Items.Count < 1) return;
            livw.Items[0].EnsureVisible();
        }


	}
}
