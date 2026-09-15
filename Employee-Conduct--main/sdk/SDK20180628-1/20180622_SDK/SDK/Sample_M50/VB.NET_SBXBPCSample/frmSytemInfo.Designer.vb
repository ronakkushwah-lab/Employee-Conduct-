<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSystemInfo
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
	Public WithEvents cmdSetDeviceTime As System.Windows.Forms.Button
	Public WithEvents cmdGetDeviceTime As System.Windows.Forms.Button
	Public WithEvents cmdGetDeviceInfo As System.Windows.Forms.Button
	Public WithEvents cmdExit As System.Windows.Forms.Button
	Public WithEvents cmdPowerOn As System.Windows.Forms.Button
	Public WithEvents PowerOffDevice As System.Windows.Forms.Button
	Public WithEvents cmdSetDeviceInfo As System.Windows.Forms.Button
	Public WithEvents cmdEnableDevice As System.Windows.Forms.Button
	Public WithEvents cmbSatus As System.Windows.Forms.ComboBox
	Public WithEvents txtSetDevInfo As System.Windows.Forms.TextBox
	Public WithEvents cmdGetDeviceStaus As System.Windows.Forms.Button
	Public WithEvents chkEnableDevice As System.Windows.Forms.CheckBox
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lblStatus As System.Windows.Forms.Label
	Public WithEvents lblMessage As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSystemInfo))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSetDeviceTime = New System.Windows.Forms.Button
        Me.cmdGetDeviceTime = New System.Windows.Forms.Button
        Me.cmdGetDeviceInfo = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.cmdPowerOn = New System.Windows.Forms.Button
        Me.PowerOffDevice = New System.Windows.Forms.Button
        Me.cmdSetDeviceInfo = New System.Windows.Forms.Button
        Me.cmdEnableDevice = New System.Windows.Forms.Button
        Me.cmbSatus = New System.Windows.Forms.ComboBox
        Me.txtSetDevInfo = New System.Windows.Forms.TextBox
        Me.cmdGetDeviceStaus = New System.Windows.Forms.Button
        Me.chkEnableDevice = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblStatus = New System.Windows.Forms.Label
        Me.lblMessage = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdSetDeviceTime
        '
        Me.cmdSetDeviceTime.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetDeviceTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetDeviceTime.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetDeviceTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetDeviceTime.Location = New System.Drawing.Point(20, 120)
        Me.cmdSetDeviceTime.Name = "cmdSetDeviceTime"
        Me.cmdSetDeviceTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetDeviceTime.Size = New System.Drawing.Size(125, 32)
        Me.cmdSetDeviceTime.TabIndex = 14
        Me.cmdSetDeviceTime.Text = "SetDeviceTime"
        Me.cmdSetDeviceTime.UseVisualStyleBackColor = False
        '
        'cmdGetDeviceTime
        '
        Me.cmdGetDeviceTime.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetDeviceTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetDeviceTime.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetDeviceTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetDeviceTime.Location = New System.Drawing.Point(20, 78)
        Me.cmdGetDeviceTime.Name = "cmdGetDeviceTime"
        Me.cmdGetDeviceTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetDeviceTime.Size = New System.Drawing.Size(125, 32)
        Me.cmdGetDeviceTime.TabIndex = 13
        Me.cmdGetDeviceTime.Text = "GetDeviceTime"
        Me.cmdGetDeviceTime.UseVisualStyleBackColor = False
        '
        'cmdGetDeviceInfo
        '
        Me.cmdGetDeviceInfo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetDeviceInfo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetDeviceInfo.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetDeviceInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetDeviceInfo.Location = New System.Drawing.Point(20, 221)
        Me.cmdGetDeviceInfo.Name = "cmdGetDeviceInfo"
        Me.cmdGetDeviceInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetDeviceInfo.Size = New System.Drawing.Size(125, 32)
        Me.cmdGetDeviceInfo.TabIndex = 12
        Me.cmdGetDeviceInfo.Text = "GetDeviceInfo"
        Me.cmdGetDeviceInfo.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(311, 120)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(125, 32)
        Me.cmdExit.TabIndex = 11
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdPowerOn
        '
        Me.cmdPowerOn.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPowerOn.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPowerOn.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPowerOn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPowerOn.Location = New System.Drawing.Point(162, 78)
        Me.cmdPowerOn.Name = "cmdPowerOn"
        Me.cmdPowerOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPowerOn.Size = New System.Drawing.Size(125, 32)
        Me.cmdPowerOn.TabIndex = 10
        Me.cmdPowerOn.Text = "PowerOnDevice"
        Me.cmdPowerOn.UseVisualStyleBackColor = False
        '
        'PowerOffDevice
        '
        Me.PowerOffDevice.BackColor = System.Drawing.SystemColors.Control
        Me.PowerOffDevice.Cursor = System.Windows.Forms.Cursors.Default
        Me.PowerOffDevice.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PowerOffDevice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PowerOffDevice.Location = New System.Drawing.Point(162, 120)
        Me.PowerOffDevice.Name = "PowerOffDevice"
        Me.PowerOffDevice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PowerOffDevice.Size = New System.Drawing.Size(125, 32)
        Me.PowerOffDevice.TabIndex = 9
        Me.PowerOffDevice.Text = "PowerOffDevice"
        Me.PowerOffDevice.UseVisualStyleBackColor = False
        '
        'cmdSetDeviceInfo
        '
        Me.cmdSetDeviceInfo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetDeviceInfo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetDeviceInfo.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetDeviceInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetDeviceInfo.Location = New System.Drawing.Point(162, 221)
        Me.cmdSetDeviceInfo.Name = "cmdSetDeviceInfo"
        Me.cmdSetDeviceInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetDeviceInfo.Size = New System.Drawing.Size(125, 32)
        Me.cmdSetDeviceInfo.TabIndex = 8
        Me.cmdSetDeviceInfo.Text = "SetDeviceInfo"
        Me.cmdSetDeviceInfo.UseVisualStyleBackColor = False
        '
        'cmdEnableDevice
        '
        Me.cmdEnableDevice.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnableDevice.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnableDevice.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEnableDevice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnableDevice.Location = New System.Drawing.Point(311, 79)
        Me.cmdEnableDevice.Name = "cmdEnableDevice"
        Me.cmdEnableDevice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnableDevice.Size = New System.Drawing.Size(125, 32)
        Me.cmdEnableDevice.TabIndex = 7
        Me.cmdEnableDevice.Text = "DisableDevice"
        Me.cmdEnableDevice.UseVisualStyleBackColor = False
        '
        'cmbSatus
        '
        Me.cmbSatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbSatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSatus.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSatus.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"})
        Me.cmbSatus.Location = New System.Drawing.Point(156, 176)
        Me.cmbSatus.Name = "cmbSatus"
        Me.cmbSatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbSatus.Size = New System.Drawing.Size(88, 27)
        Me.cmbSatus.TabIndex = 5
        '
        'txtSetDevInfo
        '
        Me.txtSetDevInfo.AcceptsReturn = True
        Me.txtSetDevInfo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSetDevInfo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSetDevInfo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSetDevInfo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSetDevInfo.Location = New System.Drawing.Point(368, 173)
        Me.txtSetDevInfo.MaxLength = 0
        Me.txtSetDevInfo.Name = "txtSetDevInfo"
        Me.txtSetDevInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSetDevInfo.Size = New System.Drawing.Size(59, 26)
        Me.txtSetDevInfo.TabIndex = 4
        '
        'cmdGetDeviceStaus
        '
        Me.cmdGetDeviceStaus.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetDeviceStaus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetDeviceStaus.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetDeviceStaus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetDeviceStaus.Location = New System.Drawing.Point(311, 221)
        Me.cmdGetDeviceStaus.Name = "cmdGetDeviceStaus"
        Me.cmdGetDeviceStaus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetDeviceStaus.Size = New System.Drawing.Size(125, 32)
        Me.cmdGetDeviceStaus.TabIndex = 2
        Me.cmdGetDeviceStaus.Text = "GetDeviceStatus"
        Me.cmdGetDeviceStaus.UseVisualStyleBackColor = False
        '
        'chkEnableDevice
        '
        Me.chkEnableDevice.BackColor = System.Drawing.SystemColors.Control
        Me.chkEnableDevice.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkEnableDevice.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnableDevice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEnableDevice.Location = New System.Drawing.Point(294, 84)
        Me.chkEnableDevice.Name = "chkEnableDevice"
        Me.chkEnableDevice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkEnableDevice.Size = New System.Drawing.Size(15, 23)
        Me.chkEnableDevice.TabIndex = 1
        Me.chkEnableDevice.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(270, 178)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(97, 23)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Status Value:"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatus.Location = New System.Drawing.Point(28, 169)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.Size = New System.Drawing.Size(137, 41)
        Me.lblStatus.TabIndex = 3
        Me.lblStatus.Text = "Status Paramerter:  Info Paramerter:"
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMessage.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(23, 27)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMessage.Size = New System.Drawing.Size(414, 28)
        Me.lblMessage.TabIndex = 0
        Me.lblMessage.Text = "Message"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmSystemInfo
        '
        Me.AcceptButton = Me.cmdExit
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(457, 271)
        Me.Controls.Add(Me.cmdSetDeviceTime)
        Me.Controls.Add(Me.cmdGetDeviceTime)
        Me.Controls.Add(Me.cmdGetDeviceInfo)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdPowerOn)
        Me.Controls.Add(Me.PowerOffDevice)
        Me.Controls.Add(Me.cmdSetDeviceInfo)
        Me.Controls.Add(Me.cmdEnableDevice)
        Me.Controls.Add(Me.cmbSatus)
        Me.Controls.Add(Me.txtSetDevInfo)
        Me.Controls.Add(Me.cmdGetDeviceStaus)
        Me.Controls.Add(Me.chkEnableDevice)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblMessage)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(333, 207)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSystemInfo"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage System Info"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region 
End Class