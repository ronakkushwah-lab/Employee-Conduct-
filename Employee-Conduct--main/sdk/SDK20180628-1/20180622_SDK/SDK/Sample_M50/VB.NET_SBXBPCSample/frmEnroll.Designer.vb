<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEnroll
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents txtCardNumber As System.Windows.Forms.TextBox
	Public WithEvents cmdDeleteCompany As System.Windows.Forms.Button
	Public WithEvents cmdSetCompany As System.Windows.Forms.Button
	Public WithEvents cmdSendAllEnrollData As System.Windows.Forms.Button
	Public WithEvents cmdGetName As System.Windows.Forms.Button
	Public WithEvents cmdSetName As System.Windows.Forms.Button
	Public WithEvents cmdModifyPrivilege As System.Windows.Forms.Button
	Public WithEvents cmdEnableUser As System.Windows.Forms.Button
	Public WithEvents cmdSetAllEnrollData As System.Windows.Forms.Button
	Public WithEvents cmdGetAllEnrollData As System.Windows.Forms.Button
	Public WithEvents cmdGetEnrollData As System.Windows.Forms.Button
	Public WithEvents cmdClearData As System.Windows.Forms.Button
	Public WithEvents cmdExit As System.Windows.Forms.Button
	Public WithEvents cmdGetEnrollInfo As System.Windows.Forms.Button
	Public WithEvents cmdDeleteEnrollData As System.Windows.Forms.Button
	Public WithEvents cmdSetEnrollData As System.Windows.Forms.Button
	Public WithEvents cmdEmptyEnrollData As System.Windows.Forms.Button
	Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents chkEnable As System.Windows.Forms.CheckBox
	Public WithEvents cmdDel As System.Windows.Forms.Button
    Public WithEvents cmbEMachineNumber As System.Windows.Forms.ComboBox
	Public WithEvents cmbPrivilege As System.Windows.Forms.ComboBox
	Public WithEvents lstEnrollData As System.Windows.Forms.ListBox
	Public WithEvents txtEnrollNumber As System.Windows.Forms.TextBox
	Public WithEvents cmbBackupNumber As System.Windows.Forms.ComboBox
    Public WithEvents lbName As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents lblEMachineNumber As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lblMessage As System.Windows.Forms.Label
	Public WithEvents lblEnrollData As System.Windows.Forms.Label
	Public WithEvents lblBackupNumber As System.Windows.Forms.Label
	Public WithEvents _lblEnrollNum_0 As System.Windows.Forms.Label
	Public WithEvents lblEnrollNum As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEnroll))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdGetName = New System.Windows.Forms.Button()
        Me.cmdSetName = New System.Windows.Forms.Button()
        Me.cmdSetAllEnrollData = New System.Windows.Forms.Button()
        Me.cmdGetAllEnrollData = New System.Windows.Forms.Button()
        Me.cmdGetEnrollData = New System.Windows.Forms.Button()
        Me.cmdClearData = New System.Windows.Forms.Button()
        Me.cmdGetEnrollInfo = New System.Windows.Forms.Button()
        Me.cmdDeleteEnrollData = New System.Windows.Forms.Button()
        Me.cmdSetEnrollData = New System.Windows.Forms.Button()
        Me.cmdDel = New System.Windows.Forms.Button()
        Me.txtCardNumber = New System.Windows.Forms.TextBox()
        Me.cmdDeleteCompany = New System.Windows.Forms.Button()
        Me.cmdSetCompany = New System.Windows.Forms.Button()
        Me.cmdSendAllEnrollData = New System.Windows.Forms.Button()
        Me.cmdModifyPrivilege = New System.Windows.Forms.Button()
        Me.cmdEnableUser = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdEmptyEnrollData = New System.Windows.Forms.Button()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.chkEnable = New System.Windows.Forms.CheckBox()
        Me.cmbEMachineNumber = New System.Windows.Forms.ComboBox()
        Me.cmbPrivilege = New System.Windows.Forms.ComboBox()
        Me.lstEnrollData = New System.Windows.Forms.ListBox()
        Me.txtEnrollNumber = New System.Windows.Forms.TextBox()
        Me.cmbBackupNumber = New System.Windows.Forms.ComboBox()
        Me.lbName = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblEMachineNumber = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.lblEnrollData = New System.Windows.Forms.Label()
        Me.lblBackupNumber = New System.Windows.Forms.Label()
        Me._lblEnrollNum_0 = New System.Windows.Forms.Label()
        Me.lblEnrollNum = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.txtDepart = New System.Windows.Forms.TextBox()
        Me.label10 = New System.Windows.Forms.Label()
        Me.txtUserTz5 = New System.Windows.Forms.TextBox()
        Me.txtUserTz4 = New System.Windows.Forms.TextBox()
        Me.txtUserTz3 = New System.Windows.Forms.TextBox()
        Me.txtUserTz2 = New System.Windows.Forms.TextBox()
        Me.txtUserTz1 = New System.Windows.Forms.TextBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.cmbDuressSetting = New System.Windows.Forms.ComboBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.lblCardNum = New System.Windows.Forms.Label()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.picUserPhoto = New System.Windows.Forms.PictureBox()
        Me.txtUserPhotoFile = New System.Windows.Forms.TextBox()
        Me.cmdUserPhotoBrowse = New System.Windows.Forms.Button()
        Me.cmdModifyDuressFP = New System.Windows.Forms.Button()
        Me.cmdDeleteUserPhoto = New System.Windows.Forms.Button()
        Me.cmdSetUserPhoto = New System.Windows.Forms.Button()
        Me.cmdGetUserPhoto = New System.Windows.Forms.Button()
        Me.OpenFileDlg = New System.Windows.Forms.OpenFileDialog()
        Me.cmdRemoteEnroll = New System.Windows.Forms.Button()
        CType(Me.lblEnrollNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox1.SuspendLayout()
        CType(Me.picUserPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdGetName
        '
        Me.cmdGetName.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetName.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetName.Location = New System.Drawing.Point(593, 165)
        Me.cmdGetName.Name = "cmdGetName"
        Me.cmdGetName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetName.Size = New System.Drawing.Size(201, 30)
        Me.cmdGetName.TabIndex = 30
        Me.cmdGetName.Text = "Get Name Data"
        Me.ToolTip1.SetToolTip(Me.cmdGetName, "Get All Enroll Data From Device And Save To DataBase")
        Me.cmdGetName.UseVisualStyleBackColor = False
        '
        'cmdSetName
        '
        Me.cmdSetName.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetName.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetName.Location = New System.Drawing.Point(798, 165)
        Me.cmdSetName.Name = "cmdSetName"
        Me.cmdSetName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetName.Size = New System.Drawing.Size(201, 30)
        Me.cmdSetName.TabIndex = 29
        Me.cmdSetName.Text = "Set Name Data"
        Me.ToolTip1.SetToolTip(Me.cmdSetName, "Load All Enroll Data From DataBase And Set To Device")
        Me.cmdSetName.UseVisualStyleBackColor = False
        '
        'cmdSetAllEnrollData
        '
        Me.cmdSetAllEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetAllEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetAllEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetAllEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetAllEnrollData.Location = New System.Drawing.Point(799, 129)
        Me.cmdSetAllEnrollData.Name = "cmdSetAllEnrollData"
        Me.cmdSetAllEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetAllEnrollData.Size = New System.Drawing.Size(200, 30)
        Me.cmdSetAllEnrollData.TabIndex = 26
        Me.cmdSetAllEnrollData.Text = "Set All Enroll Data"
        Me.ToolTip1.SetToolTip(Me.cmdSetAllEnrollData, "Load All Enroll Data From DataBase And Set To Device")
        Me.cmdSetAllEnrollData.UseVisualStyleBackColor = False
        '
        'cmdGetAllEnrollData
        '
        Me.cmdGetAllEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetAllEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetAllEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetAllEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetAllEnrollData.Location = New System.Drawing.Point(593, 129)
        Me.cmdGetAllEnrollData.Name = "cmdGetAllEnrollData"
        Me.cmdGetAllEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetAllEnrollData.Size = New System.Drawing.Size(200, 30)
        Me.cmdGetAllEnrollData.TabIndex = 25
        Me.cmdGetAllEnrollData.Text = "Get All Enroll Data"
        Me.ToolTip1.SetToolTip(Me.cmdGetAllEnrollData, "Get All Enroll Data From Device And Save To DataBase")
        Me.cmdGetAllEnrollData.UseVisualStyleBackColor = False
        '
        'cmdGetEnrollData
        '
        Me.cmdGetEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetEnrollData.Location = New System.Drawing.Point(593, 64)
        Me.cmdGetEnrollData.Name = "cmdGetEnrollData"
        Me.cmdGetEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetEnrollData.Size = New System.Drawing.Size(200, 30)
        Me.cmdGetEnrollData.TabIndex = 24
        Me.cmdGetEnrollData.Text = "Get Enroll Data"
        Me.ToolTip1.SetToolTip(Me.cmdGetEnrollData, "Get EnrollData From Device")
        Me.cmdGetEnrollData.UseVisualStyleBackColor = False
        '
        'cmdClearData
        '
        Me.cmdClearData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClearData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearData.Location = New System.Drawing.Point(799, 376)
        Me.cmdClearData.Name = "cmdClearData"
        Me.cmdClearData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearData.Size = New System.Drawing.Size(200, 30)
        Me.cmdClearData.TabIndex = 23
        Me.cmdClearData.Text = "Clear All Data(E,GL,SL) "
        Me.ToolTip1.SetToolTip(Me.cmdClearData, "Clear EnrollData and LogDat Into Device")
        Me.cmdClearData.UseVisualStyleBackColor = False
        '
        'cmdGetEnrollInfo
        '
        Me.cmdGetEnrollInfo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetEnrollInfo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetEnrollInfo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetEnrollInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetEnrollInfo.Location = New System.Drawing.Point(594, 234)
        Me.cmdGetEnrollInfo.Name = "cmdGetEnrollInfo"
        Me.cmdGetEnrollInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetEnrollInfo.Size = New System.Drawing.Size(200, 30)
        Me.cmdGetEnrollInfo.TabIndex = 21
        Me.cmdGetEnrollInfo.Text = "Get Enroll Info"
        Me.ToolTip1.SetToolTip(Me.cmdGetEnrollInfo, "Get All Enrolled User Info From Device")
        Me.cmdGetEnrollInfo.UseVisualStyleBackColor = False
        '
        'cmdDeleteEnrollData
        '
        Me.cmdDeleteEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDeleteEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDeleteEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeleteEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeleteEnrollData.Location = New System.Drawing.Point(799, 97)
        Me.cmdDeleteEnrollData.Name = "cmdDeleteEnrollData"
        Me.cmdDeleteEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDeleteEnrollData.Size = New System.Drawing.Size(200, 30)
        Me.cmdDeleteEnrollData.TabIndex = 20
        Me.cmdDeleteEnrollData.Text = "Delete Enroll Data"
        Me.ToolTip1.SetToolTip(Me.cmdDeleteEnrollData, "Delete Enroll Data Into Device")
        Me.cmdDeleteEnrollData.UseVisualStyleBackColor = False
        '
        'cmdSetEnrollData
        '
        Me.cmdSetEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetEnrollData.Location = New System.Drawing.Point(799, 64)
        Me.cmdSetEnrollData.Name = "cmdSetEnrollData"
        Me.cmdSetEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetEnrollData.Size = New System.Drawing.Size(200, 30)
        Me.cmdSetEnrollData.TabIndex = 19
        Me.cmdSetEnrollData.Text = "Set Enroll Data"
        Me.ToolTip1.SetToolTip(Me.cmdSetEnrollData, "Set EnrollData To Device")
        Me.cmdSetEnrollData.UseVisualStyleBackColor = False
        '
        'cmdDel
        '
        Me.cmdDel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDel.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDel.Location = New System.Drawing.Point(440, 471)
        Me.cmdDel.Name = "cmdDel"
        Me.cmdDel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDel.Size = New System.Drawing.Size(100, 38)
        Me.cmdDel.TabIndex = 11
        Me.cmdDel.Text = "Delete DB"
        Me.ToolTip1.SetToolTip(Me.cmdDel, "Delete All Saved Data From DataBase")
        Me.cmdDel.UseVisualStyleBackColor = False
        '
        'txtCardNumber
        '
        Me.txtCardNumber.AcceptsReturn = True
        Me.txtCardNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtCardNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCardNumber.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCardNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCardNumber.Location = New System.Drawing.Point(160, 108)
        Me.txtCardNumber.MaxLength = 0
        Me.txtCardNumber.Name = "txtCardNumber"
        Me.txtCardNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCardNumber.Size = New System.Drawing.Size(81, 26)
        Me.txtCardNumber.TabIndex = 35
        '
        'cmdDeleteCompany
        '
        Me.cmdDeleteCompany.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDeleteCompany.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDeleteCompany.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeleteCompany.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeleteCompany.Location = New System.Drawing.Point(798, 198)
        Me.cmdDeleteCompany.Name = "cmdDeleteCompany"
        Me.cmdDeleteCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDeleteCompany.Size = New System.Drawing.Size(201, 30)
        Me.cmdDeleteCompany.TabIndex = 33
        Me.cmdDeleteCompany.Text = "Delete Company Name"
        Me.cmdDeleteCompany.UseVisualStyleBackColor = False
        '
        'cmdSetCompany
        '
        Me.cmdSetCompany.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetCompany.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetCompany.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetCompany.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetCompany.Location = New System.Drawing.Point(593, 198)
        Me.cmdSetCompany.Name = "cmdSetCompany"
        Me.cmdSetCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetCompany.Size = New System.Drawing.Size(201, 30)
        Me.cmdSetCompany.TabIndex = 32
        Me.cmdSetCompany.Text = "Set Company Name"
        Me.cmdSetCompany.UseVisualStyleBackColor = False
        '
        'cmdSendAllEnrollData
        '
        Me.cmdSendAllEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendAllEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSendAllEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSendAllEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendAllEnrollData.Location = New System.Drawing.Point(593, 506)
        Me.cmdSendAllEnrollData.Name = "cmdSendAllEnrollData"
        Me.cmdSendAllEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSendAllEnrollData.Size = New System.Drawing.Size(371, 30)
        Me.cmdSendAllEnrollData.TabIndex = 31
        Me.cmdSendAllEnrollData.Text = "Send All Enroll Data (Not supported)"
        Me.cmdSendAllEnrollData.UseVisualStyleBackColor = False
        Me.cmdSendAllEnrollData.Visible = False
        '
        'cmdModifyPrivilege
        '
        Me.cmdModifyPrivilege.BackColor = System.Drawing.SystemColors.Control
        Me.cmdModifyPrivilege.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModifyPrivilege.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModifyPrivilege.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModifyPrivilege.Location = New System.Drawing.Point(594, 270)
        Me.cmdModifyPrivilege.Name = "cmdModifyPrivilege"
        Me.cmdModifyPrivilege.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModifyPrivilege.Size = New System.Drawing.Size(200, 30)
        Me.cmdModifyPrivilege.TabIndex = 28
        Me.cmdModifyPrivilege.Text = "ModifyPrivilege"
        Me.cmdModifyPrivilege.UseVisualStyleBackColor = False
        '
        'cmdEnableUser
        '
        Me.cmdEnableUser.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnableUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnableUser.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEnableUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnableUser.Location = New System.Drawing.Point(800, 234)
        Me.cmdEnableUser.Name = "cmdEnableUser"
        Me.cmdEnableUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnableUser.Size = New System.Drawing.Size(200, 30)
        Me.cmdEnableUser.TabIndex = 27
        Me.cmdEnableUser.Text = "EnableUser"
        Me.cmdEnableUser.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(697, 442)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(200, 30)
        Me.cmdExit.TabIndex = 22
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdEmptyEnrollData
        '
        Me.cmdEmptyEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEmptyEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEmptyEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEmptyEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEmptyEnrollData.Location = New System.Drawing.Point(593, 375)
        Me.cmdEmptyEnrollData.Name = "cmdEmptyEnrollData"
        Me.cmdEmptyEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEmptyEnrollData.Size = New System.Drawing.Size(200, 30)
        Me.cmdEmptyEnrollData.TabIndex = 18
        Me.cmdEmptyEnrollData.Text = "Empty Enroll Data"
        Me.cmdEmptyEnrollData.UseVisualStyleBackColor = False
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtName.Location = New System.Drawing.Point(341, 63)
        Me.txtName.MaxLength = 10
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(154, 26)
        Me.txtName.TabIndex = 16
        '
        'chkEnable
        '
        Me.chkEnable.BackColor = System.Drawing.SystemColors.Control
        Me.chkEnable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkEnable.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEnable.Location = New System.Drawing.Point(292, 95)
        Me.chkEnable.Name = "chkEnable"
        Me.chkEnable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkEnable.Size = New System.Drawing.Size(147, 19)
        Me.chkEnable.TabIndex = 13
        Me.chkEnable.Text = "Disable User"
        Me.chkEnable.UseVisualStyleBackColor = False
        '
        'cmbEMachineNumber
        '
        Me.cmbEMachineNumber.BackColor = System.Drawing.SystemColors.Window
        Me.cmbEMachineNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbEMachineNumber.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEMachineNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbEMachineNumber.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9"})
        Me.cmbEMachineNumber.Location = New System.Drawing.Point(159, 165)
        Me.cmbEMachineNumber.Name = "cmbEMachineNumber"
        Me.cmbEMachineNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbEMachineNumber.Size = New System.Drawing.Size(81, 27)
        Me.cmbEMachineNumber.TabIndex = 9
        Me.cmbEMachineNumber.Text = "cmbEMachineNumber"
        '
        'cmbPrivilege
        '
        Me.cmbPrivilege.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPrivilege.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPrivilege.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPrivilege.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPrivilege.Items.AddRange(New Object() {"0", "1", "2"})
        Me.cmbPrivilege.Location = New System.Drawing.Point(159, 229)
        Me.cmbPrivilege.Name = "cmbPrivilege"
        Me.cmbPrivilege.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPrivilege.Size = New System.Drawing.Size(81, 27)
        Me.cmbPrivilege.TabIndex = 7
        Me.cmbPrivilege.Text = "cmbPrivilege"
        '
        'lstEnrollData
        '
        Me.lstEnrollData.BackColor = System.Drawing.SystemColors.Window
        Me.lstEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstEnrollData.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstEnrollData.Location = New System.Drawing.Point(285, 144)
        Me.lstEnrollData.Name = "lstEnrollData"
        Me.lstEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstEnrollData.Size = New System.Drawing.Size(220, 134)
        Me.lstEnrollData.TabIndex = 4
        '
        'txtEnrollNumber
        '
        Me.txtEnrollNumber.AcceptsReturn = True
        Me.txtEnrollNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtEnrollNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEnrollNumber.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEnrollNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEnrollNumber.Location = New System.Drawing.Point(160, 71)
        Me.txtEnrollNumber.MaxLength = 8
        Me.txtEnrollNumber.Name = "txtEnrollNumber"
        Me.txtEnrollNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEnrollNumber.Size = New System.Drawing.Size(81, 26)
        Me.txtEnrollNumber.TabIndex = 2
        '
        'cmbBackupNumber
        '
        Me.cmbBackupNumber.BackColor = System.Drawing.SystemColors.Window
        Me.cmbBackupNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbBackupNumber.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBackupNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbBackupNumber.Items.AddRange(New Object() {"0", "1", "2", "10", "11", "12", "13", "14", "15", "16", "17"})
        Me.cmbBackupNumber.Location = New System.Drawing.Point(159, 197)
        Me.cmbBackupNumber.Name = "cmbBackupNumber"
        Me.cmbBackupNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbBackupNumber.Size = New System.Drawing.Size(81, 27)
        Me.cmbBackupNumber.TabIndex = 0
        Me.cmbBackupNumber.Text = "cmbBackupNumber"
        '
        'lbName
        '
        Me.lbName.AutoSize = True
        Me.lbName.BackColor = System.Drawing.SystemColors.Control
        Me.lbName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbName.Location = New System.Drawing.Point(293, 68)
        Me.lbName.Name = "lbName"
        Me.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbName.Size = New System.Drawing.Size(53, 19)
        Me.lbName.TabIndex = 17
        Me.lbName.Text = "Name :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(413, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(50, 19)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Total : "
        '
        'lblEMachineNumber
        '
        Me.lblEMachineNumber.AutoSize = True
        Me.lblEMachineNumber.BackColor = System.Drawing.SystemColors.Control
        Me.lblEMachineNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEMachineNumber.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEMachineNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEMachineNumber.Location = New System.Drawing.Point(31, 165)
        Me.lblEMachineNumber.Name = "lblEMachineNumber"
        Me.lblEMachineNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEMachineNumber.Size = New System.Drawing.Size(131, 19)
        Me.lblEMachineNumber.TabIndex = 10
        Me.lblEMachineNumber.Text = "EMachine Number :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(30, 230)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(67, 19)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Privilege :"
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMessage.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(24, 24)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMessage.Size = New System.Drawing.Size(533, 29)
        Me.lblMessage.TabIndex = 6
        Me.lblMessage.Text = "Message"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblEnrollData
        '
        Me.lblEnrollData.AutoSize = True
        Me.lblEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.lblEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEnrollData.Location = New System.Drawing.Point(293, 119)
        Me.lblEnrollData.Name = "lblEnrollData"
        Me.lblEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEnrollData.Size = New System.Drawing.Size(99, 19)
        Me.lblEnrollData.TabIndex = 5
        Me.lblEnrollData.Text = "Enrolled Data :"
        '
        'lblBackupNumber
        '
        Me.lblBackupNumber.AutoSize = True
        Me.lblBackupNumber.BackColor = System.Drawing.SystemColors.Control
        Me.lblBackupNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBackupNumber.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBackupNumber.Location = New System.Drawing.Point(31, 201)
        Me.lblBackupNumber.Name = "lblBackupNumber"
        Me.lblBackupNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBackupNumber.Size = New System.Drawing.Size(117, 19)
        Me.lblBackupNumber.TabIndex = 3
        Me.lblBackupNumber.Text = "Backup Number :"
        '
        '_lblEnrollNum_0
        '
        Me._lblEnrollNum_0.AutoSize = True
        Me._lblEnrollNum_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblEnrollNum_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblEnrollNum_0.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblEnrollNum_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEnrollNum.SetIndex(Me._lblEnrollNum_0, CType(0, Short))
        Me._lblEnrollNum_0.Location = New System.Drawing.Point(31, 75)
        Me._lblEnrollNum_0.Name = "_lblEnrollNum_0"
        Me._lblEnrollNum_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblEnrollNum_0.Size = New System.Drawing.Size(105, 19)
        Me._lblEnrollNum_0.TabIndex = 1
        Me._lblEnrollNum_0.Text = "Enroll Number :"
        '
        'txtDepart
        '
        Me.txtDepart.AcceptsReturn = True
        Me.txtDepart.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepart.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDepart.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepart.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDepart.Location = New System.Drawing.Point(160, 478)
        Me.txtDepart.MaxLength = 0
        Me.txtDepart.Name = "txtDepart"
        Me.txtDepart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDepart.Size = New System.Drawing.Size(81, 26)
        Me.txtDepart.TabIndex = 103
        '
        'label10
        '
        Me.label10.AutoSize = True
        Me.label10.BackColor = System.Drawing.SystemColors.Control
        Me.label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.label10.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label10.Location = New System.Drawing.Point(32, 483)
        Me.label10.Name = "label10"
        Me.label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label10.Size = New System.Drawing.Size(84, 19)
        Me.label10.TabIndex = 102
        Me.label10.Text = "User Depart"
        '
        'txtUserTz5
        '
        Me.txtUserTz5.AcceptsReturn = True
        Me.txtUserTz5.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserTz5.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUserTz5.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserTz5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserTz5.Location = New System.Drawing.Point(160, 435)
        Me.txtUserTz5.MaxLength = 0
        Me.txtUserTz5.Name = "txtUserTz5"
        Me.txtUserTz5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUserTz5.Size = New System.Drawing.Size(81, 26)
        Me.txtUserTz5.TabIndex = 101
        '
        'txtUserTz4
        '
        Me.txtUserTz4.AcceptsReturn = True
        Me.txtUserTz4.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserTz4.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUserTz4.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserTz4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserTz4.Location = New System.Drawing.Point(160, 407)
        Me.txtUserTz4.MaxLength = 0
        Me.txtUserTz4.Name = "txtUserTz4"
        Me.txtUserTz4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUserTz4.Size = New System.Drawing.Size(81, 26)
        Me.txtUserTz4.TabIndex = 100
        '
        'txtUserTz3
        '
        Me.txtUserTz3.AcceptsReturn = True
        Me.txtUserTz3.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserTz3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUserTz3.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserTz3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserTz3.Location = New System.Drawing.Point(160, 379)
        Me.txtUserTz3.MaxLength = 8
        Me.txtUserTz3.Name = "txtUserTz3"
        Me.txtUserTz3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUserTz3.Size = New System.Drawing.Size(81, 26)
        Me.txtUserTz3.TabIndex = 99
        '
        'txtUserTz2
        '
        Me.txtUserTz2.AcceptsReturn = True
        Me.txtUserTz2.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserTz2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUserTz2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserTz2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserTz2.Location = New System.Drawing.Point(160, 351)
        Me.txtUserTz2.MaxLength = 0
        Me.txtUserTz2.Name = "txtUserTz2"
        Me.txtUserTz2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUserTz2.Size = New System.Drawing.Size(81, 26)
        Me.txtUserTz2.TabIndex = 98
        '
        'txtUserTz1
        '
        Me.txtUserTz1.AcceptsReturn = True
        Me.txtUserTz1.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserTz1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUserTz1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserTz1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserTz1.Location = New System.Drawing.Point(160, 323)
        Me.txtUserTz1.MaxLength = 8
        Me.txtUserTz1.Name = "txtUserTz1"
        Me.txtUserTz1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUserTz1.Size = New System.Drawing.Size(81, 26)
        Me.txtUserTz1.TabIndex = 97
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.BackColor = System.Drawing.SystemColors.Control
        Me.label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.label9.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label9.Location = New System.Drawing.Point(32, 440)
        Me.label9.Name = "label9"
        Me.label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label9.Size = New System.Drawing.Size(71, 19)
        Me.label9.TabIndex = 96
        Me.label9.Text = "User TZ5:"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.BackColor = System.Drawing.SystemColors.Control
        Me.label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.label8.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label8.Location = New System.Drawing.Point(32, 414)
        Me.label8.Name = "label8"
        Me.label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label8.Size = New System.Drawing.Size(71, 19)
        Me.label8.TabIndex = 95
        Me.label8.Text = "User TZ4:"
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.BackColor = System.Drawing.SystemColors.Control
        Me.label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.label7.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label7.Location = New System.Drawing.Point(32, 387)
        Me.label7.Name = "label7"
        Me.label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label7.Size = New System.Drawing.Size(71, 19)
        Me.label7.TabIndex = 94
        Me.label7.Text = "User TZ3:"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.BackColor = System.Drawing.SystemColors.Control
        Me.label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.label6.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label6.Location = New System.Drawing.Point(32, 357)
        Me.label6.Name = "label6"
        Me.label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label6.Size = New System.Drawing.Size(71, 19)
        Me.label6.TabIndex = 93
        Me.label6.Text = "User TZ2:"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.BackColor = System.Drawing.SystemColors.Control
        Me.label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.label5.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label5.Location = New System.Drawing.Point(32, 326)
        Me.label5.Name = "label5"
        Me.label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label5.Size = New System.Drawing.Size(71, 19)
        Me.label5.TabIndex = 92
        Me.label5.Text = "User TZ1:"
        '
        'cmbDuressSetting
        '
        Me.cmbDuressSetting.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDuressSetting.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDuressSetting.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDuressSetting.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDuressSetting.Items.AddRange(New Object() {"0", "1"})
        Me.cmbDuressSetting.Location = New System.Drawing.Point(160, 278)
        Me.cmbDuressSetting.Name = "cmbDuressSetting"
        Me.cmbDuressSetting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDuressSetting.Size = New System.Drawing.Size(81, 27)
        Me.cmbDuressSetting.TabIndex = 91
        Me.cmbDuressSetting.Text = "0"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.BackColor = System.Drawing.SystemColors.Control
        Me.label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.label4.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label4.Location = New System.Drawing.Point(32, 283)
        Me.label4.Name = "label4"
        Me.label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label4.Size = New System.Drawing.Size(54, 19)
        Me.label4.TabIndex = 90
        Me.label4.Text = "Duress:"
        '
        'lblCardNum
        '
        Me.lblCardNum.BackColor = System.Drawing.SystemColors.Control
        Me.lblCardNum.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCardNum.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCardNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCardNum.Location = New System.Drawing.Point(26, 108)
        Me.lblCardNum.Name = "lblCardNum"
        Me.lblCardNum.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCardNum.Size = New System.Drawing.Size(122, 39)
        Me.lblCardNum.TabIndex = 104
        Me.lblCardNum.Text = "Card or Password Number :"
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.picUserPhoto)
        Me.groupBox1.Controls.Add(Me.txtUserPhotoFile)
        Me.groupBox1.Controls.Add(Me.cmdUserPhotoBrowse)
        Me.groupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.groupBox1.ForeColor = System.Drawing.Color.Black
        Me.groupBox1.Location = New System.Drawing.Point(285, 294)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(267, 160)
        Me.groupBox1.TabIndex = 105
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "User Photo Related"
        '
        'picUserPhoto
        '
        Me.picUserPhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picUserPhoto.Location = New System.Drawing.Point(17, 52)
        Me.picUserPhoto.Name = "picUserPhoto"
        Me.picUserPhoto.Size = New System.Drawing.Size(117, 99)
        Me.picUserPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picUserPhoto.TabIndex = 2
        Me.picUserPhoto.TabStop = False
        '
        'txtUserPhotoFile
        '
        Me.txtUserPhotoFile.Location = New System.Drawing.Point(12, 22)
        Me.txtUserPhotoFile.Name = "txtUserPhotoFile"
        Me.txtUserPhotoFile.Size = New System.Drawing.Size(216, 23)
        Me.txtUserPhotoFile.TabIndex = 1
        '
        'cmdUserPhotoBrowse
        '
        Me.cmdUserPhotoBrowse.Location = New System.Drawing.Point(228, 21)
        Me.cmdUserPhotoBrowse.Name = "cmdUserPhotoBrowse"
        Me.cmdUserPhotoBrowse.Size = New System.Drawing.Size(27, 25)
        Me.cmdUserPhotoBrowse.TabIndex = 0
        Me.cmdUserPhotoBrowse.Text = "..."
        Me.cmdUserPhotoBrowse.UseVisualStyleBackColor = True
        '
        'cmdModifyDuressFP
        '
        Me.cmdModifyDuressFP.BackColor = System.Drawing.SystemColors.Control
        Me.cmdModifyDuressFP.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModifyDuressFP.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModifyDuressFP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModifyDuressFP.Location = New System.Drawing.Point(800, 270)
        Me.cmdModifyDuressFP.Name = "cmdModifyDuressFP"
        Me.cmdModifyDuressFP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModifyDuressFP.Size = New System.Drawing.Size(200, 30)
        Me.cmdModifyDuressFP.TabIndex = 106
        Me.cmdModifyDuressFP.Text = "Modify Duress FP"
        Me.cmdModifyDuressFP.UseVisualStyleBackColor = False
        '
        'cmdDeleteUserPhoto
        '
        Me.cmdDeleteUserPhoto.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDeleteUserPhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDeleteUserPhoto.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeleteUserPhoto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeleteUserPhoto.Location = New System.Drawing.Point(800, 340)
        Me.cmdDeleteUserPhoto.Name = "cmdDeleteUserPhoto"
        Me.cmdDeleteUserPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDeleteUserPhoto.Size = New System.Drawing.Size(200, 30)
        Me.cmdDeleteUserPhoto.TabIndex = 109
        Me.cmdDeleteUserPhoto.Text = "Delete User Photo"
        Me.cmdDeleteUserPhoto.UseVisualStyleBackColor = False
        '
        'cmdSetUserPhoto
        '
        Me.cmdSetUserPhoto.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetUserPhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetUserPhoto.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetUserPhoto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetUserPhoto.Location = New System.Drawing.Point(800, 304)
        Me.cmdSetUserPhoto.Name = "cmdSetUserPhoto"
        Me.cmdSetUserPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetUserPhoto.Size = New System.Drawing.Size(200, 30)
        Me.cmdSetUserPhoto.TabIndex = 108
        Me.cmdSetUserPhoto.Text = "Set User Photo"
        Me.cmdSetUserPhoto.UseVisualStyleBackColor = False
        '
        'cmdGetUserPhoto
        '
        Me.cmdGetUserPhoto.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetUserPhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetUserPhoto.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetUserPhoto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetUserPhoto.Location = New System.Drawing.Point(594, 305)
        Me.cmdGetUserPhoto.Name = "cmdGetUserPhoto"
        Me.cmdGetUserPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetUserPhoto.Size = New System.Drawing.Size(200, 30)
        Me.cmdGetUserPhoto.TabIndex = 107
        Me.cmdGetUserPhoto.Text = "Get User Photo"
        Me.cmdGetUserPhoto.UseVisualStyleBackColor = False
        '
        'OpenFileDlg
        '
        Me.OpenFileDlg.FileName = "OpenFileDialog1"
        '
        'cmdRemoteEnroll
        '
        Me.cmdRemoteEnroll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRemoteEnroll.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRemoteEnroll.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRemoteEnroll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRemoteEnroll.Location = New System.Drawing.Point(593, 341)
        Me.cmdRemoteEnroll.Name = "cmdRemoteEnroll"
        Me.cmdRemoteEnroll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRemoteEnroll.Size = New System.Drawing.Size(200, 29)
        Me.cmdRemoteEnroll.TabIndex = 110
        Me.cmdRemoteEnroll.Text = "Remote Enroll"
        Me.cmdRemoteEnroll.UseVisualStyleBackColor = False
        '
        'frmEnroll
        '
        Me.AcceptButton = Me.cmdExit
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1023, 526)
        Me.Controls.Add(Me.cmdRemoteEnroll)
        Me.Controls.Add(Me.cmdDeleteUserPhoto)
        Me.Controls.Add(Me.cmdSetUserPhoto)
        Me.Controls.Add(Me.cmdGetUserPhoto)
        Me.Controls.Add(Me.cmdModifyDuressFP)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.lblCardNum)
        Me.Controls.Add(Me.txtDepart)
        Me.Controls.Add(Me.label10)
        Me.Controls.Add(Me.txtUserTz5)
        Me.Controls.Add(Me.txtUserTz4)
        Me.Controls.Add(Me.txtUserTz3)
        Me.Controls.Add(Me.txtUserTz2)
        Me.Controls.Add(Me.txtUserTz1)
        Me.Controls.Add(Me.label9)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.cmbDuressSetting)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.txtCardNumber)
        Me.Controls.Add(Me.cmdDeleteCompany)
        Me.Controls.Add(Me.cmdSetCompany)
        Me.Controls.Add(Me.cmdSendAllEnrollData)
        Me.Controls.Add(Me.cmdGetName)
        Me.Controls.Add(Me.cmdSetName)
        Me.Controls.Add(Me.cmdModifyPrivilege)
        Me.Controls.Add(Me.cmdEnableUser)
        Me.Controls.Add(Me.cmdSetAllEnrollData)
        Me.Controls.Add(Me.cmdGetAllEnrollData)
        Me.Controls.Add(Me.cmdGetEnrollData)
        Me.Controls.Add(Me.cmdClearData)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdGetEnrollInfo)
        Me.Controls.Add(Me.cmdDeleteEnrollData)
        Me.Controls.Add(Me.cmdSetEnrollData)
        Me.Controls.Add(Me.cmdEmptyEnrollData)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.chkEnable)
        Me.Controls.Add(Me.cmdDel)
        Me.Controls.Add(Me.cmbEMachineNumber)
        Me.Controls.Add(Me.cmbPrivilege)
        Me.Controls.Add(Me.lstEnrollData)
        Me.Controls.Add(Me.txtEnrollNumber)
        Me.Controls.Add(Me.cmbBackupNumber)
        Me.Controls.Add(Me.lbName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblEMachineNumber)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.lblEnrollData)
        Me.Controls.Add(Me.lblBackupNumber)
        Me.Controls.Add(Me._lblEnrollNum_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(205, 102)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEnroll"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage Enroll Data"
        CType(Me.lblEnrollNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        CType(Me.picUserPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtDepart As System.Windows.Forms.TextBox
    Public WithEvents label10 As System.Windows.Forms.Label
    Public WithEvents txtUserTz5 As System.Windows.Forms.TextBox
    Public WithEvents txtUserTz4 As System.Windows.Forms.TextBox
    Public WithEvents txtUserTz3 As System.Windows.Forms.TextBox
    Public WithEvents txtUserTz2 As System.Windows.Forms.TextBox
    Public WithEvents txtUserTz1 As System.Windows.Forms.TextBox
    Public WithEvents label9 As System.Windows.Forms.Label
    Public WithEvents label8 As System.Windows.Forms.Label
    Public WithEvents label7 As System.Windows.Forms.Label
    Public WithEvents label6 As System.Windows.Forms.Label
    Public WithEvents label5 As System.Windows.Forms.Label
    Public WithEvents cmbDuressSetting As System.Windows.Forms.ComboBox
    Public WithEvents label4 As System.Windows.Forms.Label
    Public WithEvents lblCardNum As System.Windows.Forms.Label
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents picUserPhoto As System.Windows.Forms.PictureBox
    Private WithEvents txtUserPhotoFile As System.Windows.Forms.TextBox
    Private WithEvents cmdUserPhotoBrowse As System.Windows.Forms.Button
    Public WithEvents cmdModifyDuressFP As System.Windows.Forms.Button
    Public WithEvents cmdDeleteUserPhoto As System.Windows.Forms.Button
    Public WithEvents cmdSetUserPhoto As System.Windows.Forms.Button
    Public WithEvents cmdGetUserPhoto As System.Windows.Forms.Button
    Friend WithEvents OpenFileDlg As System.Windows.Forms.OpenFileDialog
    Public WithEvents cmdRemoteEnroll As System.Windows.Forms.Button
#End Region 
End Class