<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScreenSaver
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdDebugOutFileBrowse = New System.Windows.Forms.Button
        Me.txtDebugOutFile = New System.Windows.Forms.TextBox
        Me.chkDebugOut = New System.Windows.Forms.CheckBox
        Me.cmdExit = New System.Windows.Forms.Button
        Me.cmdSetSleepMsg = New System.Windows.Forms.Button
        Me.cmdGetSleepMsg = New System.Windows.Forms.Button
        Me.chkStrikeOut = New System.Windows.Forms.CheckBox
        Me.chkUnderline = New System.Windows.Forms.CheckBox
        Me.chkItalic = New System.Windows.Forms.CheckBox
        Me.cmdGetGlyphSize = New System.Windows.Forms.Button
        Me.txtFontWeight = New System.Windows.Forms.TextBox
        Me.label7 = New System.Windows.Forms.Label
        Me.txtFontWidth = New System.Windows.Forms.TextBox
        Me.label6 = New System.Windows.Forms.Label
        Me.txtFontHeight = New System.Windows.Forms.TextBox
        Me.label5 = New System.Windows.Forms.Label
        Me.txtGlyphHeight = New System.Windows.Forms.TextBox
        Me.label4 = New System.Windows.Forms.Label
        Me.txtGlyphWidth = New System.Windows.Forms.TextBox
        Me.OpenSaveDlg = New System.Windows.Forms.SaveFileDialog
        Me.label3 = New System.Windows.Forms.Label
        Me.txtSleepMessage = New System.Windows.Forms.TextBox
        Me.cmdSetCustomerInfo = New System.Windows.Forms.Button
        Me.cmdGetCustomerInfo = New System.Windows.Forms.Button
        Me.txtCustomerName = New System.Windows.Forms.TextBox
        Me.txtCompanyName = New System.Windows.Forms.TextBox
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.lblEnrollData = New System.Windows.Forms.Label
        Me.lblMessage = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdDebugOutFileBrowse
        '
        Me.cmdDebugOutFileBrowse.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDebugOutFileBrowse.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDebugOutFileBrowse.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDebugOutFileBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDebugOutFileBrowse.Location = New System.Drawing.Point(514, 496)
        Me.cmdDebugOutFileBrowse.Name = "cmdDebugOutFileBrowse"
        Me.cmdDebugOutFileBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDebugOutFileBrowse.Size = New System.Drawing.Size(30, 27)
        Me.cmdDebugOutFileBrowse.TabIndex = 106
        Me.cmdDebugOutFileBrowse.Text = "..."
        Me.cmdDebugOutFileBrowse.UseVisualStyleBackColor = False
        '
        'txtDebugOutFile
        '
        Me.txtDebugOutFile.AcceptsReturn = True
        Me.txtDebugOutFile.BackColor = System.Drawing.SystemColors.Window
        Me.txtDebugOutFile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDebugOutFile.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDebugOutFile.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDebugOutFile.Location = New System.Drawing.Point(159, 497)
        Me.txtDebugOutFile.MaxLength = 8
        Me.txtDebugOutFile.Name = "txtDebugOutFile"
        Me.txtDebugOutFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDebugOutFile.Size = New System.Drawing.Size(355, 26)
        Me.txtDebugOutFile.TabIndex = 105
        Me.txtDebugOutFile.Text = "C:\temp.bmp"
        '
        'chkDebugOut
        '
        Me.chkDebugOut.AutoSize = True
        Me.chkDebugOut.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkDebugOut.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.chkDebugOut.Location = New System.Drawing.Point(52, 497)
        Me.chkDebugOut.Name = "chkDebugOut"
        Me.chkDebugOut.Size = New System.Drawing.Size(97, 24)
        Me.chkDebugOut.TabIndex = 104
        Me.chkDebugOut.Text = "DebugOut"
        Me.chkDebugOut.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(458, 547)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 30)
        Me.cmdExit.TabIndex = 103
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdSetSleepMsg
        '
        Me.cmdSetSleepMsg.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetSleepMsg.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetSleepMsg.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetSleepMsg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetSleepMsg.Location = New System.Drawing.Point(243, 547)
        Me.cmdSetSleepMsg.Name = "cmdSetSleepMsg"
        Me.cmdSetSleepMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetSleepMsg.Size = New System.Drawing.Size(200, 30)
        Me.cmdSetSleepMsg.TabIndex = 102
        Me.cmdSetSleepMsg.Text = "Set Sleep Message"
        Me.cmdSetSleepMsg.UseVisualStyleBackColor = False
        '
        'cmdGetSleepMsg
        '
        Me.cmdGetSleepMsg.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetSleepMsg.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetSleepMsg.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetSleepMsg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetSleepMsg.Location = New System.Drawing.Point(37, 547)
        Me.cmdGetSleepMsg.Name = "cmdGetSleepMsg"
        Me.cmdGetSleepMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetSleepMsg.Size = New System.Drawing.Size(200, 30)
        Me.cmdGetSleepMsg.TabIndex = 101
        Me.cmdGetSleepMsg.Text = "Get Sleep Message"
        Me.cmdGetSleepMsg.UseVisualStyleBackColor = False
        '
        'chkStrikeOut
        '
        Me.chkStrikeOut.AutoSize = True
        Me.chkStrikeOut.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkStrikeOut.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.chkStrikeOut.Location = New System.Drawing.Point(380, 441)
        Me.chkStrikeOut.Name = "chkStrikeOut"
        Me.chkStrikeOut.Size = New System.Drawing.Size(93, 24)
        Me.chkStrikeOut.TabIndex = 100
        Me.chkStrikeOut.Text = "StrikeOut"
        Me.chkStrikeOut.UseVisualStyleBackColor = True
        '
        'chkUnderline
        '
        Me.chkUnderline.AutoSize = True
        Me.chkUnderline.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkUnderline.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.chkUnderline.Location = New System.Drawing.Point(380, 408)
        Me.chkUnderline.Name = "chkUnderline"
        Me.chkUnderline.Size = New System.Drawing.Size(92, 24)
        Me.chkUnderline.TabIndex = 99
        Me.chkUnderline.Text = "Underline"
        Me.chkUnderline.UseVisualStyleBackColor = True
        '
        'chkItalic
        '
        Me.chkItalic.AutoSize = True
        Me.chkItalic.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkItalic.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.chkItalic.Location = New System.Drawing.Point(380, 376)
        Me.chkItalic.Name = "chkItalic"
        Me.chkItalic.Size = New System.Drawing.Size(63, 24)
        Me.chkItalic.TabIndex = 98
        Me.chkItalic.Text = "Italic"
        Me.chkItalic.UseVisualStyleBackColor = True
        '
        'cmdGetGlyphSize
        '
        Me.cmdGetGlyphSize.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetGlyphSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetGlyphSize.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetGlyphSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetGlyphSize.Location = New System.Drawing.Point(351, 329)
        Me.cmdGetGlyphSize.Name = "cmdGetGlyphSize"
        Me.cmdGetGlyphSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetGlyphSize.Size = New System.Drawing.Size(200, 30)
        Me.cmdGetGlyphSize.TabIndex = 97
        Me.cmdGetGlyphSize.Text = "Get Glyph Size"
        Me.cmdGetGlyphSize.UseVisualStyleBackColor = False
        '
        'txtFontWeight
        '
        Me.txtFontWeight.AcceptsReturn = True
        Me.txtFontWeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtFontWeight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFontWeight.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFontWeight.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFontWeight.Location = New System.Drawing.Point(144, 446)
        Me.txtFontWeight.MaxLength = 8
        Me.txtFontWeight.Name = "txtFontWeight"
        Me.txtFontWeight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFontWeight.Size = New System.Drawing.Size(133, 26)
        Me.txtFontWeight.TabIndex = 96
        Me.txtFontWeight.Text = "700"
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.BackColor = System.Drawing.SystemColors.Control
        Me.label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.label7.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label7.Location = New System.Drawing.Point(26, 449)
        Me.label7.Name = "label7"
        Me.label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label7.Size = New System.Drawing.Size(87, 19)
        Me.label7.TabIndex = 95
        Me.label7.Text = "Font Weight:"
        '
        'txtFontWidth
        '
        Me.txtFontWidth.AcceptsReturn = True
        Me.txtFontWidth.BackColor = System.Drawing.SystemColors.Window
        Me.txtFontWidth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFontWidth.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFontWidth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFontWidth.Location = New System.Drawing.Point(144, 413)
        Me.txtFontWidth.MaxLength = 8
        Me.txtFontWidth.Name = "txtFontWidth"
        Me.txtFontWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFontWidth.Size = New System.Drawing.Size(133, 26)
        Me.txtFontWidth.TabIndex = 94
        Me.txtFontWidth.Text = "11"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.BackColor = System.Drawing.SystemColors.Control
        Me.label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.label6.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label6.Location = New System.Drawing.Point(26, 416)
        Me.label6.Name = "label6"
        Me.label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label6.Size = New System.Drawing.Size(81, 19)
        Me.label6.TabIndex = 93
        Me.label6.Text = "Font Width:"
        '
        'txtFontHeight
        '
        Me.txtFontHeight.AcceptsReturn = True
        Me.txtFontHeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtFontHeight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFontHeight.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFontHeight.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFontHeight.Location = New System.Drawing.Point(144, 381)
        Me.txtFontHeight.MaxLength = 8
        Me.txtFontHeight.Name = "txtFontHeight"
        Me.txtFontHeight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFontHeight.Size = New System.Drawing.Size(133, 26)
        Me.txtFontHeight.TabIndex = 92
        Me.txtFontHeight.Text = "20"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.BackColor = System.Drawing.SystemColors.Control
        Me.label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.label5.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label5.Location = New System.Drawing.Point(26, 384)
        Me.label5.Name = "label5"
        Me.label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label5.Size = New System.Drawing.Size(83, 19)
        Me.label5.TabIndex = 91
        Me.label5.Text = "Font Height:"
        '
        'txtGlyphHeight
        '
        Me.txtGlyphHeight.AcceptsReturn = True
        Me.txtGlyphHeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtGlyphHeight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGlyphHeight.Enabled = False
        Me.txtGlyphHeight.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGlyphHeight.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGlyphHeight.Location = New System.Drawing.Point(144, 348)
        Me.txtGlyphHeight.MaxLength = 8
        Me.txtGlyphHeight.Name = "txtGlyphHeight"
        Me.txtGlyphHeight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGlyphHeight.Size = New System.Drawing.Size(133, 26)
        Me.txtGlyphHeight.TabIndex = 90
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.BackColor = System.Drawing.SystemColors.Control
        Me.label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.label4.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label4.Location = New System.Drawing.Point(26, 351)
        Me.label4.Name = "label4"
        Me.label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label4.Size = New System.Drawing.Size(91, 19)
        Me.label4.TabIndex = 89
        Me.label4.Text = "Glyph Height:"
        '
        'txtGlyphWidth
        '
        Me.txtGlyphWidth.AcceptsReturn = True
        Me.txtGlyphWidth.BackColor = System.Drawing.SystemColors.Window
        Me.txtGlyphWidth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGlyphWidth.Enabled = False
        Me.txtGlyphWidth.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGlyphWidth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGlyphWidth.Location = New System.Drawing.Point(144, 317)
        Me.txtGlyphWidth.MaxLength = 8
        Me.txtGlyphWidth.Name = "txtGlyphWidth"
        Me.txtGlyphWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGlyphWidth.Size = New System.Drawing.Size(133, 26)
        Me.txtGlyphWidth.TabIndex = 88
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.SystemColors.Control
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.label3.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Location = New System.Drawing.Point(26, 320)
        Me.label3.Name = "label3"
        Me.label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label3.Size = New System.Drawing.Size(89, 19)
        Me.label3.TabIndex = 87
        Me.label3.Text = "Glyph Width:"
        '
        'txtSleepMessage
        '
        Me.txtSleepMessage.Location = New System.Drawing.Point(140, 209)
        Me.txtSleepMessage.MaxLength = 128
        Me.txtSleepMessage.Multiline = True
        Me.txtSleepMessage.Name = "txtSleepMessage"
        Me.txtSleepMessage.Size = New System.Drawing.Size(429, 83)
        Me.txtSleepMessage.TabIndex = 86
        '
        'cmdSetCustomerInfo
        '
        Me.cmdSetCustomerInfo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetCustomerInfo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetCustomerInfo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetCustomerInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetCustomerInfo.Location = New System.Drawing.Point(336, 157)
        Me.cmdSetCustomerInfo.Name = "cmdSetCustomerInfo"
        Me.cmdSetCustomerInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetCustomerInfo.Size = New System.Drawing.Size(200, 30)
        Me.cmdSetCustomerInfo.TabIndex = 85
        Me.cmdSetCustomerInfo.Text = "Set Customer Info"
        Me.cmdSetCustomerInfo.UseVisualStyleBackColor = False
        '
        'cmdGetCustomerInfo
        '
        Me.cmdGetCustomerInfo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetCustomerInfo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetCustomerInfo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetCustomerInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetCustomerInfo.Location = New System.Drawing.Point(92, 157)
        Me.cmdGetCustomerInfo.Name = "cmdGetCustomerInfo"
        Me.cmdGetCustomerInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetCustomerInfo.Size = New System.Drawing.Size(200, 30)
        Me.cmdGetCustomerInfo.TabIndex = 84
        Me.cmdGetCustomerInfo.Text = "Get Customer Info"
        Me.cmdGetCustomerInfo.UseVisualStyleBackColor = False
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AcceptsReturn = True
        Me.txtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomerName.Location = New System.Drawing.Point(197, 105)
        Me.txtCustomerName.MaxLength = 64
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerName.Size = New System.Drawing.Size(373, 26)
        Me.txtCustomerName.TabIndex = 83
        '
        'txtCompanyName
        '
        Me.txtCompanyName.AcceptsReturn = True
        Me.txtCompanyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompanyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCompanyName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCompanyName.Location = New System.Drawing.Point(197, 66)
        Me.txtCompanyName.MaxLength = 64
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompanyName.Size = New System.Drawing.Size(373, 26)
        Me.txtCompanyName.TabIndex = 82
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.SystemColors.Control
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(48, 109)
        Me.label2.Name = "label2"
        Me.label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label2.Size = New System.Drawing.Size(112, 19)
        Me.label2.TabIndex = 81
        Me.label2.Text = "Customer Name:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.SystemColors.Control
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(26, 209)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(104, 19)
        Me.label1.TabIndex = 80
        Me.label1.Text = "Sleep Message:"
        '
        'lblEnrollData
        '
        Me.lblEnrollData.AutoSize = True
        Me.lblEnrollData.BackColor = System.Drawing.SystemColors.Control
        Me.lblEnrollData.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEnrollData.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnrollData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEnrollData.Location = New System.Drawing.Point(48, 67)
        Me.lblEnrollData.Name = "lblEnrollData"
        Me.lblEnrollData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEnrollData.Size = New System.Drawing.Size(112, 19)
        Me.lblEnrollData.TabIndex = 79
        Me.lblEnrollData.Text = "Comapny Name:"
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMessage.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(12, 9)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMessage.Size = New System.Drawing.Size(574, 28)
        Me.lblMessage.TabIndex = 78
        Me.lblMessage.Text = "Message"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmScreenSaver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 597)
        Me.Controls.Add(Me.cmdDebugOutFileBrowse)
        Me.Controls.Add(Me.txtDebugOutFile)
        Me.Controls.Add(Me.chkDebugOut)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSetSleepMsg)
        Me.Controls.Add(Me.cmdGetSleepMsg)
        Me.Controls.Add(Me.chkStrikeOut)
        Me.Controls.Add(Me.chkUnderline)
        Me.Controls.Add(Me.chkItalic)
        Me.Controls.Add(Me.cmdGetGlyphSize)
        Me.Controls.Add(Me.txtFontWeight)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.txtFontWidth)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.txtFontHeight)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.txtGlyphHeight)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.txtGlyphWidth)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.txtSleepMessage)
        Me.Controls.Add(Me.cmdSetCustomerInfo)
        Me.Controls.Add(Me.cmdGetCustomerInfo)
        Me.Controls.Add(Me.txtCustomerName)
        Me.Controls.Add(Me.txtCompanyName)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.lblEnrollData)
        Me.Controls.Add(Me.lblMessage)
        Me.Name = "frmScreenSaver"
        Me.Text = "frmScreenSaver"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdDebugOutFileBrowse As System.Windows.Forms.Button
    Public WithEvents txtDebugOutFile As System.Windows.Forms.TextBox
    Private WithEvents chkDebugOut As System.Windows.Forms.CheckBox
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdSetSleepMsg As System.Windows.Forms.Button
    Public WithEvents cmdGetSleepMsg As System.Windows.Forms.Button
    Private WithEvents chkStrikeOut As System.Windows.Forms.CheckBox
    Private WithEvents chkUnderline As System.Windows.Forms.CheckBox
    Private WithEvents chkItalic As System.Windows.Forms.CheckBox
    Public WithEvents cmdGetGlyphSize As System.Windows.Forms.Button
    Public WithEvents txtFontWeight As System.Windows.Forms.TextBox
    Public WithEvents label7 As System.Windows.Forms.Label
    Public WithEvents txtFontWidth As System.Windows.Forms.TextBox
    Public WithEvents label6 As System.Windows.Forms.Label
    Public WithEvents txtFontHeight As System.Windows.Forms.TextBox
    Public WithEvents label5 As System.Windows.Forms.Label
    Public WithEvents txtGlyphHeight As System.Windows.Forms.TextBox
    Public WithEvents label4 As System.Windows.Forms.Label
    Public WithEvents txtGlyphWidth As System.Windows.Forms.TextBox
    Private WithEvents OpenSaveDlg As System.Windows.Forms.SaveFileDialog
    Public WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents txtSleepMessage As System.Windows.Forms.TextBox
    Public WithEvents cmdSetCustomerInfo As System.Windows.Forms.Button
    Public WithEvents cmdGetCustomerInfo As System.Windows.Forms.Button
    Public WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Public WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Public WithEvents label2 As System.Windows.Forms.Label
    Public WithEvents label1 As System.Windows.Forms.Label
    Public WithEvents lblEnrollData As System.Windows.Forms.Label
    Public WithEvents lblMessage As System.Windows.Forms.Label
End Class
