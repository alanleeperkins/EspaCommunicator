namespace EspaCommunicator
{
    partial class FormEditEspaRecord
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
            this.tcEspaRecordEditor = new System.Windows.Forms.TabControl();
            this.tpCallAddress = new System.Windows.Forms.TabPage();
            this.tbCallAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpDisplayText = new System.Windows.Forms.TabPage();
            this.tbDisplayText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tpBeepCoding = new System.Windows.Forms.TabPage();
            this.coboBeepCoding = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tpCallType = new System.Windows.Forms.TabPage();
            this.coboCallType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tpNumerOfTransmissions = new System.Windows.Forms.TabPage();
            this.tbNumberOfTransmissions = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tpPriority = new System.Windows.Forms.TabPage();
            this.coboPriorityLevel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tpCallStatus = new System.Windows.Forms.TabPage();
            this.coboCallStatus = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tpSystemStatus = new System.Windows.Forms.TabPage();
            this.coboSystemStatus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.tcEspaRecordEditor.SuspendLayout();
            this.tpCallAddress.SuspendLayout();
            this.tpDisplayText.SuspendLayout();
            this.tpBeepCoding.SuspendLayout();
            this.tpCallType.SuspendLayout();
            this.tpNumerOfTransmissions.SuspendLayout();
            this.tpPriority.SuspendLayout();
            this.tpCallStatus.SuspendLayout();
            this.tpSystemStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcEspaRecordEditor
            // 
            this.tcEspaRecordEditor.Controls.Add(this.tpCallAddress);
            this.tcEspaRecordEditor.Controls.Add(this.tpDisplayText);
            this.tcEspaRecordEditor.Controls.Add(this.tpBeepCoding);
            this.tcEspaRecordEditor.Controls.Add(this.tpCallType);
            this.tcEspaRecordEditor.Controls.Add(this.tpNumerOfTransmissions);
            this.tcEspaRecordEditor.Controls.Add(this.tpPriority);
            this.tcEspaRecordEditor.Controls.Add(this.tpCallStatus);
            this.tcEspaRecordEditor.Controls.Add(this.tpSystemStatus);
            this.tcEspaRecordEditor.Location = new System.Drawing.Point(12, 12);
            this.tcEspaRecordEditor.Name = "tcEspaRecordEditor";
            this.tcEspaRecordEditor.SelectedIndex = 0;
            this.tcEspaRecordEditor.Size = new System.Drawing.Size(552, 104);
            this.tcEspaRecordEditor.TabIndex = 0;
            // 
            // tpCallAddress
            // 
            this.tpCallAddress.Controls.Add(this.tbCallAddress);
            this.tpCallAddress.Controls.Add(this.label1);
            this.tpCallAddress.Location = new System.Drawing.Point(4, 22);
            this.tpCallAddress.Name = "tpCallAddress";
            this.tpCallAddress.Padding = new System.Windows.Forms.Padding(3);
            this.tpCallAddress.Size = new System.Drawing.Size(544, 78);
            this.tpCallAddress.TabIndex = 0;
            this.tpCallAddress.Text = "Call Address";
            this.tpCallAddress.UseVisualStyleBackColor = true;
            // 
            // tbCallAddress
            // 
            this.tbCallAddress.Location = new System.Drawing.Point(82, 19);
            this.tbCallAddress.MaxLength = 16;
            this.tbCallAddress.Name = "tbCallAddress";
            this.tbCallAddress.Size = new System.Drawing.Size(143, 20);
            this.tbCallAddress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Call Address";
            // 
            // tpDisplayText
            // 
            this.tpDisplayText.Controls.Add(this.tbDisplayText);
            this.tpDisplayText.Controls.Add(this.label2);
            this.tpDisplayText.Location = new System.Drawing.Point(4, 22);
            this.tpDisplayText.Name = "tpDisplayText";
            this.tpDisplayText.Padding = new System.Windows.Forms.Padding(3);
            this.tpDisplayText.Size = new System.Drawing.Size(544, 78);
            this.tpDisplayText.TabIndex = 1;
            this.tpDisplayText.Text = "Display Text";
            this.tpDisplayText.UseVisualStyleBackColor = true;
            // 
            // tbDisplayText
            // 
            this.tbDisplayText.Location = new System.Drawing.Point(77, 13);
            this.tbDisplayText.MaxLength = 128;
            this.tbDisplayText.Multiline = true;
            this.tbDisplayText.Name = "tbDisplayText";
            this.tbDisplayText.Size = new System.Drawing.Size(443, 35);
            this.tbDisplayText.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Display Text";
            // 
            // tpBeepCoding
            // 
            this.tpBeepCoding.Controls.Add(this.coboBeepCoding);
            this.tpBeepCoding.Controls.Add(this.label3);
            this.tpBeepCoding.Location = new System.Drawing.Point(4, 22);
            this.tpBeepCoding.Name = "tpBeepCoding";
            this.tpBeepCoding.Size = new System.Drawing.Size(544, 78);
            this.tpBeepCoding.TabIndex = 2;
            this.tpBeepCoding.Text = "Beep Coding";
            this.tpBeepCoding.UseVisualStyleBackColor = true;
            // 
            // coboBeepCoding
            // 
            this.coboBeepCoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboBeepCoding.FormattingEnabled = true;
            this.coboBeepCoding.Items.AddRange(new object[] {
            "\'0\'=Reserved",
            "\'1\'",
            "\'2\'",
            "\'3\'",
            "\'4\'",
            "\'5\'",
            "\'6\'",
            "\'7\'",
            "\'8\'",
            "\'9\'"});
            this.coboBeepCoding.Location = new System.Drawing.Point(99, 15);
            this.coboBeepCoding.Name = "coboBeepCoding";
            this.coboBeepCoding.Size = new System.Drawing.Size(121, 21);
            this.coboBeepCoding.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Beep Coding";
            // 
            // tpCallType
            // 
            this.tpCallType.Controls.Add(this.coboCallType);
            this.tpCallType.Controls.Add(this.label4);
            this.tpCallType.Location = new System.Drawing.Point(4, 22);
            this.tpCallType.Name = "tpCallType";
            this.tpCallType.Size = new System.Drawing.Size(544, 78);
            this.tpCallType.TabIndex = 3;
            this.tpCallType.Text = "Call Type";
            this.tpCallType.UseVisualStyleBackColor = true;
            // 
            // coboCallType
            // 
            this.coboCallType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboCallType.FormattingEnabled = true;
            this.coboCallType.Items.AddRange(new object[] {
            "\'0\'=Reserved",
            "\'1\'=Reset (cancel all)",
            "\'2\'=Speech call",
            "\'3\'=Standard call (no speech)"});
            this.coboCallType.Location = new System.Drawing.Point(99, 12);
            this.coboCallType.Name = "coboCallType";
            this.coboCallType.Size = new System.Drawing.Size(121, 21);
            this.coboCallType.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Call Type";
            // 
            // tpNumerOfTransmissions
            // 
            this.tpNumerOfTransmissions.Controls.Add(this.tbNumberOfTransmissions);
            this.tpNumerOfTransmissions.Controls.Add(this.label5);
            this.tpNumerOfTransmissions.Location = new System.Drawing.Point(4, 22);
            this.tpNumerOfTransmissions.Name = "tpNumerOfTransmissions";
            this.tpNumerOfTransmissions.Size = new System.Drawing.Size(544, 78);
            this.tpNumerOfTransmissions.TabIndex = 4;
            this.tpNumerOfTransmissions.Text = "No. of Transm.";
            this.tpNumerOfTransmissions.UseVisualStyleBackColor = true;
            // 
            // tbNumberOfTransmissions
            // 
            this.tbNumberOfTransmissions.Location = new System.Drawing.Point(142, 11);
            this.tbNumberOfTransmissions.MaxLength = 16;
            this.tbNumberOfTransmissions.Name = "tbNumberOfTransmissions";
            this.tbNumberOfTransmissions.Size = new System.Drawing.Size(143, 20);
            this.tbNumberOfTransmissions.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Number of Transmissions";
            // 
            // tpPriority
            // 
            this.tpPriority.Controls.Add(this.coboPriorityLevel);
            this.tpPriority.Controls.Add(this.label6);
            this.tpPriority.Location = new System.Drawing.Point(4, 22);
            this.tpPriority.Name = "tpPriority";
            this.tpPriority.Size = new System.Drawing.Size(544, 78);
            this.tpPriority.TabIndex = 5;
            this.tpPriority.Text = "Priority";
            this.tpPriority.UseVisualStyleBackColor = true;
            // 
            // coboPriorityLevel
            // 
            this.coboPriorityLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboPriorityLevel.FormattingEnabled = true;
            this.coboPriorityLevel.Items.AddRange(new object[] {
            "\'0\'=Reserved",
            "\'1\'=Alarm",
            "\'2\'=High",
            "\'3\'=Normal"});
            this.coboPriorityLevel.Location = new System.Drawing.Point(101, 13);
            this.coboPriorityLevel.Name = "coboPriorityLevel";
            this.coboPriorityLevel.Size = new System.Drawing.Size(121, 21);
            this.coboPriorityLevel.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Priority Level";
            // 
            // tpCallStatus
            // 
            this.tpCallStatus.Controls.Add(this.coboCallStatus);
            this.tpCallStatus.Controls.Add(this.label7);
            this.tpCallStatus.Location = new System.Drawing.Point(4, 22);
            this.tpCallStatus.Name = "tpCallStatus";
            this.tpCallStatus.Size = new System.Drawing.Size(544, 78);
            this.tpCallStatus.TabIndex = 6;
            this.tpCallStatus.Text = "Call Status";
            this.tpCallStatus.UseVisualStyleBackColor = true;
            // 
            // coboCallStatus
            // 
            this.coboCallStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboCallStatus.FormattingEnabled = true;
            this.coboCallStatus.Items.AddRange(new object[] {
            "\'0\'=Reserved",
            "\'1\'=Busy",
            "\'2\'=In Queue",
            "\'3\'=Paged",
            "\'4\'=Absent",
            "\'5\'=Call terminated",
            "\'6\'=Ack. from called party",
            "\'7\'=Speech channel open",
            "\'71\'=Speech channel open (paged)",
            "\'72\'=Speech channel open (absent)",
            "\'8\'=Fault indications"});
            this.coboCallStatus.Location = new System.Drawing.Point(98, 10);
            this.coboCallStatus.Name = "coboCallStatus";
            this.coboCallStatus.Size = new System.Drawing.Size(204, 21);
            this.coboCallStatus.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Call Status";
            // 
            // tpSystemStatus
            // 
            this.tpSystemStatus.Controls.Add(this.coboSystemStatus);
            this.tpSystemStatus.Controls.Add(this.label8);
            this.tpSystemStatus.Location = new System.Drawing.Point(4, 22);
            this.tpSystemStatus.Name = "tpSystemStatus";
            this.tpSystemStatus.Size = new System.Drawing.Size(544, 78);
            this.tpSystemStatus.TabIndex = 7;
            this.tpSystemStatus.Text = "System Status";
            this.tpSystemStatus.UseVisualStyleBackColor = true;
            // 
            // coboSystemStatus
            // 
            this.coboSystemStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboSystemStatus.FormattingEnabled = true;
            this.coboSystemStatus.Items.AddRange(new object[] {
            "\'0\'=Reserved",
            "\'1\'=Transmitter failure"});
            this.coboSystemStatus.Location = new System.Drawing.Point(99, 12);
            this.coboSystemStatus.Name = "coboSystemStatus";
            this.coboSystemStatus.Size = new System.Drawing.Size(148, 21);
            this.coboSystemStatus.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "System Status";
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(404, 129);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(485, 129);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 2;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // FormEditEspaRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 161);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.tcEspaRecordEditor);
            this.Name = "FormEditEspaRecord";
            this.Text = "EditRecord";
            this.tcEspaRecordEditor.ResumeLayout(false);
            this.tpCallAddress.ResumeLayout(false);
            this.tpCallAddress.PerformLayout();
            this.tpDisplayText.ResumeLayout(false);
            this.tpDisplayText.PerformLayout();
            this.tpBeepCoding.ResumeLayout(false);
            this.tpBeepCoding.PerformLayout();
            this.tpCallType.ResumeLayout(false);
            this.tpCallType.PerformLayout();
            this.tpNumerOfTransmissions.ResumeLayout(false);
            this.tpNumerOfTransmissions.PerformLayout();
            this.tpPriority.ResumeLayout(false);
            this.tpPriority.PerformLayout();
            this.tpCallStatus.ResumeLayout(false);
            this.tpCallStatus.PerformLayout();
            this.tpSystemStatus.ResumeLayout(false);
            this.tpSystemStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcEspaRecordEditor;
        private System.Windows.Forms.TabPage tpCallAddress;
        private System.Windows.Forms.TabPage tpDisplayText;
        private System.Windows.Forms.TabPage tpBeepCoding;
        private System.Windows.Forms.TabPage tpCallType;
        private System.Windows.Forms.TabPage tpNumerOfTransmissions;
        private System.Windows.Forms.TabPage tpPriority;
        private System.Windows.Forms.TabPage tpCallStatus;
        private System.Windows.Forms.TabPage tpSystemStatus;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TextBox tbCallAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDisplayText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox coboBeepCoding;
        private System.Windows.Forms.ComboBox coboCallType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNumberOfTransmissions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox coboPriorityLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox coboCallStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox coboSystemStatus;
        private System.Windows.Forms.Label label8;
    }
}