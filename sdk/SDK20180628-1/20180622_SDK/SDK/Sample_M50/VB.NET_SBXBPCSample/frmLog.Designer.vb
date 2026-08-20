<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmLog
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
	Public WithEvents chkAndDelete As System.Windows.Forms.CheckBox
	Public WithEvents cmdEmptySLog As System.Windows.Forms.Button
	Public WithEvents cmdEmptyGLog As System.Windows.Forms.Button
	Public WithEvents chkReadMark As System.Windows.Forms.CheckBox
	Public WithEvents cmdAllGLogData As System.Windows.Forms.Button
	Public WithEvents cmdAllSLogData As System.Windows.Forms.Button
	Public WithEvents cmdExit As System.Windows.Forms.Button
	Public WithEvents cmdGlogData As System.Windows.Forms.Button
	Public WithEvents cmdSLogData As System.Windows.Forms.Button
	Public WithEvents gridSLogData As AxMSFlexGridLib.AxMSFlexGrid
	Public WithEvents gridSLogData1 As AxMSFlexGridLib.AxMSFlexGrid
	Public WithEvents gridSLogData2 As AxMSFlexGridLib.AxMSFlexGrid
	Public WithEvents lblEnrollData As System.Windows.Forms.Label
	Public WithEvents LabelTotal As System.Windows.Forms.Label
	Public WithEvents lblMessage As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLog))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkAndDelete = New System.Windows.Forms.CheckBox
        Me.cmdEmptySLog = New System.Windows.Forms.Button
        Me.cmdEmptyGLog = New System.Windows.Forms.Button
        Me.chkReadMark = New System.Windows.Forms.CheckBox
        Me.cmdAllGLogData = New System.Windows.Forms.Button
        Me.cmdAllSLogData = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.cmdGlogData = New System.Windows.Forms.Button
        Me.cmdSLogData = New System.Windows.Forms.Button
        Me.gridSLogData = New AxMSFlexGridLib.AxMSFlexGrid
        Me.gridSLogData1 = New AxMSFlexGridLib.AxMSFlexGrid
        Me.gridSLogData2 = New AxMSFlexGridLib.AxMSFlexGrid
        Me.lblEnrollData = New System.Windows.Forms.Label
        Me.LabelTotal = New System.Windows.Forms.Label
        Me.lblMessage = New System.Windows.Forms.Label
        Me.chkShowGLogPhoto = New System.Windows.Forms.CheckBox
        Me.picGLogPhoto = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtEnd = New System.Windows.Forms.DateTimePicker
        Me.dtStart = New System.Windows.Forms.DateTimePicker
        Me.cmdSetRange = New System.Windows.Forms.Button
        Me.chkUseSearchRange = New System.Windows.Forms.CheckBox
        CType(Me.gridSLogData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridSLogData1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridSLogData2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGLogPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkAndDelete
        '
        Me.chkAndDelete.BackColor = System.Drawing.SystemColors.Control
        Me.chkAndDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAndDelete.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAndDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAndDelete.Location = New System.Drawing.Point(479, 81)
        Me.chkAndDelete.Name = "chkAndDelete"
        Me.chkAndDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAndDelete.Size = New System.Drawing.Size(110, 19)
        Me.chkAndDelete.TabIndex = 14
        Me.chkAndDelete.Text = "and Delete "
        Me.chkAndDelete.UseVisualStyleBackColor = False
        '
        'cmdEmptySLog
        '
        Me.cmdEmptySLog.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEmptySLog.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEmptySLog.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEmptySLog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEmptySLog.Location = New System.Drawing.Point(220, 426)
        Me.cmdEmptySLog.Name = "cmdEmptySLog"
        Me.cmdEmptySLog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEmptySLog.Size = New System.Drawing.Size(94, 43)
        Me.cmdEmptySLog.TabIndex = 11
        Me.cmdEmptySLog.Text = "Empty SLogData"
        Me.cmdEmptySLog.UseVisualStyleBackColor = False
        '
        'cmdEmptyGLog
        '
        Me.cmdEmptyGLog.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEmptyGLog.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEmptyGLog.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEmptyGLog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEmptyGLog.Location = New System.Drawing.Point(513, 426)
        Me.cmdEmptyGLog.Name = "cmdEmptyGLog"
        Me.cmdEmptyGLog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEmptyGLog.Size = New System.Drawing.Size(94, 43)
        Me.cmdEmptyGLog.TabIndex = 10
        Me.cmdEmptyGLog.Text = "Empty GLogData"
        Me.cmdEmptyGLog.UseVisualStyleBackColor = False
        '
        'chkReadMark
        '
        Me.chkReadMark.BackColor = System.Drawing.SystemColors.Control
        Me.chkReadMark.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkReadMark.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReadMark.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkReadMark.Location = New System.Drawing.Point(606, 83)
        Me.chkReadMark.Name = "chkReadMark"
        Me.chkReadMark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkReadMark.Size = New System.Drawing.Size(102, 19)
        Me.chkReadMark.TabIndex = 7
        Me.chkReadMark.Text = "ReadMark"
        Me.chkReadMark.UseVisualStyleBackColor = False
        '
        'cmdAllGLogData
        '
        Me.cmdAllGLogData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAllGLogData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAllGLogData.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAllGLogData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAllGLogData.Location = New System.Drawing.Point(415, 426)
        Me.cmdAllGLogData.Name = "cmdAllGLogData"
        Me.cmdAllGLogData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAllGLogData.Size = New System.Drawing.Size(94, 43)
        Me.cmdAllGLogData.TabIndex = 6
        Me.cmdAllGLogData.Text = "Read All GLogData"
        Me.cmdAllGLogData.UseVisualStyleBackColor = False
        '
        'cmdAllSLogData
        '
        Me.cmdAllSLogData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAllSLogData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAllSLogData.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAllSLogData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAllSLogData.Location = New System.Drawing.Point(122, 426)
        Me.cmdAllSLogData.Name = "cmdAllSLogData"
        Me.cmdAllSLogData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAllSLogData.Size = New System.Drawing.Size(94, 43)
        Me.cmdAllSLogData.TabIndex = 4
        Me.cmdAllSLogData.Text = "Read All SLogData"
        Me.cmdAllSLogData.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(611, 426)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(94, 43)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdGlogData
        '
        Me.cmdGlogData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGlogData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGlogData.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGlogData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGlogData.Location = New System.Drawing.Point(317, 426)
        Me.cmdGlogData.Name = "cmdGlogData"
        Me.cmdGlogData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGlogData.Size = New System.Drawing.Size(94, 43)
        Me.cmdGlogData.TabIndex = 2
        Me.cmdGlogData.Text = "Read GLogData"
        Me.cmdGlogData.UseVisualStyleBackColor = False
        '
        'cmdSLogData
        '
        Me.cmdSLogData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSLogData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSLogData.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSLogData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSLogData.Location = New System.Drawing.Point(24, 426)
        Me.cmdSLogData.Name = "cmdSLogData"
        Me.cmdSLogData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSLogData.Size = New System.Drawing.Size(94, 43)
        Me.cmdSLogData.TabIndex = 1
        Me.cmdSLogData.Text = "Read SLogData"
        Me.cmdSLogData.UseVisualStyleBackColor = False
        '
        'gridSLogData
        '
        Me.gridSLogData.Location = New System.Drawing.Point(24, 104)
        Me.gridSLogData.Name = "gridSLogData"
        Me.gridSLogData.OcxState = CType(resources.GetObject("gridSLogData.OcxState"), System.Windows.Forms.AxHost.State)
        Me.gridSLogData.Size = New System.Drawing.Size(679, 320)
        Me.gridSLogData.TabIndex = 5
        '
        'gridSLogData1
        '
        Me.gridSLogData1.Location = New System.Drawing.Point(24, 211)
        Me.gridSLogData1.Name = "gridSLogData1"
        Me.gridSLogData1.OcxState = CType(resources.GetObject("gridSLogData1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.gridSLogData1.Size = New System.Drawing.Size(679, 214)
        Me.gridSLogData1.TabIndex = 12
        '
        'gridSLogData2
        '
        Me.gridSLogData2.Location = New System.Drawing.Point(24, 318)
        Me.gridSLogData2.Name = "gridSLogData2"
        Me.gridSLogData2.OcxState = CType(resources.GetObject("gridSLogData2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.gridSLogData2.Size = New System.Drawing.Size(679, 107)
        Me.gridSLogData2.TabIndex = 13
        '
        'lblEnrollData
        '
        Me.lblEnrollData.AutoSize = True
        Me.lblEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.lblEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEnrollData.Location = New System.Drawing.Point(27, 86)
        Me.lblEnrollData.Name = "lblEnrollData"
        Me.lblEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEnrollData.Size = New System.Drawing.Size(73, 19)
        Me.lblEnrollData.TabIndex = 9
        Me.lblEnrollData.Text = "Log Data :"
        '
        'LabelTotal
        '
        Me.LabelTotal.AutoSize = True
        Me.LabelTotal.BackColor = System.Drawing.SystemColors.Control
        Me.LabelTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelTotal.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelTotal.Location = New System.Drawing.Point(128, 86)
        Me.LabelTotal.Name = "LabelTotal"
        Me.LabelTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelTotal.Size = New System.Drawing.Size(47, 19)
        Me.LabelTotal.TabIndex = 8
        Me.LabelTotal.Text = "Total :"
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMessage.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(24, 40)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMessage.Size = New System.Drawing.Size(679, 28)
        Me.lblMessage.TabIndex = 0
        Me.lblMessage.Text = "Message"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'chkShowGLogPhoto
        '
        Me.chkShowGLogPhoto.BackColor = System.Drawing.SystemColors.Control
        Me.chkShowGLogPhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShowGLogPhoto.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowGLogPhoto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShowGLogPhoto.Location = New System.Drawing.Point(723, 81)
        Me.chkShowGLogPhoto.Name = "chkShowGLogPhoto"
        Me.chkShowGLogPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShowGLogPhoto.Size = New System.Drawing.Size(155, 24)
        Me.chkShowGLogPhoto.TabIndex = 15
        Me.chkShowGLogPhoto.Text = "Show GLog Photo"
        Me.chkShowGLogPhoto.UseVisualStyleBackColor = False
        '
        'picGLogPhoto
        '
        Me.picGLogPhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picGLogPhoto.Location = New System.Drawing.Point(719, 115)
        Me.picGLogPhoto.Name = "picGLogPhoto"
        Me.picGLogPhoto.Size = New System.Drawing.Size(159, 136)
        Me.picGLogPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picGLogPhoto.TabIndex = 16
        Me.picGLogPhoto.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtEnd)
        Me.GroupBox1.Controls.Add(Me.dtStart)
        Me.GroupBox1.Controls.Add(Me.cmdSetRange)
        Me.GroupBox1.Controls.Add(Me.chkUseSearchRange)
        Me.GroupBox1.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(709, 275)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(190, 184)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search Range"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(10, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(36, 19)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "End:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(10, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(41, 19)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Start:"
        '
        'dtEnd
        '
        Me.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEnd.Location = New System.Drawing.Point(72, 107)
        Me.dtEnd.Name = "dtEnd"
        Me.dtEnd.Size = New System.Drawing.Size(112, 26)
        Me.dtEnd.TabIndex = 20
        '
        'dtStart
        '
        Me.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStart.Location = New System.Drawing.Point(72, 75)
        Me.dtStart.Name = "dtStart"
        Me.dtStart.Size = New System.Drawing.Size(112, 26)
        Me.dtStart.TabIndex = 19
        '
        'cmdSetRange
        '
        Me.cmdSetRange.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetRange.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetRange.Location = New System.Drawing.Point(50, 144)
        Me.cmdSetRange.Name = "cmdSetRange"
        Me.cmdSetRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetRange.Size = New System.Drawing.Size(94, 25)
        Me.cmdSetRange.TabIndex = 18
        Me.cmdSetRange.Text = "Set Range"
        Me.cmdSetRange.UseVisualStyleBackColor = False
        '
        'chkUseSearchRange
        '
        Me.chkUseSearchRange.BackColor = System.Drawing.SystemColors.Control
        Me.chkUseSearchRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkUseSearchRange.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseSearchRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkUseSearchRange.Location = New System.Drawing.Point(14, 34)
        Me.chkUseSearchRange.Name = "chkUseSearchRange"
        Me.chkUseSearchRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkUseSearchRange.Size = New System.Drawing.Size(157, 24)
        Me.chkUseSearchRange.TabIndex = 18
        Me.chkUseSearchRange.Text = "Use Search Range"
        Me.chkUseSearchRange.UseVisualStyleBackColor = False
        '
        'frmLog
        '
        Me.AcceptButton = Me.cmdExit
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(911, 482)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.picGLogPhoto)
        Me.Controls.Add(Me.chkShowGLogPhoto)
        Me.Controls.Add(Me.chkAndDelete)
        Me.Controls.Add(Me.cmdEmptySLog)
        Me.Controls.Add(Me.cmdEmptyGLog)
        Me.Controls.Add(Me.chkReadMark)
        Me.Controls.Add(Me.cmdAllGLogData)
        Me.Controls.Add(Me.cmdAllSLogData)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdGlogData)
        Me.Controls.Add(Me.cmdSLogData)
        Me.Controls.Add(Me.gridSLogData)
        Me.Controls.Add(Me.gridSLogData1)
        Me.Controls.Add(Me.gridSLogData2)
        Me.Controls.Add(Me.lblEnrollData)
        Me.Controls.Add(Me.LabelTotal)
        Me.Controls.Add(Me.lblMessage)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(321, 209)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLog"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage Log Data"
        CType(Me.gridSLogData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridSLogData1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridSLogData2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGLogPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents chkShowGLogPhoto As System.Windows.Forms.CheckBox
    Friend WithEvents picGLogPhoto As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents chkUseSearchRange As System.Windows.Forms.CheckBox
    Public WithEvents cmdSetRange As System.Windows.Forms.Button
    Friend WithEvents dtEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtStart As System.Windows.Forms.DateTimePicker
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region 
End Class