namespace EspaCommunicator
{
    partial class FormAddLicenseKey
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
            this.tbLicenseUID = new System.Windows.Forms.TextBox();
            this.tbLicenseString = new System.Windows.Forms.TextBox();
            this.bgLicenseUID = new System.Windows.Forms.GroupBox();
            this.gbLicenseString = new System.Windows.Forms.GroupBox();
            this.btSave = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.bgLicenseUID.SuspendLayout();
            this.gbLicenseString.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLicenseUID
            // 
            this.tbLicenseUID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLicenseUID.Location = new System.Drawing.Point(12, 21);
            this.tbLicenseUID.Name = "tbLicenseUID";
            this.tbLicenseUID.ReadOnly = true;
            this.tbLicenseUID.Size = new System.Drawing.Size(284, 22);
            this.tbLicenseUID.TabIndex = 0;
            this.tbLicenseUID.Text = "XXXX-XXXX-XXXX-XXXX";
            this.tbLicenseUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbLicenseString
            // 
            this.tbLicenseString.Location = new System.Drawing.Point(12, 19);
            this.tbLicenseString.Multiline = true;
            this.tbLicenseString.Name = "tbLicenseString";
            this.tbLicenseString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLicenseString.Size = new System.Drawing.Size(285, 267);
            this.tbLicenseString.TabIndex = 1;
            // 
            // bgLicenseUID
            // 
            this.bgLicenseUID.Controls.Add(this.tbLicenseUID);
            this.bgLicenseUID.Location = new System.Drawing.Point(12, 12);
            this.bgLicenseUID.Name = "bgLicenseUID";
            this.bgLicenseUID.Size = new System.Drawing.Size(308, 59);
            this.bgLicenseUID.TabIndex = 2;
            this.bgLicenseUID.TabStop = false;
            this.bgLicenseUID.Text = "License UID";
            // 
            // gbLicenseString
            // 
            this.gbLicenseString.Controls.Add(this.tbLicenseString);
            this.gbLicenseString.Location = new System.Drawing.Point(12, 80);
            this.gbLicenseString.Name = "gbLicenseString";
            this.gbLicenseString.Size = new System.Drawing.Size(308, 295);
            this.gbLicenseString.TabIndex = 3;
            this.gbLicenseString.TabStop = false;
            this.gbLicenseString.Text = "License String";
            // 
            // btSave
            // 
            this.btSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btSave.Location = new System.Drawing.Point(180, 384);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(140, 40);
            this.btSave.TabIndex = 4;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(12, 384);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(93, 40);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // FormAddLicenseKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 433);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.gbLicenseString);
            this.Controls.Add(this.bgLicenseUID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormAddLicenseKey";
            this.Text = "FormSingleLicenseEditor";
            this.bgLicenseUID.ResumeLayout(false);
            this.bgLicenseUID.PerformLayout();
            this.gbLicenseString.ResumeLayout(false);
            this.gbLicenseString.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbLicenseUID;
        private System.Windows.Forms.TextBox tbLicenseString;
        private System.Windows.Forms.GroupBox bgLicenseUID;
        private System.Windows.Forms.GroupBox gbLicenseString;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
    }
}