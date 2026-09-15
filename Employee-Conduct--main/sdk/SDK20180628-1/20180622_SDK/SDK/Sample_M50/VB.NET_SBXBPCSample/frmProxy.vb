Option Strict Off
Option Explicit On
Imports System.Runtime.InteropServices

Public Class frmProxy
    Const PROXY_COUNT As Integer = 20
    Dim proxyNameList(PROXY_COUNT - 1) As String
    Private Sub frmProxy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ProxyNameInit()
        DrawProxyNameList()
    End Sub

    Private Sub ProxyNameInit()
        Dim i As Integer
        For i = 0 To PROXY_COUNT - 1
            proxyNameList(i) = ""
        Next
    End Sub

    Private Sub DrawProxyNameList()
        Dim i As Integer
        lstProxy.Items.Clear()
        For i = 0 To PROXY_COUNT - 1
            lstProxy.Items.Add("[No.] " & VB6.Format(CStr(i), "0# ") & _
                                "[Name] " & proxyNameList(i))
        Next
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        If lstProxy.SelectedIndex = -1 Then
            Exit Sub
        End If

        proxyNameList(lstProxy.SelectedIndex) = txtProxyName.Text
        DrawProxyNameList()
    End Sub

    Private Sub lstProxy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstProxy.SelectedIndexChanged
        If lstProxy.SelectedIndex = -1 Then
            Exit Sub
        End If

        txtProxyName.Text = proxyNameList(lstProxy.SelectedIndex)
        txtProxyName.Focus()
    End Sub

    Private Sub frmProxy_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        frmMain.Visible = True
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Close()
    End Sub

    Private Sub cmdRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRead.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer
        Dim i As Integer
        Dim departmentName As String = ""

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, False)
        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        For i = 0 To PROXY_COUNT - 1

            bRet = sbxpc.SBXPCDLL.GetDepartName(frmMain.gMachineNumber, i + 1, 1, departmentName)

            If bRet Then
                lblMessage.Text = "Success"
                proxyNameList(i) = departmentName
            Else
                sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
                lblMessage.Text = ErrorPrint(vErrorCode)
                proxyNameList(i) = ""
            End If
        Next

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, True)

        DrawProxyNameList()
    End Sub

    Private Sub cmdWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWrite.Click
        Dim bRet As Boolean
        Dim vErrorCode As Integer
        Dim i As Integer

        lblMessage.Text = "Working..."
        Application.DoEvents()

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, False)
        If Not bRet Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        For i = 0 To PROXY_COUNT - 1

            bRet = sbxpc.SBXPCDLL.SetDepartName(frmMain.gMachineNumber, i + 1, 1, proxyNameList(i))

            If bRet Then
                lblMessage.Text = "Success"
            Else
                sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
                lblMessage.Text = ErrorPrint(vErrorCode)
            End If
        Next

        bRet = sbxpc.SBXPCDLL.EnableDevice(frmMain.gMachineNumber, True)
    End Sub
End Class