namespace SBXPCDLLSampleCSharp
{
    partial class frmEvent
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbBaudrate = new System.Windows.Forms.ComboBox();
            this.cmbComPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEnrollData = new System.Windows.Forms.Label();
            this.radioSerialDevice = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPortNumber = new System.Windows.Forms.TextBox();
            this.txtSourceIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioNetworkDevice = new System.Windows.Forms.RadioButton();
            this.cmdStartMoniter = new System.Windows.Forms.Button();
            this.cmdEndMoniter = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.lstEvents = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbBaudrate);
            this.groupBox1.Controls.Add(this.cmbComPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblEnrollData);
            this.groupBox1.Controls.Add(this.radioSerialDevice);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(51, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmbBaudrate
            // 
            this.cmbBaudrate.FormattingEnabled = true;
            this.cmbBaudrate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudrate.Location = new System.Drawing.Point(115, 70);
            this.cmbBaudrate.Name = "cmbBaudrate";
            this.cmbBaudrate.Size = new System.Drawing.Size(132, 27);
            this.cmbBaudrate.TabIndex = 74;
            // 
            // cmbComPort
            // 
            this.cmbComPort.DisplayMember = "0";
            this.cmbComPort.FormattingEnabled = true;
            this.cmbComPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8"});
            this.cmbComPort.Location = new System.Drawing.Point(115, 37);
            this.cmbComPort.Name = "cmbComPort";
            this.cmbComPort.Size = new System.Drawing.Size(132, 27);
            this.cmbComPort.TabIndex = 73;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(29, 74);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(67, 19);
            this.label1.TabIndex = 72;
            this.label1.Text = "Baudrate:";
            // 
            // lblEnrollData
            // 
            this.lblEnrollData.AutoSize = true;
            this.lblEnrollData.BackColor = System.Drawing.SystemColors.Control;
            this.lblEnrollData.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblEnrollData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrollData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEnrollData.Location = new System.Drawing.Point(29, 40);
            this.lblEnrollData.Name = "lblEnrollData";
            this.lblEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblEnrollData.Size = new System.Drawing.Size(68, 19);
            this.lblEnrollData.TabIndex = 71;
            this.lblEnrollData.Text = "ComPort:";
            // 
            // radioSerialDevice
            // 
            this.radioSerialDevice.AutoSize = true;
            this.radioSerialDevice.Location = new System.Drawing.Point(6, 0);
            this.radioSerialDevice.Name = "radioSerialDevice";
            this.radioSerialDevice.Size = new System.Drawing.Size(107, 23);
            this.radioSerialDevice.TabIndex = 1;
            this.radioSerialDevice.TabStop = true;
            this.radioSerialDevice.Text = "Serial Device";
            this.radioSerialDevice.UseVisualStyleBackColor = true;
            this.radioSerialDevice.Click += new System.EventHandler(this.radioSerial_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPortNumber);
            this.groupBox2.Controls.Add(this.txtSourceIP);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.radioNetworkDevice);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(411, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 112);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtPortNumber
            // 
            this.txtPortNumber.Location = new System.Drawing.Point(132, 71);
            this.txtPortNumber.Name = "txtPortNumber";
            this.txtPortNumber.Size = new System.Drawing.Size(134, 26);
            this.txtPortNumber.TabIndex = 76;
            this.txtPortNumber.Text = "5005";
            // 
            // txtSourceIP
            // 
            this.txtSourceIP.Location = new System.Drawing.Point(132, 39);
            this.txtSourceIP.Name = "txtSourceIP";
            this.txtSourceIP.Size = new System.Drawing.Size(134, 26);
            this.txtSourceIP.TabIndex = 75;
            this.txtSourceIP.Text = "0.0.0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(30, 73);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(92, 19);
            this.label2.TabIndex = 74;
            this.label2.Text = "Port Number:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(30, 39);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(73, 19);
            this.label3.TabIndex = 73;
            this.label3.Text = "Source IP:";
            // 
            // radioNetworkDevice
            // 
            this.radioNetworkDevice.AutoSize = true;
            this.radioNetworkDevice.Checked = true;
            this.radioNetworkDevice.Location = new System.Drawing.Point(6, 0);
            this.radioNetworkDevice.Name = "radioNetworkDevice";
            this.radioNetworkDevice.Size = new System.Drawing.Size(128, 23);
            this.radioNetworkDevice.TabIndex = 0;
            this.radioNetworkDevice.TabStop = true;
            this.radioNetworkDevice.Text = "Network Device";
            this.radioNetworkDevice.UseVisualStyleBackColor = true;
            this.radioNetworkDevice.Click += new System.EventHandler(this.radioNetwork_Click);
            // 
            // cmdStartMoniter
            // 
            this.cmdStartMoniter.BackColor = System.Drawing.SystemColors.Control;
            this.cmdStartMoniter.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdStartMoniter.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStartMoniter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdStartMoniter.Location = new System.Drawing.Point(126, 155);
            this.cmdStartMoniter.Name = "cmdStartMoniter";
            this.cmdStartMoniter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdStartMoniter.Size = new System.Drawing.Size(149, 30);
            this.cmdStartMoniter.TabIndex = 73;
            this.cmdStartMoniter.Text = "Start Moniter";
            this.cmdStartMoniter.UseVisualStyleBackColor = false;
            this.cmdStartMoniter.Click += new System.EventHandler(this.cmdStartMoniter_Click);
            // 
            // cmdEndMoniter
            // 
            this.cmdEndMoniter.BackColor = System.Drawing.SystemColors.Control;
            this.cmdEndMoniter.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdEndMoniter.Enabled = false;
            this.cmdEndMoniter.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEndMoniter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdEndMoniter.Location = new System.Drawing.Point(298, 155);
            this.cmdEndMoniter.Name = "cmdEndMoniter";
            this.cmdEndMoniter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdEndMoniter.Size = new System.Drawing.Size(149, 30);
            this.cmdEndMoniter.TabIndex = 74;
            this.cmdEndMoniter.Text = "End Moniter";
            this.cmdEndMoniter.UseVisualStyleBackColor = false;
            this.cmdEndMoniter.Click += new System.EventHandler(this.cmbEndMoniter_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.BackColor = System.Drawing.SystemColors.Control;
            this.cmdClear.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdClear.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdClear.Location = new System.Drawing.Point(472, 155);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdClear.Size = new System.Drawing.Size(149, 30);
            this.cmdClear.TabIndex = 75;
            this.cmdClear.Text = "Clear";
            this.cmdClear.UseVisualStyleBackColor = false;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // lstEvents
            // 
            this.lstEvents.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEvents.FormattingEnabled = true;
            this.lstEvents.ItemHeight = 15;
            this.lstEvents.Location = new System.Drawing.Point(17, 208);
            this.lstEvents.Name = "lstEvents";
            this.lstEvents.Size = new System.Drawing.Size(719, 274);
            this.lstEvents.TabIndex = 76;
            // 
            // frmEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 505);
            this.Controls.Add(this.lstEvents);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.cmdEndMoniter);
            this.Controls.Add(this.cmdStartMoniter);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmEvent";
            this.Text = "frmEvent";
            this.Load += new System.EventHandler(this.frmEvent_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEvent_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button cmdStartMoniter;
        public System.Windows.Forms.Button cmdEndMoniter;
        public System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.RadioButton radioSerialDevice;
        private System.Windows.Forms.RadioButton radioNetworkDevice;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblEnrollData;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbComPort;
        private System.Windows.Forms.TextBox txtPortNumber;
        private System.Windows.Forms.TextBox txtSourceIP;
        private System.Windows.Forms.ComboBox cmbBaudrate;
        private System.Windows.Forms.ListBox lstEvents;
    }
}