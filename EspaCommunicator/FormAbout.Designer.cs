namespace EspaCommunicator
{
    partial class FormAbout
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
            this.lbSoftwareName = new System.Windows.Forms.Label();
            this.lbDeveloper = new System.Windows.Forms.Label();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.lbBuildTime = new System.Windows.Forms.Label();
            this.lbBuildType = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.btClose = new System.Windows.Forms.Button();
            this.tbSupportInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbSoftwareName
            // 
            this.lbSoftwareName.AutoSize = true;
            this.lbSoftwareName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSoftwareName.Location = new System.Drawing.Point(16, 11);
            this.lbSoftwareName.Name = "lbSoftwareName";
            this.lbSoftwareName.Size = new System.Drawing.Size(46, 18);
            this.lbSoftwareName.TabIndex = 0;
            this.lbSoftwareName.Text = "label1";
            // 
            // lbDeveloper
            // 
            this.lbDeveloper.AutoSize = true;
            this.lbDeveloper.Location = new System.Drawing.Point(16, 42);
            this.lbDeveloper.Name = "lbDeveloper";
            this.lbDeveloper.Size = new System.Drawing.Size(35, 13);
            this.lbDeveloper.TabIndex = 1;
            this.lbDeveloper.Text = "label2";
            // 
            // lbCopyright
            // 
            this.lbCopyright.AutoSize = true;
            this.lbCopyright.Location = new System.Drawing.Point(16, 68);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Size = new System.Drawing.Size(35, 13);
            this.lbCopyright.TabIndex = 2;
            this.lbCopyright.Text = "label3";
            // 
            // lbBuildTime
            // 
            this.lbBuildTime.AutoSize = true;
            this.lbBuildTime.Location = new System.Drawing.Point(16, 94);
            this.lbBuildTime.Name = "lbBuildTime";
            this.lbBuildTime.Size = new System.Drawing.Size(35, 13);
            this.lbBuildTime.TabIndex = 3;
            this.lbBuildTime.Text = "label4";
            // 
            // lbBuildType
            // 
            this.lbBuildType.AutoSize = true;
            this.lbBuildType.Location = new System.Drawing.Point(16, 120);
            this.lbBuildType.Name = "lbBuildType";
            this.lbBuildType.Size = new System.Drawing.Size(35, 13);
            this.lbBuildType.TabIndex = 4;
            this.lbBuildType.Text = "label5";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(16, 146);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(35, 13);
            this.lbVersion.TabIndex = 5;
            this.lbVersion.Text = "label6";
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(177, 249);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(200, 34);
            this.btClose.TabIndex = 6;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // tbSupportInfo
            // 
            this.tbSupportInfo.Location = new System.Drawing.Point(19, 178);
            this.tbSupportInfo.Multiline = true;
            this.tbSupportInfo.Name = "tbSupportInfo";
            this.tbSupportInfo.ReadOnly = true;
            this.tbSupportInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSupportInfo.Size = new System.Drawing.Size(358, 65);
            this.tbSupportInfo.TabIndex = 8;
            this.tbSupportInfo.Text = "Supports ESPA 4.4.4";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 292);
            this.Controls.Add(this.tbSupportInfo);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbBuildType);
            this.Controls.Add(this.lbBuildTime);
            this.Controls.Add(this.lbCopyright);
            this.Controls.Add(this.lbDeveloper);
            this.Controls.Add(this.lbSoftwareName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.Text = "E.S.P.A. Communicator  - About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSoftwareName;
        private System.Windows.Forms.Label lbDeveloper;
        private System.Windows.Forms.Label lbCopyright;
        private System.Windows.Forms.Label lbBuildTime;
        private System.Windows.Forms.Label lbBuildType;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.TextBox tbSupportInfo;
    }
}