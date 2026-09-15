Option Strict Off
Option Explicit On

Public Class frmEvent

    Private Sub radioSerial_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles radioSerial.MouseClick
        If radioSerial.Checked Then
            radioNetwork.Checked = False
            EnableControls(False)
        End If
    End Sub

    Private Sub EnableControls(ByVal bComPort As Boolean)
        cmbComPort.Enabled = Not bComPort
        cmbBaudrate.Enabled = Not bComPort
        txtSourceIP.Enabled = bComPort
        txtPortNumber.Enabled = bComPort
    End Sub

    Private Sub radioNetwork_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles radioNetwork.MouseClick
        If radioNetwork.Checked Then
            radioSerial.Checked = False
            EnableControls(True)
        End If
    End Sub

    Private Sub frmEvent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbComPort.SelectedIndex = 0
        cmbBaudrate.SelectedIndex = 0
        radioSerial.Checked = False
        radioNetwork.Checked = True
        EnableControls(True)
    End Sub

    Private Sub frmEvent_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        frmMain.Visible = True
        sbxpc.SBXPCDLL.StopEventCapture()
    End Sub

    Public Sub ReceiveEventXML(ByVal strEventXML As String)
        Dim year As Integer, month As Integer, day As Integer, hour As Integer, minute As Integer, second As Integer, weekday As Integer
        Dim strXML As String, eventItemString As String
        Dim strMachineType As String = "", strEventType As String = ""
        Dim machinId As Integer
        Dim managerId As Integer, userId As Integer, result As Integer
        Dim str1 As String = "", str2 As String = "", str3 As String = "", str4 As String = ""

        strXML = strEventXML
        year = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "Year")
        month = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "Month")
        day = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "Day")
        hour = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "Hour")
        minute = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "Minute")
        second = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "Second")
        weekday = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "Weekday")

        machinId = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "MachineID")
        sbxpc.SBXPCDLL.XML_ParseString(strXML, "MachineType", strMachineType)
        sbxpc.SBXPCDLL.XML_ParseString(strXML, "EventType", strEventType)

        eventItemString = String.Format("{0:D2}-", year)
        eventItemString &= String.Format("{0:D2}-", month)
        eventItemString &= String.Format("{0:D2} ", day)
        eventItemString &= String.Format("{0:D2}:", hour)
        eventItemString &= String.Format("{0:D2}:", minute)
        eventItemString &= String.Format("{0:D2} ", second)

        eventItemString &= "[" & strMachineType & ":"
        eventItemString &= machinId & "] "
        eventItemString &= strEventType & ", "

        Select Case strEventType
            Case "Management Log"
                managerId = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "ManagerID")
                userId = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "UserID")
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "Action", str1)
                result = sbxpc.SBXPCDLL.XML_ParseLong(strXML, "Result")
                eventItemString &= "Manager ID = " & String.Format("{0:D5}, ", managerId)
                eventItemString &= "User ID = " & String.Format("{0:D5}, ", userId)
                eventItemString &= "Action = " & str1 & ", "
                eventItemString &= "Result = " & String.Format("{0:D}", result)
            Case "Time Log"
                userId = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "UserID")
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "AttendanceStatus", str1)
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "VerificationMode", str2)
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "AntipassStatus", str3)
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "Photo", str4)
                eventItemString &= "User ID = " & String.Format("{0:D5}, ", userId)
                eventItemString &= "AttendanceStatus = " & str1 & ", "
                eventItemString &= "VerificationMode = " & str2 & ", "
                eventItemString &= "AntipassStatus = " & str3 & ", "
                eventItemString &= "Photo = " & str4
            Case "Verification Success"
                userId = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "UserID")
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "VerificationMode", str1)
                eventItemString &= "User ID = " & String.Format("{0:D5}, ", userId)
                eventItemString &= "VerificationMode = " & str1
            Case "Verification Failure"
                userId = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "UserID")
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "VerificationMode", str1)
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "ReasonOfFailure", str2)
                eventItemString &= "User ID = " & String.Format("{0:D5}, ", userId)
                eventItemString &= "VerificationMode = " & str1 & ", "
                eventItemString &= "ReasonOfFailure = " & str2
            Case "Alarm On"
                userId = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "UserID")
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "AlarmType", str1)
                eventItemString &= "User ID = " & String.Format("{0:D5}, ", userId)
                eventItemString &= "AlarmType = " & str1
            Case "Alarm Off"
                userId = sbxpc.SBXPCDLL.XML_ParseInt(strXML, "UserID")
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "AlarmType", str1)
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "AlarmOffMethod", str2)
                eventItemString &= "User ID = " & String.Format("{0:D5}", userId)
                eventItemString &= "AlarmType = " & str1 & ", "
                eventItemString &= "AlarmOffMethod = " & str2
            Case "DoorBell"
                sbxpc.SBXPCDLL.XML_ParseString(strXML, "InputType", str1)
                eventItemString &= "Input Type = " & str1
        End Select
        lstEvents.Items.Add(eventItemString)
    End Sub

    Private Sub cmdStartMoniter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartMoniter.Click
        Dim bRet As Boolean
        If (radioNetwork.Checked) Then
            bRet = sbxpc.SBXPCDLL.StartEventCapture(0, pubIPAddrToLong(txtSourceIP.Text), Convert.ToInt32(txtPortNumber.Text), _
                                                    New sbxpc.SBXPCDLL.EventCallback(AddressOf ReceiveEventXML), IntPtr.Zero)
        Else
            bRet = sbxpc.SBXPCDLL.StartEventCapture(1, cmbComPort.SelectedIndex + 1, Convert.ToInt32(cmbBaudrate.Text), _
                                                    New sbxpc.SBXPCDLL.EventCallback(AddressOf ReceiveEventXML), IntPtr.Zero)
        End If
        cmdStartMoniter.Enabled = False
        cmdEndMoniter.Enabled = True
    End Sub

    Private Sub cmdEndMoniter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEndMoniter.Click
        sbxpc.SBXPCDLL.StopEventCapture()
        cmdStartMoniter.Enabled = True
        cmdEndMoniter.Enabled = False
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        lstEvents.Items.Clear()
    End Sub
End Class