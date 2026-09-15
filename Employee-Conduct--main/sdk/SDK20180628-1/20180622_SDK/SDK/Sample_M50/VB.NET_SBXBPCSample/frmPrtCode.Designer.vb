<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSerialNo
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
	Public WithEvents txtProductCode As System.Windows.Forms.TextBox
	Public WithEvents cmdGetProductCode As System.Windows.Forms.Button
	Public WithEvents cmdGetBackupNumber As System.Windows.Forms.Button
	Public WithEvents txtBackupNo As System.Windows.Forms.TextBox
	Public WithEvents txtSerialNo As System.Windows.Forms.TextBox
	Public WithEvents cmdExit As System.Windows.Forms.Button
	Public WithEvents cmdGetSerialNumber As System.Windows.Forms.Button
	Public WithEvents lblBackuplNo As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lblSerialNo As System.Windows.Forms.Label
	Public WithEvents lblMessage As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSerialNo))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtProductCode = New System.Windows.Forms.TextBox
        Me.cmdGetProductCode = New System.Windows.Forms.Button
        Me.cmdGetBackupNumber = New System.Windows.Forms.Button
        Me.txtBackupNo = New System.Windows.Forms.TextBox
        Me.txtSerialNo = New System.Windows.Forms.TextBox
        Me.cmdExit = New System.Windows.Forms.Button
        Me.cmdGetSerialNumber = New System.Windows.Forms.Button
        Me.lblBackuplNo = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblSerialNo = New System.Windows.Forms.Label
        Me.lblMessage = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtProductCode
        '
        Me.txtProductCode.AcceptsReturn = True
        Me.txtProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductCode.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductCode.Location = New System.Drawing.Point(171, 145)
        Me.txtProductCode.MaxLength = 32
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductCode.Size = New System.Drawing.Size(319, 26)
        Me.txtProductCode.TabIndex = 6
        '
        'cmdGetProductCode
        '
        Me.cmdGetProductCode.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetProductCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetProductCode.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetProductCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetProductCode.Location = New System.Drawing.Point(269, 202)
        Me.cmdGetProductCode.Name = "cmdGetProductCode"
        Me.cmdGetProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetProductCode.Size = New System.Drawing.Size(121, 45)
        Me.cmdGetProductCode.TabIndex = 5
        Me.cmdGetProductCode.Text = "Get ProductCode"
        Me.cmdGetProductCode.UseVisualStyleBackColor = False
        '
        'cmdGetBackupNumber
        '
        Me.cmdGetBackupNumber.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetBackupNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetBackupNumber.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetBackupNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetBackupNumber.Location = New System.Drawing.Point(142, 202)
        Me.cmdGetBackupNumber.Name = "cmdGetBackupNumber"
        Me.cmdGetBackupNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetBackupNumber.Size = New System.Drawing.Size(121, 45)
        Me.cmdGetBackupNumber.TabIndex = 4
        Me.cmdGetBackupNumber.Text = "Get BackupNumber"
        Me.cmdGetBackupNumber.UseVisualStyleBackColor = False
        '
        'txtBackupNo
        '
        Me.txtBackupNo.AcceptsReturn = True
        Me.txtBackupNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBackupNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBackupNo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackupNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBackupNo.Location = New System.Drawing.Point(171, 103)
        Me.txtBackupNo.MaxLength = 32
        Me.txtBackupNo.Name = "txtBackupNo"
        Me.txtBackupNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBackupNo.Size = New System.Drawing.Size(319, 26)
        Me.txtBackupNo.TabIndex = 3
        '
        'txtSerialNo
        '
        Me.txtSerialNo.AcceptsReturn = True
        Me.txtSerialNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSerialNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSerialNo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSerialNo.Location = New System.Drawing.Point(171, 63)
        Me.txtSerialNo.MaxLength = 32
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSerialNo.Size = New System.Drawing.Size(319, 26)
        Me.txtSerialNo.TabIndex = 2
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(396, 202)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(121, 45)
        Me.cmdExit.TabIndex = 1
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdGetSerialNumber
        '
        Me.cmdGetSerialNumber.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetSerialNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetSerialNumber.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetSerialNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetSerialNumber.Location = New System.Drawing.Point(15, 202)
        Me.cmdGetSerialNumber.Name = "cmdGetSerialNumber"
        Me.cmdGetSerialNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetSerialNumber.Size = New System.Drawing.Size(121, 45)
        Me.cmdGetSerialNumber.TabIndex = 0
        Me.cmdGetSerialNumber.Text = "Get SerialNumber"
        Me.cmdGetSerialNumber.UseVisualStyleBackColor = False
        '
        'lblBackuplNo
        '
        Me.lblBackuplNo.AutoSize = True
        Me.lblBackuplNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblBackuplNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBackuplNo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackuplNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBackuplNo.Location = New System.Drawing.Point(34, 107)
        Me.lblBackuplNo.Name = "lblBackuplNo"
        Me.lblBackuplNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBackuplNo.Size = New System.Drawing.Size(127, 19)
        Me.lblBackuplNo.TabIndex = 10
        Me.lblBackuplNo.Text = "Backup Number :"
        Me.lblBackuplNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(34, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(108, 19)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Product Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSerialNo
        '
        Me.lblSerialNo.AutoSize = True
        Me.lblSerialNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblSerialNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSerialNo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerialNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSerialNo.Location = New System.Drawing.Point(34, 67)
        Me.lblSerialNo.Name = "lblSerialNo"
        Me.lblSerialNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSerialNo.Size = New System.Drawing.Size(115, 19)
        Me.lblSerialNo.TabIndex = 8
        Me.lblSerialNo.Text = "Serial Number :"
        Me.lblSerialNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMessage.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(18, 23)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMessage.Size = New System.Drawing.Size(495, 28)
        Me.lblMessage.TabIndex = 7
        Me.lblMessage.Text = "Message"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmSerialNo
        '
        Me.AcceptButton = Me.cmdExit
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(531, 274)
        Me.Controls.Add(Me.txtProductCode)
        Me.Controls.Add(Me.cmdGetProductCode)
        Me.Controls.Add(Me.cmdGetBackupNumber)
        Me.Controls.Add(Me.txtBackupNo)
        Me.Controls.Add(Me.txtSerialNo)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdGetSerialNumber)
        Me.Controls.Add(Me.lblBackuplNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSerialNo)
        Me.Controls.Add(Me.lblMessage)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(334, 208)
        Me.Name = "frmSerialNo"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Serial Number"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region 
End Class