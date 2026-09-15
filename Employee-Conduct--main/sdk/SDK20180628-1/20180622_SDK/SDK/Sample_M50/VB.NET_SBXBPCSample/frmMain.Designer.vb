<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMain
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
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.cmbMachineNumber = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optSerialDevice = New System.Windows.Forms.RadioButton()
        Me.cmbBaudrate = New System.Windows.Forms.ComboBox()
        Me.lblBaudrate = New System.Windows.Forms.Label()
        Me.cmbComPort = New System.Windows.Forms.ComboBox()
        Me.lblComPort = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.optNetworkDevice = New System.Windows.Forms.RadioButton()
        Me.txtDevId = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPortNo = New System.Windows.Forms.TextBox()
        Me.txtIPAddress = New System.Windows.Forms.TextBox()
        Me.lblPortNo = New System.Windows.Forms.Label()
        Me.lblIPAddress = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdScreenSaver = New System.Windows.Forms.Button()
        Me.cmdDaigong = New System.Windows.Forms.Button()
        Me.cmdDepartment = New System.Windows.Forms.Button()
        Me.cmdAccessTz = New System.Windows.Forms.Button()
        Me.cmdProductCode = New System.Windows.Forms.Button()
        Me.cmdEventCapture = New System.Windows.Forms.Button()
        Me.cmdTrMode = New System.Windows.Forms.Button()
        Me.cmdBellInfo = New System.Windows.Forms.Button()
        Me.cmdLockCtl = New System.Windows.Forms.Button()
        Me.cmdSystemInfo = New System.Windows.Forms.Button()
        Me.cmdLogData = New System.Windows.Forms.Button()
        Me.cmdEnrollData = New System.Windows.Forms.Button()
        Me.optUSBDevice = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(199, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(196, 31)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "SBXPC Sample"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 16.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(254, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 25)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "F500"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdClose)
        Me.GroupBox1.Controls.Add(Me.cmdOpen)
        Me.GroupBox1.Controls.Add(Me.cmbMachineNumber)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(13, 92)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(568, 73)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Connect"
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(453, 25)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(98, 33)
        Me.cmdClose.TabIndex = 11
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdOpen
        '
        Me.cmdOpen.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOpen.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOpen.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOpen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOpen.Location = New System.Drawing.Point(322, 25)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOpen.Size = New System.Drawing.Size(98, 33)
        Me.cmdOpen.TabIndex = 10
        Me.cmdOpen.Text = "Open"
        Me.cmdOpen.UseVisualStyleBackColor = False
        '
        'cmbMachineNumber
        '
        Me.cmbMachineNumber.FormattingEnabled = True
        Me.cmbMachineNumber.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9"})
        Me.cmbMachineNumber.Location = New System.Drawing.Point(158, 31)
        Me.cmbMachineNumber.Name = "cmbMachineNumber"
        Me.cmbMachineNumber.Size = New System.Drawing.Size(113, 27)
        Me.cmbMachineNumber.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.Label4.Location = New System.Drawing.Point(19, 34)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(118, 19)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Machine Number:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optSerialDevice)
        Me.GroupBox2.Controls.Add(Me.cmbBaudrate)
        Me.GroupBox2.Controls.Add(Me.lblBaudrate)
        Me.GroupBox2.Controls.Add(Me.cmbComPort)
        Me.GroupBox2.Controls.Add(Me.lblComPort)
        Me.GroupBox2.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(13, 171)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(239, 107)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        '
        'optSerialDevice
        '
        Me.optSerialDevice.AutoSize = True
        Me.optSerialDevice.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.optSerialDevice.Location = New System.Drawing.Point(6, 0)
        Me.optSerialDevice.Name = "optSerialDevice"
        Me.optSerialDevice.Size = New System.Drawing.Size(117, 23)
        Me.optSerialDevice.TabIndex = 4
        Me.optSerialDevice.Text = "Serial Device"
        Me.optSerialDevice.UseVisualStyleBackColor = True
        '
        'cmbBaudrate
        '
        Me.cmbBaudrate.FormattingEnabled = True
        Me.cmbBaudrate.Items.AddRange(New Object() {"9600", "19200", "38400", "57600", "115200"})
        Me.cmbBaudrate.Location = New System.Drawing.Point(106, 67)
        Me.cmbBaudrate.Name = "cmbBaudrate"
        Me.cmbBaudrate.Size = New System.Drawing.Size(113, 27)
        Me.cmbBaudrate.TabIndex = 3
        '
        'lblBaudrate
        '
        Me.lblBaudrate.AutoSize = True
        Me.lblBaudrate.Location = New System.Drawing.Point(19, 70)
        Me.lblBaudrate.Name = "lblBaudrate"
        Me.lblBaudrate.Size = New System.Drawing.Size(67, 19)
        Me.lblBaudrate.TabIndex = 2
        Me.lblBaudrate.Text = "Baudrate:"
        '
        'cmbComPort
        '
        Me.cmbComPort.FormattingEnabled = True
        Me.cmbComPort.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8"})
        Me.cmbComPort.Location = New System.Drawing.Point(106, 31)
        Me.cmbComPort.Name = "cmbComPort"
        Me.cmbComPort.Size = New System.Drawing.Size(113, 27)
        Me.cmbComPort.TabIndex = 1
        '
        'lblComPort
        '
        Me.lblComPort.AutoSize = True
        Me.lblComPort.Location = New System.Drawing.Point(19, 34)
        Me.lblComPort.Name = "lblComPort"
        Me.lblComPort.Size = New System.Drawing.Size(68, 19)
        Me.lblComPort.TabIndex = 0
        Me.lblComPort.Text = "ComPort:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.optNetworkDevice)
        Me.GroupBox3.Controls.Add(Me.txtDevId)
        Me.GroupBox3.Controls.Add(Me.txtPassword)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.lblPassword)
        Me.GroupBox3.Controls.Add(Me.txtPortNo)
        Me.GroupBox3.Controls.Add(Me.txtIPAddress)
        Me.GroupBox3.Controls.Add(Me.lblPortNo)
        Me.GroupBox3.Controls.Add(Me.lblIPAddress)
        Me.GroupBox3.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.GroupBox3.ForeColor = System.Drawing.Color.Black
        Me.GroupBox3.Location = New System.Drawing.Point(13, 284)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(239, 189)
        Me.GroupBox3.TabIndex = 13
        Me.GroupBox3.TabStop = False
        '
        'optNetworkDevice
        '
        Me.optNetworkDevice.AutoSize = True
        Me.optNetworkDevice.Checked = True
        Me.optNetworkDevice.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.optNetworkDevice.Location = New System.Drawing.Point(6, 0)
        Me.optNetworkDevice.Name = "optNetworkDevice"
        Me.optNetworkDevice.Size = New System.Drawing.Size(136, 23)
        Me.optNetworkDevice.TabIndex = 5
        Me.optNetworkDevice.TabStop = True
        Me.optNetworkDevice.Text = "Network Device"
        Me.optNetworkDevice.UseVisualStyleBackColor = True
        '
        'txtDevId
        '
        Me.txtDevId.Location = New System.Drawing.Point(106, 136)
        Me.txtDevId.Name = "txtDevId"
        Me.txtDevId.Size = New System.Drawing.Size(120, 26)
        Me.txtDevId.TabIndex = 6
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(106, 101)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(120, 26)
        Me.txtPassword.TabIndex = 6
        Me.txtPassword.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 139)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 19)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Device ID:"
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(13, 104)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(72, 19)
        Me.lblPassword.TabIndex = 5
        Me.lblPassword.Text = "Password:"
        '
        'txtPortNo
        '
        Me.txtPortNo.Location = New System.Drawing.Point(106, 67)
        Me.txtPortNo.Name = "txtPortNo"
        Me.txtPortNo.Size = New System.Drawing.Size(120, 26)
        Me.txtPortNo.TabIndex = 4
        Me.txtPortNo.Text = "5005"
        '
        'txtIPAddress
        '
        Me.txtIPAddress.Location = New System.Drawing.Point(106, 31)
        Me.txtIPAddress.Name = "txtIPAddress"
        Me.txtIPAddress.Size = New System.Drawing.Size(120, 26)
        Me.txtIPAddress.TabIndex = 3
        Me.txtIPAddress.Text = "192.168.1.224"
        '
        'lblPortNo
        '
        Me.lblPortNo.AutoSize = True
        Me.lblPortNo.Location = New System.Drawing.Point(13, 70)
        Me.lblPortNo.Name = "lblPortNo"
        Me.lblPortNo.Size = New System.Drawing.Size(92, 19)
        Me.lblPortNo.TabIndex = 2
        Me.lblPortNo.Text = "Port Number:"
        '
        'lblIPAddress
        '
        Me.lblIPAddress.AutoSize = True
        Me.lblIPAddress.Location = New System.Drawing.Point(13, 34)
        Me.lblIPAddress.Name = "lblIPAddress"
        Me.lblIPAddress.Size = New System.Drawing.Size(79, 19)
        Me.lblIPAddress.TabIndex = 0
        Me.lblIPAddress.Text = "IP Address:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cmdExit)
        Me.GroupBox4.Controls.Add(Me.cmdScreenSaver)
        Me.GroupBox4.Controls.Add(Me.cmdDaigong)
        Me.GroupBox4.Controls.Add(Me.cmdDepartment)
        Me.GroupBox4.Controls.Add(Me.cmdAccessTz)
        Me.GroupBox4.Controls.Add(Me.cmdProductCode)
        Me.GroupBox4.Controls.Add(Me.cmdEventCapture)
        Me.GroupBox4.Controls.Add(Me.cmdTrMode)
        Me.GroupBox4.Controls.Add(Me.cmdBellInfo)
        Me.GroupBox4.Controls.Add(Me.cmdLockCtl)
        Me.GroupBox4.Controls.Add(Me.cmdSystemInfo)
        Me.GroupBox4.Controls.Add(Me.cmdLogData)
        Me.GroupBox4.Controls.Add(Me.cmdEnrollData)
        Me.GroupBox4.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.GroupBox4.ForeColor = System.Drawing.Color.Black
        Me.GroupBox4.Location = New System.Drawing.Point(259, 171)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(322, 343)
        Me.GroupBox4.TabIndex = 14
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Management Group"
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(26, 298)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(279, 33)
        Me.cmdExit.TabIndex = 24
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdScreenSaver
        '
        Me.cmdScreenSaver.BackColor = System.Drawing.SystemColors.Control
        Me.cmdScreenSaver.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdScreenSaver.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdScreenSaver.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdScreenSaver.Location = New System.Drawing.Point(24, 238)
        Me.cmdScreenSaver.Name = "cmdScreenSaver"
        Me.cmdScreenSaver.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdScreenSaver.Size = New System.Drawing.Size(131, 33)
        Me.cmdScreenSaver.TabIndex = 22
        Me.cmdScreenSaver.Text = "Screen Saver"
        Me.cmdScreenSaver.UseVisualStyleBackColor = False
        '
        'cmdDaigong
        '
        Me.cmdDaigong.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDaigong.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDaigong.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDaigong.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDaigong.Location = New System.Drawing.Point(161, 203)
        Me.cmdDaigong.Name = "cmdDaigong"
        Me.cmdDaigong.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDaigong.Size = New System.Drawing.Size(141, 33)
        Me.cmdDaigong.TabIndex = 21
        Me.cmdDaigong.Text = "Daigong"
        Me.cmdDaigong.UseVisualStyleBackColor = False
        '
        'cmdDepartment
        '
        Me.cmdDepartment.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDepartment.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDepartment.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDepartment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDepartment.Location = New System.Drawing.Point(23, 203)
        Me.cmdDepartment.Name = "cmdDepartment"
        Me.cmdDepartment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDepartment.Size = New System.Drawing.Size(132, 33)
        Me.cmdDepartment.TabIndex = 20
        Me.cmdDepartment.Text = "Department"
        Me.cmdDepartment.UseVisualStyleBackColor = False
        '
        'cmdAccessTz
        '
        Me.cmdAccessTz.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAccessTz.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAccessTz.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAccessTz.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAccessTz.Location = New System.Drawing.Point(161, 169)
        Me.cmdAccessTz.Name = "cmdAccessTz"
        Me.cmdAccessTz.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAccessTz.Size = New System.Drawing.Size(141, 33)
        Me.cmdAccessTz.TabIndex = 19
        Me.cmdAccessTz.Text = "Access TZone"
        Me.cmdAccessTz.UseVisualStyleBackColor = False
        '
        'cmdProductCode
        '
        Me.cmdProductCode.BackColor = System.Drawing.SystemColors.Control
        Me.cmdProductCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdProductCode.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdProductCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdProductCode.Location = New System.Drawing.Point(23, 169)
        Me.cmdProductCode.Name = "cmdProductCode"
        Me.cmdProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdProductCode.Size = New System.Drawing.Size(132, 33)
        Me.cmdProductCode.TabIndex = 18
        Me.cmdProductCode.Text = "Get SN"
        Me.cmdProductCode.UseVisualStyleBackColor = False
        '
        'cmdEventCapture
        '
        Me.cmdEventCapture.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEventCapture.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEventCapture.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEventCapture.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEventCapture.Location = New System.Drawing.Point(161, 238)
        Me.cmdEventCapture.Name = "cmdEventCapture"
        Me.cmdEventCapture.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEventCapture.Size = New System.Drawing.Size(141, 33)
        Me.cmdEventCapture.TabIndex = 13
        Me.cmdEventCapture.Text = "Event Monitor"
        Me.cmdEventCapture.UseVisualStyleBackColor = False
        '
        'cmdTrMode
        '
        Me.cmdTrMode.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTrMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTrMode.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTrMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTrMode.Location = New System.Drawing.Point(161, 135)
        Me.cmdTrMode.Name = "cmdTrMode"
        Me.cmdTrMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTrMode.Size = New System.Drawing.Size(141, 33)
        Me.cmdTrMode.TabIndex = 17
        Me.cmdTrMode.Text = "Log TZone"
        Me.cmdTrMode.UseVisualStyleBackColor = False
        '
        'cmdBellInfo
        '
        Me.cmdBellInfo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBellInfo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBellInfo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBellInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBellInfo.Location = New System.Drawing.Point(23, 135)
        Me.cmdBellInfo.Name = "cmdBellInfo"
        Me.cmdBellInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBellInfo.Size = New System.Drawing.Size(132, 33)
        Me.cmdBellInfo.TabIndex = 16
        Me.cmdBellInfo.Text = "Bell Time"
        Me.cmdBellInfo.UseVisualStyleBackColor = False
        '
        'cmdLockCtl
        '
        Me.cmdLockCtl.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLockCtl.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLockCtl.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLockCtl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLockCtl.Location = New System.Drawing.Point(161, 101)
        Me.cmdLockCtl.Name = "cmdLockCtl"
        Me.cmdLockCtl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLockCtl.Size = New System.Drawing.Size(141, 33)
        Me.cmdLockCtl.TabIndex = 15
        Me.cmdLockCtl.Text = "Lock Control"
        Me.cmdLockCtl.UseVisualStyleBackColor = False
        '
        'cmdSystemInfo
        '
        Me.cmdSystemInfo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSystemInfo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSystemInfo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSystemInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSystemInfo.Location = New System.Drawing.Point(23, 101)
        Me.cmdSystemInfo.Name = "cmdSystemInfo"
        Me.cmdSystemInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSystemInfo.Size = New System.Drawing.Size(132, 33)
        Me.cmdSystemInfo.TabIndex = 14
        Me.cmdSystemInfo.Text = "System info"
        Me.cmdSystemInfo.UseVisualStyleBackColor = False
        '
        'cmdLogData
        '
        Me.cmdLogData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLogData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLogData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLogData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLogData.Location = New System.Drawing.Point(23, 67)
        Me.cmdLogData.Name = "cmdLogData"
        Me.cmdLogData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLogData.Size = New System.Drawing.Size(279, 33)
        Me.cmdLogData.TabIndex = 12
        Me.cmdLogData.Text = "Log Data Management"
        Me.cmdLogData.UseVisualStyleBackColor = False
        '
        'cmdEnrollData
        '
        Me.cmdEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnrollData.Location = New System.Drawing.Point(23, 34)
        Me.cmdEnrollData.Name = "cmdEnrollData"
        Me.cmdEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnrollData.Size = New System.Drawing.Size(279, 33)
        Me.cmdEnrollData.TabIndex = 11
        Me.cmdEnrollData.Text = "Enroll Data Management"
        Me.cmdEnrollData.UseVisualStyleBackColor = False
        '
        'optUSBDevice
        '
        Me.optUSBDevice.AutoSize = True
        Me.optUSBDevice.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.optUSBDevice.Location = New System.Drawing.Point(19, 479)
        Me.optUSBDevice.Name = "optUSBDevice"
        Me.optUSBDevice.Size = New System.Drawing.Size(109, 23)
        Me.optUSBDevice.TabIndex = 7
        Me.optUSBDevice.Text = "USB Device"
        Me.optUSBDevice.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.ClientSize = New System.Drawing.Size(594, 558)
        Me.Controls.Add(Me.optUSBDevice)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Name = "frmMain"
        Me.Text = "SBXPC Sample"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbMachineNumber As System.Windows.Forms.ComboBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbComPort As System.Windows.Forms.ComboBox
    Friend WithEvents lblComPort As System.Windows.Forms.Label
    Friend WithEvents cmbBaudrate As System.Windows.Forms.ComboBox
    Friend WithEvents lblBaudrate As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblPortNo As System.Windows.Forms.Label
    Friend WithEvents lblIPAddress As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPortNo As System.Windows.Forms.TextBox
    Friend WithEvents txtIPAddress As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Public WithEvents cmdEnrollData As System.Windows.Forms.Button
    Public WithEvents cmdLockCtl As System.Windows.Forms.Button
    Public WithEvents cmdSystemInfo As System.Windows.Forms.Button
    Public WithEvents cmdEventCapture As System.Windows.Forms.Button
    Public WithEvents cmdLogData As System.Windows.Forms.Button
    Public WithEvents cmdScreenSaver As System.Windows.Forms.Button
    Public WithEvents cmdDaigong As System.Windows.Forms.Button
    Public WithEvents cmdDepartment As System.Windows.Forms.Button
    Public WithEvents cmdAccessTz As System.Windows.Forms.Button
    Public WithEvents cmdProductCode As System.Windows.Forms.Button
    Public WithEvents cmdTrMode As System.Windows.Forms.Button
    Public WithEvents cmdBellInfo As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents optSerialDevice As System.Windows.Forms.RadioButton
    Friend WithEvents optNetworkDevice As System.Windows.Forms.RadioButton
    Friend WithEvents optUSBDevice As System.Windows.Forms.RadioButton
    Friend WithEvents txtDevId As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
#End Region
End Class