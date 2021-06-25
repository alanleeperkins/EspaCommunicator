using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EspaCommunicator
{
    public partial class FormEditEspaRecord : Form
    {
        private eEditMode EditMode = eEditMode.New;
        private SingleEspaRecord record;

        public SingleEspaRecord Record
        {
            get { return record; }
            set { record = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public FormEditEspaRecord(eEditMode editmode, int recordid, string recorddata="" )
        {
            InitializeComponent();
            Text = String.Format("{0} - Record Editor", Program.GlobalVars.DisplayApplicationName);

            record = new SingleEspaRecord();

            EditMode = editmode;
            record.RecordID = recordid;

            // show the right tab
            if (tcEspaRecordEditor.TabPages.Count >=8)
            {
                if (recordid >= 1 && recordid <= 8)
                {
                    String PageKey = GetSelectedPageKey(recordid - 1);

                    EnableSingleTab(PageKey);
                }
            }

            // maybe we have to set some values (in EDIT Mode) 
            if (EditMode == eEditMode.Edit)
            {
                record.RecordData = recorddata;
                UpdateUI();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TabPos"></param>
        /// <returns></returns>
        private String GetSelectedPageKey(int TabPos)
        {
            String Key = "";
            for (int i = 0; i < tcEspaRecordEditor.TabPages.Count; i++)
            {
                if (i==TabPos)
                {
                    Key = tcEspaRecordEditor.TabPages[i].Name;
                    break;
                }
            }
            return Key;
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateUI()
        {
            switch (record.RecordID)
            {
                case 1:
                    tbCallAddress.Text = record.RecordData;
                    break;
                case 2:
                    tbDisplayText.Text = record.RecordData;
                    break;
                case 3:
                    SelectComboBoxIndex(ref coboBeepCoding, Convert.ToInt32(record.RecordData));
                    break;
                case 4:
                    SelectComboBoxIndex(ref coboCallType, Convert.ToInt32(record.RecordData));
                    break;
                case 5:
                    tbNumberOfTransmissions.Text = record.RecordData;
                    break;
                case 6:
                    SelectComboBoxIndex(ref coboPriorityLevel, Convert.ToInt32(record.RecordData));
                    break;
                case 7:
                    SelectComboBoxIndex(ref coboCallStatus, Convert.ToInt32(record.RecordData));
                    break;
                case 8:
                    SelectComboBoxIndex(ref coboSystemStatus, Convert.ToInt32(record.RecordData));
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
        /// <param name="Index"></param>
        private void SelectComboBoxIndex(ref ComboBox box, int Index)
        {
            if (box == null) return;
            if (Index < 0) return;
            if (Index >= box.Items.Count) return;

            box.SelectedIndex = Index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TabPos"></param>
        /// <returns></returns>
        private bool EnableSingleTab(String PageKey)
        {
            List<String> ToBeRemoved = new List<string>();

            for (int i = 0; i < tcEspaRecordEditor.TabPages.Count; i++)
            {
                if (tcEspaRecordEditor.TabPages[i].Name != PageKey)
                {
                    ToBeRemoved.Add(tcEspaRecordEditor.TabPages[i].Name);
                }
            }

            foreach (var item in ToBeRemoved)
            {
                tcEspaRecordEditor.TabPages.RemoveByKey(item);        
            }

            return true; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {

            if (CheckUserInput() == false)
            {
                MessageBox.Show("ERROR: Your input seems to be incorrect!","E.S.P.A. Communicator - Error",MessageBoxButtons.OK);
                return;
            }

            switch (record.RecordID)
            {
                case 1:
                    record.RecordData = tbCallAddress.Text;
                    break;
                case 2:
                    record.RecordData = tbDisplayText.Text;
                    break;
                case 3:
                    record.RecordData = coboBeepCoding.SelectedIndex.ToString();
                    break;
                case 4:
                    record.RecordData = coboCallType.SelectedIndex.ToString();
                    break;
                case 5:
                    record.RecordData = tbNumberOfTransmissions.Text;
                    break;
                case 6:
                    record.RecordData = coboPriorityLevel.SelectedIndex.ToString();
                    break;
                case 7:
                    record.RecordData = coboCallStatus.SelectedIndex.ToString();
                    break;
                case 8:
                    record.RecordData = coboSystemStatus.SelectedIndex.ToString();
                    break;

                default:
                    break;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckUserInput()
        {
            switch (record.RecordID)
            {
                case 1:
                    if (tbCallAddress.Text == "") return false;
                    break;
                case 2:
                    if (tbDisplayText.Text == "") return false;
                    break;
                case 3:
                    if (coboBeepCoding.SelectedIndex < 0) return false;
                    break;
                case 4:
                    if (coboCallType.SelectedIndex < 0) return false;
                    break;
                case 5:
                    if (tbNumberOfTransmissions.Text == "") return false;
                    return Regex.IsMatch(tbNumberOfTransmissions.Text, @"^\d+$");

                case 6:
                    if (coboPriorityLevel.SelectedIndex < 0) return false;
                    break;
                case 7:
                    if (coboCallStatus.SelectedIndex < 0) return false;
                    break;
                case 8:
                    if (coboSystemStatus.SelectedIndex < 0) return false;
                    break;

                default:
                    break;
            }

            return true;
        }
    }
}
