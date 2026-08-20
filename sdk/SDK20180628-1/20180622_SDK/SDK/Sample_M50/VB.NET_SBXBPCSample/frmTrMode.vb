Option Strict Off
Option Explicit On
Imports System.Runtime.InteropServices

Public Class frmTrMode

    Const DB_TRMODE_COUNT As Integer = 10
    Const DB_TRMODE_BYTE_COUNT As Integer = 5
    Dim trModeInfo(DB_TRMODE_COUNT * DB_TRMODE_BYTE_COUNT - 1) As UInt32

    Private Sub frmTrMode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        trModeInit()
        DrawTrMode()
        dtStart.ShowUpDown = True
        dtEnd.ShowUpDown = True
    End Sub

    Private Sub trModeInit()
        Dim i As Integer
        For i = 0 To DB_TRMODE_COUNT - 1
            trModeInfo(i * DB_TRMODE_BYTE_COUNT + 0) = 0
            trModeInfo(i * DB_TRMODE_BYTE_COUNT + 1) = 0
            trModeInfo(i * DB_TRMODE_BYTE_COUNT + 2) = 0
            trModeInfo(i * DB_TRMODE_BYTE_COUNT + 3) = 0
            trModeInfo(i * DB_TRMODE_BYTE_COUNT + 4) = 0
        Next
    End Sub

    Private Sub DrawTrMode()
        Dim itemString As String
        Dim i As Integer
        lstTrMode.Items.Clear()

        For i = 0 To DB_TRMODE_COUNT - 1
            itemString = "[No.]" + String.Format("{0:D2}", i) + " "
            itemString += "[S]"
            itemString += String.Format("{0:D2}", trModeInfo(i * DB_TRMODE_BYTE_COUNT + 1)) & ":"
            itemString += String.Format("{0:D2}", trModeInfo(i * DB_TRMODE_BYTE_COUNT + 2)) & " "
            itemString += "[E]"
            itemString += String.Format("{0:D2}", trModeInfo(i * DB_TRMODE_BYTE_COUNT + 3)) & ":"
            itemString += String.Format("{0:D2}", trModeInfo(i * DB_TRMODE_BYTE_COUNT + 4)) & " "
            itemString += cmbTrMode.Items(trModeInfo(i * DB_TRMODE_BYTE_COUNT))
            lstTrMode.Items.Add(itemString)
        Next
    End Sub

    Private Sub cmdRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRead.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, False)
        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        Dim gh As GCHandle = GCHandle.Alloc(trModeInfo, GCHandleType.Pinned)
        Dim AddrOftimZoneInfo As IntPtr = gh.AddrOfPinnedObject()
        bRet = sbxpc.SBXPCDLL.GetDeviceLongInfo(frmMain.gMachineNumber, 5, AddrOftimZoneInfo.ToInt32())

        If bRet Then
            lblMessage.Text = "Success"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, True)

        DrawTrMode()
    End Sub

    Private Sub cmdWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWrite.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, False)
        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        Dim gh As GCHandle = GCHandle.Alloc(trModeInfo, GCHandleType.Pinned)
        Dim AddrOftimZoneInfo As IntPtr = gh.AddrOfPinnedObject()
        bRet = sbxpc.SBXPCDLL.SetDeviceLongInfo(frmMain.gMachineNumber, 5, AddrOftimZoneInfo.ToInt32())

        If bRet Then
            lblMessage.Text = "Success"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, True)
    End Sub

    Private Sub frmTrMode_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        frmMain.Visible = True
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Close()
    End Sub

    Private Sub lstTrMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTrMode.SelectedIndexChanged
        If lstTrMode.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim index As Integer = lstTrMode.SelectedIndex * DB_TRMODE_BYTE_COUNT
        dtStart.Value = New Date(2000, 1, 1, trModeInfo(index + 1), trModeInfo(index + 2), 0)
        dtEnd.Value = New Date(2000, 1, 1, trModeInfo(index + 3), trModeInfo(index + 4), 0)
        cmbTrMode.SelectedIndex = trModeInfo(index)
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        If lstTrMode.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim index As Integer = lstTrMode.SelectedIndex * DB_TRMODE_BYTE_COUNT
        trModeInfo(index) = cmbTrMode.SelectedIndex
        trModeInfo(index + 1) = dtStart.Value.Hour
        trModeInfo(index + 2) = dtStart.Value.Minute
        trModeInfo(index + 3) = dtEnd.Value.Hour
        trModeInfo(index + 4) = dtEnd.Value.Minute

        DrawTrMode()
    End Sub
End Class