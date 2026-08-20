Option Strict Off
Option Explicit On
Imports System.Runtime.InteropServices

Friend Class frmBellInfo
    Inherits System.Windows.Forms.Form

    Dim mbyBellInfo(MAX_BELLCOUNT_DAY * 3 - 1) As Byte

    Dim mBellCount As Integer
    Dim mBellInfo As BellInfo
    Dim mMachineNumber As Integer

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdRead_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRead.Click
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Waiting..."
        System.Windows.Forms.Application.DoEvents()

        If sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False) = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        Dim gh As GCHandle = GCHandle.Alloc(mbyBellInfo, GCHandleType.Pinned)
        Dim AddrOfmbyBellInfo As IntPtr = gh.AddrOfPinnedObject()

        vRet = sbxpc.SBXPCDLL.GetBellTime(mMachineNumber, _
                                             mBellCount, _
                                             AddrOfmbyBellInfo.ToInt32())
        gh.Free()

        If vRet = True Then
            ByteToBellInfo(mbyBellInfo, mBellInfo)

            ShowValue()
            lblMessage.Text = "Success!"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub

    Private Sub cmdWrite_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdWrite.Click
        Dim vRet As Boolean
        Dim vErrorCode As Integer

        lblMessage.Text = "Waiting..."
        System.Windows.Forms.Application.DoEvents()

        If sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, False) = False Then
            lblMessage.Text = gstrNoDevice
            Exit Sub
        End If

        GetValue()

        BellInfotoByte(mBellInfo, mbyBellInfo)

        Dim gh As GCHandle = GCHandle.Alloc(mbyBellInfo, GCHandleType.Pinned)
        Dim AddrOfmbyBellInfo As IntPtr = gh.AddrOfPinnedObject()

        vRet = sbxpc.SBXPCDLL.SetBellTime(mMachineNumber, _
                                             mBellCount, _
                                             AddrOfmbyBellInfo.ToInt32())

        gh.Free()

        If vRet = True Then
            lblMessage.Text = "Success!"
        Else
            sbxpc.SBXPCDLL.GetLastError(frmMain.gMachineNumber, vErrorCode)
            lblMessage.Text = ErrorPrint(vErrorCode)
        End If

        sbxpc.SBXPCDLL.EnableDevice(mMachineNumber, True)
    End Sub


    Private Sub frmBellInfo_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mMachineNumber = frmMain.gMachineNumber

        mBellInfo.Initialize()
    End Sub

    Private Sub frmBellInfo_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmMain.Visible = True
    End Sub

    Private Sub ShowValue()
        Dim i As Integer

        For i = 0 To MAX_BELLCOUNT_DAY - 1
            txtHour(i).Text = CStr(mBellInfo.mHour(i))
            txtMinute(i).Text = CStr(mBellInfo.mMinute(i))
            If mBellInfo.mValid(i) > 1 Then mBellInfo.mValid(i) = 0
            chkValid(i).CheckState = mBellInfo.mValid(i)
        Next i
        txtBellCount.Text = CStr(mBellCount)
    End Sub

    Private Sub GetValue()
        Dim i As Integer

        For i = 0 To MAX_BELLCOUNT_DAY - 1
            mBellInfo.mHour(i) = Val(txtHour(i).Text)
            mBellInfo.mMinute(i) = Val(txtMinute(i).Text)
            mBellInfo.mValid(i) = chkValid(i).CheckState
        Next i
        mBellCount = Val(txtBellCount.Text)
    End Sub

    Public Sub ByteToBellInfo(ByVal data As Byte(), ByRef bellinfo As BellInfo)
        For i As Integer = 0 To (MAX_BELLCOUNT_DAY - 1)
            bellinfo.mValid(i) = data(i)
            bellinfo.mHour(i) = data(MAX_BELLCOUNT_DAY + i)
            bellinfo.mMinute(i) = data(MAX_BELLCOUNT_DAY * 2 + i)
        Next
    End Sub

    Public Sub BellInfotoByte(ByVal bellinfo As BellInfo, ByRef data As Byte())
        For i As Integer = 0 To (MAX_BELLCOUNT_DAY - 1)
            data(i) = bellinfo.mValid(i)
            data(MAX_BELLCOUNT_DAY + i) = bellinfo.mHour(i)
            data(MAX_BELLCOUNT_DAY * 2 + i) = bellinfo.mMinute(i)
        Next
    End Sub
End Class