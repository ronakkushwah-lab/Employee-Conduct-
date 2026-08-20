namespace SBXPCDLLSampleCSharp
{
    partial class frmLogServerSetting
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
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnGetDnsSettings = new System.Windows.Forms.Button();
            this.btnSetDnsSettings = new System.Windows.Forms.Button();
            this.txtServerDomainName = new System.Windows.Forms.TextBox();
            this.textBgServerPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "Server port:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(132, 13);
            this.label12.TabIndex = 35;
            this.label12.Text = "Server domain name or IP:";
            // 
            // btnGetDnsSettings
            // 
            this.btnGetDnsSettings.Location = new System.Drawing.Point(82, 120);
            this.btnGetDnsSettings.Name = "btnGetDnsSettings";
            this.btnGetDnsSettings.Size = new System.Drawing.Size(81, 33);
            this.btnGetDnsSettings.TabIndex = 36;
            this.btnGetDnsSettings.Text = "Get";
            this.btnGetDnsSettings.UseVisualStyleBackColor = true;
            this.btnGetDnsSettings.Click += new System.EventHandler(this.btnGetDnsSettings_Click);
            // 
            // btnSetDnsSettings
            // 
            this.btnSetDnsSettings.Location = new System.Drawing.Point(189, 120);
            this.btnSetDnsSettings.Name = "btnSetDnsSettings";
            this.btnSetDnsSettings.Size = new System.Drawing.Size(81, 33);
            this.btnSetDnsSettings.TabIndex = 37;
            this.btnSetDnsSettings.Text = "Set";
            this.btnSetDnsSettings.UseVisualStyleBackColor = true;
            this.btnSetDnsSettings.Click += new System.EventHandler(this.btnSetDnsSettings_Click);
            // 
            // txtServerDomainName
            // 
            this.txtServerDomainName.AcceptsReturn = true;
            this.txtServerDomainName.BackColor = System.Drawing.SystemColors.Window;
            this.txtServerDomainName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtServerDomainName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerDomainName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtServerDomainName.Location = new System.Drawing.Point(173, 26);
            this.txtServerDomainName.MaxLength = 0;
            this.txtServerDomainName.Name = "txtServerDomainName";
            this.txtServerDomainName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtServerDomainName.Size = new System.Drawing.Size(152, 26);
            this.txtServerDomainName.TabIndex = 38;
            this.txtServerDomainName.Text = "logserver.test.domain";
            // 
            // textBgServerPort
            // 
            this.textBgServerPort.AcceptsReturn = true;
            this.textBgServerPort.BackColor = System.Drawing.SystemColors.Window;
            this.textBgServerPort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBgServerPort.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBgServerPort.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBgServerPort.Location = new System.Drawing.Point(173, 64);
            this.textBgServerPort.MaxLength = 0;
            this.textBgServerPort.Name = "textBgServerPort";
            this.textBgServerPort.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBgServerPort.Size = new System.Drawing.Size(152, 26);
            this.textBgServerPort.TabIndex = 39;
            this.textBgServerPort.Text = "5005";
            // 
            // frmLogServerSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 193);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnGetDnsSettings);
            this.Controls.Add(this.btnSetDnsSettings);
            this.Controls.Add(this.txtServerDomainName);
            this.Controls.Add(this.textBgServerPort);
            this.Name = "frmLogServerSetting";
            this.Text = "frmLogServerSetting";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLogServerSetting_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnGetDnsSettings;
        private System.Windows.Forms.Button btnSetDnsSettings;
        public System.Windows.Forms.TextBox txtServerDomainName;
        public System.Windows.Forms.TextBox textBgServerPort;
    }
}