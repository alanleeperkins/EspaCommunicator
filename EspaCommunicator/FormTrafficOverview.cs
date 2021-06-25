using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EspaCommunicator
{
    public partial class FormTrafficOverview : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public FormTrafficOverview()
        {
            InitializeComponent();

            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool Init()
        {
            FillComPortSelection();
            UpdateConnectionState();

            Text = String.Format("{0} - Traffic Log", Program.GlobalVars.DisplayApplicationName);
           
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillComPortSelection(String Selection="")
        {
            string[] ports = Helper.GetAllSerialPorts();

            connectToolStripMenuItem.DropDown.Items.Clear();

            foreach (var item in ports)
            {
                ToolStripMenuItem ComPortSelector = new ToolStripMenuItem(item);
                ComPortSelector.Click += btConnect_Click;

                connectToolStripMenuItem.DropDown.Items.Add(ComPortSelector);
            }
        }

        #region dummy data
        /// <summary>
        /// 
        /// </summary>
        private void InsertDummyTraffic()
        {
            AddDummySendTrafficLog();

            AddDummyReceiveTrafficLog();
            AddDummyReceiveTrafficLog();
            AddDummyReceiveTrafficLog();
            AddDummyReceiveTrafficLog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddDummySendTrafficLog()
        {
            AddSingleTrafficInfoToLog(new SingleEspaTrafficInfo
            {
                Direction = eTrafficDirection.Send,
                TrafficDate = DateTime.Now,
                TrafficContent = Helper.ConvertToReadableEspa(new byte[] { 2, 0x41, 0x42, 5 })
            });
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddDummyReceiveTrafficLog()
        {
            AddSingleTrafficInfoToLog(new SingleEspaTrafficInfo
            {
                Direction = eTrafficDirection.Receive,
                TrafficDate = DateTime.Now,
                TrafficContent = Helper.ConvertToReadableEspa(new byte[] { 2, 0x48, 0x61, 0x6e, 0x73, 5 })
            });
        }
        #endregion 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrafficInfo"></param>
        private void AddSingleTrafficInfoToLog(SingleEspaTrafficInfo TrafficInfo)
        {
            Program.GlobalVars.ActiveTrafficLogData.Add(TrafficInfo);
            UpdateTrafficLog();
        }


        /// <summary>
        /// add the new traffic info to the table on the UI
        /// </summary>
        /// <param name="TrafficInfo"></param>
        private void AddSingleTrafficInfoToTable(SingleEspaTrafficInfo TrafficInfo)
        {
            string[] saLvwItem = new string[3];
            saLvwItem[0] = TrafficInfo.TrafficDate.ToString();
            saLvwItem[1] = TrafficInfo.Direction.ToString();
            saLvwItem[2] = TrafficInfo.TrafficContent;

            ListViewItem lvi = new ListViewItem(saLvwItem);

            // choose the color
            switch (TrafficInfo.Direction)
            {
                case eTrafficDirection.Send:
                    lvi.ForeColor = System.Drawing.Color.Black;
                    lvi.BackColor = System.Drawing.Color.Beige;
                    break;

                case eTrafficDirection.Receive:
                    lvi.ForeColor = System.Drawing.Color.Black;
                    lvi.BackColor = System.Drawing.Color.Azure;
                    break;

                default:
                    break;
            }

            lvNetworkTraffic.Items.Add(lvi);         
        }

        private void ClearTrafficLog()
        {
            Program.GlobalVars.ActiveTrafficLogData.Clear();
            UpdateTrafficLog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateTrafficLog()
        {
            lvNetworkTraffic.Items.Clear();

            foreach (var item in Program.GlobalVars.ActiveTrafficLogData.TrafficList)
            {
                AddSingleTrafficInfoToTable(item);
            }
            ScrollDownTrafficLog();
        }

        /// <summary>
        /// scroll to the top of the bottom log
        /// </summary>
        private void ScrollDownTrafficLog()
        {
            if (lvNetworkTraffic.Items.Count <= 10) return;
            lvNetworkTraffic.Items[lvNetworkTraffic.Items.Count - 1].EnsureVisible();
        }

        /// <summary>
        /// scroll to the top of the traffic log
        /// </summary>
        private void ScrollUpTrafficLog()
        {
            if (lvNetworkTraffic.Items.Count < 1) return;
            lvNetworkTraffic.Items[0].EnsureVisible();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateConnectionState()
        {
            if (Program.GlobalVars.SerialInterface.IsConnected)
            {
                tssConnectionState.Text = String.Format("Connection State: Online [{0}]", Program.GlobalVars.SerialInterface.ComPort.PortName);
                tssConnectionState.ForeColor = System.Drawing.Color.Navy;
            }
            else
            {
                tssConnectionState.Text = "Connection State: Offline";
                tssConnectionState.ForeColor = System.Drawing.Color.Maroon;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool Connect(String ConnPortName)
        {
            if (ConnPortName == "") return false;

            int ConnBaudrate = 9600;
            Parity ConnParity = Parity.None;
            int ConnDataBits = 8;

            bool Successful = Program.GlobalVars.SerialInterface.Connect(ConnPortName, ConnBaudrate, ConnParity, ConnDataBits);

            if (Successful)
            {
                Program.GlobalVars.SerialInterface.SerialDataReceivedEvent += SerialInterface_SerialDataReceivedEvent;
            }

            return Successful;
        }


        /// <summary>
        /// 
        /// </summary>
        private void Disconnect()
        {
            if (Program.GlobalVars.SerialInterface.IsConnected)
            {
                Program.GlobalVars.SerialInterface.Disconnect();
                Program.GlobalVars.SerialInterface.SerialDataReceivedEvent -= SerialInterface_SerialDataReceivedEvent;
            }
        }

        /// <summary>
        /// returns if the software is connected to a device
        /// </summary>
        /// <returns></returns>
        private bool IsConnected()
        {
            if (Program.GlobalVars.SerialInterface == null) return false;
            return Program.GlobalVars.SerialInterface.IsConnected;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SerialInterface_SerialDataReceivedEvent(object sender, SerialDataReceivedArgs e)
        {
            SingleEspaTrafficInfo LoggingInput = new SingleEspaTrafficInfo();
            LoggingInput.TrafficContent = Helper.ConvertToReadableEspa(e.ReceivedData, e.DataLength);
            LoggingInput.Direction = eTrafficDirection.Receive;
            LoggingInput.TrafficDate = e.Date;           
        }

        /// <summary>
        /// send the globa me
        /// </summary>
        /// <returns></returns>
        private bool SendMessagePackage(SingleEspaMessage MessagePackage, out SingleEspaTrafficInfo LoggingOutput)
        {
            // TODO: check the connection state
            char[] cStructure = Enumerable.Repeat((char)0, Constants.MAX_STRUCTURE_SIZE).ToArray();
            int iStructurePointer = 0;

            ///////////////////////////////////////////////////////////////////
            // insert head 1*
            cStructure[iStructurePointer++] = Constants.SOH;
            cStructure[iStructurePointer++] = Constants.EOT;
            cStructure[iStructurePointer++] = Constants.EOT;
            cStructure[iStructurePointer++] = Constants.SOH;
            cStructure[iStructurePointer++] = Constants.ESPA_H_STAT_INFO;
            cStructure[iStructurePointer++] = Constants.STX;

            // send the records
            foreach (var item in MessagePackage.Records)
            {
                // DataIdentifier
                cStructure[iStructurePointer++] = (char)item.RecordID;
                // UnitSeperator
                cStructure[iStructurePointer++] = Constants.US;
                // Data
                for (int i = 0; i <item.RecordData.Length; i++)
                {
                    cStructure[iStructurePointer++] = (char)item.RecordData[i];
                }
                // RecordSeperator
                cStructure[iStructurePointer++] = Constants.RS;          
            }

            ///////////////////////////////////////////////////////////////////
            // send tail 1*
            char cCalcBCC = (char)0; 
            cStructure[iStructurePointer++] = Constants.ETX;
            cCalcBCC = (char)Helper.CalculateBCC(cStructure, Constants.MAX_STRUCTURE_SIZE);
            cStructure[iStructurePointer++] = cCalcBCC;

            ///////////////////////////////////////////////////////////////////
            //check the data structure
            if (Helper.CheckBCC(cStructure, Constants.MAX_STRUCTURE_SIZE) == 1)
            {
                Console.WriteLine("bcc OK");
            }
            else
            {
                Console.WriteLine("bcc ERROR");
            }

            ///////////////////////////////////////////////////////////////////
            // finish the data structure
            cStructure[iStructurePointer] = (char)0;
    
            ///////////////////////////////////////////////////////////////////
            // now send it and wait for the answer
            bool SendSuccessful = Program.GlobalVars.SerialInterface.SendData(cStructure, iStructurePointer);

            if (SendSuccessful==true)
            {
                LoggingOutput = new SingleEspaTrafficInfo();
                LoggingOutput.TrafficContent = Helper.ConvertToReadableEspa(cStructure, iStructurePointer);
                LoggingOutput.Direction = eTrafficDirection.Send;
                LoggingOutput.TrafficDate = DateTime.Now;
            }
            else
            {
                LoggingOutput = null; 
            }

            return SendSuccessful;
        }

        #region buttonevents
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btConnect_Click(object sender, EventArgs e)
        {
            var menuitem = sender as ToolStripMenuItem;
            String ComPortName = menuitem.Text;
            Connect(ComPortName);
            UpdateConnectionState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDisConnect_Click(object sender, EventArgs e)
        {
            OnClickDisconnect();
        }   
        #endregion

        private void OnClickDisconnect()
        {
            Disconnect();
            UpdateConnectionState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEditMessagePackage_Click(object sender, EventArgs e)
        {
            OnClickEditMessagePackage();
        }

        private void OnClickEditMessagePackage()
        {
            FormEspaMessage dlg = new FormEspaMessage();
            dlg.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSendMessagePackage_Click(object sender, EventArgs e)
        {
            OnClickSendMessagePackage();  
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnClickSendMessagePackage()
        {
            if (IsConnected() == false) return;
            if (Program.GlobalVars.ActiveEspaMessage.Records.Count <= 0) return;

            SingleEspaTrafficInfo LoggingOutput = new SingleEspaTrafficInfo();
            if (SendMessagePackage(Program.GlobalVars.ActiveEspaMessage, out LoggingOutput) == true)
            {
                AddSingleTrafficInfoToLog(LoggingOutput);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClearTrafficLog_Click(object sender, EventArgs e)
        {
            ClearTrafficLog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSaveTrafficLog_Click(object sender, EventArgs e)
        {
            OnClickSaveTrafficLog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnClickSaveTrafficLog()
        {
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.Title = "Save Traffic Log";
            sfdlg.Filter = "Traffic Log files (*.tlog)|*.tlog";

            if (sfdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Helper.SaveTrafficLogInFile(Program.GlobalVars.ActiveTrafficLogData, sfdlg.FileName, true) == false)
                {
                    MessageBox.Show("Error Saving Traffic Log into File! Please try again later!", "ERROR", MessageBoxButtons.OK);
                }
            }  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLoadTrafficLog_Click(object sender, EventArgs e)
        {
            OnClickLoadTrafficLog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnClickLoadTrafficLog()
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Title = "Open Traffic Log File";
            ofdlg.CheckFileExists = true;
            ofdlg.CheckPathExists = true;
            ofdlg.Filter = "tlog files (*.tlog)|*.tlog";

            if (ofdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TrafficLog LogData = Helper.LoadTrafficLogFromFile(ofdlg.FileName);
                if (LogData != null)
                {
                    Program.GlobalVars.ActiveTrafficLogData = LogData;
                    UpdateTrafficLog();
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormTrafficOverview_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            switch (e.KeyChar)
            {
                case 's':
                    AddDummySendTrafficLog();
                    break;

                case 'r':
                      AddDummyReceiveTrafficLog();
                      break;

                default:
                    break;
            }
            */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Disconnect();
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearTrafficLog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnClickSaveTrafficLog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnClickLoadTrafficLog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editMessagePackageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OnClickEditMessagePackage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendMessagePackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnClickSendMessagePackage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout dlg = new FormAbout();
            dlg.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnClickDisconnect();
        }
    }
}
