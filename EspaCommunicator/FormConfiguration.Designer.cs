namespace EspaCommunicator
{
    partial class FormConfiguration
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
            this.btSave = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.gpSerialCommunicationSettings = new System.Windows.Forms.GroupBox();
            this.coboStopBits = new System.Windows.Forms.ComboBox();
            this.lbStopBits = new System.Windows.Forms.Label();
            this.coboDataBits = new System.Windows.Forms.ComboBox();
            this.lbDataBits = new System.Windows.Forms.Label();
            this.coboParity = new System.Windows.Forms.ComboBox();
            this.lbParity = new System.Windows.Forms.Label();
            this.coboBaudrate = new System.Windows.Forms.ComboBox();
            this.lbBaudrate = new System.Windows.Forms.Label();
            this.btSetDefaultEspaSerialSettings = new System.Windows.Forms.Button();
            this.gpSerialCommunicationSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(181, 230);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(159, 42);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "Save && Close";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(16, 230);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(159, 42);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // gpSerialCommunicationSettings
            // 
            this.gpSerialCommunicationSettings.Controls.Add(this.coboStopBits);
            this.gpSerialCommunicationSettings.Controls.Add(this.lbStopBits);
            this.gpSerialCommunicationSettings.Controls.Add(this.coboDataBits);
            this.gpSerialCommunicationSettings.Controls.Add(this.lbDataBits);
            this.gpSerialCommunicationSettings.Controls.Add(this.coboParity);
            this.gpSerialCommunicationSettings.Controls.Add(this.lbParity);
            this.gpSerialCommunicationSettings.Controls.Add(this.coboBaudrate);
            this.gpSerialCommunicationSettings.Controls.Add(this.lbBaudrate);
            this.gpSerialCommunicationSettings.Controls.Add(this.btSetDefaultEspaSerialSettings);
            this.gpSerialCommunicationSettings.Location = new System.Drawing.Point(12, 12);
            this.gpSerialCommunicationSettings.Name = "gpSerialCommunicationSettings";
            this.gpSerialCommunicationSettings.Size = new System.Drawing.Size(277, 199);
            this.gpSerialCommunicationSettings.TabIndex = 8;
            this.gpSerialCommunicationSettings.TabStop = false;
            this.gpSerialCommunicationSettings.Text = "Serial Communication Settings";
            // 
            // coboStopBits
            // 
            this.coboStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboStopBits.FormattingEnabled = true;
            this.coboStopBits.Location = new System.Drawing.Point(84, 105);
            this.coboStopBits.Name = "coboStopBits";
            this.coboStopBits.Size = new System.Drawing.Size(139, 21);
            this.coboStopBits.TabIndex = 17;
            // 
            // lbStopBits
            // 
            this.lbStopBits.AutoSize = true;
            this.lbStopBits.Location = new System.Drawing.Point(18, 109);
            this.lbStopBits.Name = "lbStopBits";
            this.lbStopBits.Size = new System.Drawing.Size(49, 13);
            this.lbStopBits.TabIndex = 16;
            this.lbStopBits.Text = "Stop Bits";
            // 
            // coboDataBits
            // 
            this.coboDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboDataBits.FormattingEnabled = true;
            this.coboDataBits.Location = new System.Drawing.Point(84, 78);
            this.coboDataBits.Name = "coboDataBits";
            this.coboDataBits.Size = new System.Drawing.Size(139, 21);
            this.coboDataBits.TabIndex = 15;
            // 
            // lbDataBits
            // 
            this.lbDataBits.AutoSize = true;
            this.lbDataBits.Location = new System.Drawing.Point(18, 82);
            this.lbDataBits.Name = "lbDataBits";
            this.lbDataBits.Size = new System.Drawing.Size(50, 13);
            this.lbDataBits.TabIndex = 14;
            this.lbDataBits.Text = "Data Bits";
            // 
            // coboParity
            // 
            this.coboParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboParity.FormattingEnabled = true;
            this.coboParity.Location = new System.Drawing.Point(84, 51);
            this.coboParity.Name = "coboParity";
            this.coboParity.Size = new System.Drawing.Size(139, 21);
            this.coboParity.TabIndex = 13;
            // 
            // lbParity
            // 
            this.lbParity.AutoSize = true;
            this.lbParity.Location = new System.Drawing.Point(18, 55);
            this.lbParity.Name = "lbParity";
            this.lbParity.Size = new System.Drawing.Size(33, 13);
            this.lbParity.TabIndex = 12;
            this.lbParity.Text = "Parity";
            // 
            // coboBaudrate
            // 
            this.coboBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coboBaudrate.FormattingEnabled = true;
            this.coboBaudrate.Location = new System.Drawing.Point(84, 24);
            this.coboBaudrate.Name = "coboBaudrate";
            this.coboBaudrate.Size = new System.Drawing.Size(139, 21);
            this.coboBaudrate.TabIndex = 11;
            // 
            // lbBaudrate
            // 
            this.lbBaudrate.AutoSize = true;
            this.lbBaudrate.Location = new System.Drawing.Point(18, 28);
            this.lbBaudrate.Name = "lbBaudrate";
            this.lbBaudrate.Size = new System.Drawing.Size(50, 13);
            this.lbBaudrate.TabIndex = 10;
            this.lbBaudrate.Text = "Baudrate";
            // 
            // btSetDefaultEspaSerialSettings
            // 
            this.btSetDefaultEspaSerialSettings.Location = new System.Drawing.Point(84, 150);
            this.btSetDefaultEspaSerialSettings.Name = "btSetDefaultEspaSerialSettings";
            this.btSetDefaultEspaSerialSettings.Size = new System.Drawing.Size(139, 36);
            this.btSetDefaultEspaSerialSettings.TabIndex = 9;
            this.btSetDefaultEspaSerialSettings.Text = "Reset to Default";
            this.btSetDefaultEspaSerialSettings.UseVisualStyleBackColor = true;
            this.btSetDefaultEspaSerialSettings.Click += new System.EventHandler(this.btSetDefaultEspaSerialSettings_Click);
            // 
            // FormConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 285);
            this.Controls.Add(this.gpSerialCommunicationSettings);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfiguration";
            this.Text = "FormConfiguratoin";
            this.gpSerialCommunicationSettings.ResumeLayout(false);
            this.gpSerialCommunicationSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox gpSerialCommunicationSettings;
        private System.Windows.Forms.Button btSetDefaultEspaSerialSettings;
        private System.Windows.Forms.ComboBox coboBaudrate;
        private System.Windows.Forms.Label lbBaudrate;
        private System.Windows.Forms.ComboBox coboStopBits;
        private System.Windows.Forms.Label lbStopBits;
        private System.Windows.Forms.ComboBox coboDataBits;
        private System.Windows.Forms.Label lbDataBits;
        private System.Windows.Forms.ComboBox coboParity;
        private System.Windows.Forms.Label lbParity;
    }
}