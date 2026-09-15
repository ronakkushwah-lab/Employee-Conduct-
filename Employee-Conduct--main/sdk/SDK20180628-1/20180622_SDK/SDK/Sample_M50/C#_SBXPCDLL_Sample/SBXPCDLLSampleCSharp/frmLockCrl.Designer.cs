namespace SBXPCDLLSampleCSharp
{
    partial class frmLockCtrl
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
            this.components = new System.ComponentModel.Container();
            this.cmdWarnCancel = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdRestart = new System.Windows.Forms.Button();
            this.cmdAutoRecover = new System.Windows.Forms.Button();
            this.cmdUncondClose = new System.Windows.Forms.Button();
            this.cmdUncondOpen = new System.Windows.Forms.Button();
            this.cmdGetDoorStatus = new System.Windows.Forms.Button();
            this.cmdDoorOpen = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenExt = new System.Windows.Forms.Button();
            this.textDoorOpenTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdWarnCancel
            // 
            this.cmdWarnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.cmdWarnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdWarnCancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdWarnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdWarnCancel.Location = new System.Drawing.Point(283, 133);
            this.cmdWarnCancel.Name = "cmdWarnCancel";
            this.cmdWarnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdWarnCancel.Size = new System.Drawing.Size(125, 33);
            this.cmdWarnCancel.TabIndex = 15;
            this.cmdWarnCancel.Text = "Warn Cancel";
            this.cmdWarnCancel.UseVisualStyleBackColor = false;
            this.cmdWarnCancel.Click += new System.EventHandler(this.cmdWarnCancel_Click);
            // 
            // cmdRestart
            // 
            this.cmdRestart.BackColor = System.Drawing.SystemColors.Control;
            this.cmdRestart.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdRestart.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRestart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdRestart.Location = new System.Drawing.Point(143, 134);
            this.cmdRestart.Name = "cmdRestart";
            this.cmdRestart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdRestart.Size = new System.Drawing.Size(124, 32);
            this.cmdRestart.TabIndex = 14;
            this.cmdRestart.Text = "Reboot";
            this.cmdRestart.UseVisualStyleBackColor = false;
            this.cmdRestart.Click += new System.EventHandler(this.cmdRestart_Click);
            // 
            // cmdAutoRecover
            // 
            this.cmdAutoRecover.BackColor = System.Drawing.SystemColors.Control;
            this.cmdAutoRecover.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdAutoRecover.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAutoRecover.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdAutoRecover.Location = new System.Drawing.Point(143, 95);
            this.cmdAutoRecover.Name = "cmdAutoRecover";
            this.cmdAutoRecover.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdAutoRecover.Size = new System.Drawing.Size(124, 34);
            this.cmdAutoRecover.TabIndex = 13;
            this.cmdAutoRecover.Text = "Auto Recover";
            this.cmdAutoRecover.UseVisualStyleBackColor = false;
            this.cmdAutoRecover.Click += new System.EventHandler(this.cmdAutoRecover_Click);
            // 
            // cmdUncondClose
            // 
            this.cmdUncondClose.BackColor = System.Drawing.SystemColors.Control;
            this.cmdUncondClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdUncondClose.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUncondClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdUncondClose.Location = new System.Drawing.Point(283, 94);
            this.cmdUncondClose.Name = "cmdUncondClose";
            this.cmdUncondClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdUncondClose.Size = new System.Drawing.Size(126, 33);
            this.cmdUncondClose.TabIndex = 12;
            this.cmdUncondClose.Text = "Uncond Close";
            this.cmdUncondClose.UseVisualStyleBackColor = false;
            this.cmdUncondClose.Click += new System.EventHandler(this.cmdUncondClose_Click);
            // 
            // cmdUncondOpen
            // 
            this.cmdUncondOpen.BackColor = System.Drawing.SystemColors.Control;
            this.cmdUncondOpen.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdUncondOpen.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUncondOpen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdUncondOpen.Location = new System.Drawing.Point(284, 57);
            this.cmdUncondOpen.Name = "cmdUncondOpen";
            this.cmdUncondOpen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdUncondOpen.Size = new System.Drawing.Size(126, 33);
            this.cmdUncondOpen.TabIndex = 11;
            this.cmdUncondOpen.Text = "Uncond Open";
            this.cmdUncondOpen.UseVisualStyleBackColor = false;
            this.cmdUncondOpen.Click += new System.EventHandler(this.cmdUncondOpen_Click);
            // 
            // cmdGetDoorStatus
            // 
            this.cmdGetDoorStatus.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGetDoorStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGetDoorStatus.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetDoorStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGetDoorStatus.Location = new System.Drawing.Point(7, 59);
            this.cmdGetDoorStatus.Name = "cmdGetDoorStatus";
            this.cmdGetDoorStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGetDoorStatus.Size = new System.Drawing.Size(122, 33);
            this.cmdGetDoorStatus.TabIndex = 10;
            this.cmdGetDoorStatus.Text = "Get DoorStatus";
            this.cmdGetDoorStatus.UseVisualStyleBackColor = false;
            this.cmdGetDoorStatus.Click += new System.EventHandler(this.cmdGetDoorStatus_Click);
            // 
            // cmdDoorOpen
            // 
            this.cmdDoorOpen.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDoorOpen.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdDoorOpen.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDoorOpen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDoorOpen.Location = new System.Drawing.Point(143, 57);
            this.cmdDoorOpen.Name = "cmdDoorOpen";
            this.cmdDoorOpen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdDoorOpen.Size = new System.Drawing.Size(125, 33);
            this.cmdDoorOpen.TabIndex = 9;
            this.cmdDoorOpen.Text = "Door Open";
            this.cmdDoorOpen.UseVisualStyleBackColor = false;
            this.cmdDoorOpen.Click += new System.EventHandler(this.cmdDoorOpen_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.Control;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMessage.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessage.Location = new System.Drawing.Point(7, 14);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMessage.Size = new System.Drawing.Size(401, 28);
            this.lblMessage.TabIndex = 8;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textDoorOpenTime);
            this.groupBox1.Controls.Add(this.btnOpenExt);
            this.groupBox1.Location = new System.Drawing.Point(7, 172);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 100);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unconditional Open With Time";
            // 
            // btnOpenExt
            // 
            this.btnOpenExt.BackColor = System.Drawing.SystemColors.Control;
            this.btnOpenExt.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOpenExt.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenExt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenExt.Location = new System.Drawing.Point(136, 61);
            this.btnOpenExt.Name = "btnOpenExt";
            this.btnOpenExt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOpenExt.Size = new System.Drawing.Size(119, 33);
            this.btnOpenExt.TabIndex = 12;
            this.btnOpenExt.Text = "Open";
            this.btnOpenExt.UseVisualStyleBackColor = false;
            this.btnOpenExt.Click += new System.EventHandler(this.btnOpenExt_Click);
            // 
            // textDoorOpenTime
            // 
            this.textDoorOpenTime.Location = new System.Drawing.Point(136, 37);
            this.textDoorOpenTime.Name = "textDoorOpenTime";
            this.textDoorOpenTime.Size = new System.Drawing.Size(119, 20);
            this.textDoorOpenTime.TabIndex = 17;
            this.textDoorOpenTime.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Open time(seconds):";
            // 
            // frmLockCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 279);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdWarnCancel);
            this.Controls.Add(this.cmdRestart);
            this.Controls.Add(this.cmdAutoRecover);
            this.Controls.Add(this.cmdUncondClose);
            this.Controls.Add(this.cmdUncondOpen);
            this.Controls.Add(this.cmdGetDoorStatus);
            this.Controls.Add(this.cmdDoorOpen);
            this.Controls.Add(this.lblMessage);
            this.MaximizeBox = false;
            this.Name = "frmLockCtrl";
            this.Text = "frmLockCrl";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLockCrl_FormClosed);
            this.Load += new System.EventHandler(this.frmLockCrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button cmdWarnCancel;
        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.Button cmdRestart;
        public System.Windows.Forms.Button cmdAutoRecover;
        public System.Windows.Forms.Button cmdUncondClose;
        public System.Windows.Forms.Button cmdUncondOpen;
        public System.Windows.Forms.Button cmdGetDoorStatus;
        public System.Windows.Forms.Button cmdDoorOpen;
        public System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textDoorOpenTime;
        public System.Windows.Forms.Button btnOpenExt;
    }
}