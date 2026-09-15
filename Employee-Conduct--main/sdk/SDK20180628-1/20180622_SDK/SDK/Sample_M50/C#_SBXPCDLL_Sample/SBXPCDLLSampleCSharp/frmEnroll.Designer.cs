namespace SBXPCDLLSampleCSharp
{
    partial class frmEnroll
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
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.cmdDeleteCompany = new System.Windows.Forms.Button();
            this.cmdSetCompany = new System.Windows.Forms.Button();
            this.cmdGetName = new System.Windows.Forms.Button();
            this.cmdSetName = new System.Windows.Forms.Button();
            this.cmdModifyPrivilege = new System.Windows.Forms.Button();
            this.cmdEnableUser = new System.Windows.Forms.Button();
            this.cmdSetAllEnrollData = new System.Windows.Forms.Button();
            this.cmdGetAllEnrollData = new System.Windows.Forms.Button();
            this.cmdGetEnrollData = new System.Windows.Forms.Button();
            this.cmdClearData = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdGetEnrollInfo = new System.Windows.Forms.Button();
            this.cmdDeleteEnrollData = new System.Windows.Forms.Button();
            this.cmdSetEnrollData = new System.Windows.Forms.Button();
            this.cmdDel = new System.Windows.Forms.Button();
            this.btnSetUserPeriod = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdEmptyEnrollData = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkDisable = new System.Windows.Forms.CheckBox();
            this.cmbPrivilege = new System.Windows.Forms.ComboBox();
            this.lstEnrollData = new System.Windows.Forms.ListBox();
            this.txtEnrollNumber = new System.Windows.Forms.TextBox();
            this.cmbBackupNumber = new System.Windows.Forms.ComboBox();
            this.lblCardNum = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblEnrollData = new System.Windows.Forms.Label();
            this.lblBackupNumber = new System.Windows.Forms.Label();
            this._lblEnrollNum_0 = new System.Windows.Forms.Label();
            this.cmdModifyDuressFP = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDuressSetting = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUserTz2 = new System.Windows.Forms.TextBox();
            this.txtUserTz1 = new System.Windows.Forms.TextBox();
            this.txtUserTz4 = new System.Windows.Forms.TextBox();
            this.txtUserTz3 = new System.Windows.Forms.TextBox();
            this.txtUserTz5 = new System.Windows.Forms.TextBox();
            this.txtDepart = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.OpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.chkDupCheck = new System.Windows.Forms.CheckBox();
            this.btnImportM1001U = new System.Windows.Forms.Button();
            this.cmdRemoteEnroll_FP = new System.Windows.Forms.Button();
            this.cmdRemoteEnroll_Card = new System.Windows.Forms.Button();
            this.cmdRemoteEnroll_Face = new System.Windows.Forms.Button();
            this.cmdExitRemoteEnroll = new System.Windows.Forms.Button();
            this.cmdQueryRemoteEnrollStatus = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGetUserPeriod = new System.Windows.Forms.Button();
            this.dtPeriodTo = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtPeriodFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.chkUsePeriod = new System.Windows.Forms.CheckBox();
            this.cmdDeleteUserPhoto = new System.Windows.Forms.Button();
            this.cmdSetUserPhoto = new System.Windows.Forms.Button();
            this.cmdGetUserPhoto = new System.Windows.Forms.Button();
            this.picUserPhoto = new System.Windows.Forms.PictureBox();
            this.txtUserPhotoFile = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdUserPhotoBrowse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picUserPhoto)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.AcceptsReturn = true;
            this.txtCardNumber.BackColor = System.Drawing.SystemColors.Window;
            this.txtCardNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCardNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardNumber.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCardNumber.Location = new System.Drawing.Point(150, 101);
            this.txtCardNumber.MaxLength = 0;
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCardNumber.Size = new System.Drawing.Size(81, 26);
            this.txtCardNumber.TabIndex = 71;
            // 
            // cmdDeleteCompany
            // 
            this.cmdDeleteCompany.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDeleteCompany.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdDeleteCompany.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDeleteCompany.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDeleteCompany.Location = new System.Drawing.Point(779, 218);
            this.cmdDeleteCompany.Name = "cmdDeleteCompany";
            this.cmdDeleteCompany.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdDeleteCompany.Size = new System.Drawing.Size(200, 30);
            this.cmdDeleteCompany.TabIndex = 69;
            this.cmdDeleteCompany.Text = "Delete Company Name";
            this.cmdDeleteCompany.UseVisualStyleBackColor = false;
            this.cmdDeleteCompany.Click += new System.EventHandler(this.cmdDeleteCompany_Click);
            // 
            // cmdSetCompany
            // 
            this.cmdSetCompany.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSetCompany.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdSetCompany.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetCompany.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdSetCompany.Location = new System.Drawing.Point(573, 218);
            this.cmdSetCompany.Name = "cmdSetCompany";
            this.cmdSetCompany.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdSetCompany.Size = new System.Drawing.Size(200, 30);
            this.cmdSetCompany.TabIndex = 68;
            this.cmdSetCompany.Text = "Set Company Name";
            this.cmdSetCompany.UseVisualStyleBackColor = false;
            this.cmdSetCompany.Click += new System.EventHandler(this.cmdSetCompany_Click);
            // 
            // cmdGetName
            // 
            this.cmdGetName.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGetName.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGetName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGetName.Location = new System.Drawing.Point(573, 182);
            this.cmdGetName.Name = "cmdGetName";
            this.cmdGetName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGetName.Size = new System.Drawing.Size(200, 30);
            this.cmdGetName.TabIndex = 66;
            this.cmdGetName.Text = "Get Name Data";
            this.ToolTip1.SetToolTip(this.cmdGetName, "Get All Enroll Data From Device And Save To DataBase");
            this.cmdGetName.UseVisualStyleBackColor = false;
            this.cmdGetName.Click += new System.EventHandler(this.cmdGetName_Click);
            // 
            // cmdSetName
            // 
            this.cmdSetName.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSetName.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdSetName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdSetName.Location = new System.Drawing.Point(779, 182);
            this.cmdSetName.Name = "cmdSetName";
            this.cmdSetName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdSetName.Size = new System.Drawing.Size(200, 30);
            this.cmdSetName.TabIndex = 65;
            this.cmdSetName.Text = "Set Name Data";
            this.ToolTip1.SetToolTip(this.cmdSetName, "Load All Enroll Data From DataBase And Set To Device");
            this.cmdSetName.UseVisualStyleBackColor = false;
            this.cmdSetName.Click += new System.EventHandler(this.cmdSetName_Click);
            // 
            // cmdModifyPrivilege
            // 
            this.cmdModifyPrivilege.BackColor = System.Drawing.SystemColors.Control;
            this.cmdModifyPrivilege.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdModifyPrivilege.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdModifyPrivilege.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdModifyPrivilege.Location = new System.Drawing.Point(573, 290);
            this.cmdModifyPrivilege.Name = "cmdModifyPrivilege";
            this.cmdModifyPrivilege.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdModifyPrivilege.Size = new System.Drawing.Size(200, 30);
            this.cmdModifyPrivilege.TabIndex = 64;
            this.cmdModifyPrivilege.Text = "ModifyPrivilege";
            this.cmdModifyPrivilege.UseVisualStyleBackColor = false;
            this.cmdModifyPrivilege.Click += new System.EventHandler(this.cmdModifyPrivilege_Click);
            // 
            // cmdEnableUser
            // 
            this.cmdEnableUser.BackColor = System.Drawing.SystemColors.Control;
            this.cmdEnableUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdEnableUser.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEnableUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdEnableUser.Location = new System.Drawing.Point(779, 254);
            this.cmdEnableUser.Name = "cmdEnableUser";
            this.cmdEnableUser.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdEnableUser.Size = new System.Drawing.Size(200, 30);
            this.cmdEnableUser.TabIndex = 63;
            this.cmdEnableUser.Text = "EnableUser";
            this.cmdEnableUser.UseVisualStyleBackColor = false;
            this.cmdEnableUser.Click += new System.EventHandler(this.cmdEnableUser_Click);
            // 
            // cmdSetAllEnrollData
            // 
            this.cmdSetAllEnrollData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSetAllEnrollData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdSetAllEnrollData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetAllEnrollData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdSetAllEnrollData.Location = new System.Drawing.Point(779, 129);
            this.cmdSetAllEnrollData.Name = "cmdSetAllEnrollData";
            this.cmdSetAllEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdSetAllEnrollData.Size = new System.Drawing.Size(200, 30);
            this.cmdSetAllEnrollData.TabIndex = 61;
            this.cmdSetAllEnrollData.Text = "Set All Enroll Data";
            this.ToolTip1.SetToolTip(this.cmdSetAllEnrollData, "Load All Enroll Data From DataBase And Set To Device");
            this.cmdSetAllEnrollData.UseVisualStyleBackColor = false;
            this.cmdSetAllEnrollData.Click += new System.EventHandler(this.cmdSetAllEnrollData_Click);
            // 
            // cmdGetAllEnrollData
            // 
            this.cmdGetAllEnrollData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGetAllEnrollData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGetAllEnrollData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetAllEnrollData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGetAllEnrollData.Location = new System.Drawing.Point(573, 129);
            this.cmdGetAllEnrollData.Name = "cmdGetAllEnrollData";
            this.cmdGetAllEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGetAllEnrollData.Size = new System.Drawing.Size(200, 30);
            this.cmdGetAllEnrollData.TabIndex = 61;
            this.cmdGetAllEnrollData.Text = "Get All Enroll Data";
            this.ToolTip1.SetToolTip(this.cmdGetAllEnrollData, "Get All Enroll Data From Device And Save To DataBase");
            this.cmdGetAllEnrollData.UseVisualStyleBackColor = false;
            this.cmdGetAllEnrollData.Click += new System.EventHandler(this.cmdGetAllEnrollData_Click);
            // 
            // cmdGetEnrollData
            // 
            this.cmdGetEnrollData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGetEnrollData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGetEnrollData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetEnrollData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGetEnrollData.Location = new System.Drawing.Point(573, 60);
            this.cmdGetEnrollData.Name = "cmdGetEnrollData";
            this.cmdGetEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGetEnrollData.Size = new System.Drawing.Size(200, 30);
            this.cmdGetEnrollData.TabIndex = 60;
            this.cmdGetEnrollData.Text = "Get Enroll Data";
            this.ToolTip1.SetToolTip(this.cmdGetEnrollData, "Get EnrollData From Device");
            this.cmdGetEnrollData.UseVisualStyleBackColor = false;
            this.cmdGetEnrollData.Click += new System.EventHandler(this.cmdGetEnrollData_Click);
            // 
            // cmdClearData
            // 
            this.cmdClearData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdClearData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdClearData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClearData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdClearData.Location = new System.Drawing.Point(779, 447);
            this.cmdClearData.Name = "cmdClearData";
            this.cmdClearData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdClearData.Size = new System.Drawing.Size(200, 30);
            this.cmdClearData.TabIndex = 59;
            this.cmdClearData.Text = "Clear All Data(E,GL,SL) ";
            this.ToolTip1.SetToolTip(this.cmdClearData, "Clear EnrollData and LogDat Into Device");
            this.cmdClearData.UseVisualStyleBackColor = false;
            this.cmdClearData.Click += new System.EventHandler(this.cmdClearData_Click);
            // 
            // cmdGetEnrollInfo
            // 
            this.cmdGetEnrollInfo.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGetEnrollInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGetEnrollInfo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetEnrollInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGetEnrollInfo.Location = new System.Drawing.Point(573, 254);
            this.cmdGetEnrollInfo.Name = "cmdGetEnrollInfo";
            this.cmdGetEnrollInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGetEnrollInfo.Size = new System.Drawing.Size(200, 30);
            this.cmdGetEnrollInfo.TabIndex = 57;
            this.cmdGetEnrollInfo.Text = "Get Enroll Info";
            this.ToolTip1.SetToolTip(this.cmdGetEnrollInfo, "Get All Enrolled User Info From Device");
            this.cmdGetEnrollInfo.UseVisualStyleBackColor = false;
            this.cmdGetEnrollInfo.Click += new System.EventHandler(this.cmdGetEnrollInfo_Click);
            // 
            // cmdDeleteEnrollData
            // 
            this.cmdDeleteEnrollData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDeleteEnrollData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdDeleteEnrollData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDeleteEnrollData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDeleteEnrollData.Location = new System.Drawing.Point(779, 95);
            this.cmdDeleteEnrollData.Name = "cmdDeleteEnrollData";
            this.cmdDeleteEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdDeleteEnrollData.Size = new System.Drawing.Size(200, 30);
            this.cmdDeleteEnrollData.TabIndex = 56;
            this.cmdDeleteEnrollData.Text = "Delete Enroll Data";
            this.ToolTip1.SetToolTip(this.cmdDeleteEnrollData, "Delete Enroll Data Into Device");
            this.cmdDeleteEnrollData.UseVisualStyleBackColor = false;
            this.cmdDeleteEnrollData.Click += new System.EventHandler(this.cmdDeleteEnrollData_Click);
            // 
            // cmdSetEnrollData
            // 
            this.cmdSetEnrollData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSetEnrollData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdSetEnrollData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetEnrollData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdSetEnrollData.Location = new System.Drawing.Point(779, 60);
            this.cmdSetEnrollData.Name = "cmdSetEnrollData";
            this.cmdSetEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdSetEnrollData.Size = new System.Drawing.Size(200, 30);
            this.cmdSetEnrollData.TabIndex = 55;
            this.cmdSetEnrollData.Text = "Set Enroll Data";
            this.ToolTip1.SetToolTip(this.cmdSetEnrollData, "Set EnrollData To Device");
            this.cmdSetEnrollData.UseVisualStyleBackColor = false;
            this.cmdSetEnrollData.Click += new System.EventHandler(this.cmdSetEnrollData_Click);
            // 
            // cmdDel
            // 
            this.cmdDel.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDel.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdDel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDel.Location = new System.Drawing.Point(442, 427);
            this.cmdDel.Name = "cmdDel";
            this.cmdDel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdDel.Size = new System.Drawing.Size(100, 30);
            this.cmdDel.TabIndex = 47;
            this.cmdDel.Text = "Delete DB";
            this.ToolTip1.SetToolTip(this.cmdDel, "Delete All Saved Data From DataBase");
            this.cmdDel.UseVisualStyleBackColor = false;
            this.cmdDel.Click += new System.EventHandler(this.cmdDel_Click);
            // 
            // btnSetUserPeriod
            // 
            this.btnSetUserPeriod.BackColor = System.Drawing.SystemColors.Control;
            this.btnSetUserPeriod.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSetUserPeriod.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetUserPeriod.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSetUserPeriod.Location = new System.Drawing.Point(270, 427);
            this.btnSetUserPeriod.Name = "btnSetUserPeriod";
            this.btnSetUserPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSetUserPeriod.Size = new System.Drawing.Size(166, 30);
            this.btnSetUserPeriod.TabIndex = 101;
            this.btnSetUserPeriod.Text = "SetUserPeriod";
            this.ToolTip1.SetToolTip(this.btnSetUserPeriod, "Clear EnrollData and LogDat Into Device");
            this.btnSetUserPeriod.UseVisualStyleBackColor = false;
            this.btnSetUserPeriod.Click += new System.EventHandler(this.btnSetUserPeriod_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.SystemColors.Control;
            this.cmdExit.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdExit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdExit.Location = new System.Drawing.Point(779, 533);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdExit.Size = new System.Drawing.Size(200, 30);
            this.cmdExit.TabIndex = 58;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = false;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdEmptyEnrollData
            // 
            this.cmdEmptyEnrollData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdEmptyEnrollData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdEmptyEnrollData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEmptyEnrollData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdEmptyEnrollData.Location = new System.Drawing.Point(573, 447);
            this.cmdEmptyEnrollData.Name = "cmdEmptyEnrollData";
            this.cmdEmptyEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdEmptyEnrollData.Size = new System.Drawing.Size(200, 30);
            this.cmdEmptyEnrollData.TabIndex = 54;
            this.cmdEmptyEnrollData.Text = "Empty Enroll Data";
            this.cmdEmptyEnrollData.UseVisualStyleBackColor = false;
            this.cmdEmptyEnrollData.Click += new System.EventHandler(this.cmdEmptyEnrollData_Click);
            // 
            // txtName
            // 
            this.txtName.AcceptsReturn = true;
            this.txtName.BackColor = System.Drawing.SystemColors.Window;
            this.txtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtName.Location = new System.Drawing.Point(323, 66);
            this.txtName.MaxLength = 24;
            this.txtName.Name = "txtName";
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName.Size = new System.Drawing.Size(154, 26);
            this.txtName.TabIndex = 52;
            // 
            // chkDisable
            // 
            this.chkDisable.BackColor = System.Drawing.SystemColors.Control;
            this.chkDisable.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkDisable.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkDisable.Location = new System.Drawing.Point(267, 104);
            this.chkDisable.Name = "chkDisable";
            this.chkDisable.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkDisable.Size = new System.Drawing.Size(112, 19);
            this.chkDisable.TabIndex = 49;
            this.chkDisable.Text = "Disable User";
            this.chkDisable.UseVisualStyleBackColor = false;
            // 
            // cmbPrivilege
            // 
            this.cmbPrivilege.BackColor = System.Drawing.SystemColors.Window;
            this.cmbPrivilege.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbPrivilege.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPrivilege.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbPrivilege.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cmbPrivilege.Location = new System.Drawing.Point(150, 205);
            this.cmbPrivilege.Name = "cmbPrivilege";
            this.cmbPrivilege.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbPrivilege.Size = new System.Drawing.Size(81, 27);
            this.cmbPrivilege.TabIndex = 43;
            this.cmbPrivilege.Text = "0";
            // 
            // lstEnrollData
            // 
            this.lstEnrollData.BackColor = System.Drawing.SystemColors.Window;
            this.lstEnrollData.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstEnrollData.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lstEnrollData.HorizontalScrollbar = true;
            this.lstEnrollData.Location = new System.Drawing.Point(267, 149);
            this.lstEnrollData.Name = "lstEnrollData";
            this.lstEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstEnrollData.Size = new System.Drawing.Size(220, 134);
            this.lstEnrollData.TabIndex = 40;
            // 
            // txtEnrollNumber
            // 
            this.txtEnrollNumber.AcceptsReturn = true;
            this.txtEnrollNumber.BackColor = System.Drawing.SystemColors.Window;
            this.txtEnrollNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEnrollNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnrollNumber.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtEnrollNumber.Location = new System.Drawing.Point(150, 64);
            this.txtEnrollNumber.MaxLength = 8;
            this.txtEnrollNumber.Name = "txtEnrollNumber";
            this.txtEnrollNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEnrollNumber.Size = new System.Drawing.Size(81, 26);
            this.txtEnrollNumber.TabIndex = 38;
            this.txtEnrollNumber.Text = "1";
            // 
            // cmbBackupNumber
            // 
            this.cmbBackupNumber.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBackupNumber.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbBackupNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBackupNumber.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbBackupNumber.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17"});
            this.cmbBackupNumber.Location = new System.Drawing.Point(150, 172);
            this.cmbBackupNumber.Name = "cmbBackupNumber";
            this.cmbBackupNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbBackupNumber.Size = new System.Drawing.Size(81, 27);
            this.cmbBackupNumber.TabIndex = 36;
            this.cmbBackupNumber.Text = "0";
            // 
            // lblCardNum
            // 
            this.lblCardNum.BackColor = System.Drawing.SystemColors.Control;
            this.lblCardNum.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblCardNum.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardNum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCardNum.Location = new System.Drawing.Point(22, 105);
            this.lblCardNum.Name = "lblCardNum";
            this.lblCardNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCardNum.Size = new System.Drawing.Size(122, 39);
            this.lblCardNum.TabIndex = 70;
            this.lblCardNum.Text = "Card or Password Number :";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.BackColor = System.Drawing.SystemColors.Control;
            this.lbName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbName.Location = new System.Drawing.Point(275, 71);
            this.lbName.Name = "lbName";
            this.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbName.Size = new System.Drawing.Size(53, 19);
            this.lbName.TabIndex = 53;
            this.lbName.Text = "Name :";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(388, 125);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(50, 19);
            this.Label2.TabIndex = 48;
            this.Label2.Text = "Total : ";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(22, 206);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(67, 19);
            this.Label1.TabIndex = 44;
            this.Label1.Text = "Privilege :";
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.Control;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMessage.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessage.Location = new System.Drawing.Point(14, 17);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMessage.Size = new System.Drawing.Size(528, 29);
            this.lblMessage.TabIndex = 42;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblEnrollData
            // 
            this.lblEnrollData.AutoSize = true;
            this.lblEnrollData.BackColor = System.Drawing.SystemColors.Control;
            this.lblEnrollData.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblEnrollData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrollData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEnrollData.Location = new System.Drawing.Point(268, 124);
            this.lblEnrollData.Name = "lblEnrollData";
            this.lblEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblEnrollData.Size = new System.Drawing.Size(99, 19);
            this.lblEnrollData.TabIndex = 41;
            this.lblEnrollData.Text = "Enrolled Data :";
            // 
            // lblBackupNumber
            // 
            this.lblBackupNumber.AutoSize = true;
            this.lblBackupNumber.BackColor = System.Drawing.SystemColors.Control;
            this.lblBackupNumber.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblBackupNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackupNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBackupNumber.Location = new System.Drawing.Point(22, 176);
            this.lblBackupNumber.Name = "lblBackupNumber";
            this.lblBackupNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblBackupNumber.Size = new System.Drawing.Size(117, 19);
            this.lblBackupNumber.TabIndex = 39;
            this.lblBackupNumber.Text = "Backup Number :";
            // 
            // _lblEnrollNum_0
            // 
            this._lblEnrollNum_0.AutoSize = true;
            this._lblEnrollNum_0.BackColor = System.Drawing.SystemColors.Control;
            this._lblEnrollNum_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._lblEnrollNum_0.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblEnrollNum_0.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblEnrollNum_0.Location = new System.Drawing.Point(22, 68);
            this._lblEnrollNum_0.Name = "_lblEnrollNum_0";
            this._lblEnrollNum_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._lblEnrollNum_0.Size = new System.Drawing.Size(105, 19);
            this._lblEnrollNum_0.TabIndex = 37;
            this._lblEnrollNum_0.Text = "Enroll Number :";
            // 
            // cmdModifyDuressFP
            // 
            this.cmdModifyDuressFP.BackColor = System.Drawing.SystemColors.Control;
            this.cmdModifyDuressFP.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdModifyDuressFP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdModifyDuressFP.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdModifyDuressFP.Location = new System.Drawing.Point(779, 289);
            this.cmdModifyDuressFP.Name = "cmdModifyDuressFP";
            this.cmdModifyDuressFP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdModifyDuressFP.Size = new System.Drawing.Size(200, 30);
            this.cmdModifyDuressFP.TabIndex = 72;
            this.cmdModifyDuressFP.Text = "Modify Duress FP";
            this.cmdModifyDuressFP.UseVisualStyleBackColor = false;
            this.cmdModifyDuressFP.Click += new System.EventHandler(this.cmdModifyDuressFP_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(22, 243);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(54, 19);
            this.label4.TabIndex = 76;
            this.label4.Text = "Duress:";
            // 
            // cmbDuressSetting
            // 
            this.cmbDuressSetting.BackColor = System.Drawing.SystemColors.Window;
            this.cmbDuressSetting.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbDuressSetting.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDuressSetting.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbDuressSetting.Items.AddRange(new object[] {
            "0",
            "1"});
            this.cmbDuressSetting.Location = new System.Drawing.Point(150, 238);
            this.cmbDuressSetting.Name = "cmbDuressSetting";
            this.cmbDuressSetting.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbDuressSetting.Size = new System.Drawing.Size(81, 27);
            this.cmbDuressSetting.TabIndex = 77;
            this.cmbDuressSetting.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(22, 318);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(71, 19);
            this.label5.TabIndex = 78;
            this.label5.Text = "User TZ1:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(22, 349);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(71, 19);
            this.label6.TabIndex = 79;
            this.label6.Text = "User TZ2:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(22, 379);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(71, 19);
            this.label7.TabIndex = 80;
            this.label7.Text = "User TZ3:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(22, 406);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(71, 19);
            this.label8.TabIndex = 81;
            this.label8.Text = "User TZ4:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.Cursor = System.Windows.Forms.Cursors.Default;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(22, 432);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label9.Size = new System.Drawing.Size(71, 19);
            this.label9.TabIndex = 82;
            this.label9.Text = "User TZ5:";
            // 
            // txtUserTz2
            // 
            this.txtUserTz2.AcceptsReturn = true;
            this.txtUserTz2.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserTz2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUserTz2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserTz2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUserTz2.Location = new System.Drawing.Point(150, 343);
            this.txtUserTz2.MaxLength = 0;
            this.txtUserTz2.Name = "txtUserTz2";
            this.txtUserTz2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUserTz2.Size = new System.Drawing.Size(81, 26);
            this.txtUserTz2.TabIndex = 84;
            // 
            // txtUserTz1
            // 
            this.txtUserTz1.AcceptsReturn = true;
            this.txtUserTz1.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserTz1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUserTz1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserTz1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUserTz1.Location = new System.Drawing.Point(150, 315);
            this.txtUserTz1.MaxLength = 8;
            this.txtUserTz1.Name = "txtUserTz1";
            this.txtUserTz1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUserTz1.Size = new System.Drawing.Size(81, 26);
            this.txtUserTz1.TabIndex = 83;
            // 
            // txtUserTz4
            // 
            this.txtUserTz4.AcceptsReturn = true;
            this.txtUserTz4.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserTz4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUserTz4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserTz4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUserTz4.Location = new System.Drawing.Point(150, 399);
            this.txtUserTz4.MaxLength = 0;
            this.txtUserTz4.Name = "txtUserTz4";
            this.txtUserTz4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUserTz4.Size = new System.Drawing.Size(81, 26);
            this.txtUserTz4.TabIndex = 86;
            // 
            // txtUserTz3
            // 
            this.txtUserTz3.AcceptsReturn = true;
            this.txtUserTz3.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserTz3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUserTz3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserTz3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUserTz3.Location = new System.Drawing.Point(150, 371);
            this.txtUserTz3.MaxLength = 8;
            this.txtUserTz3.Name = "txtUserTz3";
            this.txtUserTz3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUserTz3.Size = new System.Drawing.Size(81, 26);
            this.txtUserTz3.TabIndex = 85;
            // 
            // txtUserTz5
            // 
            this.txtUserTz5.AcceptsReturn = true;
            this.txtUserTz5.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserTz5.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUserTz5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserTz5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUserTz5.Location = new System.Drawing.Point(150, 427);
            this.txtUserTz5.MaxLength = 0;
            this.txtUserTz5.Name = "txtUserTz5";
            this.txtUserTz5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUserTz5.Size = new System.Drawing.Size(81, 26);
            this.txtUserTz5.TabIndex = 87;
            // 
            // txtDepart
            // 
            this.txtDepart.AcceptsReturn = true;
            this.txtDepart.BackColor = System.Drawing.SystemColors.Window;
            this.txtDepart.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDepart.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepart.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDepart.Location = new System.Drawing.Point(150, 470);
            this.txtDepart.MaxLength = 0;
            this.txtDepart.Name = "txtDepart";
            this.txtDepart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDepart.Size = new System.Drawing.Size(81, 26);
            this.txtDepart.TabIndex = 89;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.Cursor = System.Windows.Forms.Cursors.Default;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(22, 475);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(84, 19);
            this.label10.TabIndex = 88;
            this.label10.Text = "User Depart";
            // 
            // OpenFileDlg
            // 
            this.OpenFileDlg.FileName = "openFileDialog1";
            // 
            // chkDupCheck
            // 
            this.chkDupCheck.AutoSize = true;
            this.chkDupCheck.Checked = true;
            this.chkDupCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDupCheck.Location = new System.Drawing.Point(779, 159);
            this.chkDupCheck.Name = "chkDupCheck";
            this.chkDupCheck.Size = new System.Drawing.Size(165, 17);
            this.chkDupCheck.TabIndex = 91;
            this.chkDupCheck.Text = "Fingerprint Duplication Check";
            this.chkDupCheck.UseVisualStyleBackColor = true;
            // 
            // btnImportM1001U
            // 
            this.btnImportM1001U.BackColor = System.Drawing.SystemColors.Control;
            this.btnImportM1001U.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnImportM1001U.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportM1001U.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnImportM1001U.Location = new System.Drawing.Point(573, 95);
            this.btnImportM1001U.Name = "btnImportM1001U";
            this.btnImportM1001U.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnImportM1001U.Size = new System.Drawing.Size(200, 30);
            this.btnImportM1001U.TabIndex = 61;
            this.btnImportM1001U.Text = "Import M1001U DB";
            this.btnImportM1001U.UseVisualStyleBackColor = false;
            this.btnImportM1001U.Click += new System.EventHandler(this.btnImportM1001U_Click);
            // 
            // cmdRemoteEnroll_FP
            // 
            this.cmdRemoteEnroll_FP.BackColor = System.Drawing.SystemColors.Control;
            this.cmdRemoteEnroll_FP.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdRemoteEnroll_FP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRemoteEnroll_FP.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdRemoteEnroll_FP.Location = new System.Drawing.Point(573, 339);
            this.cmdRemoteEnroll_FP.Name = "cmdRemoteEnroll_FP";
            this.cmdRemoteEnroll_FP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdRemoteEnroll_FP.Size = new System.Drawing.Size(200, 30);
            this.cmdRemoteEnroll_FP.TabIndex = 64;
            this.cmdRemoteEnroll_FP.Text = "Remote Enroll FP";
            this.cmdRemoteEnroll_FP.UseVisualStyleBackColor = false;
            this.cmdRemoteEnroll_FP.Click += new System.EventHandler(this.cmdRemoteEnroll_FP_Click);
            // 
            // cmdRemoteEnroll_Card
            // 
            this.cmdRemoteEnroll_Card.BackColor = System.Drawing.SystemColors.Control;
            this.cmdRemoteEnroll_Card.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdRemoteEnroll_Card.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRemoteEnroll_Card.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdRemoteEnroll_Card.Location = new System.Drawing.Point(779, 338);
            this.cmdRemoteEnroll_Card.Name = "cmdRemoteEnroll_Card";
            this.cmdRemoteEnroll_Card.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdRemoteEnroll_Card.Size = new System.Drawing.Size(200, 30);
            this.cmdRemoteEnroll_Card.TabIndex = 64;
            this.cmdRemoteEnroll_Card.Text = "Remote Enroll Card";
            this.cmdRemoteEnroll_Card.UseVisualStyleBackColor = false;
            this.cmdRemoteEnroll_Card.Click += new System.EventHandler(this.cmdRemoteEnroll_Card_Click);
            // 
            // cmdRemoteEnroll_Face
            // 
            this.cmdRemoteEnroll_Face.BackColor = System.Drawing.SystemColors.Control;
            this.cmdRemoteEnroll_Face.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdRemoteEnroll_Face.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRemoteEnroll_Face.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdRemoteEnroll_Face.Location = new System.Drawing.Point(573, 372);
            this.cmdRemoteEnroll_Face.Name = "cmdRemoteEnroll_Face";
            this.cmdRemoteEnroll_Face.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdRemoteEnroll_Face.Size = new System.Drawing.Size(200, 30);
            this.cmdRemoteEnroll_Face.TabIndex = 64;
            this.cmdRemoteEnroll_Face.Text = "Remote Enroll Face";
            this.cmdRemoteEnroll_Face.UseVisualStyleBackColor = false;
            this.cmdRemoteEnroll_Face.Click += new System.EventHandler(this.cmdRemoteEnroll_Face_Click);
            // 
            // cmdExitRemoteEnroll
            // 
            this.cmdExitRemoteEnroll.BackColor = System.Drawing.SystemColors.Control;
            this.cmdExitRemoteEnroll.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdExitRemoteEnroll.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExitRemoteEnroll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdExitRemoteEnroll.Location = new System.Drawing.Point(779, 372);
            this.cmdExitRemoteEnroll.Name = "cmdExitRemoteEnroll";
            this.cmdExitRemoteEnroll.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdExitRemoteEnroll.Size = new System.Drawing.Size(200, 30);
            this.cmdExitRemoteEnroll.TabIndex = 64;
            this.cmdExitRemoteEnroll.Text = "Exit Remote Enroll";
            this.cmdExitRemoteEnroll.UseVisualStyleBackColor = false;
            this.cmdExitRemoteEnroll.Click += new System.EventHandler(this.cmdExitRemoteEnroll_Click);
            // 
            // cmdQueryRemoteEnrollStatus
            // 
            this.cmdQueryRemoteEnrollStatus.BackColor = System.Drawing.SystemColors.Control;
            this.cmdQueryRemoteEnrollStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdQueryRemoteEnrollStatus.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdQueryRemoteEnrollStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdQueryRemoteEnrollStatus.Location = new System.Drawing.Point(573, 406);
            this.cmdQueryRemoteEnrollStatus.Name = "cmdQueryRemoteEnrollStatus";
            this.cmdQueryRemoteEnrollStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdQueryRemoteEnrollStatus.Size = new System.Drawing.Size(212, 30);
            this.cmdQueryRemoteEnrollStatus.TabIndex = 93;
            this.cmdQueryRemoteEnrollStatus.Text = "Query Remote Enroll Status";
            this.cmdQueryRemoteEnrollStatus.UseVisualStyleBackColor = false;
            this.cmdQueryRemoteEnrollStatus.Click += new System.EventHandler(this.cmdQueryRemoteEnrollStatus_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 13);
            this.label3.TabIndex = 94;
            this.label3.Text = "(0-9:FP, 11:Card, 15:Pwd, 17:Face)";
            // 
            // btnGetUserPeriod
            // 
            this.btnGetUserPeriod.BackColor = System.Drawing.SystemColors.Control;
            this.btnGetUserPeriod.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnGetUserPeriod.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetUserPeriod.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGetUserPeriod.Location = new System.Drawing.Point(270, 391);
            this.btnGetUserPeriod.Name = "btnGetUserPeriod";
            this.btnGetUserPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnGetUserPeriod.Size = new System.Drawing.Size(166, 30);
            this.btnGetUserPeriod.TabIndex = 100;
            this.btnGetUserPeriod.Text = "GetUserPeriod";
            this.btnGetUserPeriod.UseVisualStyleBackColor = false;
            this.btnGetUserPeriod.Click += new System.EventHandler(this.btnGetUserPeriod_Click);
            // 
            // dtPeriodTo
            // 
            this.dtPeriodTo.Enabled = false;
            this.dtPeriodTo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPeriodTo.Location = new System.Drawing.Point(345, 351);
            this.dtPeriodTo.Name = "dtPeriodTo";
            this.dtPeriodTo.Size = new System.Drawing.Size(132, 26);
            this.dtPeriodTo.TabIndex = 99;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.BackColor = System.Drawing.SystemColors.Control;
            this.lblTo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTo.Location = new System.Drawing.Point(286, 354);
            this.lblTo.Name = "lblTo";
            this.lblTo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTo.Size = new System.Drawing.Size(28, 19);
            this.lblTo.TabIndex = 98;
            this.lblTo.Text = "To:";
            // 
            // dtPeriodFrom
            // 
            this.dtPeriodFrom.Enabled = false;
            this.dtPeriodFrom.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPeriodFrom.Location = new System.Drawing.Point(345, 322);
            this.dtPeriodFrom.Name = "dtPeriodFrom";
            this.dtPeriodFrom.Size = new System.Drawing.Size(132, 26);
            this.dtPeriodFrom.TabIndex = 97;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.BackColor = System.Drawing.SystemColors.Control;
            this.lblFrom.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblFrom.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFrom.Location = new System.Drawing.Point(286, 325);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFrom.Size = new System.Drawing.Size(45, 19);
            this.lblFrom.TabIndex = 96;
            this.lblFrom.Text = "From:";
            // 
            // chkUsePeriod
            // 
            this.chkUsePeriod.BackColor = System.Drawing.SystemColors.Control;
            this.chkUsePeriod.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkUsePeriod.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUsePeriod.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkUsePeriod.Location = new System.Drawing.Point(267, 294);
            this.chkUsePeriod.Name = "chkUsePeriod";
            this.chkUsePeriod.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkUsePeriod.Size = new System.Drawing.Size(143, 26);
            this.chkUsePeriod.TabIndex = 95;
            this.chkUsePeriod.Text = "Use Period";
            this.chkUsePeriod.UseVisualStyleBackColor = false;
            this.chkUsePeriod.CheckedChanged += new System.EventHandler(this.chkUsePeriod_CheckedChanged);
            // 
            // cmdDeleteUserPhoto
            // 
            this.cmdDeleteUserPhoto.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDeleteUserPhoto.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdDeleteUserPhoto.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDeleteUserPhoto.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDeleteUserPhoto.Location = new System.Drawing.Point(573, 532);
            this.cmdDeleteUserPhoto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdDeleteUserPhoto.Name = "cmdDeleteUserPhoto";
            this.cmdDeleteUserPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdDeleteUserPhoto.Size = new System.Drawing.Size(200, 30);
            this.cmdDeleteUserPhoto.TabIndex = 104;
            this.cmdDeleteUserPhoto.Text = "Delete User Photo";
            this.cmdDeleteUserPhoto.UseVisualStyleBackColor = false;
            this.cmdDeleteUserPhoto.Click += new System.EventHandler(this.cmdDeleteUserPhoto_Click);
            // 
            // cmdSetUserPhoto
            // 
            this.cmdSetUserPhoto.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSetUserPhoto.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdSetUserPhoto.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetUserPhoto.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdSetUserPhoto.Location = new System.Drawing.Point(779, 498);
            this.cmdSetUserPhoto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdSetUserPhoto.Name = "cmdSetUserPhoto";
            this.cmdSetUserPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdSetUserPhoto.Size = new System.Drawing.Size(200, 30);
            this.cmdSetUserPhoto.TabIndex = 103;
            this.cmdSetUserPhoto.Text = "Set User Photo";
            this.cmdSetUserPhoto.UseVisualStyleBackColor = false;
            this.cmdSetUserPhoto.Click += new System.EventHandler(this.cmdSetUserPhoto_Click);
            // 
            // cmdGetUserPhoto
            // 
            this.cmdGetUserPhoto.BackColor = System.Drawing.SystemColors.Control;
            this.cmdGetUserPhoto.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdGetUserPhoto.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetUserPhoto.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdGetUserPhoto.Location = new System.Drawing.Point(573, 498);
            this.cmdGetUserPhoto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdGetUserPhoto.Name = "cmdGetUserPhoto";
            this.cmdGetUserPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdGetUserPhoto.Size = new System.Drawing.Size(200, 30);
            this.cmdGetUserPhoto.TabIndex = 102;
            this.cmdGetUserPhoto.Text = "Get User Photo";
            this.cmdGetUserPhoto.UseVisualStyleBackColor = false;
            this.cmdGetUserPhoto.Click += new System.EventHandler(this.cmdGetUserPhoto_Click);
            // 
            // picUserPhoto
            // 
            this.picUserPhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picUserPhoto.Location = new System.Drawing.Point(17, 52);
            this.picUserPhoto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picUserPhoto.Name = "picUserPhoto";
            this.picUserPhoto.Size = new System.Drawing.Size(117, 99);
            this.picUserPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUserPhoto.TabIndex = 2;
            this.picUserPhoto.TabStop = false;
            // 
            // txtUserPhotoFile
            // 
            this.txtUserPhotoFile.Location = new System.Drawing.Point(12, 22);
            this.txtUserPhotoFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUserPhotoFile.Name = "txtUserPhotoFile";
            this.txtUserPhotoFile.Size = new System.Drawing.Size(216, 24);
            this.txtUserPhotoFile.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picUserPhoto);
            this.groupBox1.Controls.Add(this.txtUserPhotoFile);
            this.groupBox1.Controls.Add(this.cmdUserPhotoBrowse);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(270, 470);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(267, 160);
            this.groupBox1.TabIndex = 105;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Photo Related";
            // 
            // cmdUserPhotoBrowse
            // 
            this.cmdUserPhotoBrowse.Location = new System.Drawing.Point(228, 21);
            this.cmdUserPhotoBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdUserPhotoBrowse.Name = "cmdUserPhotoBrowse";
            this.cmdUserPhotoBrowse.Size = new System.Drawing.Size(27, 25);
            this.cmdUserPhotoBrowse.TabIndex = 0;
            this.cmdUserPhotoBrowse.Text = "...";
            this.cmdUserPhotoBrowse.UseVisualStyleBackColor = true;
            this.cmdUserPhotoBrowse.Click += new System.EventHandler(this.cmdUserPhotoBrowse_Click);
            // 
            // frmEnroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 639);
            this.Controls.Add(this.cmdDeleteUserPhoto);
            this.Controls.Add(this.cmdSetUserPhoto);
            this.Controls.Add(this.cmdGetUserPhoto);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSetUserPeriod);
            this.Controls.Add(this.btnGetUserPeriod);
            this.Controls.Add(this.dtPeriodTo);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtPeriodFrom);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.chkUsePeriod);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdQueryRemoteEnrollStatus);
            this.Controls.Add(this.cmdSetAllEnrollData);
            this.Controls.Add(this.chkDupCheck);
            this.Controls.Add(this.btnImportM1001U);
            this.Controls.Add(this.txtDepart);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtUserTz5);
            this.Controls.Add(this.txtUserTz4);
            this.Controls.Add(this.txtUserTz3);
            this.Controls.Add(this.txtUserTz2);
            this.Controls.Add(this.txtUserTz1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbDuressSetting);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdModifyDuressFP);
            this.Controls.Add(this.txtCardNumber);
            this.Controls.Add(this.cmdDeleteCompany);
            this.Controls.Add(this.cmdSetCompany);
            this.Controls.Add(this.cmdGetName);
            this.Controls.Add(this.cmdSetName);
            this.Controls.Add(this.cmdRemoteEnroll_Card);
            this.Controls.Add(this.cmdExitRemoteEnroll);
            this.Controls.Add(this.cmdRemoteEnroll_Face);
            this.Controls.Add(this.cmdRemoteEnroll_FP);
            this.Controls.Add(this.cmdModifyPrivilege);
            this.Controls.Add(this.cmdEnableUser);
            this.Controls.Add(this.cmdGetAllEnrollData);
            this.Controls.Add(this.cmdGetEnrollData);
            this.Controls.Add(this.cmdClearData);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdGetEnrollInfo);
            this.Controls.Add(this.cmdDeleteEnrollData);
            this.Controls.Add(this.cmdSetEnrollData);
            this.Controls.Add(this.cmdEmptyEnrollData);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.chkDisable);
            this.Controls.Add(this.cmdDel);
            this.Controls.Add(this.cmbPrivilege);
            this.Controls.Add(this.lstEnrollData);
            this.Controls.Add(this.txtEnrollNumber);
            this.Controls.Add(this.cmbBackupNumber);
            this.Controls.Add(this.lblCardNum);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblEnrollData);
            this.Controls.Add(this.lblBackupNumber);
            this.Controls.Add(this._lblEnrollNum_0);
            this.Name = "frmEnroll";
            this.Text = "frmEnroll";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEnroll_FormClosed);
            this.Load += new System.EventHandler(this.frmEnroll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picUserPhoto)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtCardNumber;
        public System.Windows.Forms.Button cmdDeleteCompany;
        public System.Windows.Forms.Button cmdSetCompany;
        public System.Windows.Forms.Button cmdGetName;
        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.Button cmdSetName;
        public System.Windows.Forms.Button cmdModifyPrivilege;
        public System.Windows.Forms.Button cmdEnableUser;
        public System.Windows.Forms.Button cmdSetAllEnrollData;
        public System.Windows.Forms.Button cmdGetAllEnrollData;
        public System.Windows.Forms.Button cmdGetEnrollData;
        public System.Windows.Forms.Button cmdClearData;
        public System.Windows.Forms.Button cmdGetEnrollInfo;
        public System.Windows.Forms.Button cmdDeleteEnrollData;
        public System.Windows.Forms.Button cmdSetEnrollData;
        public System.Windows.Forms.Button cmdDel;
        public System.Windows.Forms.Button cmdExit;
        public System.Windows.Forms.Button cmdEmptyEnrollData;
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.CheckBox chkDisable;
        public System.Windows.Forms.ComboBox cmbPrivilege;
        public System.Windows.Forms.ListBox lstEnrollData;
        public System.Windows.Forms.TextBox txtEnrollNumber;
        public System.Windows.Forms.ComboBox cmbBackupNumber;
        public System.Windows.Forms.Label lblCardNum;
        public System.Windows.Forms.Label lbName;
        public System.Windows.Forms.Label Label2;
        public System.Windows.Forms.Label Label1;
        public System.Windows.Forms.Label lblMessage;
        public System.Windows.Forms.Label lblEnrollData;
        public System.Windows.Forms.Label lblBackupNumber;
        public System.Windows.Forms.Label _lblEnrollNum_0;
        public System.Windows.Forms.Button cmdModifyDuressFP;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cmbDuressSetting;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtUserTz2;
        public System.Windows.Forms.TextBox txtUserTz1;
        public System.Windows.Forms.TextBox txtUserTz4;
        public System.Windows.Forms.TextBox txtUserTz3;
        public System.Windows.Forms.TextBox txtUserTz5;
        public System.Windows.Forms.TextBox txtDepart;
        public System.Windows.Forms.Label label10;
        private System.Windows.Forms.OpenFileDialog OpenFileDlg;
        private System.Windows.Forms.CheckBox chkDupCheck;
		public System.Windows.Forms.Button btnImportM1001U;
        public System.Windows.Forms.Button cmdRemoteEnroll_FP;
        public System.Windows.Forms.Button cmdRemoteEnroll_Card;
        public System.Windows.Forms.Button cmdRemoteEnroll_Face;
        public System.Windows.Forms.Button cmdExitRemoteEnroll;
        public System.Windows.Forms.Button cmdQueryRemoteEnrollStatus;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btnSetUserPeriod;
        public System.Windows.Forms.Button btnGetUserPeriod;
        private System.Windows.Forms.DateTimePicker dtPeriodTo;
        public System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtPeriodFrom;
        public System.Windows.Forms.Label lblFrom;
        public System.Windows.Forms.CheckBox chkUsePeriod;
        public System.Windows.Forms.Button cmdDeleteUserPhoto;
        public System.Windows.Forms.Button cmdSetUserPhoto;
        public System.Windows.Forms.Button cmdGetUserPhoto;
        private System.Windows.Forms.PictureBox picUserPhoto;
        private System.Windows.Forms.TextBox txtUserPhotoFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdUserPhotoBrowse;
    }
}