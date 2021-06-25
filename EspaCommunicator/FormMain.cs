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
using EspaLib;
using System.IO;

namespace EspaCommunicator
{
    public partial class FormMain : Form
    {
        #region delegates
        private delegate void UpdateTrafficLogCaller(SingleEspaTrafficInfo LoggingInput);
        private delegate void UpdateMethodWithNoArgsCaller();
        #endregion

        #region member data
        private SingleEspaDataBlock LoadedEspaRecordsDataBlock = new SingleEspaDataBlock();
        private EspaTrafficLog LiveDataBusCommunicationLog = new EspaTrafficLog();
        private Point MouseLocationOverTabBarDataBusComLog = new Point();
        private ToolTip ToolTipControl = new ToolTip();
        private FormDataBlockConfigurator DataBlockConfiguration = null;
        private FormDeviceStatus fDeviceStatus = null;
        #endregion

        #region UI init methods 
        /// <summary>
        /// constructor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            RequestEspaDeviceTypeSimulation();

            InitUI();

            SetUserInterfaceToDefault();

            if (Program.GlobalVars.IsSetArgumentToOpenFilePath)
            {
                AddNewTabDataBusCommunicatonLog(Program.GlobalVars.ArgumentToOpenFilePath);
            }
        }

        /// <summary>
        /// initialize the user interface
        /// </summary>
        /// <returns></returns>
        private bool InitUI()
        {
            Text = String.Format("{0}", Program.GlobalVars.DisplayApplicationName);
            Program.GlobalVars.FormMainRectangle = RectangleToScreen(this.ClientRectangle);

            FillComPortSelection();
            UpdateConnectionState();
            InitPropertyGrids();
            RebuildTableTransactionOutputQueue(livwTransactionInputQueue, false);
            RebuildTableTransactionOutputQueue(livwTransactionOutputQueue, true);
            RebuildTableDataBusCommunicationLog(livwLiveDataBusCommLog);

            UpdateViewMenu();
            ShowCommunicationStateOnUI(IsConnected());

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table"></param>
        /// <returns></returns>
        private bool RebuildTableDataBusCommunicationLog(ListView Table)
        {
            if (Table == null) return false;

            try
            {
                Table.Columns.Clear();
                Table.Items.Clear();

                ColumnHeader chDate = new ColumnHeader();
                chDate.Tag = "Date";
                chDate.Text = "Date";
                chDate.Width = 125;

                ColumnHeader chDirection = new ColumnHeader();
                chDirection.Tag = "Direction";
                chDirection.Text = "Direction";

                ColumnHeader chContent = new ColumnHeader();
                chContent.Tag = "Content";
                chContent.Text = "Content";
                chContent.Width = 600;

                Table.Columns.AddRange(new ColumnHeader[] {
                                                    chDate,
                                                    chDirection,
                                                    chContent});

            }
            catch (Exception)
            {

                return false;
            }

            return true; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="Output"></param>
        private bool RebuildTableTransactionOutputQueue(ListView Table, bool Output)
        {
            if (Table == null) return false;

            try
            {
                Table.Columns.Clear();
                Table.Items.Clear();

                ColumnHeader clhAddress = new ColumnHeader();
                clhAddress.Tag = "Address";
                clhAddress.Text = "Address";

                ColumnHeader clhDisplayMessage = new ColumnHeader();
                clhDisplayMessage.Tag = "Content";
                clhDisplayMessage.Text = "Display Message";
                clhDisplayMessage.Width = 300;

                ColumnHeader clhBleep = new ColumnHeader();
                clhBleep.Tag = "Bleep";
                clhBleep.Text = "Bleep";

                ColumnHeader clhCallType = new ColumnHeader();
                clhCallType.Tag = "CallType";
                clhCallType.Text = "Call Type";

                ColumnHeader clhPriority = new ColumnHeader();
                clhPriority.Tag = "Priority";
                clhPriority.Text = "Priority";

                ColumnHeader clhResponse = new ColumnHeader();
                clhResponse.Tag = "Response";
                clhResponse.Text = "Response";

                Table.Columns.AddRange(new ColumnHeader[] {
                                                    clhAddress,
                                                    clhDisplayMessage,
                                                    clhBleep,
                                                    clhCallType,
                                                    clhPriority,
                                                    clhResponse});

            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }
 
        /// <summary>
        /// initialize the property grids
        /// </summary>
        private void InitPropertyGrids()
        {
            if (Program.GlobalVars.DeviceConfiguration.EspaLocalDeviceType == eEspaDeviceType.ControlStation)
            {
                prgConfigSimulation.SelectedObject = Program.GlobalVars.PropGridObjectSimControlStation;
            }
            else if (Program.GlobalVars.DeviceConfiguration.EspaLocalDeviceType == eEspaDeviceType.Station)
            {
                tbcConfiguration.TabPages.RemoveByKey("tpConfigSimulation");
                prgConfigSimulation.SelectedObject = Program.GlobalVars.PropGridObjectSimStation;
            }
 
            prgConfigDevice.SelectedObject = Program.GlobalVars.DeviceConfiguration;
        }

        /// <summary>
        /// fill the com port selection with all available serial com ports on the computer
        /// </summary>
        private void FillComPortSelection(String Selection="")
        {
            string[] ports = Helper.GetAllLocalSerialPorts();

            tsmiConnect.DropDown.Items.Clear();

            foreach (var item in ports)
            {
                ToolStripMenuItem ComPortSelector = new ToolStripMenuItem(item);
                ComPortSelector.Tag = "Connect";
                ComPortSelector.Click += ToolStripMenuPressed;

                tsmiConnect.DropDown.Items.Add(ComPortSelector);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            AllowDrop = true;
        }

        #endregion UI init methods

        #region serial port connection
        /// <summary>
        /// opens a serial port connection to an espa device
        /// </summary>
        /// <returns></returns>
        private bool Connect(String ConnPortName, SerialComVars ComSettings)
        {
            if (ConnPortName == "") return false;

            eReturnValue retval = Program.GlobalVars.EspaCom.Connect(ConnPortName, ComSettings.Baudrate, ComSettings.Parity, ComSettings.SerialDataBits, ComSettings.StopBits, Program.GlobalVars.DeviceConfiguration.EspaLocalDeviceType);

            if (retval == eReturnValue.Ok)
            {
                Program.GlobalVars.EspaCom.UpdateTrafficLogEvent += EspaCom_UpdateTrafficLogEvent;
                ShowCommunicationStateOnUI(IsConnected());
            }
            else
            {
                switch (retval)
                {
                    case eReturnValue.LicenseError:
                        Helper.ShowErrorBox_NoLegalLicense();
                        break;
                    case eReturnValue.GeneralError:
                        break;
                }
            }

            return true;
        }

        /// <summary>
        /// disconnects a serial port connection to an espa device
        /// </summary>
        private void Disconnect()
        {
            if (IsConnected())
            {
                Program.GlobalVars.EspaCom.Disconnect();
                Program.GlobalVars.EspaCom.UpdateTrafficLogEvent -= EspaCom_UpdateTrafficLogEvent;
            }
            ShowCommunicationStateOnUI(IsConnected());
        }

        /// <summary>
        /// returns if the software is connected to a device
        /// </summary>
        /// <returns></returns>
        private bool IsConnected()
        {
            return Program.GlobalVars.EspaCom.IsConnected();
        }
      
        #endregion

        #region UI control
        /// <summary>
        /// show the default configuration for the UI
        /// </summary>
        private void SetUserInterfaceToDefault()
        {
            spcLog.Panel2Collapsed = true;

            UpdateViewMenu();
        }

        /// <summary>
        /// closes the current selected data bus communication log tab
        /// </summary>
        private void CloseSelectedDataBusCommLogTab()
        {
            if (tcDataBusComLogContents.SelectedIndex < 0) return;
            if (tcDataBusComLogContents.SelectedIndex == 0)
            {
                // it's our live communication log
                if (MessageBox.Show("Clear all communiction data logging data?", "Clear Communication Data Log", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    LiveDataBusCommunicationLog.Clear();
                    ReloadLiveDataBusCommLogTable();
                }
            }
            else
            {
                Program.GlobalVars.FileManager.RemoveFile(tcDataBusComLogContents.TabPages[tcDataBusComLogContents.SelectedIndex].Tag.ToString());
                tcDataBusComLogContents.TabPages.RemoveAt(tcDataBusComLogContents.SelectedIndex);
            }
        }

        /// <summary>
        /// depending on the main form visibility state, we hide or show the child forms
        /// </summary>
        /// <param name="WindowState"></param>
        private void ToogleActiveFormVisibility()
        {
            bool IsVisible = true;

            switch (WindowState)
            {
                case FormWindowState.Maximized:
                    IsVisible = true;
                    break;
                case FormWindowState.Normal:
                    IsVisible = true;
                    break;
                case FormWindowState.Minimized:
                    IsVisible = false;
                    break;
            }

            if (IsVisible)
            {
                if (DataBlockConfiguration != null) DataBlockConfiguration.Show();
                if (fDeviceStatus != null) fDeviceStatus.Show();
            }
            else
            {
                if (DataBlockConfiguration != null) DataBlockConfiguration.Hide();
                if (fDeviceStatus != null) fDeviceStatus.Hide();
            }
        }

        /// <summary>
        /// let the user choose the type of simulation it wants to run
        /// </summary>
        private void RequestEspaDeviceTypeSimulation()
        {
            FormEspaDeviceTypeSelection dlg = new FormEspaDeviceTypeSelection();
            dlg.ShowDialog();
            ChangeDeviceTypeSimulation(dlg.ChoosenDeviceType);
        }
  
        /// <summary>
        /// update the user specified simulation and show the update on the UI 
        /// </summary>
        /// <param name="DeviceType"></param>
        private void ChangeDeviceTypeSimulation(eEspaDeviceType DeviceType)
        {
            switch (DeviceType)
            {
                case eEspaDeviceType.None:
                    Program.GlobalVars.DeviceConfiguration.EspaMyLocalAddress = (char)eEspaStandards.AddressControlStation;
                    break;

                case eEspaDeviceType.Station:
                    Program.GlobalVars.DeviceConfiguration.EspaMyLocalAddress = (char)eEspaStandards.StandardSlaveAddress;
                    break;

                case eEspaDeviceType.ControlStation:
                    Program.GlobalVars.DeviceConfiguration.EspaMyLocalAddress = (char)eEspaStandards.AddressControlStation;
                    break;

                default:
                    Program.GlobalVars.DeviceConfiguration.EspaMyLocalAddress = (char)eEspaStandards.AddressControlStation;
                    break;
            }
            Program.GlobalVars.DeviceConfiguration.EspaLocalDeviceType = DeviceType;

            UpdateDeviceConfigurationInformation();
        }

        /// <summary>
        /// TODO: Updates the information about the active license
        /// </summary>
        private void UpdateLicenseInfo()
        {

        }

        /// <summary>
        /// updates the connection state information on the bottom status label
        /// </summary>
        private void UpdateConnectionState()
        {
            String ConnectedPortName = "";

            if (Program.GlobalVars.EspaCom.IsConnected())
            {
                tssConnectionState.Text = String.Format("Connection State: Online  [  {0}  ]", Program.GlobalVars.EspaCom.ConnectionInfoString());
                tssConnectionState.ForeColor = System.Drawing.Color.Navy;

                ConnectedPortName = Program.GlobalVars.EspaCom.SerialComPortName;
            }
            else
            {
                tssConnectionState.Text = "Connection State: Offline";
                tssConnectionState.ForeColor = System.Drawing.Color.Maroon;
            }

            // show the connection state in the port info
            int PortInfoCount = tsmiConnect.DropDown.Items.Count;
            for (int i = 0; i < PortInfoCount; i++)
            {
                if (tsmiConnect.DropDown.Items[i].Text.Equals(ConnectedPortName))
                {
                    ((ToolStripMenuItem)tsmiConnect.DropDown.Items[i]).Checked = true;
                }
                else
                {
                    ((ToolStripMenuItem)tsmiConnect.DropDown.Items[i]).Checked = false;
                }
            }
        }

        /// <summary>
        /// updates the 'View' Part of the top menu
        /// </summary>
        private void UpdateViewMenu()
        {
            int PanelCount = tsmiView.DropDown.Items.Count;
            for (int i = 0; i < PanelCount; i++)
            {
                switch (tsmiView.DropDown.Items[i].Tag.ToString())
                {
                    case "StatusControl":
                        ((ToolStripMenuItem)tsmiView.DropDown.Items[i]).Checked = !spctMain.Panel1Collapsed;
                        break;

                    case "DeviceStatus":
                        ((ToolStripMenuItem)tsmiView.DropDown.Items[i]).Checked = (fDeviceStatus != null);
                        break;

                    case "DataBlockEditor":
                        ((ToolStripMenuItem)tsmiView.DropDown.Items[i]).Checked = (DataBlockConfiguration != null);
                        break;

                    case "TransactionQueue":
                        ((ToolStripMenuItem)tsmiView.DropDown.Items[i]).Checked = !spcLog.Panel2Collapsed; 
                        break;                        
                }
            }
        }

        /// <summary>
        /// update the property grid with the device configuration variables
        /// </summary>
        public void UpdateDeviceConfigurationInformation()
        {
            if (InvokeRequired)
            {
                try
                {
                    UpdateMethodWithNoArgsCaller d = new UpdateMethodWithNoArgsCaller(UpdateDeviceConfigurationInformation);
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
                prgConfigDevice.Refresh();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        /// <summary>
        /// shows the communication state on the UI (disable or enable user inputs)
        /// </summary>
        /// <param name="Online"></param>
        private void ShowCommunicationStateOnUI(bool Online)
        {


        }

        #endregion
        
        #region send espa data into the communication data bus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datablock"></param>
        private void AddRecordDataBlockIntoOutputQueue(SingleEspaDataBlock datablock)
        {
            Program.GlobalVars.EspaCom.AddIntoOutputQueue(new SingleEspaRequest(eEspaRequestType.RecordDataBlock, datablock.Clone()));
        }

        /// <summary>
        /// send an espa request into the espa communication data bus
        /// </summary>
        /// <param name="RequestType"></param>
        /// <param name="data"></param>
        /// <param name="LoggingOutput"></param>
        private void SendEspaRequest(eEspaRequestType RequestType, object data, bool SendEotAfterSuccess = false)
        {
            if (IsConnected() == false) return;

            SingleEspaTrafficInfo LoggingOutput = new SingleEspaTrafficInfo();
            LoggingOutput.Direction = eTrafficDirection.Send;
            LoggingOutput.TrafficDate = DateTime.Now;

            switch (RequestType)
            {
                case eEspaRequestType.ControlSign:
                    if (data != null && data is eAsciiCtrl)
                    {  
                        SendEspaControlSign((eAsciiCtrl)data);                  
                    }
                    break;

                case eEspaRequestType.Poll:
                    if (data != null && data is EspaPollSequence)
                    {
                        Program.GlobalVars.EspaCom.SendRequest(RequestType, data);
                    }
                    break;

                case eEspaRequestType.Select:
                    if (data != null && data is EspaSelectSequence)
                    {
                        Program.GlobalVars.EspaCom.SendRequest(RequestType, data);
                    }

                    break;

                case eEspaRequestType.RecordDataBlock:
                    if (data != null && data is SingleEspaDataBlock)
                    {
                        if (((SingleEspaDataBlock)data).Records.Count <= 0) return;
                        if (((SingleEspaDataBlock)data).HeaderIdentifier <= 0) return;

                        AddRecordDataBlockIntoOutputQueue(data as SingleEspaDataBlock);
                    }
                   break;
            }
        }
     
        /// <summary>
        /// sends one single ascii control sign into the espa communication data bus
        /// </summary>
        /// <param name="ControlSign"></param>
        private void SendEspaControlSign(eAsciiCtrl ControlSign)
        {
            if (IsConnected() == false) return;

            SingleEspaTrafficInfo LoggingOutput = new SingleEspaTrafficInfo();
            LoggingOutput.Direction = eTrafficDirection.Send;
            LoggingOutput.TrafficDate = DateTime.Now;


            // check if it's special response sign
            bool IsResponse = true;
            switch (ControlSign)
            {
                case eAsciiCtrl.EOT:                    
                    break;
                case eAsciiCtrl.ACK:
                    break;
                case eAsciiCtrl.NAK:
                    break;
                default:
                    IsResponse = false;
                    break;
            }

            byte[] data = new byte[] { (byte)ControlSign };

            if (IsResponse == false)
            {
                if (Program.GlobalVars.EspaCom.SendData(data, data.Length, out LoggingOutput))
                {
                    AddSingleDataBusCommunicationLogInfoToLog(LoggingOutput);
                }
            }
            else
            {
                Program.GlobalVars.EspaCom.SendResponse(ControlSign);
            }
        }

        #endregion

        #region data bus communication log
        /// <summary>
        /// adds a new entry into the LiveDataBusCommunication-Log and the ListView LiveDataBusCommunication-Table on the UI
        /// </summary>
        /// <param name="TrafficInfo"></param>
        private void AddSingleDataBusCommunicationLogInfoToLog(SingleEspaTrafficInfo TrafficInfo)
        {
            if (InvokeRequired)
            {
                UpdateTrafficLogCaller d = new UpdateTrafficLogCaller(AddSingleDataBusCommunicationLogInfoToLog);
                this.Invoke(d, new object[] { TrafficInfo });
                return;
            }

            LiveDataBusCommunicationLog.Add(TrafficInfo);
            AddSingleDataBusCommunicationLogInfoToTable(livwLiveDataBusCommLog, TrafficInfo);
            Helper.ScrollDownListView(livwLiveDataBusCommLog);
        }

        /// <summary>
        /// adds the new entry into the ListView LiveDataBusCommunication-Table on the UI
        /// (NO update of the LiveDataBusCommunication-Log!)
        /// </summary>
        /// <param name="TrafficInfo"></param>
        private void AddSingleDataBusCommunicationLogInfoToTable(ListView ListViewTable, SingleEspaTrafficInfo TrafficInfo)
        {
            string[] saLvwItem = new string[3];

            string[] EspaInfoStrings = Helper.ConvertEspaInfosForTable(TrafficInfo.TrafficContent);
            if (EspaInfoStrings == null || EspaInfoStrings.Length < 1) return;

            saLvwItem[0] = TrafficInfo.TrafficDate.ToString();
            saLvwItem[1] = TrafficInfo.Direction.ToString();
            saLvwItem[2] = EspaInfoStrings[0];

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

            // lvNetworkTraffic.Items.Insert(0, lvi);
            ListViewTable.Items.Add(lvi);

            // look for subinfos
            if (EspaInfoStrings.Length > 1)
            {
                for (int i = 1; i < EspaInfoStrings.Length; i++)
                {
                    EspaInfoStrings[i] = EspaInfoStrings[i].Trim();
                    if (EspaInfoStrings[i].Length > 0)
                    {
                        AddSingleDataBusCommunicationLogSubInfoToTable(ListViewTable, TrafficInfo.Direction, EspaInfoStrings[i]);
                    }
                }
            }
        }

        /// <summary>
        /// adds the new sub-entry into the ListView LiveDataBusCommunication-Table on the UI
        /// (NO update of the LiveDataBusCommunication-Log!)
        /// </summary>
        /// <param name="ListViewTable"></param>
        /// <param name="Direction"></param>
        /// <param name="Subinfo"></param>
        private void AddSingleDataBusCommunicationLogSubInfoToTable(ListView ListViewTable, eTrafficDirection Direction, String Subinfo)
        {
            string[] saLvwItem = new string[3];

            saLvwItem[0] = "";
            saLvwItem[1] = "";
            saLvwItem[2] = Subinfo;

            ListViewItem lvi = new ListViewItem(saLvwItem);

            // choose the color
            switch (Direction)
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

            ListViewTable.Items.Add(lvi);
        }

        /// <summary>
        /// clears the LiveDataBusCommunication-Log and updates the LiveDataBusCommunication-Table on the UI
        /// </summary>
        private void ClearLiveDataBusCommunicationLog()
        {
            LiveDataBusCommunicationLog.Clear();
            ReloadLiveDataBusCommLogTable();
        }

        /// <summary>
        /// reloads the content of the LiveDataBusCommunication-Log ListView Table
        /// </summary>
        private void ReloadLiveDataBusCommLogTable(bool ScrollToBottomInsteadOfTop = true)
        {
            livwLiveDataBusCommLog.Items.Clear();

            foreach (var item in LiveDataBusCommunicationLog.TrafficList)
            {
                AddSingleDataBusCommunicationLogInfoToTable(livwLiveDataBusCommLog, item);
            }

            if (ScrollToBottomInsteadOfTop)
            {
                Helper.ScrollDownListView(livwLiveDataBusCommLog);
            }
            else
            {
                Helper.ScrollUpListView(livwLiveDataBusCommLog);
            }
        }

        /// <summary>
        /// writes a DataBusCommunication-Log into a file
        /// </summary>
        private void OnClickSaveDataBusCommLog(EspaTrafficLog LogData, bool OpenInNewTab=false)
        {
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.Title = "Save Traffic Log";
            sfdlg.Filter = "Traffic Log files (*.tlog)|*.tlog";

            if (sfdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (EspaLib.EspaFileIO.SaveTrafficLogInFile(LogData, sfdlg.FileName, true) == false)
                {
                    MessageBox.Show("Error Saving Traffic Log into File! Please try again later!", "ERROR", MessageBoxButtons.OK);
                }
                else
                {
                    String SafeFileName = EspaLib.Helper.GetFileName(sfdlg.FileName);
                    AddNewTabDataBusCommunicatonLog(SafeFileName, sfdlg.FileName, LogData);
                }
            }
        }

        /// <summary>
        /// reads a DataBusCommunication-Log from a file and adds the content either into 
        /// a new Tab (ListView) or updates an existing when the file is already in our FileManager List
        /// </summary>
        private void OnClickLoadDataBusCommLog()
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Title = "Open Traffic Log File";
            ofdlg.CheckFileExists = true;
            ofdlg.CheckPathExists = true;
            ofdlg.Filter = "tlog files (*.tlog)|*.tlog";

            if (ofdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                EspaTrafficLog LogData = EspaLib.EspaFileIO.LoadTrafficLogFromFile(ofdlg.FileName);
                if (LogData != null)
                {
                    if (Program.GlobalVars.FileManager.AddFile(ofdlg.FileName, LogData))
                    {
                        AddNewTabDataBusCommunicatonLog(ofdlg.SafeFileName, ofdlg.FileName, LogData);
                    }
                    else
                    {
                        MessageBox.Show("Error while trying to load file", "File loading error", MessageBoxButtons.OK);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filepath"></param>
        private void AddNewTabDataBusCommunicatonLog(String Filepath)
        {
            String SafeFileName = EspaLib.Helper.GetFileName(Filepath);

            EspaTrafficLog LogData = EspaLib.EspaFileIO.LoadTrafficLogFromFile(Filepath);
             if (LogData != null)
             {
                 AddNewTabDataBusCommunicatonLog(SafeFileName, Filepath, LogData);
             }
        }

        /// <summary>
        /// either inserts a new Tab (ListView) with the content of an loaded log file or if the filename already exists, it updates the relevant Tab (ListView)
        /// </summary>
        /// <param name="LogData"></param>
        private void AddNewTabDataBusCommunicatonLog(String Filename, String Filepath, EspaTrafficLog LogData)
        {
            if (tcDataBusComLogContents.TabPages.ContainsKey(Filename))
            {
                // update an existing tab page
               int Index = tcDataBusComLogContents.TabPages.IndexOfKey(Filename);
               if (tcDataBusComLogContents.TabPages[Index].Controls.Count >= 1 && tcDataBusComLogContents.TabPages[Index].Controls[0] is ListView)
               {
                   ((ListView)tcDataBusComLogContents.TabPages[Index].Controls[0]).Items.Clear();
                   tcDataBusComLogContents.SelectedIndex = Index;

                   foreach (var item in LogData.TrafficList)
                   {
                       AddSingleDataBusCommunicationLogInfoToTable(((ListView)tcDataBusComLogContents.TabPages[Index].Controls[0]), item);
                   }
               }
            }
            else
            {
                // insert a new tab page
                TabPage LogPage = new TabPage(Filename);
                LogPage.Name = Filename;
                LogPage.Tag = Filepath;

                ColumnHeader chLogDate = new ColumnHeader();
                chLogDate.Text = "Date";
                chLogDate.Width = 125;

                ColumnHeader chLogDirection = new ColumnHeader();
                chLogDirection.Text = "Direction";

                ColumnHeader chLogContent = new ColumnHeader();
                chLogContent.Text = "Content";
                chLogContent.Width = 600;


                // create the list view
                ListView lvLogContentList = new ListView();
                lvLogContentList.Columns.AddRange(new ColumnHeader[] { chLogDate, chLogDirection, chLogContent });

                lvLogContentList.ContextMenuStrip = cmsEditDataBusCommunication;
                lvLogContentList.Dock = DockStyle.Fill;
                lvLogContentList.FullRowSelect = true;
                lvLogContentList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                lvLogContentList.Location = new Point(3, 3);
                lvLogContentList.Name = Filename;
                lvLogContentList.Size = new Size(927, 168);
                lvLogContentList.TabIndex = 0;
                lvLogContentList.UseCompatibleStateImageBehavior = false;
                lvLogContentList.View = View.Details;

                LogPage.Controls.Add(lvLogContentList);

                tcDataBusComLogContents.TabPages.Add(LogPage);
                tcDataBusComLogContents.SelectedIndex = tcDataBusComLogContents.TabPages.Count - 1;

                foreach (var item in LogData.TrafficList)
                {
                    AddSingleDataBusCommunicationLogInfoToTable(lvLogContentList, item);
                }
            }
        }
        #endregion

        #region general toolstrip
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuPressed(object sender, EventArgs e)
        {
            String ItemText = "";
            String ItemTag = "";
            String[] TagParts = null;

            if (sender is Button)
            {
                var item = sender as Button;
                ItemText = item.Text;
                ItemTag = (item.Tag != null) ? item.Tag.ToString() : "";
                TagParts = ItemTag.Split(':');
                if (TagParts == null || TagParts.Length == 0) return;
            }
            else if (sender is ToolStripDropDownItem)
            {
                var item = sender as ToolStripDropDownItem;
                ItemText = item.Text;
                ItemTag = (item.Tag != null) ? item.Tag.ToString() : "";
                TagParts = ItemTag.Split(':');
                if (TagParts == null || TagParts.Length == 0) return;
            }

            // now let's work this the Tag and TagParts we received 
            switch (TagParts[0])
            { 
                case "OpenAbout":
                    {
                        FormAbout dlg = new FormAbout();
                        dlg.ShowDialog();
                    }
                    break; 

                case "StatusControl":
                    {
                        /// hide/show the Container with the PropertyGrids
                        spctMain.Panel1Collapsed = !spctMain.Panel1Collapsed;
                        UpdateViewMenu();
                    }
                    break; 

                case "DataBlockEditor":
                    {
                        ToggleFormDataBlockConfiguration();
                    }
                    break;

                case "DeviceStatus":
                    {
                        ToggleFormDeviceStatus();
                    }
                    break;

                case "TransactionQueue":
                    {
                        spcLog.Panel2Collapsed = !spcLog.Panel2Collapsed;
                        UpdateViewMenu();
                    }
                    break;

                case "DataBusCommLogSaveAs":
                    {
                        if (tcDataBusComLogContents.SelectedIndex < 0) return;

                        if (tcDataBusComLogContents.SelectedIndex == 0)
                        {
                            OnClickSaveDataBusCommLog(LiveDataBusCommunicationLog);
                        }
                        else
                        {
                            if (tcDataBusComLogContents.TabPages[tcDataBusComLogContents.SelectedIndex].Controls.Count < 1) return;

                            if (tcDataBusComLogContents.TabPages[tcDataBusComLogContents.SelectedIndex].Controls[0] is ListView)
                            {

                                OnClickSaveDataBusCommLog(LiveDataBusCommunicationLog);
                            }
                        }
                    }
                    break; 

                case "CloseDataBusCommLog":
                    {
                        CloseSelectedDataBusCommLogTab();
                    }
                    break; 

                case "LoadDataBusCommLog":
                    {
                        OnClickLoadDataBusCommLog();
                    }
                    break; 

                case "ClearDataBusCommLog":
                    {
                        ClearLiveDataBusCommunicationLog();
                    }
                    break;

                case "Connect":
                    {
                        if (IsConnected())
                        {
                            Disconnect();
                        }

                        String ComPortName = ItemText;

                        Connect(ComPortName, Program.SettingsMng.ActiveSettings.SerialComSettings);

                        UpdateConnectionState();

                    }
                    break;

                case "Disconnect":
                    {
                        Disconnect();
                        UpdateConnectionState();
                    }
                    break;

                case "ExitApplication":
                    {
                        Disconnect();
                        Close();
                    }
                    break;

                case "OpenConfiguration":
                    {
                        FormConfiguration dlg = new FormConfiguration();
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            Program.SettingsMng.SetRegistrySettingsActive();
                            Program.ApplySettingsOnUI();
                            UpdateDeviceConfigurationInformation();
                        }
                    }
                    break;

                case "OpenLicenseManager":
                    {
                        FormLicenseManager dlg = new FormLicenseManager();
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {

                        }

                        UpdateLicenseInfo();
                    }
                    break;

                case "ChangeESPADeviceType":
                    {
                        Disconnect();
                        Application.Restart();
                    }
                    break;
            }                              
        }
        #endregion general toolstrip 

        #region data block configuratordevice status
        /// <summary>
        /// 
        /// </summary>
        private void ToggleFormDeviceStatus()
        {
            if (fDeviceStatus == null)
            {
                OpenFormDeviceStatus();
            }
            else
            {
                CloseFormDeviceStatus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenFormDeviceStatus()
        {
            if (fDeviceStatus == null)
            {
                fDeviceStatus = new FormDeviceStatus();
                fDeviceStatus.FormClosed += fDeviceStatus_FormClosed;
                fDeviceStatus.Owner = this;
            }
            fDeviceStatus.Show();

            UpdateViewMenu();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CloseFormDeviceStatus()
        {
            if (fDeviceStatus == null) return;

            try
            {
                fDeviceStatus.Close();
                fDeviceStatus.Dispose();
                fDeviceStatus = null;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            UpdateViewMenu();
        }
        #endregion device status

        #region data block configurator
        /// <summary>
        /// 
        /// </summary>
        private void ToggleFormDataBlockConfiguration()
        {
           if (DataBlockConfiguration == null)
           {
               OpenFormDataBlockConfiguration();
           }
           else
           {
               CloseFormDataBlockConfiguration();
           }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenFormDataBlockConfiguration()
        {
            if (DataBlockConfiguration == null)
            {
                DataBlockConfiguration = new FormDataBlockConfigurator();
                DataBlockConfiguration.FormClosed += DataBlockConfiguration_FormClosed;
                DataBlockConfiguration.SendDataIntoEspaDataBusEvent += DataBlockConfiguration_SendDataIntoEspaDataBusEvent;
                DataBlockConfiguration.Owner = this;
            }
            DataBlockConfiguration.Show();

            UpdateViewMenu();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CloseFormDataBlockConfiguration()
        {
            if (DataBlockConfiguration == null) return;
          
            try
            {
                DataBlockConfiguration.Close();
                DataBlockConfiguration.Dispose();
                DataBlockConfiguration = null;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            UpdateViewMenu();
        }
        #endregion data block configurator

        #region events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            DoDragDrop(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            DoDragEnter(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcDataBusComLogContents_DragEnter(object sender, DragEventArgs e)
        {
            DoDragEnter(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcDataBusComLogContents_DragDrop(object sender, DragEventArgs e)
        {
            DoDragDrop(sender, e);
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataBlockConfiguration_SendDataIntoEspaDataBusEvent(object sender, SendDataIntoEspaDataBusArgs e)
        {
            SendEspaRequest(e.Type, e.Content);        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fDeviceStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            fDeviceStatus = null;
            UpdateViewMenu();
        }       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataBlockConfiguration_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataBlockConfiguration = null;
            UpdateViewMenu();
        } 

        /// <summary>
        /// checks the changed property item
        /// </summary>
        /// <param name="e"></param>
        private void PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (s is PropertyGrid)
            {
                var item = s as PropertyGrid;
                String ItemTag = (item.Tag != null) ? item.Tag.ToString() : "";

                if (ItemTag.Equals("ConfigDevice"))
                {
                    if (e.ChangedItem.Label.Equals("ESPA Address"))
                    {
                        if ((char)e.ChangedItem.Value < '1' || (char)e.ChangedItem.Value > '9')
                        {
                            Program.GlobalVars.DeviceConfiguration.EspaMyLocalAddress = (char)e.OldValue;
                        }
                        // TODO: SelectNextFreeSlaveStationAddress();
                    }
                }

                else if (ItemTag.Equals("ConfigSimulation"))
                {
                    if (e.ChangedItem.Label.Equals("Polling Interval"))
                    {
                        if ((int)e.ChangedItem.Value < (int)eEspaConstants.MinPollingInteval || (int)e.ChangedItem.Value > (int)eEspaConstants.MaxPollingInterval)
                        {
                            Program.GlobalVars.PropGridObjectSimControlStation.PollingInterval = (int)e.OldValue;
                        }

                        Program.GlobalVars.DeviceConfiguration.PollingInterval = Program.GlobalVars.PropGridObjectSimControlStation.PollingInterval;
                    }

                    else if (e.ChangedItem.Label.Equals("Polling"))
                    {
                        Program.GlobalVars.DeviceConfiguration.IsActivePolling = Program.GlobalVars.PropGridObjectSimControlStation.IsActivePolling;
                    }

                    else if (e.ChangedItem.Label.Equals("Poll Addresses"))
                    {
                        Program.GlobalVars.DeviceConfiguration.PollingAddresses = Program.GlobalVars.PropGridObjectSimControlStation.PollingAddresses;
                    }

                    Program.GlobalVars.DeviceConfiguration.PollingInterval = Program.GlobalVars.PropGridObjectSimControlStation.PollingInterval;
                    Program.GlobalVars.DeviceConfiguration.IsActivePolling = Program.GlobalVars.PropGridObjectSimControlStation.IsActivePolling;
                    Program.GlobalVars.DeviceConfiguration.IsActivePolling = Program.GlobalVars.PropGridObjectSimControlStation.IsActivePolling;
                    Program.GlobalVars.DeviceConfiguration.PollingAddresses = Program.GlobalVars.PropGridObjectSimControlStation.PollingAddresses;
                }
            }
        }

        /// <summary>
        /// an event triggered when a new data bus communication log entry was created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EspaCom_UpdateTrafficLogEvent(object sender, UpdateTrafficLogArgs e)
        {
            AddSingleDataBusCommunicationLogInfoToLog(e.LogData);
        }

        /// <summary>
        /// TabControl tcDataBusComLogContents Selected Index Changed : Change the ContectMenuStrip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcDataBusComLogContents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcDataBusComLogContents.SelectedIndex <=0)
            {
                cmsTabDataBusCommunication.Items[0].Text = "Clear Log";
            }
            else
            {
                cmsTabDataBusCommunication.Items[0].Text = "Close File";
            }            
        }

        /// <summary>
        /// Mouse Move other the TabControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcDataBusComLogContents_MouseMove(object sender, MouseEventArgs e)
        {
            MouseLocationOverTabBarDataBusComLog = e.Location;
        }

        /// <summary>
        /// ContectMenuStrip Opens for the Tab: cmsTabDataBusCommunication 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsTabDataBusCommunication_Opening(object sender, CancelEventArgs e)
        {
            Rectangle mouseRect = new Rectangle(MouseLocationOverTabBarDataBusComLog.X, MouseLocationOverTabBarDataBusComLog.Y, 1, 1);
            for (int i = 0; i < tcDataBusComLogContents.TabCount; i++)
            {
                if (tcDataBusComLogContents.GetTabRect(i).IntersectsWith(mouseRect))
                {
                    tcDataBusComLogContents.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// Mouse Over and stays at the TabControl:  tcDataBusComLogContents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcDataBusComLogContents_MouseHover(object sender, EventArgs e)
        {
            Rectangle mouseRect = new Rectangle(MouseLocationOverTabBarDataBusComLog.X, MouseLocationOverTabBarDataBusComLog.Y, 1, 1);
            for (int i = 0; i < tcDataBusComLogContents.TabCount; i++)
            {
                if (tcDataBusComLogContents.GetTabRect(i).IntersectsWith(mouseRect))
                {
                    if (i>0)
                    {
                        ToolTipControl.SetToolTip(tcDataBusComLogContents, tcDataBusComLogContents.TabPages[i].Tag.ToString());
                    }
                    else
                    {
                        ToolTipControl.RemoveAll();
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Resize(object sender, EventArgs e)
        {
            ToogleActiveFormVisibility();
            Program.GlobalVars.FormMainRectangle = RectangleToScreen(this.ClientRectangle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Move(object sender, EventArgs e)
        {
            Program.GlobalVars.FormMainRectangle = RectangleToScreen(this.ClientRectangle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcDataBusComLogContents_MouseClick(object sender, MouseEventArgs e)
        {
            if (tcDataBusComLogContents.SelectedIndex > 0)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                {
                    CloseSelectedDataBusCommLogTab();
                }
            }
            base.OnMouseClick(e);
        }
        #endregion  

        #region drag and drop
        /// <summary>
        /// open a file via drag and drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    if (Helper.IsLegalFile(fileLoc))
                    {
                        AddNewTabDataBusCommunicatonLog(fileLoc);                        
                    }

                }
            }
        }

        /// <summary>
        /// open a file via drag and drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }         
        #endregion

    }
}
