namespace SBXPCDLLSampleCSharp
{
	partial class frmLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblEnrollData = new System.Windows.Forms.Label();
            this.chkAndDelete = new System.Windows.Forms.CheckBox();
            this.chkReadMark = new System.Windows.Forms.CheckBox();
            this.LabelTotal = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdGlogData = new System.Windows.Forms.Button();
            this.cmdSLogData = new System.Windows.Forms.Button();
            this.cmdEmptySLog = new System.Windows.Forms.Button();
            this.cmdEmptyGLog = new System.Windows.Forms.Button();
            this.cmdAllGLogData = new System.Windows.Forms.Button();
            this.cmdAllSLogData = new System.Windows.Forms.Button();
            this.txtGLogStartPos = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGLogEndPos = new System.Windows.Forms.TextBox();
            this.cmdGetGLogPosInfo = new System.Windows.Forms.Button();
            this.cmdReadGLogWithPos = new System.Windows.Forms.Button();
            this.cmdDeleteGLogWithPos = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.grpOperGLogByPos = new System.Windows.Forms.GroupBox();
            this.txtSLogStartPos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSLogEndPos = new System.Windows.Forms.TextBox();
            this.cmdGetSLogPosInfo = new System.Windows.Forms.Button();
            this.cmdReadSLogWithPos = new System.Windows.Forms.Button();
            this.cmdDeleteSLogWithPos = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grpOperSLogByPos = new System.Windows.Forms.GroupBox();
            this.grdSlog = new System.Windows.Forms.DataGridView();
            this.tmachine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.senroll = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.smachine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genroll = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gmachine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manipulation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdGlog = new System.Windows.Forms.DataGridView();
            this.photo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enroll = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.verify_mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.glogtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdDeleteGLogPhoto = new System.Windows.Forms.Button();
            this.cmdClearPhoto_Glog = new System.Windows.Forms.Button();
            this.cmdClearPhoto_User = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdClearPhoto_All = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.grpOperGLogByPos.SuspendLayout();
            this.grpOperSLogByPos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSlog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGlog)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEnrollData
            // 
            this.lblEnrollData.AutoSize = true;
            this.lblEnrollData.BackColor = System.Drawing.SystemColors.Control;
            this.lblEnrollData.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblEnrollData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrollData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEnrollData.Location = new System.Drawing.Point(27, 75);
            this.lblEnrollData.Name = "lblEnrollData";
            this.lblEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblEnrollData.Size = new System.Drawing.Size(73, 19);
            this.lblEnrollData.TabIndex = 35;
            this.lblEnrollData.Text = "Log Data :";
            // 
            // chkAndDelete
            // 
            this.chkAndDelete.BackColor = System.Drawing.SystemColors.Control;
            this.chkAndDelete.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkAndDelete.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAndDelete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkAndDelete.Location = new System.Drawing.Point(457, 74);
            this.chkAndDelete.Name = "chkAndDelete";
            this.chkAndDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkAndDelete.Size = new System.Drawing.Size(138, 19);
            this.chkAndDelete.TabIndex = 38;
            this.chkAndDelete.Text = "and Delete ";
            this.chkAndDelete.UseVisualStyleBackColor = true;
            // 
            // chkReadMark
            // 
            this.chkReadMark.BackColor = System.Drawing.SystemColors.Control;
            this.chkReadMark.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkReadMark.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReadMark.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkReadMark.Location = new System.Drawing.Point(601, 74);
            this.chkReadMark.Name = "chkReadMark";
            this.chkReadMark.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkReadMark.Size = new System.Drawing.Size(101, 19);
            this.chkReadMark.TabIndex = 33;
            this.chkReadMark.Text = "ReadMark";
            this.chkReadMark.UseVisualStyleBackColor = true;
            // 
            // LabelTotal
            // 
            this.LabelTotal.AutoSize = true;
            this.LabelTotal.BackColor = System.Drawing.SystemColors.Control;
            this.LabelTotal.Cursor = System.Windows.Forms.Cursors.Default;
            this.LabelTotal.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LabelTotal.Location = new System.Drawing.Point(128, 75);
            this.LabelTotal.Name = "LabelTotal";
            this.LabelTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LabelTotal.Size = new System.Drawing.Size(46, 19);
            this.LabelTotal.TabIndex = 34;
            this.LabelTotal.Text = "Total :";
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.Control;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMessage.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessage.Location = new System.Drawing.Point(24, 29);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMessage.Size = new System.Drawing.Size(686, 28);
            this.lblMessage.TabIndex = 27;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdGlogData);
            this.groupBox2.Controls.Add(this.cmdSLogData);
            this.groupBox2.Controls.Add(this.cmdEmptySLog);
            this.groupBox2.Controls.Add(this.cmdEmptyGLog);
            this.groupBox2.Controls.Add(this.cmdAllGLogData);
            this.groupBox2.Controls.Add(this.cmdAllSLogData);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.groupBox2.Location = new System.Drawing.Point(24, 490);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(686, 80);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Operating in a Traditional Way";
            // 
            // cmdGlogData
            // 
            this.cmdGlogData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGlogData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGlogData.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGlogData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGlogData.Location = new System.Drawing.Point(343, 25);
            this.cmdGlogData.Name = "cmdGlogData";
            this.cmdGlogData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGlogData.Size = new System.Drawing.Size(94, 43);
            this.cmdGlogData.TabIndex = 39;
            this.cmdGlogData.Text = "Read GLogData";
            this.cmdGlogData.UseVisualStyleBackColor = true;
            this.cmdGlogData.Click += new System.EventHandler(this.cmdGlogData_Click);
            // 
            // cmdSLogData
            // 
            this.cmdSLogData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSLogData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdSLogData.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSLogData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdSLogData.Location = new System.Drawing.Point(19, 25);
            this.cmdSLogData.Name = "cmdSLogData";
            this.cmdSLogData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdSLogData.Size = new System.Drawing.Size(94, 43);
            this.cmdSLogData.TabIndex = 38;
            this.cmdSLogData.Text = "Read SLogData";
            this.cmdSLogData.UseVisualStyleBackColor = true;
            this.cmdSLogData.Click += new System.EventHandler(this.cmdSLogData_Click);
            // 
            // cmdEmptySLog
            // 
            this.cmdEmptySLog.BackColor = System.Drawing.SystemColors.Control;
            this.cmdEmptySLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdEmptySLog.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEmptySLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdEmptySLog.Location = new System.Drawing.Point(219, 25);
            this.cmdEmptySLog.Name = "cmdEmptySLog";
            this.cmdEmptySLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdEmptySLog.Size = new System.Drawing.Size(94, 43);
            this.cmdEmptySLog.TabIndex = 43;
            this.cmdEmptySLog.Text = "Empty SLogData";
            this.cmdEmptySLog.UseVisualStyleBackColor = true;
            this.cmdEmptySLog.Click += new System.EventHandler(this.cmdEmptySLog_Click);
            // 
            // cmdEmptyGLog
            // 
            this.cmdEmptyGLog.BackColor = System.Drawing.SystemColors.Control;
            this.cmdEmptyGLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdEmptyGLog.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEmptyGLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdEmptyGLog.Location = new System.Drawing.Point(543, 25);
            this.cmdEmptyGLog.Name = "cmdEmptyGLog";
            this.cmdEmptyGLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdEmptyGLog.Size = new System.Drawing.Size(94, 43);
            this.cmdEmptyGLog.TabIndex = 42;
            this.cmdEmptyGLog.Text = "Empty GLogData";
            this.cmdEmptyGLog.UseVisualStyleBackColor = true;
            this.cmdEmptyGLog.Click += new System.EventHandler(this.cmdEmptyGLog_Click);
            // 
            // cmdAllGLogData
            // 
            this.cmdAllGLogData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdAllGLogData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdAllGLogData.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAllGLogData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdAllGLogData.Location = new System.Drawing.Point(443, 25);
            this.cmdAllGLogData.Name = "cmdAllGLogData";
            this.cmdAllGLogData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdAllGLogData.Size = new System.Drawing.Size(94, 43);
            this.cmdAllGLogData.TabIndex = 41;
            this.cmdAllGLogData.Text = "Read All GLogData";
            this.cmdAllGLogData.UseVisualStyleBackColor = true;
            this.cmdAllGLogData.Click += new System.EventHandler(this.cmdAllGLogData_Click);
            // 
            // cmdAllSLogData
            // 
            this.cmdAllSLogData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdAllSLogData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdAllSLogData.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAllSLogData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdAllSLogData.Location = new System.Drawing.Point(119, 25);
            this.cmdAllSLogData.Name = "cmdAllSLogData";
            this.cmdAllSLogData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdAllSLogData.Size = new System.Drawing.Size(94, 43);
            this.cmdAllSLogData.TabIndex = 40;
            this.cmdAllSLogData.Text = "Read All SLogData";
            this.cmdAllSLogData.UseVisualStyleBackColor = true;
            this.cmdAllSLogData.Click += new System.EventHandler(this.cmdAllSLogData_Click);
            // 
            // txtGLogStartPos
            // 
            this.txtGLogStartPos.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.txtGLogStartPos.Location = new System.Drawing.Point(19, 52);
            this.txtGLogStartPos.MaxLength = 6;
            this.txtGLogStartPos.Name = "txtGLogStartPos";
            this.txtGLogStartPos.Size = new System.Drawing.Size(70, 23);
            this.txtGLogStartPos.TabIndex = 45;
            this.txtGLogStartPos.Text = "0";
            this.txtGLogStartPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(16, 33);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(59, 16);
            this.label6.TabIndex = 46;
            this.label6.Text = "StartPos:";
            // 
            // txtGLogEndPos
            // 
            this.txtGLogEndPos.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.txtGLogEndPos.Location = new System.Drawing.Point(110, 52);
            this.txtGLogEndPos.MaxLength = 6;
            this.txtGLogEndPos.Name = "txtGLogEndPos";
            this.txtGLogEndPos.Size = new System.Drawing.Size(70, 23);
            this.txtGLogEndPos.TabIndex = 47;
            this.txtGLogEndPos.Text = "0";
            this.txtGLogEndPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmdGetGLogPosInfo
            // 
            this.cmdGetGLogPosInfo.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGetGLogPosInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGetGLogPosInfo.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmdGetGLogPosInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGetGLogPosInfo.Location = new System.Drawing.Point(19, 87);
            this.cmdGetGLogPosInfo.Name = "cmdGetGLogPosInfo";
            this.cmdGetGLogPosInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGetGLogPosInfo.Size = new System.Drawing.Size(161, 30);
            this.cmdGetGLogPosInfo.TabIndex = 49;
            this.cmdGetGLogPosInfo.Text = "GetGLogPosInfo";
            this.cmdGetGLogPosInfo.UseVisualStyleBackColor = true;
            this.cmdGetGLogPosInfo.Click += new System.EventHandler(this.cmdGetGLogPosInfo_Click);
            // 
            // cmdReadGLogWithPos
            // 
            this.cmdReadGLogWithPos.BackColor = System.Drawing.SystemColors.Control;
            this.cmdReadGLogWithPos.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdReadGLogWithPos.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmdReadGLogWithPos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdReadGLogWithPos.Location = new System.Drawing.Point(19, 123);
            this.cmdReadGLogWithPos.Name = "cmdReadGLogWithPos";
            this.cmdReadGLogWithPos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdReadGLogWithPos.Size = new System.Drawing.Size(161, 30);
            this.cmdReadGLogWithPos.TabIndex = 50;
            this.cmdReadGLogWithPos.Text = "ReadGLogWithPos";
            this.cmdReadGLogWithPos.UseVisualStyleBackColor = true;
            this.cmdReadGLogWithPos.Click += new System.EventHandler(this.cmdReadGLogWithPos_Click);
            // 
            // cmdDeleteGLogWithPos
            // 
            this.cmdDeleteGLogWithPos.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDeleteGLogWithPos.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdDeleteGLogWithPos.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmdDeleteGLogWithPos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDeleteGLogWithPos.Location = new System.Drawing.Point(19, 159);
            this.cmdDeleteGLogWithPos.Name = "cmdDeleteGLogWithPos";
            this.cmdDeleteGLogWithPos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdDeleteGLogWithPos.Size = new System.Drawing.Size(161, 30);
            this.cmdDeleteGLogWithPos.TabIndex = 51;
            this.cmdDeleteGLogWithPos.Text = "DeleteGLogWithPos";
            this.cmdDeleteGLogWithPos.UseVisualStyleBackColor = true;
            this.cmdDeleteGLogWithPos.Click += new System.EventHandler(this.cmdDeleteGLogWithPos_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(110, 33);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 56;
            this.label4.Text = "EndPos:";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(89, 56);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(21, 16);
            this.label7.TabIndex = 58;
            this.label7.Text = "->";
            // 
            // grpOperGLogByPos
            // 
            this.grpOperGLogByPos.Controls.Add(this.label7);
            this.grpOperGLogByPos.Controls.Add(this.label4);
            this.grpOperGLogByPos.Controls.Add(this.cmdDeleteGLogWithPos);
            this.grpOperGLogByPos.Controls.Add(this.cmdReadGLogWithPos);
            this.grpOperGLogByPos.Controls.Add(this.cmdGetGLogPosInfo);
            this.grpOperGLogByPos.Controls.Add(this.txtGLogEndPos);
            this.grpOperGLogByPos.Controls.Add(this.label6);
            this.grpOperGLogByPos.Controls.Add(this.txtGLogStartPos);
            this.grpOperGLogByPos.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.grpOperGLogByPos.Location = new System.Drawing.Point(738, 231);
            this.grpOperGLogByPos.Name = "grpOperGLogByPos";
            this.grpOperGLogByPos.Size = new System.Drawing.Size(198, 203);
            this.grpOperGLogByPos.TabIndex = 57;
            this.grpOperGLogByPos.TabStop = false;
            this.grpOperGLogByPos.Text = "GLog by Position";
            // 
            // txtSLogStartPos
            // 
            this.txtSLogStartPos.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.txtSLogStartPos.Location = new System.Drawing.Point(19, 52);
            this.txtSLogStartPos.MaxLength = 6;
            this.txtSLogStartPos.Name = "txtSLogStartPos";
            this.txtSLogStartPos.Size = new System.Drawing.Size(70, 23);
            this.txtSLogStartPos.TabIndex = 45;
            this.txtSLogStartPos.Text = "0";
            this.txtSLogStartPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(16, 33);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 46;
            this.label2.Text = "StartPos:";
            // 
            // txtSLogEndPos
            // 
            this.txtSLogEndPos.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.txtSLogEndPos.Location = new System.Drawing.Point(110, 52);
            this.txtSLogEndPos.MaxLength = 6;
            this.txtSLogEndPos.Name = "txtSLogEndPos";
            this.txtSLogEndPos.Size = new System.Drawing.Size(70, 23);
            this.txtSLogEndPos.TabIndex = 47;
            this.txtSLogEndPos.Text = "0";
            this.txtSLogEndPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmdGetSLogPosInfo
            // 
            this.cmdGetSLogPosInfo.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGetSLogPosInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGetSLogPosInfo.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmdGetSLogPosInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGetSLogPosInfo.Location = new System.Drawing.Point(19, 87);
            this.cmdGetSLogPosInfo.Name = "cmdGetSLogPosInfo";
            this.cmdGetSLogPosInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGetSLogPosInfo.Size = new System.Drawing.Size(161, 30);
            this.cmdGetSLogPosInfo.TabIndex = 49;
            this.cmdGetSLogPosInfo.Text = "GetSLogPosInfo";
            this.cmdGetSLogPosInfo.UseVisualStyleBackColor = true;
            this.cmdGetSLogPosInfo.Click += new System.EventHandler(this.cmdGetSLogPosInfo_Click);
            // 
            // cmdReadSLogWithPos
            // 
            this.cmdReadSLogWithPos.BackColor = System.Drawing.SystemColors.Control;
            this.cmdReadSLogWithPos.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdReadSLogWithPos.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmdReadSLogWithPos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdReadSLogWithPos.Location = new System.Drawing.Point(19, 123);
            this.cmdReadSLogWithPos.Name = "cmdReadSLogWithPos";
            this.cmdReadSLogWithPos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdReadSLogWithPos.Size = new System.Drawing.Size(161, 30);
            this.cmdReadSLogWithPos.TabIndex = 50;
            this.cmdReadSLogWithPos.Text = "ReadSLogWithPos";
            this.cmdReadSLogWithPos.UseVisualStyleBackColor = true;
            this.cmdReadSLogWithPos.Click += new System.EventHandler(this.cmdReadSLogWithPos_Click);
            // 
            // cmdDeleteSLogWithPos
            // 
            this.cmdDeleteSLogWithPos.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDeleteSLogWithPos.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdDeleteSLogWithPos.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmdDeleteSLogWithPos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDeleteSLogWithPos.Location = new System.Drawing.Point(19, 159);
            this.cmdDeleteSLogWithPos.Name = "cmdDeleteSLogWithPos";
            this.cmdDeleteSLogWithPos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdDeleteSLogWithPos.Size = new System.Drawing.Size(161, 30);
            this.cmdDeleteSLogWithPos.TabIndex = 51;
            this.cmdDeleteSLogWithPos.Text = "DeleteSLogWithPos";
            this.cmdDeleteSLogWithPos.UseVisualStyleBackColor = true;
            this.cmdDeleteSLogWithPos.Click += new System.EventHandler(this.cmdDeleteSLogWithPos_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(89, 55);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(21, 16);
            this.label1.TabIndex = 55;
            this.label1.Text = "->";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(110, 33);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 56;
            this.label3.Text = "EndPos:";
            // 
            // grpOperSLogByPos
            // 
            this.grpOperSLogByPos.Controls.Add(this.label3);
            this.grpOperSLogByPos.Controls.Add(this.label1);
            this.grpOperSLogByPos.Controls.Add(this.cmdDeleteSLogWithPos);
            this.grpOperSLogByPos.Controls.Add(this.cmdReadSLogWithPos);
            this.grpOperSLogByPos.Controls.Add(this.cmdGetSLogPosInfo);
            this.grpOperSLogByPos.Controls.Add(this.txtSLogEndPos);
            this.grpOperSLogByPos.Controls.Add(this.label2);
            this.grpOperSLogByPos.Controls.Add(this.txtSLogStartPos);
            this.grpOperSLogByPos.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.grpOperSLogByPos.Location = new System.Drawing.Point(738, 20);
            this.grpOperSLogByPos.Name = "grpOperSLogByPos";
            this.grpOperSLogByPos.Size = new System.Drawing.Size(198, 203);
            this.grpOperSLogByPos.TabIndex = 45;
            this.grpOperSLogByPos.TabStop = false;
            this.grpOperSLogByPos.Text = "SLog by Position";
            // 
            // grdSlog
            // 
            this.grdSlog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSlog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tmachine,
            this.senroll,
            this.smachine,
            this.genroll,
            this.gmachine,
            this.manipulation,
            this.finger,
            this.logtime});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdSlog.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdSlog.Location = new System.Drawing.Point(24, 97);
            this.grdSlog.Name = "grdSlog";
            this.grdSlog.Size = new System.Drawing.Size(686, 353);
            this.grdSlog.TabIndex = 60;
            // 
            // tmachine
            // 
            this.tmachine.DataPropertyName = "tmachine";
            this.tmachine.HeaderText = "TMachineNo";
            this.tmachine.Name = "tmachine";
            this.tmachine.Width = 70;
            // 
            // senroll
            // 
            this.senroll.DataPropertyName = "senroll";
            this.senroll.HeaderText = "SEnrollNo";
            this.senroll.Name = "senroll";
            this.senroll.Width = 70;
            // 
            // smachine
            // 
            this.smachine.DataPropertyName = "smachine";
            this.smachine.HeaderText = "SMachineNo";
            this.smachine.Name = "smachine";
            this.smachine.Width = 70;
            // 
            // genroll
            // 
            this.genroll.DataPropertyName = "genroll";
            this.genroll.HeaderText = "GEnrollNo";
            this.genroll.Name = "genroll";
            this.genroll.Width = 70;
            // 
            // gmachine
            // 
            this.gmachine.DataPropertyName = "gmachine";
            this.gmachine.HeaderText = "GMachineNo";
            this.gmachine.Name = "gmachine";
            this.gmachine.Width = 70;
            // 
            // manipulation
            // 
            this.manipulation.DataPropertyName = "manipulation";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.manipulation.DefaultCellStyle = dataGridViewCellStyle1;
            this.manipulation.HeaderText = "Manipulation";
            this.manipulation.Name = "manipulation";
            this.manipulation.Width = 150;
            // 
            // finger
            // 
            this.finger.DataPropertyName = "finger";
            this.finger.HeaderText = "FP No";
            this.finger.Name = "finger";
            this.finger.Width = 60;
            // 
            // logtime
            // 
            this.logtime.DataPropertyName = "logtime";
            this.logtime.HeaderText = "Date & Time";
            this.logtime.Name = "logtime";
            this.logtime.Width = 120;
            // 
            // grdGlog
            // 
            this.grdGlog.AllowUserToAddRows = false;
            this.grdGlog.AllowUserToDeleteRows = false;
            this.grdGlog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdGlog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.photo,
            this.enroll,
            this.machine,
            this.verify_mode,
            this.glogtime});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdGlog.DefaultCellStyle = dataGridViewCellStyle4;
            this.grdGlog.Location = new System.Drawing.Point(24, 97);
            this.grdGlog.Name = "grdGlog";
            this.grdGlog.ReadOnly = true;
            this.grdGlog.Size = new System.Drawing.Size(686, 387);
            this.grdGlog.TabIndex = 61;
            // 
            // photo
            // 
            this.photo.DataPropertyName = "photo";
            this.photo.HeaderText = "PhotoNo";
            this.photo.Name = "photo";
            this.photo.ReadOnly = true;
            this.photo.Width = 80;
            // 
            // enroll
            // 
            this.enroll.DataPropertyName = "enroll";
            this.enroll.HeaderText = "EnrollNo";
            this.enroll.Name = "enroll";
            this.enroll.ReadOnly = true;
            this.enroll.Width = 80;
            // 
            // machine
            // 
            this.machine.DataPropertyName = "machine";
            this.machine.HeaderText = "MachineNo";
            this.machine.Name = "machine";
            this.machine.ReadOnly = true;
            this.machine.Width = 80;
            // 
            // verify_mode
            // 
            this.verify_mode.DataPropertyName = "verify_mode";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.verify_mode.DefaultCellStyle = dataGridViewCellStyle3;
            this.verify_mode.HeaderText = "VerifyMode";
            this.verify_mode.Name = "verify_mode";
            this.verify_mode.ReadOnly = true;
            this.verify_mode.Width = 180;
            // 
            // glogtime
            // 
            this.glogtime.DataPropertyName = "logtime";
            this.glogtime.HeaderText = "Date & Time";
            this.glogtime.Name = "glogtime";
            this.glogtime.ReadOnly = true;
            this.glogtime.Width = 120;
            // 
            // cmdDeleteGLogPhoto
            // 
            this.cmdDeleteGLogPhoto.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDeleteGLogPhoto.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdDeleteGLogPhoto.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmdDeleteGLogPhoto.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDeleteGLogPhoto.Location = new System.Drawing.Point(757, 440);
            this.cmdDeleteGLogPhoto.Name = "cmdDeleteGLogPhoto";
            this.cmdDeleteGLogPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdDeleteGLogPhoto.Size = new System.Drawing.Size(161, 30);
            this.cmdDeleteGLogPhoto.TabIndex = 51;
            this.cmdDeleteGLogPhoto.Text = "DeleteGLogPhoto(one)";
            this.cmdDeleteGLogPhoto.UseVisualStyleBackColor = true;
            this.cmdDeleteGLogPhoto.Click += new System.EventHandler(this.cmdDeleteGLogPhoto_Click);
            // 
            // cmdClearPhoto_Glog
            // 
            this.cmdClearPhoto_Glog.BackColor = System.Drawing.SystemColors.Control;
            this.cmdClearPhoto_Glog.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdClearPhoto_Glog.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmdClearPhoto_Glog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdClearPhoto_Glog.Location = new System.Drawing.Point(757, 478);
            this.cmdClearPhoto_Glog.Name = "cmdClearPhoto_Glog";
            this.cmdClearPhoto_Glog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdClearPhoto_Glog.Size = new System.Drawing.Size(161, 30);
            this.cmdClearPhoto_Glog.TabIndex = 51;
            this.cmdClearPhoto_Glog.Text = "Clear All Glog Photo";
            this.cmdClearPhoto_Glog.UseVisualStyleBackColor = true;
            this.cmdClearPhoto_Glog.Click += new System.EventHandler(this.cmdClearPhoto_Glog_Click);
            // 
            // cmdClearPhoto_User
            // 
            this.cmdClearPhoto_User.BackColor = System.Drawing.SystemColors.Control;
            this.cmdClearPhoto_User.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdClearPhoto_User.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmdClearPhoto_User.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdClearPhoto_User.Location = new System.Drawing.Point(757, 509);
            this.cmdClearPhoto_User.Name = "cmdClearPhoto_User";
            this.cmdClearPhoto_User.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdClearPhoto_User.Size = new System.Drawing.Size(161, 30);
            this.cmdClearPhoto_User.TabIndex = 51;
            this.cmdClearPhoto_User.Text = "Clear All Enroll Photo";
            this.cmdClearPhoto_User.UseVisualStyleBackColor = true;
            this.cmdClearPhoto_User.Click += new System.EventHandler(this.cmdClearPhoto_User_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.SystemColors.Control;
            this.cmdExit.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdExit.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdExit.Location = new System.Drawing.Point(716, 544);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdExit.Size = new System.Drawing.Size(40, 26);
            this.cmdExit.TabIndex = 30;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Visible = false;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdClearPhoto_All
            // 
            this.cmdClearPhoto_All.BackColor = System.Drawing.SystemColors.Control;
            this.cmdClearPhoto_All.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdClearPhoto_All.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.cmdClearPhoto_All.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdClearPhoto_All.Location = new System.Drawing.Point(757, 540);
            this.cmdClearPhoto_All.Name = "cmdClearPhoto_All";
            this.cmdClearPhoto_All.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdClearPhoto_All.Size = new System.Drawing.Size(161, 30);
            this.cmdClearPhoto_All.TabIndex = 51;
            this.cmdClearPhoto_All.Text = "Clear All Photo";
            this.cmdClearPhoto_All.UseVisualStyleBackColor = true;
            this.cmdClearPhoto_All.Click += new System.EventHandler(this.cmdClearPhoto_All_Click);
            // 
            // frmLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 576);
            this.Controls.Add(this.grdGlog);
            this.Controls.Add(this.grdSlog);
            this.Controls.Add(this.cmdClearPhoto_All);
            this.Controls.Add(this.cmdClearPhoto_User);
            this.Controls.Add(this.cmdClearPhoto_Glog);
            this.Controls.Add(this.cmdDeleteGLogPhoto);
            this.Controls.Add(this.grpOperGLogByPos);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpOperSLogByPos);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.lblEnrollData);
            this.Controls.Add(this.chkAndDelete);
            this.Controls.Add(this.chkReadMark);
            this.Controls.Add(this.LabelTotal);
            this.Controls.Add(this.lblMessage);
            this.Name = "frmLog";
            this.Text = "frmLog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLog_FormClosing);
            this.Load += new System.EventHandler(this.frmLog_Load);
            this.groupBox2.ResumeLayout(false);
            this.grpOperGLogByPos.ResumeLayout(false);
            this.grpOperGLogByPos.PerformLayout();
            this.grpOperSLogByPos.ResumeLayout(false);
            this.grpOperSLogByPos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSlog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGlog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.Label lblEnrollData;
		public System.Windows.Forms.CheckBox chkAndDelete;
		public System.Windows.Forms.CheckBox chkReadMark;
        public System.Windows.Forms.Label LabelTotal;
        public System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox groupBox2;
		public System.Windows.Forms.Button cmdGlogData;
		public System.Windows.Forms.Button cmdSLogData;
		public System.Windows.Forms.Button cmdEmptySLog;
		public System.Windows.Forms.Button cmdEmptyGLog;
		public System.Windows.Forms.Button cmdAllGLogData;
		public System.Windows.Forms.Button cmdAllSLogData;
		private System.Windows.Forms.TextBox txtGLogStartPos;
		public System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtGLogEndPos;
		public System.Windows.Forms.Button cmdGetGLogPosInfo;
		public System.Windows.Forms.Button cmdReadGLogWithPos;
		public System.Windows.Forms.Button cmdDeleteGLogWithPos;
		public System.Windows.Forms.Label label4;
		public System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox grpOperGLogByPos;
		private System.Windows.Forms.TextBox txtSLogStartPos;
		public System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtSLogEndPos;
		public System.Windows.Forms.Button cmdGetSLogPosInfo;
		public System.Windows.Forms.Button cmdReadSLogWithPos;
		public System.Windows.Forms.Button cmdDeleteSLogWithPos;
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox grpOperSLogByPos;
		private System.Windows.Forms.DataGridView grdSlog;
		private System.Windows.Forms.DataGridView grdGlog;
		private System.Windows.Forms.DataGridViewTextBoxColumn tmachine;
		private System.Windows.Forms.DataGridViewTextBoxColumn senroll;
		private System.Windows.Forms.DataGridViewTextBoxColumn smachine;
		private System.Windows.Forms.DataGridViewTextBoxColumn genroll;
		private System.Windows.Forms.DataGridViewTextBoxColumn gmachine;
		private System.Windows.Forms.DataGridViewTextBoxColumn manipulation;
		private System.Windows.Forms.DataGridViewTextBoxColumn finger;
		private System.Windows.Forms.DataGridViewTextBoxColumn logtime;
		private System.Windows.Forms.DataGridViewTextBoxColumn photo;
		private System.Windows.Forms.DataGridViewTextBoxColumn enroll;
		private System.Windows.Forms.DataGridViewTextBoxColumn machine;
		private System.Windows.Forms.DataGridViewTextBoxColumn verify_mode;
		private System.Windows.Forms.DataGridViewTextBoxColumn glogtime;
        public System.Windows.Forms.Button cmdDeleteGLogPhoto;
        public System.Windows.Forms.Button cmdClearPhoto_Glog;
        public System.Windows.Forms.Button cmdClearPhoto_User;
        public System.Windows.Forms.Button cmdExit;
        public System.Windows.Forms.Button cmdClearPhoto_All;
    }
}