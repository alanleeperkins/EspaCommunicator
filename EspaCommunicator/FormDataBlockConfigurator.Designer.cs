namespace EspaCommunicator
{
    partial class FormDataBlockConfigurator
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
            this.components = new System.ComponentModel.Container();
            this.lbTempSlaveStationAddress = new System.Windows.Forms.Label();
            this.coboSlaveStationAddress = new System.Windows.Forms.ComboBox();
            this.lbDataBlockHeaderType = new System.Windows.Forms.Label();
            this.btInsertRecordDataBlockIntoSendQueue = new System.Windows.Forms.Button();
            this.coboDataBlockHeaderType = new System.Windows.Forms.ComboBox();
            this.livwRecordDataBlock = new System.Windows.Forms.ListView();
            this.cmsRecordDataBlock = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiClearRecordDataBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadRecordDataBlockFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveRecordDataBlockInFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addRecordToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iD1CallAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iD2DisplayTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iD3BeepCodingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iD4CallTypeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iD5NumberOfTransmissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iD6PriorityToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iD7CallStatusToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iD8SystemStatusToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpRecordDataBlockEditor = new System.Windows.Forms.TabPage();
            this.tlpRecordData = new System.Windows.Forms.TableLayoutPanel();
            this.tbpSpecialCommands = new System.Windows.Forms.TabPage();
            this.lbInfoSelectSequence = new System.Windows.Forms.Label();
            this.btSendSelectSequence = new System.Windows.Forms.Button();
            this.lbInfoPolling = new System.Windows.Forms.Label();
            this.btSendPolling = new System.Windows.Forms.Button();
            this.coboSpecialCharacters = new System.Windows.Forms.ComboBox();
            this.btSendSelectedCharacter = new System.Windows.Forms.Button();
            this.cmsRecordDataBlock.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbpRecordDataBlockEditor.SuspendLayout();
            this.tlpRecordData.SuspendLayout();
            this.tbpSpecialCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTempSlaveStationAddress
            // 
            this.lbTempSlaveStationAddress.AutoSize = true;
            this.lbTempSlaveStationAddress.Location = new System.Drawing.Point(3, 0);
            this.lbTempSlaveStationAddress.Name = "lbTempSlaveStationAddress";
            this.lbTempSlaveStationAddress.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lbTempSlaveStationAddress.Size = new System.Drawing.Size(111, 20);
            this.lbTempSlaveStationAddress.TabIndex = 29;
            this.lbTempSlaveStationAddress.Text = "Slave Station Address";
            // 
            // coboSlaveStationAddress
            // 
            this.coboSlaveStationAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboSlaveStationAddress.FormattingEnabled = true;
            this.coboSlaveStationAddress.Location = new System.Drawing.Point(120, 3);
            this.coboSlaveStationAddress.Name = "coboSlaveStationAddress";
            this.coboSlaveStationAddress.Size = new System.Drawing.Size(175, 21);
            this.coboSlaveStationAddress.TabIndex = 31;
            this.coboSlaveStationAddress.SelectedIndexChanged += new System.EventHandler(this.coboSlaveStationAddress_SelectedIndexChanged);
            // 
            // lbDataBlockHeaderType
            // 
            this.lbDataBlockHeaderType.AutoSize = true;
            this.lbDataBlockHeaderType.Location = new System.Drawing.Point(3, 30);
            this.lbDataBlockHeaderType.Name = "lbDataBlockHeaderType";
            this.lbDataBlockHeaderType.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lbDataBlockHeaderType.Size = new System.Drawing.Size(69, 20);
            this.lbDataBlockHeaderType.TabIndex = 25;
            this.lbDataBlockHeaderType.Text = "Header Type";
            // 
            // btInsertRecordDataBlockIntoSendQueue
            // 
            this.tlpRecordData.SetColumnSpan(this.btInsertRecordDataBlockIntoSendQueue, 2);
            this.btInsertRecordDataBlockIntoSendQueue.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btInsertRecordDataBlockIntoSendQueue.Location = new System.Drawing.Point(3, 220);
            this.btInsertRecordDataBlockIntoSendQueue.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.btInsertRecordDataBlockIntoSendQueue.Name = "btInsertRecordDataBlockIntoSendQueue";
            this.btInsertRecordDataBlockIntoSendQueue.Size = new System.Drawing.Size(247, 41);
            this.btInsertRecordDataBlockIntoSendQueue.TabIndex = 28;
            this.btInsertRecordDataBlockIntoSendQueue.Tag = "InsertRecordDataBlockIntoSendQueue";
            this.btInsertRecordDataBlockIntoSendQueue.Text = "Insert into Send Qeue";
            this.btInsertRecordDataBlockIntoSendQueue.UseVisualStyleBackColor = true;
            this.btInsertRecordDataBlockIntoSendQueue.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // coboDataBlockHeaderType
            // 
            this.coboDataBlockHeaderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboDataBlockHeaderType.FormattingEnabled = true;
            this.coboDataBlockHeaderType.Location = new System.Drawing.Point(120, 33);
            this.coboDataBlockHeaderType.Name = "coboDataBlockHeaderType";
            this.coboDataBlockHeaderType.Size = new System.Drawing.Size(175, 21);
            this.coboDataBlockHeaderType.TabIndex = 26;
            this.coboDataBlockHeaderType.SelectedIndexChanged += new System.EventHandler(this.coboDataBlockHeaderType_SelectedIndexChanged);
            // 
            // livwRecordDataBlock
            // 
            this.tlpRecordData.SetColumnSpan(this.livwRecordDataBlock, 2);
            this.livwRecordDataBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.livwRecordDataBlock.FullRowSelect = true;
            this.livwRecordDataBlock.Location = new System.Drawing.Point(3, 63);
            this.livwRecordDataBlock.Name = "livwRecordDataBlock";
            this.livwRecordDataBlock.Size = new System.Drawing.Size(461, 144);
            this.livwRecordDataBlock.TabIndex = 23;
            this.livwRecordDataBlock.UseCompatibleStateImageBehavior = false;
            this.livwRecordDataBlock.View = System.Windows.Forms.View.Details;
            // 
            // cmsRecordDataBlock
            // 
            this.cmsRecordDataBlock.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClearRecordDataBlock,
            this.tsmiLoadRecordDataBlockFromFile,
            this.tsmiSaveRecordDataBlockInFile,
            this.toolStripSeparator2,
            this.addRecordToolStripMenuItem1});
            this.cmsRecordDataBlock.Name = "cmsRecordDataBlock";
            this.cmsRecordDataBlock.Size = new System.Drawing.Size(152, 98);
            // 
            // tsmiClearRecordDataBlock
            // 
            this.tsmiClearRecordDataBlock.Name = "tsmiClearRecordDataBlock";
            this.tsmiClearRecordDataBlock.Size = new System.Drawing.Size(151, 22);
            this.tsmiClearRecordDataBlock.Tag = "ClearRecordDataBlock";
            this.tsmiClearRecordDataBlock.Text = "Clear...";
            this.tsmiClearRecordDataBlock.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // tsmiLoadRecordDataBlockFromFile
            // 
            this.tsmiLoadRecordDataBlockFromFile.Name = "tsmiLoadRecordDataBlockFromFile";
            this.tsmiLoadRecordDataBlockFromFile.Size = new System.Drawing.Size(151, 22);
            this.tsmiLoadRecordDataBlockFromFile.Tag = "LoadRecordDataBlockFromFile";
            this.tsmiLoadRecordDataBlockFromFile.Text = "Load from file...";
            this.tsmiLoadRecordDataBlockFromFile.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // tsmiSaveRecordDataBlockInFile
            // 
            this.tsmiSaveRecordDataBlockInFile.Name = "tsmiSaveRecordDataBlockInFile";
            this.tsmiSaveRecordDataBlockInFile.Size = new System.Drawing.Size(151, 22);
            this.tsmiSaveRecordDataBlockInFile.Tag = "SaveRecordDataBlockInFile";
            this.tsmiSaveRecordDataBlockInFile.Text = "Save in file...";
            this.tsmiSaveRecordDataBlockInFile.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(148, 6);
            // 
            // addRecordToolStripMenuItem1
            // 
            this.addRecordToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iD1CallAddressToolStripMenuItem,
            this.iD2DisplayTextToolStripMenuItem,
            this.iD3BeepCodingToolStripMenuItem1,
            this.iD4CallTypeToolStripMenuItem1,
            this.iD5NumberOfTransmissionToolStripMenuItem,
            this.iD6PriorityToolStripMenuItem1,
            this.iD7CallStatusToolStripMenuItem1,
            this.iD8SystemStatusToolStripMenuItem1});
            this.addRecordToolStripMenuItem1.Name = "addRecordToolStripMenuItem1";
            this.addRecordToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.addRecordToolStripMenuItem1.Text = "Add Record...";
            this.addRecordToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // iD1CallAddressToolStripMenuItem
            // 
            this.iD1CallAddressToolStripMenuItem.Name = "iD1CallAddressToolStripMenuItem";
            this.iD1CallAddressToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.iD1CallAddressToolStripMenuItem.Tag = "AddRecord:1";
            this.iD1CallAddressToolStripMenuItem.Text = "ID1 Call Address";
            this.iD1CallAddressToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // iD2DisplayTextToolStripMenuItem
            // 
            this.iD2DisplayTextToolStripMenuItem.Name = "iD2DisplayTextToolStripMenuItem";
            this.iD2DisplayTextToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.iD2DisplayTextToolStripMenuItem.Tag = "AddRecord:2";
            this.iD2DisplayTextToolStripMenuItem.Text = "ID2 Display Text";
            this.iD2DisplayTextToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // iD3BeepCodingToolStripMenuItem1
            // 
            this.iD3BeepCodingToolStripMenuItem1.Name = "iD3BeepCodingToolStripMenuItem1";
            this.iD3BeepCodingToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.iD3BeepCodingToolStripMenuItem1.Tag = "AddRecord:3";
            this.iD3BeepCodingToolStripMenuItem1.Text = "ID3 Beep Coding";
            this.iD3BeepCodingToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // iD4CallTypeToolStripMenuItem1
            // 
            this.iD4CallTypeToolStripMenuItem1.Name = "iD4CallTypeToolStripMenuItem1";
            this.iD4CallTypeToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.iD4CallTypeToolStripMenuItem1.Tag = "AddRecord:4";
            this.iD4CallTypeToolStripMenuItem1.Text = "ID4 Call Type";
            this.iD4CallTypeToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // iD5NumberOfTransmissionToolStripMenuItem
            // 
            this.iD5NumberOfTransmissionToolStripMenuItem.Name = "iD5NumberOfTransmissionToolStripMenuItem";
            this.iD5NumberOfTransmissionToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.iD5NumberOfTransmissionToolStripMenuItem.Tag = "AddRecord:5";
            this.iD5NumberOfTransmissionToolStripMenuItem.Text = "ID5 Number of transmission";
            this.iD5NumberOfTransmissionToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // iD6PriorityToolStripMenuItem1
            // 
            this.iD6PriorityToolStripMenuItem1.Name = "iD6PriorityToolStripMenuItem1";
            this.iD6PriorityToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.iD6PriorityToolStripMenuItem1.Tag = "AddRecord:6";
            this.iD6PriorityToolStripMenuItem1.Text = "ID6 Priority";
            this.iD6PriorityToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // iD7CallStatusToolStripMenuItem1
            // 
            this.iD7CallStatusToolStripMenuItem1.Name = "iD7CallStatusToolStripMenuItem1";
            this.iD7CallStatusToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.iD7CallStatusToolStripMenuItem1.Tag = "AddRecord:7";
            this.iD7CallStatusToolStripMenuItem1.Text = "ID7 Call Status";
            this.iD7CallStatusToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // iD8SystemStatusToolStripMenuItem1
            // 
            this.iD8SystemStatusToolStripMenuItem1.Name = "iD8SystemStatusToolStripMenuItem1";
            this.iD8SystemStatusToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.iD8SystemStatusToolStripMenuItem1.Tag = "AddRecord:8";
            this.iD8SystemStatusToolStripMenuItem1.Text = "ID8 System Status";
            this.iD8SystemStatusToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpRecordDataBlockEditor);
            this.tabControl1.Controls.Add(this.tbpSpecialCommands);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(481, 346);
            this.tabControl1.TabIndex = 32;
            // 
            // tbpRecordDataBlockEditor
            // 
            this.tbpRecordDataBlockEditor.Controls.Add(this.tlpRecordData);
            this.tbpRecordDataBlockEditor.Location = new System.Drawing.Point(4, 22);
            this.tbpRecordDataBlockEditor.Name = "tbpRecordDataBlockEditor";
            this.tbpRecordDataBlockEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tbpRecordDataBlockEditor.Size = new System.Drawing.Size(473, 320);
            this.tbpRecordDataBlockEditor.TabIndex = 0;
            this.tbpRecordDataBlockEditor.Tag = "RecordDataBlockEditor";
            this.tbpRecordDataBlockEditor.Text = "Record Data Block Editor";
            this.tbpRecordDataBlockEditor.UseVisualStyleBackColor = true;
            // 
            // tlpRecordData
            // 
            this.tlpRecordData.ColumnCount = 2;
            this.tlpRecordData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRecordData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRecordData.Controls.Add(this.lbTempSlaveStationAddress, 0, 0);
            this.tlpRecordData.Controls.Add(this.btInsertRecordDataBlockIntoSendQueue, 0, 3);
            this.tlpRecordData.Controls.Add(this.livwRecordDataBlock, 0, 2);
            this.tlpRecordData.Controls.Add(this.coboDataBlockHeaderType, 1, 1);
            this.tlpRecordData.Controls.Add(this.coboSlaveStationAddress, 1, 0);
            this.tlpRecordData.Controls.Add(this.lbDataBlockHeaderType, 0, 1);
            this.tlpRecordData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRecordData.Location = new System.Drawing.Point(3, 3);
            this.tlpRecordData.Name = "tlpRecordData";
            this.tlpRecordData.RowCount = 4;
            this.tlpRecordData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpRecordData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpRecordData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpRecordData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpRecordData.Size = new System.Drawing.Size(467, 314);
            this.tlpRecordData.TabIndex = 32;
            // 
            // tbpSpecialCommands
            // 
            this.tbpSpecialCommands.Controls.Add(this.lbInfoSelectSequence);
            this.tbpSpecialCommands.Controls.Add(this.btSendSelectSequence);
            this.tbpSpecialCommands.Controls.Add(this.lbInfoPolling);
            this.tbpSpecialCommands.Controls.Add(this.btSendPolling);
            this.tbpSpecialCommands.Controls.Add(this.coboSpecialCharacters);
            this.tbpSpecialCommands.Controls.Add(this.btSendSelectedCharacter);
            this.tbpSpecialCommands.Location = new System.Drawing.Point(4, 22);
            this.tbpSpecialCommands.Name = "tbpSpecialCommands";
            this.tbpSpecialCommands.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSpecialCommands.Size = new System.Drawing.Size(473, 320);
            this.tbpSpecialCommands.TabIndex = 1;
            this.tbpSpecialCommands.Tag = "SpecialCommands";
            this.tbpSpecialCommands.Text = "Special Commands";
            this.tbpSpecialCommands.UseVisualStyleBackColor = true;
            // 
            // lbInfoSelectSequence
            // 
            this.lbInfoSelectSequence.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfoSelectSequence.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbInfoSelectSequence.Location = new System.Drawing.Point(12, 144);
            this.lbInfoSelectSequence.Name = "lbInfoSelectSequence";
            this.lbInfoSelectSequence.Size = new System.Drawing.Size(278, 55);
            this.lbInfoSelectSequence.TabIndex = 31;
            this.lbInfoSelectSequence.Text = "The \'Temporary Master Station\' selects a device to which it has data to transfer." +
    " The choosen device agrees with sending <ACK> and becomes  \'Slave Station\' ";
            // 
            // btSendSelectSequence
            // 
            this.btSendSelectSequence.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btSendSelectSequence.Location = new System.Drawing.Point(15, 204);
            this.btSendSelectSequence.Name = "btSendSelectSequence";
            this.btSendSelectSequence.Size = new System.Drawing.Size(247, 41);
            this.btSendSelectSequence.TabIndex = 30;
            this.btSendSelectSequence.Tag = "SendSelectSequence";
            this.btSendSelectSequence.Text = "Send Poll Sequence + Select Sequence";
            this.btSendSelectSequence.UseVisualStyleBackColor = true;
            this.btSendSelectSequence.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // lbInfoPolling
            // 
            this.lbInfoPolling.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfoPolling.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbInfoPolling.Location = new System.Drawing.Point(12, 35);
            this.lbInfoPolling.Name = "lbInfoPolling";
            this.lbInfoPolling.Size = new System.Drawing.Size(287, 45);
            this.lbInfoPolling.TabIndex = 29;
            this.lbInfoPolling.Text = "The control station polls an \'Idle\' device on the communication line. The receive" +
    "r becomes a \'Temporary Master Station\'";
            // 
            // btSendPolling
            // 
            this.btSendPolling.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btSendPolling.Location = new System.Drawing.Point(15, 83);
            this.btSendPolling.Name = "btSendPolling";
            this.btSendPolling.Size = new System.Drawing.Size(247, 41);
            this.btSendPolling.TabIndex = 28;
            this.btSendPolling.Tag = "SendPollSequence";
            this.btSendPolling.Text = "Send Poll Sequence";
            this.btSendPolling.UseVisualStyleBackColor = true;
            this.btSendPolling.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // coboSpecialCharacters
            // 
            this.coboSpecialCharacters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboSpecialCharacters.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.coboSpecialCharacters.FormattingEnabled = true;
            this.coboSpecialCharacters.Location = new System.Drawing.Point(175, 269);
            this.coboSpecialCharacters.Name = "coboSpecialCharacters";
            this.coboSpecialCharacters.Size = new System.Drawing.Size(87, 28);
            this.coboSpecialCharacters.TabIndex = 27;
            // 
            // btSendSelectedCharacter
            // 
            this.btSendSelectedCharacter.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btSendSelectedCharacter.Location = new System.Drawing.Point(15, 263);
            this.btSendSelectedCharacter.Name = "btSendSelectedCharacter";
            this.btSendSelectedCharacter.Size = new System.Drawing.Size(150, 41);
            this.btSendSelectedCharacter.TabIndex = 26;
            this.btSendSelectedCharacter.Tag = "SendSpecialCharacter";
            this.btSendSelectedCharacter.Text = "Send Special Character";
            this.btSendSelectedCharacter.UseVisualStyleBackColor = true;
            this.btSendSelectedCharacter.Click += new System.EventHandler(this.ToolStripMenuPressed);
            // 
            // FormDataBlockConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 346);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDataBlockConfigurator";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "FormDataBlockConfiguratior";
            this.Text = "Data Block Configuratior";
            this.cmsRecordDataBlock.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbpRecordDataBlockEditor.ResumeLayout(false);
            this.tlpRecordData.ResumeLayout(false);
            this.tlpRecordData.PerformLayout();
            this.tbpSpecialCommands.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTempSlaveStationAddress;
        private System.Windows.Forms.ComboBox coboSlaveStationAddress;
        private System.Windows.Forms.Label lbDataBlockHeaderType;
        private System.Windows.Forms.Button btInsertRecordDataBlockIntoSendQueue;
        private System.Windows.Forms.ComboBox coboDataBlockHeaderType;
        private System.Windows.Forms.ListView livwRecordDataBlock;
        private System.Windows.Forms.ContextMenuStrip cmsRecordDataBlock;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearRecordDataBlock;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadRecordDataBlockFromFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveRecordDataBlockInFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem addRecordToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iD1CallAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iD2DisplayTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iD3BeepCodingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iD4CallTypeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iD5NumberOfTransmissionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iD6PriorityToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iD7CallStatusToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iD8SystemStatusToolStripMenuItem1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpRecordDataBlockEditor;
        private System.Windows.Forms.TabPage tbpSpecialCommands;
        private System.Windows.Forms.Label lbInfoSelectSequence;
        private System.Windows.Forms.Button btSendSelectSequence;
        private System.Windows.Forms.Label lbInfoPolling;
        private System.Windows.Forms.Button btSendPolling;
        private System.Windows.Forms.ComboBox coboSpecialCharacters;
        private System.Windows.Forms.Button btSendSelectedCharacter;
        private System.Windows.Forms.TableLayoutPanel tlpRecordData;
    }
}