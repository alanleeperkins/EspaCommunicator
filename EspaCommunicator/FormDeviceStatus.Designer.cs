namespace EspaCommunicator
{
    partial class FormDeviceStatus
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
            this.tbcDeviceStatus = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.prgrDeviceStates = new System.Windows.Forms.PropertyGrid();
            this.tbcDeviceStatus.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcDeviceStatus
            // 
            this.tbcDeviceStatus.Controls.Add(this.tabPage1);
            this.tbcDeviceStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcDeviceStatus.Location = new System.Drawing.Point(0, 0);
            this.tbcDeviceStatus.Name = "tbcDeviceStatus";
            this.tbcDeviceStatus.SelectedIndex = 0;
            this.tbcDeviceStatus.Size = new System.Drawing.Size(346, 302);
            this.tbcDeviceStatus.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.prgrDeviceStates);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(338, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Device Status Information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // prgrDeviceStates
            // 
            this.prgrDeviceStates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgrDeviceStates.Location = new System.Drawing.Point(3, 3);
            this.prgrDeviceStates.Name = "prgrDeviceStates";
            this.prgrDeviceStates.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.prgrDeviceStates.Size = new System.Drawing.Size(332, 270);
            this.prgrDeviceStates.TabIndex = 16;
            // 
            // FormDeviceStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 302);
            this.Controls.Add(this.tbcDeviceStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDeviceStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "FormDeviceStatus";
            this.Text = "Device Status";
            this.tbcDeviceStatus.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcDeviceStatus;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PropertyGrid prgrDeviceStates;
    }
}