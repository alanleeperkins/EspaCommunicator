using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EspaLib;
using System.Timers;


namespace EspaCommunicator
{
    public partial class FormDataBlockConfigurator : SpecialToolForm
    {
        #region events
        public event SendDataIntoEspaDataBusHandler SendDataIntoEspaDataBusEvent;
        #endregion events

        #region member data
        private SingleEspaDataBlock LoadedEspaRecordsDataBlock = new SingleEspaDataBlock();
        #endregion

        #region init
        /// <summary>
        /// 
        /// </summary>
        public FormDataBlockConfigurator()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {                    
            InitUI();
        }

        /// <summary>
        /// initialize the user interface
        /// </summary>
        /// <returns></returns>
        private bool InitUI()
        {
            TopMost = false;
            Text = String.Format("{0} - Data Block Configuratior", Program.GlobalVars.DisplayApplicationName);

            FillEspaAddressSelection(coboSlaveStationAddress);
            FillEspaHeaderTypeNameSelection(coboDataBlockHeaderType);
            FillSpecialCharacterSelection(coboSpecialCharacters);
            
            InitRecordDataBlockTable();            
            UpdateRecordDataBlock();
            return true;
        }

        /// <summary>
        /// fill the most common used special characters in the combo box coboSpecialCharacters
        /// </summary>
        private void FillEspaAddressSelection(ComboBox cobo)
        {
            cobo.Items.Clear();

            for (char i = (char)eEspaStandards.LowestAddress; i <= (char)eEspaStandards.HighestAddress; i++)
            {
                cobo.Items.Add(i);

            }

            cobo.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cobo"></param>
        private void FillEspaHeaderTypeNameSelection(ComboBox cobo)
        {
            cobo.Items.Clear();

            foreach (var item in Constants.EspaHeaderTypeNames)
            {
                cobo.Items.Add(item);
            }

            cobo.SelectedIndex = 1;
        }

        /// <summary>
        /// fill the most common used special characters in the combo box coboSpecialCharacters
        /// </summary>
        private void FillSpecialCharacterSelection(ComboBox cobo)
        {
            cobo.Items.Clear();

            foreach (var item in Enum.GetValues(typeof(eAsciiCtrl)))
            {
                cobo.Items.Add(item);
            }

            cobo.SelectedIndex = 3;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitRecordDataBlockTable()
        {
            livwRecordDataBlock.Columns.Clear();
            livwRecordDataBlock.ContextMenuStrip = cmsRecordDataBlock;

            ColumnHeader chRecordType = new ColumnHeader();
            chRecordType.Text = "Record Type";
            chRecordType.Width = 130;

            ColumnHeader chContent = new ColumnHeader();
            chContent.Text = "Content";
            chContent.Width = 290;

            livwRecordDataBlock.Columns.AddRange(new  ColumnHeader[] {
                chRecordType,
                chContent
            });

            livwRecordDataBlock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvRecordDataBlock_KeyDown);
            livwRecordDataBlock.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRecordDataBlock_MouseDoubleClick);

        }
        #endregion



        #region UI control
        /// <summary>
        /// selects the next available 'Slave Station' Address (except my local address) in the combo box coboSlaveStationAddress
        /// </summary>
        private void SelectNextFreeSlaveStationAddress()
        {
            for (char Address = '1'; Address <= '9'; Address++)
            {
                if (Address != Program.GlobalVars.DeviceConfiguration.EspaMyLocalAddress)
                {
                    UpdateSlaveStationAddressSelection(Address);
                    break;
                }
            }
        }

        /// <summary>
        /// sets the selected 'Slave Station' Address in the combo box coboSlaveStationAddress
        /// </summary>
        /// <param name="SelectedValue"></param>
        private void UpdateSlaveStationAddressSelection(char SelectedValue)
        {
            int Index = SelectedValue - 48;
            if (Index <= 0) Index = 0;
            else Index -= 1;
            coboSlaveStationAddress.SelectedIndex = Index;
        }

