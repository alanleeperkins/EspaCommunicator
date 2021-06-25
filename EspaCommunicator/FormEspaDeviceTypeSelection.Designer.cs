namespace EspaCommunicator
{
    partial class FormEspaDeviceTypeSelection
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
            this.btControlStation = new System.Windows.Forms.Button();
            this.btStation = new System.Windows.Forms.Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btControlStation
            // 
            this.btControlStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btControlStation.Location = new System.Drawing.Point(103, 79);
            this.btControlStation.Name = "btControlStation";
            this.btControlStation.Size = new System.Drawing.Size(268, 74);
            this.btControlStation.TabIndex = 0;
            this.btControlStation.Text = "Control Station";
            this.btControlStation.UseVisualStyleBackColor = true;
            this.btControlStation.Click += new System.EventHandler(this.btControlStation_Click);
            // 
            // btStation
            // 
            this.btStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStation.Location = new System.Drawing.Point(103, 171);
            this.btStation.Name = "btStation";
            this.btStation.Size = new System.Drawing.Size(268, 74);
            this.btStation.TabIndex = 1;
            this.btStation.Text = "Station";
            this.btStation.UseVisualStyleBackColor = true;
            this.btStation.Click += new System.EventHandler(this.btStation_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(12, 24);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(490, 32);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "Please choose the ESPA device you want to simulate";
            // 
            // FormEspaDeviceTypeSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 272);
            this.ControlBox = false;
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.btStation);
            this.Controls.Add(this.btControlStation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormEspaDeviceTypeSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormEspaDeviceTypeSelection";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btControlStation;
        private System.Windows.Forms.Button btStation;
        private System.Windows.Forms.Label lbTitle;
    }
}