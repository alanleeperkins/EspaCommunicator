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
    public partial class FormDeviceStatus : SpecialToolForm
    {
        #region delegates
        private delegate void UpdateMethodWithNoArgsCaller();
        #endregion
        
        public FormDeviceStatus()
        {
            InitializeComponent();

            InitUI();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitUI()
        {
            InitPropertyGrids();
            Program.GlobalVars.EspaCom.UpdateStatusCommunicationLineEvent += EspaCom_UpdateStatusCommunicationLineEvent;
        }

        /// <summary>
        /// initialize the property grids
        /// </summary>
        private void InitPropertyGrids()
        {
            prgrDeviceStates.SelectedObject = Program.GlobalVars.DeviceStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EspaCom_UpdateStatusCommunicationLineEvent(object sender, EspaLib.UpdateStatusCommunicationLineArgs e)
        {
            UpdateDeviceStateInformation();
        }

        /// <summary>
        /// update the property grid with the device state information
        /// </summary>
        public void UpdateDeviceStateInformation()
        {
            if (InvokeRequired)
            {
                try
                {
                    UpdateMethodWithNoArgsCaller d = new UpdateMethodWithNoArgsCaller(UpdateDeviceStateInformation);
                    this.Invoke(d);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }

                return;
            }

            try
            {
                prgrDeviceStates.Refresh();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }


    }
}
