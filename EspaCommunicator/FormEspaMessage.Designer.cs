namespace EspaCommunicator
{
    partial class FormEspaMessage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvPackageData = new System.Windows.Forms.ListView();
            this.chRecordType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btAddRecord_ID1 = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.btLoadMessage = new System.Windows.Forms.Button();
            this.btSaveMessage = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.gbAddRecord = new System.Windows.Forms.GroupBox();
            this.btAddRecord_ID8 = new System.Windows.Forms.Button();
            this.btAddRecord_ID7 = new System.Windows.Forms.Button();
            this.btAddRecord_ID6 = new System.Windows.Forms.Button();
            this.btAddRecord_ID5 = new System.Windows.Forms.Button();
            this.btAddRecord_ID4 = new System.Windows.Forms.Button();
            this.btAddRecord_ID3 = new System.Windows.Forms.Button();
            this.btAddRecord_ID2 = new System.Windows.Forms.Button();
            this.btDeleteRecord = new System.Windows.Forms.Button();
            this.btEditRecord = new System.Windows.Forms.Button();
            this.gbAddRecord.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvPackageData
            // 
            this.lvPackageData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chRecordType,
            this.chContent});
            this.lvPackageData.FullRowSelect = true;
            this.lvPackageData.Location = new System.Drawing.Point(12, 12);
            this.lvPackageData.Name = "lvPackageData";
            this.lvPackageData.Size = new System.Drawing.Size(561, 205);
            this.lvPackageData.TabIndex = 1;
            this.lvPackageData.UseCompatibleStateImageBehavior = false;
            this.lvPackageData.View = System.Windows.Forms.View.Details;
            this.lvPackageData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPackageData_MouseDoubleClick);
            // 
            // chRecordType
            // 
            this.chRecordType.Text = "Record Type";
            this.chRecordType.Width = 130;
            // 
            // chContent
            // 
            this.chContent.Text = "Content";
            this.chContent.Width = 350;
            // 
            // btAddRecord_ID1
            // 
            this.btAddRecord_ID1.Location = new System.Drawing.Point(9, 19);
            this.btAddRecord_ID1.Name = "btAddRecord_ID1";
            this.btAddRecord_ID1.Size = new System.Drawing.Size(131, 23);
            this.btAddRecord_ID1.TabIndex = 2;
            this.btAddRecord_ID1.Text = "ID=1 (Call Address)";
            this.btAddRecord_ID1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAddRecord_ID1.UseVisualStyleBackColor = true;
            this.btAddRecord_ID1.Click += new System.EventHandler(this.btAddRecord_ID1_Click);
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(22, 357);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(108, 23);
            this.btClear.TabIndex = 3;
            this.btClear.Text = "Clear Message";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btLoadMessage
            // 
            this.btLoadMessage.Location = new System.Drawing.Point(136, 357);
            this.btLoadMessage.Name = "btLoadMessage";
            this.btLoadMessage.Size = new System.Drawing.Size(112, 23);
            this.btLoadMessage.TabIndex = 4;
            this.btLoadMessage.Text = "Load Message...";
            this.btLoadMessage.UseVisualStyleBackColor = true;
            this.btLoadMessage.Click += new System.EventHandler(this.btLoadMessage_Click);
            // 
            // btSaveMessage
            // 
            this.btSaveMessage.Location = new System.Drawing.Point(254, 357);
            this.btSaveMessage.Name = "btSaveMessage";
            this.btSaveMessage.Size = new System.Drawing.Size(107, 23);
            this.btSaveMessage.TabIndex = 5;
            this.btSaveMessage.Text = "Save Message...";
            this.btSaveMessage.UseVisualStyleBackColor = true;
            this.btSaveMessage.Click += new System.EventHandler(this.btSaveMessage_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(417, 378);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(147, 39);
            this.btClose.TabIndex = 6;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // gbAddRecord
            // 
            this.gbAddRecord.Controls.Add(this.btAddRecord_ID8);
            this.gbAddRecord.Controls.Add(this.btAddRecord_ID7);
            this.gbAddRecord.Controls.Add(this.btAddRecord_ID6);
            this.gbAddRecord.Controls.Add(this.btAddRecord_ID5);
            this.gbAddRecord.Controls.Add(this.btAddRecord_ID4);
            this.gbAddRecord.Controls.Add(this.btAddRecord_ID3);
            this.gbAddRecord.Controls.Add(this.btAddRecord_ID2);
            this.gbAddRecord.Controls.Add(this.btAddRecord_ID1);
            this.gbAddRecord.Location = new System.Drawing.Point(13, 259);
            this.gbAddRecord.Name = "gbAddRecord";
            this.gbAddRecord.Size = new System.Drawing.Size(560, 81);
            this.gbAddRecord.TabIndex = 7;
            this.gbAddRecord.TabStop = false;
            this.gbAddRecord.Text = "Add Record";
            // 
            // btAddRecord_ID8
            // 
            this.btAddRecord_ID8.Location = new System.Drawing.Point(420, 48);
            this.btAddRecord_ID8.Name = "btAddRecord_ID8";
            this.btAddRecord_ID8.Size = new System.Drawing.Size(131, 23);
            this.btAddRecord_ID8.TabIndex = 9;
            this.btAddRecord_ID8.Text = "ID=8 (Sytem Status)";
            this.btAddRecord_ID8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAddRecord_ID8.UseVisualStyleBackColor = true;
            this.btAddRecord_ID8.Click += new System.EventHandler(this.btAddRecord_ID8_Click);
            // 
            // btAddRecord_ID7
            // 
            this.btAddRecord_ID7.Location = new System.Drawing.Point(420, 19);
            this.btAddRecord_ID7.Name = "btAddRecord_ID7";
            this.btAddRecord_ID7.Size = new System.Drawing.Size(131, 23);
            this.btAddRecord_ID7.TabIndex = 8;
            this.btAddRecord_ID7.Text = "ID=7 (Call Status)";
            this.btAddRecord_ID7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAddRecord_ID7.UseVisualStyleBackColor = true;
            this.btAddRecord_ID7.Click += new System.EventHandler(this.btAddRecord_ID7_Click);
            // 
            // btAddRecord_ID6
            // 
            this.btAddRecord_ID6.Location = new System.Drawing.Point(283, 48);
            this.btAddRecord_ID6.Name = "btAddRecord_ID6";
            this.btAddRecord_ID6.Size = new System.Drawing.Size(131, 23);
            this.btAddRecord_ID6.TabIndex = 7;
            this.btAddRecord_ID6.Text = "ID=6 (Priority)";
            this.btAddRecord_ID6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAddRecord_ID6.UseVisualStyleBackColor = true;
            this.btAddRecord_ID6.Click += new System.EventHandler(this.btAddRecord_ID6_Click);
            // 
            // btAddRecord_ID5
            // 
            this.btAddRecord_ID5.Location = new System.Drawing.Point(283, 19);
            this.btAddRecord_ID5.Name = "btAddRecord_ID5";
            this.btAddRecord_ID5.Size = new System.Drawing.Size(131, 23);
            this.btAddRecord_ID5.TabIndex = 6;
            this.btAddRecord_ID5.Text = "ID=5 (No. of Transm.)";
            this.btAddRecord_ID5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAddRecord_ID5.UseVisualStyleBackColor = true;
            this.btAddRecord_ID5.Click += new System.EventHandler(this.btAddRecord_ID5_Click);
            // 
            // btAddRecord_ID4
            // 
            this.btAddRecord_ID4.Location = new System.Drawing.Point(146, 48);
            this.btAddRecord_ID4.Name = "btAddRecord_ID4";
            this.btAddRecord_ID4.Size = new System.Drawing.Size(131, 23);
            this.btAddRecord_ID4.TabIndex = 5;
            this.btAddRecord_ID4.Text = "ID=4 (Call Type)";
            this.btAddRecord_ID4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAddRecord_ID4.UseVisualStyleBackColor = true;
            this.btAddRecord_ID4.Click += new System.EventHandler(this.btAddRecord_ID4_Click);
            // 
            // btAddRecord_ID3
            // 
            this.btAddRecord_ID3.Location = new System.Drawing.Point(146, 19);
            this.btAddRecord_ID3.Name = "btAddRecord_ID3";
            this.btAddRecord_ID3.Size = new System.Drawing.Size(131, 23);
            this.btAddRecord_ID3.TabIndex = 4;
            this.btAddRecord_ID3.Text = "ID=3 (Beep Coding)";
            this.btAddRecord_ID3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAddRecord_ID3.UseVisualStyleBackColor = true;
            this.btAddRecord_ID3.Click += new System.EventHandler(this.btAddRecord_ID3_Click);
            // 
            // btAddRecord_ID2
            // 
            this.btAddRecord_ID2.Location = new System.Drawing.Point(9, 48);
            this.btAddRecord_ID2.Name = "btAddRecord_ID2";
            this.btAddRecord_ID2.Size = new System.Drawing.Size(131, 23);
            this.btAddRecord_ID2.TabIndex = 3;
            this.btAddRecord_ID2.Text = "ID=2 (Display Text)";
            this.btAddRecord_ID2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAddRecord_ID2.UseVisualStyleBackColor = true;
            this.btAddRecord_ID2.Click += new System.EventHandler(this.btAddRecord_ID2_Click);
            // 
            // btDeleteRecord
            // 
            this.btDeleteRecord.Location = new System.Drawing.Point(22, 223);
            this.btDeleteRecord.Name = "btDeleteRecord";
            this.btDeleteRecord.Size = new System.Drawing.Size(108, 23);
            this.btDeleteRecord.TabIndex = 8;
            this.btDeleteRecord.Text = "Delete Record";
            this.btDeleteRecord.UseVisualStyleBackColor = true;
            this.btDeleteRecord.Click += new System.EventHandler(this.btDeleteRecord_Click);
            // 
            // btEditRecord
            // 
            this.btEditRecord.Location = new System.Drawing.Point(140, 223);
            this.btEditRecord.Name = "btEditRecord";
            this.btEditRecord.Size = new System.Drawing.Size(108, 23);
            this.btEditRecord.TabIndex = 9;
            this.btEditRecord.Text = "Edit Record";
            this.btEditRecord.UseVisualStyleBackColor = true;
            this.btEditRecord.Click += new System.EventHandler(this.btEditRecord_Click);
            // 
            // FormEspaMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 429);
            this.Controls.Add(this.btEditRecord);
            this.Controls.Add(this.btDeleteRecord);
            this.Controls.Add(this.gbAddRecord);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btSaveMessage);
            this.Controls.Add(this.btLoadMessage);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.lvPackageData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormEspaMessage";
            this.Text = "E.S.P.A. Message";
            this.gbAddRecord.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvPackageData;
        private System.Windows.Forms.ColumnHeader chContent;
        private System.Windows.Forms.ColumnHeader chRecordType;
        private System.Windows.Forms.Button btAddRecord_ID1;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btLoadMessage;
        private System.Windows.Forms.Button btSaveMessage;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.GroupBox gbAddRecord;
        private System.Windows.Forms.Button btAddRecord_ID8;
        private System.Windows.Forms.Button btAddRecord_ID7;
        private System.Windows.Forms.Button btAddRecord_ID6;
        private System.Windows.Forms.Button btAddRecord_ID5;
        private System.Windows.Forms.Button btAddRecord_ID4;
        private System.Windows.Forms.Button btAddRecord_ID3;
        private System.Windows.Forms.Button btAddRecord_ID2;
        private System.Windows.Forms.Button btDeleteRecord;
        private System.Windows.Forms.Button btEditRecord;
    }
}