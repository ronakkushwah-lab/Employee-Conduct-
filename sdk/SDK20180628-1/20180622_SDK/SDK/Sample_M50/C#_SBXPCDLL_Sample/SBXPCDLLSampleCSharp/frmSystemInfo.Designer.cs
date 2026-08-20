namespace SBXPCDLLSampleCSharp
{
    partial class frmSystemInfo
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
            this.cmdSetDeviceTime = new System.Windows.Forms.Button();
            this.cmdGetDeviceTime = new System.Windows.Forms.Button();
            this.cmdGetDeviceInfo = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdPowerOn = new System.Windows.Forms.Button();
            this.PowerOffDevice = new System.Windows.Forms.Button();
            this.cmdSetDeviceInfo = new System.Windows.Forms.Button();
            this.cmdEnableDevice = new System.Windows.Forms.Button();
            this.cmbSatus = new System.Windows.Forms.ComboBox();
            this.txtSetDevInfo = new System.Windows.Forms.TextBox();
            this.cmdGetDeviceStaus = new System.Windows.Forms.Button();
            this.chkEnableDevice = new System.Windows.Forms.CheckBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdSetDeviceTime
            // 
            this.cmdSetDeviceTime.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSetDeviceTime.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdSetDeviceTime.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDeviceTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdSetDeviceTime.Location = new System.Drawing.Point(10, 120);
            this.cmdSetDeviceTime.Name = "cmdSetDeviceTime";
            this.cmdSetDeviceTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdSetDeviceTime.Size = new System.Drawing.Size(125, 32);
            this.cmdSetDeviceTime.TabIndex = 29;
            this.cmdSetDeviceTime.Text = "SetDeviceTime";
            this.cmdSetDeviceTime.UseVisualStyleBackColor = false;
            this.cmdSetDeviceTime.Click += new System.EventHandler(this.cmdSetDeviceTime_Click);
            // 
            // cmdGetDeviceTime
            // 
            this.cmdGetDeviceTime.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGetDeviceTime.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGetDeviceTime.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetDeviceTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGetDeviceTime.Location = new System.Drawing.Point(10, 78);
            this.cmdGetDeviceTime.Name = "cmdGetDeviceTime";
            this.cmdGetDeviceTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGetDeviceTime.Size = new System.Drawing.Size(125, 32);
            this.cmdGetDeviceTime.TabIndex = 28;
            this.cmdGetDeviceTime.Text = "GetDeviceTime";
            this.cmdGetDeviceTime.UseVisualStyleBackColor = false;
            this.cmdGetDeviceTime.Click += new System.EventHandler(this.cmdGetDeviceTime_Click);
            // 
            // cmdGetDeviceInfo
            // 
            this.cmdGetDeviceInfo.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGetDeviceInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGetDeviceInfo.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetDeviceInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGetDeviceInfo.Location = new System.Drawing.Point(10, 221);
            this.cmdGetDeviceInfo.Name = "cmdGetDeviceInfo";
            this.cmdGetDeviceInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGetDeviceInfo.Size = new System.Drawing.Size(125, 32);
            this.cmdGetDeviceInfo.TabIndex = 27;
            this.cmdGetDeviceInfo.Text = "GetDeviceInfo";
            this.cmdGetDeviceInfo.UseVisualStyleBackColor = false;
            this.cmdGetDeviceInfo.Click += new System.EventHandler(this.cmdGetDeviceInfo_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.SystemColors.Control;
            this.cmdExit.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdExit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdExit.Location = new System.Drawing.Point(301, 120);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdExit.Size = new System.Drawing.Size(125, 32);
            this.cmdExit.TabIndex = 26;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = false;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdPowerOn
            // 
            this.cmdPowerOn.BackColor = System.Drawing.SystemColors.Control;
            this.cmdPowerOn.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdPowerOn.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPowerOn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdPowerOn.Location = new System.Drawing.Point(152, 78);
            this.cmdPowerOn.Name = "cmdPowerOn";
            this.cmdPowerOn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdPowerOn.Size = new System.Drawing.Size(125, 32);
            this.cmdPowerOn.TabIndex = 25;
            this.cmdPowerOn.Text = "PowerOnDevice";
            this.cmdPowerOn.UseVisualStyleBackColor = false;
            this.cmdPowerOn.Click += new System.EventHandler(this.cmdPowerOn_Click);
            // 
            // PowerOffDevice
            // 
            this.PowerOffDevice.BackColor = System.Drawing.SystemColors.Control;
            this.PowerOffDevice.Cursor = System.Windows.Forms.Cursors.Default;
            this.PowerOffDevice.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PowerOffDevice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PowerOffDevice.Location = new System.Drawing.Point(152, 120);
            this.PowerOffDevice.Name = "PowerOffDevice";
            this.PowerOffDevice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PowerOffDevice.Size = new System.Drawing.Size(125, 32);
            this.PowerOffDevice.TabIndex = 24;
            this.PowerOffDevice.Text = "PowerOffDevice";
            this.PowerOffDevice.UseVisualStyleBackColor = false;
            this.PowerOffDevice.Click += new System.EventHandler(this.PowerOffDevice_Click);
            // 
            // cmdSetDeviceInfo
            // 
            this.cmdSetDeviceInfo.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSetDeviceInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdSetDeviceInfo.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDeviceInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdSetDeviceInfo.Location = new System.Drawing.Point(152, 221);
            this.cmdSetDeviceInfo.Name = "cmdSetDeviceInfo";
            this.cmdSetDeviceInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdSetDeviceInfo.Size = new System.Drawing.Size(125, 32);
            this.cmdSetDeviceInfo.TabIndex = 23;
            this.cmdSetDeviceInfo.Text = "SetDeviceInfo";
            this.cmdSetDeviceInfo.UseVisualStyleBackColor = false;
            this.cmdSetDeviceInfo.Click += new System.EventHandler(this.cmdSetDeviceInfo_Click);
            // 
            // cmdEnableDevice
            // 
            this.cmdEnableDevice.BackColor = System.Drawing.SystemColors.Control;
            this.cmdEnableDevice.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdEnableDevice.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEnableDevice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdEnableDevice.Location = new System.Drawing.Point(301, 79);
            this.cmdEnableDevice.Name = "cmdEnableDevice";
            this.cmdEnableDevice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdEnableDevice.Size = new System.Drawing.Size(125, 32);
            this.cmdEnableDevice.TabIndex = 22;
            this.cmdEnableDevice.Text = "DisableDevice";
            this.cmdEnableDevice.UseVisualStyleBackColor = false;
            this.cmdEnableDevice.Click += new System.EventHandler(this.cmdEnableDevice_Click);
            // 
            // cmbSatus
            // 
            this.cmbSatus.BackColor = System.Drawing.SystemColors.Window;
            this.cmbSatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbSatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSatus.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSatus.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbSatus.Items.AddRange(new object[] {
            " 1",
            " 2",
            " 3",
            " 4",
            " 5",
            " 6",
            " 7",
            " 8",
            " 9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.cmbSatus.Location = new System.Drawing.Point(146, 176);
            this.cmbSatus.Name = "cmbSatus";
            this.cmbSatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbSatus.Size = new System.Drawing.Size(88, 27);
            this.cmbSatus.TabIndex = 20;
            // 
            // txtSetDevInfo
            // 
            this.txtSetDevInfo.AcceptsReturn = true;
            this.txtSetDevInfo.BackColor = System.Drawing.SystemColors.Window;
            this.txtSetDevInfo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSetDevInfo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSetDevInfo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSetDevInfo.Location = new System.Drawing.Point(358, 173);
            this.txtSetDevInfo.MaxLength = 0;
            this.txtSetDevInfo.Name = "txtSetDevInfo";
            this.txtSetDevInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSetDevInfo.Size = new System.Drawing.Size(59, 26);
            this.txtSetDevInfo.TabIndex = 19;
            // 
            // cmdGetDeviceStaus
            // 
            this.cmdGetDeviceStaus.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGetDeviceStaus.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGetDeviceStaus.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetDeviceStaus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGetDeviceStaus.Location = new System.Drawing.Point(284, 221);
            this.cmdGetDeviceStaus.Name = "cmdGetDeviceStaus";
            this.cmdGetDeviceStaus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGetDeviceStaus.Size = new System.Drawing.Size(142, 32);
            this.cmdGetDeviceStaus.TabIndex = 17;
            this.cmdGetDeviceStaus.Text = "GetDeviceStatus";
            this.cmdGetDeviceStaus.UseVisualStyleBackColor = false;
            this.cmdGetDeviceStaus.Click += new System.EventHandler(this.cmdGetDeviceStaus_Click);
            // 
            // chkEnableDevice
            // 
            this.chkEnableDevice.BackColor = System.Drawing.SystemColors.Control;
            this.chkEnableDevice.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkEnableDevice.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableDevice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkEnableDevice.Location = new System.Drawing.Point(284, 84);
            this.chkEnableDevice.Name = "chkEnableDevice";
            this.chkEnableDevice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkEnableDevice.Size = new System.Drawing.Size(15, 23);
            this.chkEnableDevice.TabIndex = 16;
            this.chkEnableDevice.UseVisualStyleBackColor = false;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(260, 178);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(97, 23);
            this.Label1.TabIndex = 21;
            this.Label1.Text = "Status Value:";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lblStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblStatus.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStatus.Location = new System.Drawing.Point(18, 169);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblStatus.Size = new System.Drawing.Size(137, 41);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "Status Paramerter:  Info Paramerter:";
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.Control;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMessage.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessage.Location = new System.Drawing.Point(13, 27);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMessage.Size = new System.Drawing.Size(414, 28);
            this.lblMessage.TabIndex = 15;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmSystemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 270);
            this.Controls.Add(this.cmdSetDeviceTime);
            this.Controls.Add(this.cmdGetDeviceTime);
            this.Controls.Add(this.cmdGetDeviceInfo);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdPowerOn);
            this.Controls.Add(this.PowerOffDevice);
            this.Controls.Add(this.cmdSetDeviceInfo);
            this.Controls.Add(this.cmdEnableDevice);
            this.Controls.Add(this.cmbSatus);
            this.Controls.Add(this.txtSetDevInfo);
            this.Controls.Add(this.cmdGetDeviceStaus);
            this.Controls.Add(this.chkEnableDevice);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblMessage);
            this.Name = "frmSystemInfo";
            this.Text = "frmSystemInfo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSystemInfo_FormClosed);
            this.Load += new System.EventHandler(this.frmSystemInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button cmdSetDeviceTime;
        public System.Windows.Forms.Button cmdGetDeviceTime;
        public System.Windows.Forms.Button cmdGetDeviceInfo;
        public System.Windows.Forms.Button cmdExit;
        public System.Windows.Forms.Button cmdPowerOn;
        public System.Windows.Forms.Button PowerOffDevice;
        public System.Windows.Forms.Button cmdSetDeviceInfo;
        public System.Windows.Forms.Button cmdEnableDevice;
        public System.Windows.Forms.ComboBox cmbSatus;
        public System.Windows.Forms.TextBox txtSetDevInfo;
        public System.Windows.Forms.Button cmdGetDeviceStaus;
        public System.Windows.Forms.CheckBox chkEnableDevice;
        public System.Windows.Forms.Label Label1;
        public System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.Label lblMessage;
    }
}