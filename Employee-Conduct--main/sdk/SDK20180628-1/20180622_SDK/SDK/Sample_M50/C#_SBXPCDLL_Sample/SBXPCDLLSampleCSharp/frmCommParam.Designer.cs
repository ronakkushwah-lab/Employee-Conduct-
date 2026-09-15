namespace SBXPCDLLSampleCSharp
{
    partial class frmCommParam
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
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMachineID = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.chkUseDHCP = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSubnetMask = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGateway = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbEventOutType = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.AcceptsReturn = true;
            this.txtIP.BackColor = System.Drawing.SystemColors.Window;
            this.txtIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIP.Location = new System.Drawing.Point(93, 93);
            this.txtIP.MaxLength = 0;
            this.txtIP.Name = "txtIP";
            this.txtIP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtIP.Size = new System.Drawing.Size(113, 26);
            this.txtIP.TabIndex = 33;
            this.txtIP.Text = "192.168.1.224";
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(146, 357);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(81, 33);
            this.btnSet.TabIndex = 32;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(39, 357);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(81, 33);
            this.btnGet.TabIndex = 32;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "MachineID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 267);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Server IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 297);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Server Port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "UseDHCP";
            // 
            // txtMachineID
            // 
            this.txtMachineID.AcceptsReturn = true;
            this.txtMachineID.BackColor = System.Drawing.SystemColors.Window;
            this.txtMachineID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMachineID.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMachineID.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMachineID.Location = new System.Drawing.Point(93, 23);
            this.txtMachineID.MaxLength = 0;
            this.txtMachineID.Name = "txtMachineID";
            this.txtMachineID.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtMachineID.Size = new System.Drawing.Size(113, 26);
            this.txtMachineID.TabIndex = 33;
            this.txtMachineID.Text = "1";
            // 
            // txtPort
            // 
            this.txtPort.AcceptsReturn = true;
            this.txtPort.BackColor = System.Drawing.SystemColors.Window;
            this.txtPort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPort.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPort.Location = new System.Drawing.Point(93, 124);
            this.txtPort.MaxLength = 0;
            this.txtPort.Name = "txtPort";
            this.txtPort.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPort.Size = new System.Drawing.Size(113, 26);
            this.txtPort.TabIndex = 33;
            this.txtPort.Text = "5005";
            // 
            // txtServerIP
            // 
            this.txtServerIP.AcceptsReturn = true;
            this.txtServerIP.BackColor = System.Drawing.SystemColors.Window;
            this.txtServerIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtServerIP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerIP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtServerIP.Location = new System.Drawing.Point(93, 263);
            this.txtServerIP.MaxLength = 0;
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtServerIP.Size = new System.Drawing.Size(113, 26);
            this.txtServerIP.TabIndex = 33;
            this.txtServerIP.Text = "192.168.1.200";
            // 
            // txtServerPort
            // 
            this.txtServerPort.AcceptsReturn = true;
            this.txtServerPort.BackColor = System.Drawing.SystemColors.Window;
            this.txtServerPort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtServerPort.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerPort.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtServerPort.Location = new System.Drawing.Point(93, 293);
            this.txtServerPort.MaxLength = 0;
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtServerPort.Size = new System.Drawing.Size(113, 26);
            this.txtServerPort.TabIndex = 33;
            this.txtServerPort.Text = "4000";
            // 
            // chkUseDHCP
            // 
            this.chkUseDHCP.AutoSize = true;
            this.chkUseDHCP.Location = new System.Drawing.Point(139, 70);
            this.chkUseDHCP.Name = "chkUseDHCP";
            this.chkUseDHCP.Size = new System.Drawing.Size(15, 14);
            this.chkUseDHCP.TabIndex = 35;
            this.chkUseDHCP.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbEventOutType);
            this.groupBox1.Controls.Add(this.chkUseDHCP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtServerPort);
            this.groupBox1.Controls.Add(this.txtServerIP);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.txtMachineID);
            this.groupBox1.Controls.Add(this.txtGateway);
            this.groupBox1.Controls.Add(this.txtSubnetMask);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Location = new System.Drawing.Point(22, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 333);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            // 
            // txtSubnetMask
            // 
            this.txtSubnetMask.AcceptsReturn = true;
            this.txtSubnetMask.BackColor = System.Drawing.SystemColors.Window;
            this.txtSubnetMask.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSubnetMask.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubnetMask.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSubnetMask.Location = new System.Drawing.Point(93, 155);
            this.txtSubnetMask.MaxLength = 0;
            this.txtSubnetMask.Name = "txtSubnetMask";
            this.txtSubnetMask.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSubnetMask.Size = new System.Drawing.Size(113, 26);
            this.txtSubnetMask.TabIndex = 33;
            this.txtSubnetMask.Text = "255.255.255.0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Subnet Mask";
            // 
            // txtGateway
            // 
            this.txtGateway.AcceptsReturn = true;
            this.txtGateway.BackColor = System.Drawing.SystemColors.Window;
            this.txtGateway.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGateway.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGateway.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGateway.Location = new System.Drawing.Point(93, 186);
            this.txtGateway.MaxLength = 0;
            this.txtGateway.Name = "txtGateway";
            this.txtGateway.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtGateway.Size = new System.Drawing.Size(113, 26);
            this.txtGateway.TabIndex = 33;
            this.txtGateway.Text = "192.168.1.1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 190);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Gateway";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 233);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Use EventOut";
            // 
            // cmbEventOutType
            // 
            this.cmbEventOutType.FormattingEnabled = true;
            this.cmbEventOutType.Location = new System.Drawing.Point(93, 231);
            this.cmbEventOutType.Name = "cmbEventOutType";
            this.cmbEventOutType.Size = new System.Drawing.Size(113, 21);
            this.cmbEventOutType.TabIndex = 36;
            // 
            // frmCommParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 407);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnSet);
            this.Name = "frmCommParam";
            this.Text = "frmCommParam";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCommParam_FormClosed);
            this.Load += new System.EventHandler(this.frmCommParam_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtMachineID;
        public System.Windows.Forms.TextBox txtPort;
        public System.Windows.Forms.TextBox txtServerIP;
        public System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.CheckBox chkUseDHCP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtGateway;
        public System.Windows.Forms.TextBox txtSubnetMask;
        private System.Windows.Forms.ComboBox cmbEventOutType;
    }
}