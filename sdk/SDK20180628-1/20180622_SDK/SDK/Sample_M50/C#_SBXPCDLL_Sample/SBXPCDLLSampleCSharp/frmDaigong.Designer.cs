namespace SBXPCDLLSampleCSharp
{
    partial class frmDaigong
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
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdWrite = new System.Windows.Forms.Button();
            this.cmdRead = new System.Windows.Forms.Button();
            this.lstProxy = new System.Windows.Forms.ListBox();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.txtProxy = new System.Windows.Forms.TextBox();
            this.lblEnrollData = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.SystemColors.Control;
            this.cmdExit.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdExit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdExit.Location = new System.Drawing.Point(555, 462);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdExit.Size = new System.Drawing.Size(104, 46);
            this.cmdExit.TabIndex = 68;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = false;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdWrite
            // 
            this.cmdWrite.BackColor = System.Drawing.SystemColors.Control;
            this.cmdWrite.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdWrite.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdWrite.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdWrite.Location = new System.Drawing.Point(555, 398);
            this.cmdWrite.Name = "cmdWrite";
            this.cmdWrite.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdWrite.Size = new System.Drawing.Size(104, 46);
            this.cmdWrite.TabIndex = 67;
            this.cmdWrite.Text = "Write";
            this.cmdWrite.UseVisualStyleBackColor = false;
            this.cmdWrite.Click += new System.EventHandler(this.cmdWrite_Click);
            // 
            // cmdRead
            // 
            this.cmdRead.BackColor = System.Drawing.SystemColors.Control;
            this.cmdRead.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdRead.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRead.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdRead.Location = new System.Drawing.Point(555, 333);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdRead.Size = new System.Drawing.Size(104, 46);
            this.cmdRead.TabIndex = 66;
            this.cmdRead.Text = "Read";
            this.cmdRead.UseVisualStyleBackColor = false;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // lstProxy
            // 
            this.lstProxy.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lstProxy.FormattingEnabled = true;
            this.lstProxy.ItemHeight = 19;
            this.lstProxy.Location = new System.Drawing.Point(27, 97);
            this.lstProxy.Name = "lstProxy";
            this.lstProxy.Size = new System.Drawing.Size(510, 422);
            this.lstProxy.TabIndex = 65;
            this.lstProxy.SelectedIndexChanged += new System.EventHandler(this.lstDepartment_SelectedIndexChanged);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdUpdate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdUpdate.Location = new System.Drawing.Point(555, 97);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdUpdate.Size = new System.Drawing.Size(104, 46);
            this.cmdUpdate.TabIndex = 64;
            this.cmdUpdate.Text = "Update";
            this.cmdUpdate.UseVisualStyleBackColor = false;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // txtProxy
            // 
            this.txtProxy.AcceptsReturn = true;
            this.txtProxy.BackColor = System.Drawing.SystemColors.Window;
            this.txtProxy.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtProxy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProxy.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtProxy.Location = new System.Drawing.Point(148, 52);
            this.txtProxy.MaxLength = 12;
            this.txtProxy.Name = "txtProxy";
            this.txtProxy.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtProxy.Size = new System.Drawing.Size(248, 26);
            this.txtProxy.TabIndex = 63;
            // 
            // lblEnrollData
            // 
            this.lblEnrollData.AutoSize = true;
            this.lblEnrollData.BackColor = System.Drawing.SystemColors.Control;
            this.lblEnrollData.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblEnrollData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrollData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEnrollData.Location = new System.Drawing.Point(27, 55);
            this.lblEnrollData.Name = "lblEnrollData";
            this.lblEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblEnrollData.Size = new System.Drawing.Size(89, 19);
            this.lblEnrollData.TabIndex = 62;
            this.lblEnrollData.Text = "Proxy Name:";
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.Control;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMessage.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessage.Location = new System.Drawing.Point(27, 6);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMessage.Size = new System.Drawing.Size(632, 28);
            this.lblMessage.TabIndex = 61;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmDaigong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 534);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdWrite);
            this.Controls.Add(this.cmdRead);
            this.Controls.Add(this.lstProxy);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.txtProxy);
            this.Controls.Add(this.lblEnrollData);
            this.Controls.Add(this.lblMessage);
            this.Name = "frmDaigong";
            this.Text = "frmDaigong";
            this.Load += new System.EventHandler(this.frmDaigong_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDaigong_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button cmdExit;
        public System.Windows.Forms.Button cmdWrite;
        public System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.ListBox lstProxy;
        public System.Windows.Forms.Button cmdUpdate;
        public System.Windows.Forms.TextBox txtProxy;
        public System.Windows.Forms.Label lblEnrollData;
        public System.Windows.Forms.Label lblMessage;
    }
}