        /// <summary>
        /// returns the selected 'Slave Station' Address selected in the combo box coboSlaveStationAddress
        /// </summary>
        private byte GetSelectSlaveStationAddress()
        {
            byte Addr = 0;

            try
            {
                String Value = (coboSlaveStationAddress.SelectedIndex + 1).ToString();
                Addr = (byte)Value[0];
            }
            catch (Exception exc)
            {
                Console.WriteLine("GetSelectSlaveStationAddress: ERROR {0}", exc.Message);
            }

            return Addr;
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
                case "SendSpecialCharacter":
                    {
                        if (coboSpecialCharacters.SelectedIndex < 0) break;
                        Raise_SendDataIntoEspaDataBus(new SendDataIntoEspaDataBusArgs(eEspaRequestType.ControlSign, (eAsciiCtrl)coboSpecialCharacters.SelectedItem));
                    }
                    break;

                case "SendPollSequence":
                    {
                        if (GetSelectSlaveStationAddress() == 0) break;
                        EspaPollSequence poll = new EspaPollSequence((char)GetSelectSlaveStationAddress());
                        Raise_SendDataIntoEspaDataBus(new SendDataIntoEspaDataBusArgs(eEspaRequestType.Poll, poll));
                    }
                    break;

                case "SendSelectSequence":
                    {
                        if (Program.GlobalVars.DeviceConfiguration.EspaMyLocalAddress.ToString().Length != 1) break;
                        if (GetSelectSlaveStationAddress() == 0) break;
                        EspaSelectSequence select = new EspaSelectSequence(Program.GlobalVars.DeviceConfiguration.EspaMyLocalAddress, (char)GetSelectSlaveStationAddress());
                        Raise_SendDataIntoEspaDataBus(new SendDataIntoEspaDataBusArgs(eEspaRequestType.Select, select));
                    }
                    break;

                case "AddRecord":
                    {
                        int RecordTypeID;
                        if (Int32.TryParse(TagParts[1], out RecordTypeID))
                        {
                            if (RecordTypeID > 0 && RecordTypeID <= (int)eEspaConstants.MaxRecordTypes)
                            {
                                OpenAddRecordForm(RecordTypeID);
                            }
                        }
                    }
                    break;

                case "ClearRecordDataBlock":
                    {
                        /// clear espa data block
                        LoadedEspaRecordsDataBlock.ClearDataBlock();
                        UpateRecordDataBlockTable();
                    }
                    break;

                case "LoadRecordDataBlockFromFile":
                    {
                        /// load espa data block file                        
                        OpenFileDialog ofdlg = new OpenFileDialog();
                        ofdlg.Title = "Open ESPA DataBlock File";
                        ofdlg.CheckFileExists = true;
                        ofdlg.CheckPathExists = true;
                        ofdlg.Filter = "espa files (*.espa)|*.espa";

                        if (ofdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            SingleEspaDataBlock LoadedDataBlock = EspaLib.EspaFileIO.LoadEspaDataBlockFromFile(ofdlg.FileName);

                            if (LoadedDataBlock != null)
                            {
                                LoadedEspaRecordsDataBlock = LoadedDataBlock;

                                // Update some data form the settings
                                LoadedEspaRecordsDataBlock.HeaderIdentifier = GetSelectedDataBlockHeaderType();

                                UpateRecordDataBlockTable();
                            }
                        }
                    }
                    break;

                case "SaveRecordDataBlockInFile":
                    {
                        /// save espa data block in file  
                        SaveFileDialog sfdlg = new SaveFileDialog();
                        sfdlg.Title = "Save ESPA DataBlock File";
                        sfdlg.Filter = "espa files (*.espa)|*.espa";

                        if (sfdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (EspaLib.EspaFileIO.SaveEspaDataBlockInFile(LoadedEspaRecordsDataBlock, sfdlg.FileName, true) == false)
                            {
                                MessageBox.Show("Error Saving ESPA DataBlock into File! Please try again later!", "ERROR", MessageBoxButtons.OK);
                            }
                        }
                    }
                    break;

                case "InsertRecordDataBlockIntoSendQueue":
                    {
                        if (LoadedEspaRecordsDataBlock == null) break;
                        if (LoadedEspaRecordsDataBlock.CountRecords <= 0) break;

                        LoadedEspaRecordsDataBlock.ReceiverAddress = (byte)GetSelectSlaveStationAddress();
                        Raise_SendDataIntoEspaDataBus(new SendDataIntoEspaDataBusArgs(eEspaRequestType.RecordDataBlock, LoadedEspaRecordsDataBlock.Clone()));
                    }
                    break;
            }


        }

        #endregion general toolstrip 

        #region event handler methods
        /// <summary>
        /// fires the 'SendEspaDataBlockEvent' event when we have some data to send to the espa data bus
        /// </summary>
        /// <param name="e"></param>
        public void Raise_SendDataIntoEspaDataBus(SendDataIntoEspaDataBusArgs e)
        {
            if (SendDataIntoEspaDataBusEvent != null)
            {
                SendDataIntoEspaDataBusEvent(this, e);
            }
        }
        #endregion

