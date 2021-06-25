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
    public partial class FormEspaMessage : Form
    {
        public FormEspaMessage()
        {
            InitializeComponent();
            this.Shown += new System.EventHandler(this.FormEspaMessage_Shown);
            Text = String.Format("{0} - Message Editor", Program.GlobalVars.DisplayApplicationName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FormEspaMessage_Shown(object sender, EventArgs e)
        {
            UpateMessageTable();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btAddRecord_ID1_Click(object sender, EventArgs e)
        {
            OpenAddRecordForm(1);
        }

        private void btAddRecord_ID2_Click(object sender, EventArgs e)
        {
            OpenAddRecordForm(2);
        }

        private void btAddRecord_ID3_Click(object sender, EventArgs e)
        {
            OpenAddRecordForm(3);
        }

        private void btAddRecord_ID4_Click(object sender, EventArgs e)
        {
            OpenAddRecordForm(4);
        }

        private void btAddRecord_ID5_Click(object sender, EventArgs e)
        {
            OpenAddRecordForm(5);
        }

        private void btAddRecord_ID6_Click(object sender, EventArgs e)
        {
            OpenAddRecordForm(6);
        }

        private void btAddRecord_ID7_Click(object sender, EventArgs e)
        {
            OpenAddRecordForm(7);
        }

        private void btAddRecord_ID8_Click(object sender, EventArgs e)
        {
            OpenAddRecordForm(8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecordID"></param>
        private void OpenAddRecordForm(int RecordID = 2)
        {
            FormEditEspaRecord edit = new FormEditEspaRecord( eEditMode.New, RecordID);
            if (edit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AddRecordToMessage(edit.Record);
            }

            UpateMessageTable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecordID"></param>
        private void OpenEditRecordForm(int SelectedIndex)
        {
            FormEditEspaRecord edit = new FormEditEspaRecord(eEditMode.Edit, 
                                                             Program.GlobalVars.ActiveEspaMessage.Records[SelectedIndex].RecordID, 
                                                             Program.GlobalVars.ActiveEspaMessage.Records[SelectedIndex].RecordData);

            if (edit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Program.GlobalVars.ActiveEspaMessage.Records[SelectedIndex] = edit.Record;
            }

            UpateMessageTable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        private void AddRecordToMessage(SingleEspaRecord record)
        {
            Program.GlobalVars.ActiveEspaMessage.AddRecord(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        private void DeleteRecordFromMessage(SingleEspaRecord record)
        {
            Program.GlobalVars.ActiveEspaMessage.RemoveRecord(record);
        }


        /// <summary>
        /// 
        /// </summary>
        private void UpateMessageTable()
        {
            lvPackageData.Items.Clear();

            foreach (var item in Program.GlobalVars.ActiveEspaMessage.Records)
            {
                string[] saLvwItem = new string[2];
                saLvwItem[0] = String.Format("{1} ({0})", Helper.GetRecordTypeName(item.RecordID), item.RecordID);
                saLvwItem[1] = item.RecordData;

                ListViewItem lvi = new ListViewItem(saLvwItem);

                lvPackageData.Items.Add(lvi);             
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetSelectedIndex()
        {
            if (lvPackageData.SelectedItems.Count > 0)
            {
                return lvPackageData.Items.IndexOf(lvPackageData.SelectedItems[0]);
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvPackageData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int SelectedIndex = GetSelectedIndex();

            if (SelectedIndex < 0) return;
            if (SelectedIndex >= Program.GlobalVars.ActiveEspaMessage.CountRecords) return;

            OpenEditRecordForm(SelectedIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDeleteRecord_Click(object sender, EventArgs e)
        {
            int SelectedIndex = GetSelectedIndex();

            if (SelectedIndex < 0) return;
            if (SelectedIndex >= Program.GlobalVars.ActiveEspaMessage.CountRecords) return;


            if (MessageBox.Show("Delete this Record?","Delete Single Message-Record",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                DeleteRecordFromMessage(Program.GlobalVars.ActiveEspaMessage.Records[SelectedIndex]);
                UpateMessageTable();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEditRecord_Click(object sender, EventArgs e)
        {
            int SelectedIndex = GetSelectedIndex();

            if (SelectedIndex < 0) return;
            if (SelectedIndex >= Program.GlobalVars.ActiveEspaMessage.CountRecords) return;
   
            OpenEditRecordForm(SelectedIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLoadMessage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Title = "Open ESPA Message File";
            ofdlg.CheckFileExists = true;
            ofdlg.CheckPathExists = true; 
            ofdlg.Filter = "espa files (*.espa)|*.espa"; 

            if (ofdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

               SingleEspaMessage LoadedMessage = Helper.LoadEspaMessageFromFile(ofdlg.FileName);

                if (LoadedMessage!=null)
                {
                    Program.GlobalVars.ActiveEspaMessage = LoadedMessage;
                    UpateMessageTable();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSaveMessage_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.Title = "Save ESPA Message File";
            sfdlg.Filter = "espa files (*.espa)|*.espa"; 

            if (sfdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Helper.SaveEspaMessageInFile(Program.GlobalVars.ActiveEspaMessage, sfdlg.FileName,true) == false)
                {
                    MessageBox.Show("Error Saving E.S.P.A. Message into File! Please try again later!", "ERROR", MessageBoxButtons.OK);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClear_Click(object sender, EventArgs e)
        {
            Program.GlobalVars.ActiveEspaMessage.ClearMessage();
            UpateMessageTable();
        }     
    }
}
