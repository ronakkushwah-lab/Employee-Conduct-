Public Class frmScreenSaver

    Public Const SLEEP_MSG_LEN As Integer = 128 * 2 'Sleep Message can be 128 characters of UNICODE
    Public Const COMPANY_NAME_LEN As Integer = 64 * 2 ' Company Name can be 64 characters of UNICODE
    Public Const CUSTOMER_NAME_LEN As Integer = 64 * 2 ' Customer Name can be 64 characters of UNICODE

    Private Sub cmdGetGlyphSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetGlyphSize.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer = 0
        Dim strXML As String

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, False)

        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        strXML = MakeXMLCommandHeader("GetSleepMsgGlyphSize")
        bRet = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)

        If bRet Then
            lblMessage.Text = "GetSleepMsgGlyphSize OK"
            txtGlyphWidth.Text = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "Width")
            txtGlyphHeight.Text = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "Height")
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, True)
    End Sub

    Private Sub frmScreenSaver_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        frmMain.Visible = True
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Close()
    End Sub

    Private Sub cmdGetCustomerInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetCustomerInfo.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer = 0
        Dim strXML As String
        Dim strCompanyName As String = "", strCustomerName As String = ""

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, False)

        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        strXML = MakeXMLCommandHeader("GetCustomerInfo")

        bRet = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)

        If bRet Then
            lblMessage.Text = "GetCustomerInfo OK"
            sbxpc.SBXPCDLL.XML_ParseBinaryUnicode(strXML, "CompanyName", strCompanyName, COMPANY_NAME_LEN)
            sbxpc.SBXPCDLL.XML_ParseBinaryUnicode(strXML, "CustomerName", strCustomerName, CUSTOMER_NAME_LEN)

            txtCompanyName.Text = strCompanyName
            txtCustomerName.Text = strCustomerName
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, True)
    End Sub

    Private Sub cmdSetCustomerInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetCustomerInfo.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer = 0
        Dim strXML As String

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, False)

        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        strXML = MakeXMLCommandHeader("SetCustomerInfo")
        sbxpc.SBXPCDLL.XML_AddBinaryUnicode(strXML, "CompanyName", txtCompanyName.Text)
        sbxpc.SBXPCDLL.XML_AddBinaryUnicode(strXML, "CustomerName", txtCustomerName.Text)

        bRet = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)

        If bRet Then
            lblMessage.Text = "SetCustomerInfo OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, True)
    End Sub

    Private Sub cmdGetSleepMsg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetSleepMsg.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer = 0
        Dim strXML As String
        Dim strSleepMessage As String = ""

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, False)

        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        strXML = MakeXMLCommandHeader("GetSleepMessage")

        bRet = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)

        If bRet Then
            lblMessage.Text = "GetSleepMessage OK"
            sbxpc.SBXPCDLL.XML_ParseBinaryUnicode(strXML, "SleepMessage", strSleepMessage, SLEEP_MSG_LEN)

            txtSleepMessage.Text = strSleepMessage
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, True)
    End Sub

    Private Sub cmdSetSleepMsg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetSleepMsg.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer = 0
        Dim strXML As String
        Dim strFontXML As String = ""

        Dim glyphWidth As Integer, glyphHeight As Integer
        Dim fontHeight As Integer, fontWidth As Integer, fontWeight As Integer

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, False)

        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        strXML = MakeXMLCommandHeader("GetSleepMsgGlyphSize")

        bRet = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)
        If Not bRet Then
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
            GoTo _lExit
        End If

        glyphWidth = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "Width")
        glyphHeight = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "Height")
        fontHeight = Val(txtFontHeight.Text)
        fontWidth = Val(txtFontWidth.Text)
        fontWeight = Val(txtFontWeight.Text)


        sbxpc.SBXPCDLL.XML_AddString(strFontXML, "FaceName", "Arial")
        sbxpc.SBXPCDLL.XML_AddInt(strFontXML, "Height", fontHeight)
        sbxpc.SBXPCDLL.XML_AddInt(strFontXML, "Width", fontWidth)
        sbxpc.SBXPCDLL.XML_AddInt(strFontXML, "Weight", fontWeight)
        sbxpc.SBXPCDLL.XML_AddBoolean(strFontXML, "Italic", chkItalic.Checked)
        sbxpc.SBXPCDLL.XML_AddBoolean(strFontXML, "Underline", chkUnderline.Checked)
        sbxpc.SBXPCDLL.XML_AddBoolean(strFontXML, "StrikeOut", chkStrikeOut.Checked)

        If chkDebugOut.Checked And txtDebugOutFile.Text <> "" Then
            sbxpc.SBXPCDLL.XML_AddString(strFontXML, "DebugOut", txtDebugOutFile.Text)
        End If

        strXML = MakeXMLCommandHeader("SetSleepMessage")
        sbxpc.SBXPCDLL.XML_AddBinaryGlyph(strXML, txtSleepMessage.Text, glyphWidth, glyphHeight, strFontXML)

        bRet = sbxpc.SBXPCDLL.GeneralOperationXML(frmMain.gMachineNumber, strXML)

        If bRet Then
            lblMessage.Text = "SetSleepMessage OK"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

_lExit:
        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, True)
    End Sub

    Private Sub cmdDebugOutFileBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDebugOutFileBrowse.Click
        If OpenSaveDlg.ShowDialog = DialogResult.Cancel Then Exit Sub

        txtDebugOutFile.Text = OpenSaveDlg.FileName
    End Sub
End Class