        #region events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvRecordDataBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                OnClickedDeleteRecord();
            }
            else if (e.KeyCode == Keys.Return)
            {
                OnOpenEditRecordForm();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvRecordDataBlock_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnOpenEditRecordForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void coboSlaveStationAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadedEspaRecordsDataBlock.ReceiverAddress = (byte)GetSelectSlaveStationAddress();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void coboDataBlockHeaderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadedEspaRecordsDataBlock.HeaderIdentifier = GetSelectedDataBlockHeaderType();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormDataBlockConfigurator_MouseEnter(object sender, EventArgs e)
        {
            Console.WriteLine("mouse enter");
        }

        #endregion

        #region record data block
        /// <summary>
        /// refreshes the UI content of the active loaded record data block
        /// </summary>
        private void UpdateRecordDataBlock()
        {
            UpateRecordDataBlockTable();

            if (LoadedEspaRecordsDataBlock.HeaderIdentifier <= 0 || LoadedEspaRecordsDataBlock.HeaderIdentifier.ToString().Length != 1)
            {
                LoadedEspaRecordsDataBlock.HeaderIdentifier = (char)eEspaStandards.StandardHeaderIdentifier;
            }

            UpdateDataBlockHeaderTypeSelection(LoadedEspaRecordsDataBlock.HeaderIdentifier);
            SelectNextFreeSlaveStationAddress();
        }

        /// <summary>
        /// opens the single record editor for editing an existing record in the active loaded record data block
        /// </summary>
        /// <param name="RecordID"></param>
        private void OpenEditRecordForm(int SelectedIndex)
        {
            FormEditEspaRecord edit = new FormEditEspaRecord(eEditMode.Edit,
                                                             LoadedEspaRecordsDataBlock.Records[SelectedIndex].RecordID,
                                                             LoadedEspaRecordsDataBlock.Records[SelectedIndex].RecordData);

            if (edit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadedEspaRecordsDataBlock.Records[SelectedIndex] = edit.Record;
            }

            UpateRecordDataBlockTable();
        }

        /// <summary>
        /// opens the single record editor for adding a new record into the active loaded record data block
        /// </summary>
        /// <param name="RecordID"></param>
        private void OpenAddRecordForm(int RecordID = 2)
        {
            if (LoadedEspaRecordsDataBlock.Records.Count >= (int)eEspaConstants.MaxRecordsPerStructure)
            {
                MessageBox.Show("Max Records Limit reached!", "Error", MessageBoxButtons.OK);
                return;
            }

            FormEditEspaRecord edit = new FormEditEspaRecord(eEditMode.New, RecordID);
            if (edit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AddRecordToDataBlock(edit.Record);
            }

            UpateRecordDataBlockTable();
        }

        /// <summary>
        /// adds a single records into the active loaded RecordDataBlock
        /// </summary>
        /// <param name="record"></param>
        private void AddRecordToDataBlock(SingleEspaRecord record)
        {
            LoadedEspaRecordsDataBlock.AddRecord(record);
        }

        /// <summary>
        /// removes a single records from the active loaded RecordDataBlock
        /// </summary>
        /// <param name="record"></param>
        private void DeleteRecordFromDataBlock(SingleEspaRecord record)
        {
            LoadedEspaRecordsDataBlock.RemoveRecord(record);
        }

        /// <summary>
        /// refreshes the Record Data Block Table (livwRecordDataBlock) with the data of the active loaded record data block 
        /// </summary>
        private void UpateRecordDataBlockTable()
        {
            livwRecordDataBlock.Items.Clear();

            foreach (var item in LoadedEspaRecordsDataBlock.Records)
            {
                string[] saLvwItem = new string[2];
                saLvwItem[0] = String.Format("{1} ({0})", ESPA.GetRecordTypeName(item.RecordID), item.RecordID);
                saLvwItem[1] = item.RecordData;

                ListViewItem lvi = new ListViewItem(saLvwItem);

                livwRecordDataBlock.Items.Add(lvi);
            }
        }

        /// <summary>
        /// returns the selected record in the ListView livwRecordDataBlock
        /// </summary>
        /// <returns></returns>
        private int GetRecordDataBlockSelectedIndex()
        {
            if (livwRecordDataBlock.SelectedItems.Count > 0)
            {
                return livwRecordDataBlock.Items.IndexOf(livwRecordDataBlock.SelectedItems[0]);
            }
            return -1;
        }

        /// <summary>
        /// starts the editor for the selected record
        /// </summary>
        private void OnOpenEditRecordForm()
        {
            int SelectedIndex = GetRecordDataBlockSelectedIndex();

            if (SelectedIndex < 0) return;
            if (SelectedIndex >= LoadedEspaRecordsDataBlock.CountRecords) return;

            OpenEditRecordForm(SelectedIndex);
        }

        /// <summary>
        /// deletes the record selected in the record editor table
        /// </summary>
        private void OnClickedDeleteRecord()
        {
            int SelectedIndex = GetRecordDataBlockSelectedIndex();

            if (SelectedIndex < 0) return;
            if (SelectedIndex >= LoadedEspaRecordsDataBlock.CountRecords) return;

            if (MessageBox.Show("Delete this Record?", "Delete Single DataBlock-Record", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                DeleteRecordFromDataBlock(LoadedEspaRecordsDataBlock.Records[SelectedIndex]);
                UpateRecordDataBlockTable();
            }
        }

        /// <summary>
        /// sets the selected data block header type in the combo box coboDataBlockHeaderType
        /// </summary>
        /// <param name="SelectedValue"></param>
        private void UpdateDataBlockHeaderTypeSelection(char SelectedValue)
        {
            int Index = SelectedValue - 48;
            if (Index <= 0) Index = 0;
            else Index -= 1;
            coboDataBlockHeaderType.SelectedIndex = Index;
        }

        /// <summary>
        /// returns the selected data block header type selected in the combo box coboDataBlockHeaderType
        /// </summary>
        /// <returns></returns>
        public char GetSelectedDataBlockHeaderType()
        {
            String sign = "1";
            if (coboDataBlockHeaderType.SelectedIndex > -1)
            {
                sign = (coboDataBlockHeaderType.SelectedIndex + 1).ToString();
            }

            return sign[0];
        }

        #endregion
    }
}
