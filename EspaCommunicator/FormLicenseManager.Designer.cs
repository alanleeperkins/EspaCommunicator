namespace EspaCommunicator
{
    partial class FormLicenseManager
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "ESPA Control System Simulation",
            "04.02.2017",
            "04.02.2019"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "ESPA Paging Installation Simulation",
            "02.01.2017",
            "Never"}, -1);
            this.gbLicenseInformation = new System.Windows.Forms.GroupBox();
            this.btRemovedExpired = new System.Windows.Forms.Button();
            this.lvLicenseInformation = new System.Windows.Forms.ListView();
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActivationDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chExpireDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btAddLicense = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.gbLicenseInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLicenseInformation
            // 
            this.gbLicenseInformation.Controls.Add(this.btRemovedExpired);
            this.gbLicenseInformation.Controls.Add(this.lvLicenseInformation);
            this.gbLicenseInformation.Controls.Add(this.btAddLicense);
            this.gbLicenseInformation.Location = new System.Drawing.Point(12, 12);
            this.gbLicenseInformation.Name = "gbLicenseInformation";
            this.gbLicenseInformation.Size = new System.Drawing.Size(507, 301);
            this.gbLicenseInformation.TabIndex = 10;
            this.gbLicenseInformation.TabStop = false;
            this.gbLicenseInformation.Text = "License Information";
            // 
            // btRemovedExpired
            // 
            this.btRemovedExpired.Location = new System.Drawing.Point(348, 236);
            this.btRemovedExpired.Name = "btRemovedExpired";
            this.btRemovedExpired.Size = new System.Drawing.Size(148, 34);
            this.btRemovedExpired.TabIndex = 13;
            this.btRemovedExpired.Text = "Remove Expired Licenses";
            this.btRemovedExpired.UseVisualStyleBackColor = true;
            this.btRemovedExpired.Click += new System.EventHandler(this.btRemovedExpired_Click);
            // 
            // lvLicenseInformation
            // 
            this.lvLicenseInformation.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDescription,
            this.chActivationDate,
            this.chExpireDate});
            this.lvLicenseInformation.FullRowSelect = true;
            this.lvLicenseInformation.GridLines = true;
            this.lvLicenseInformation.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvLicenseInformation.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvLicenseInformation.Location = new System.Drawing.Point(12, 23);
            this.lvLicenseInformation.MultiSelect = false;
            this.lvLicenseInformation.Name = "lvLicenseInformation";
            this.lvLicenseInformation.Size = new System.Drawing.Size(484, 207);
            this.lvLicenseInformation.TabIndex = 12;
            this.lvLicenseInformation.UseCompatibleStateImageBehavior = false;
            this.lvLicenseInformation.View = System.Windows.Forms.View.Details;
            // 
            // chDescription
            // 
            this.chDescription.Text = "Description";
            this.chDescription.Width = 300;
            // 
            // chActivationDate
            // 
            this.chActivationDate.Text = "Activation Date";
            this.chActivationDate.Width = 90;
            // 
            // chExpireDate
            // 
            this.chExpireDate.Text = "Expire Date";
            this.chExpireDate.Width = 90;
            // 
            // btAddLicense
            // 
            this.btAddLicense.Location = new System.Drawing.Point(12, 236);
            this.btAddLicense.Name = "btAddLicense";
            this.btAddLicense.Size = new System.Drawing.Size(148, 34);
            this.btAddLicense.TabIndex = 10;
            this.btAddLicense.Text = "Add License...";
            this.btAddLicense.UseVisualStyleBackColor = true;
            this.btAddLicense.Click += new System.EventHandler(this.btAddLicense_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(330, 328);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(189, 34);
            this.btClose.TabIndex = 11;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // FormLicenseManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 374);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.gbLicenseInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormLicenseManager";
            this.Text = "FormLicense";
            this.gbLicenseInformation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLicenseInformation;
        private System.Windows.Forms.Button btAddLicense;
        private System.Windows.Forms.ListView lvLicenseInformation;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.ColumnHeader chActivationDate;
        private System.Windows.Forms.ColumnHeader chExpireDate;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btRemovedExpired;
    }
}