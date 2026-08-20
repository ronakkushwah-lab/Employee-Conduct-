Option Strict Off
Option Explicit On
Module mdlPublic
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
    '	Public Declare Sub CopyMemory Lib "kernel32"  Alias "RtlMoveMemory"(ByRef Destination As Any, ByRef Source As Any, ByVal Length As Integer)
    Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer)
	Public Declare Function GetSystemDefaultLangID Lib "kernel32" () As Short
	Public Const gstrNoDevice As String = "No Device"
	
	Public Const FK_Company As Short = 1
	Public Const FK_328 As Short = 101
	Public Const FK_338 As Short = 102
	Public Const FK_528 As Short = 201
	Public Const FK_526 As Short = 202
	
    Public Const MAX_BELLCOUNT_DAY As Short = 24
    Public Const gCompressPhotoSize As Integer = 8192 ' 8K
    Public Const gTempPhotoFile As String = "C:\TempPhoto.jpg"

	Structure BellInfo
		<VBFixedArray(MAX_BELLCOUNT_DAY - 1)> Dim mValid() As Byte
		<VBFixedArray(MAX_BELLCOUNT_DAY - 1)> Dim mHour() As Byte
		<VBFixedArray(MAX_BELLCOUNT_DAY - 1)> Dim mMinute() As Byte
		
		'UPGRADE_TODO: "Initialize" must be called to initialize instances of this structure. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B4BFF9E0-8631-45CF-910E-62AB3970F27B"'
		Public Sub Initialize()
			ReDim mValid(MAX_BELLCOUNT_DAY - 1)
			ReDim mHour(MAX_BELLCOUNT_DAY - 1)
			ReDim mMinute(MAX_BELLCOUNT_DAY - 1)
		End Sub
    End Structure
	
	Function ErrorPrint(ByRef aErrorCode As Integer) As String
        Select Case aErrorCode
            Case 0
                ErrorPrint = "SUCCESS"
            Case 1
                ErrorPrint = "ERR_COMPORT_ERROR"
            Case 2
                ErrorPrint = "ERR_WRITE_FAIL"
            Case 3
                ErrorPrint = "ERR_READ_FAIL"
            Case 4
                ErrorPrint = "ERR_INVALID_PARAM"
            Case 5
                ErrorPrint = "ERR_NON_CARRYOUT"
            Case 6
                ErrorPrint = "ERR_LOG_END"
            Case 7
                ErrorPrint = "ERR_MEMORY"
            Case 8
                ErrorPrint = "ERR_MULTIUSER"
            Case Else
                ErrorPrint = ""
        End Select
    End Function

    Public Function MakeXMLCommandHeader(ByVal request As String) As String
        MakeXMLCommandHeader = ""
        sbxpc.SBXPCDLL.XML_AddString(MakeXMLCommandHeader, "REQUEST", request)
        sbxpc.SBXPCDLL.XML_AddString(MakeXMLCommandHeader, "MSGTYPE", "request")
        sbxpc.SBXPCDLL.XML_AddInt(MakeXMLCommandHeader, "MachineID", frmMain.gMachineNumber)
    End Function

    Function pubIPAddrToLong(ByVal txtIP As String) As Integer
        Dim dwIP As Double
        Dim k As Integer
        Dim szTmp As String
        k = InStr(1, txtIP, ".", CompareMethod.Text)
        If k = 0 Then
            pubIPAddrToLong = 0
            Exit Function
        End If
        szTmp = Left(txtIP, k - 1)
        txtIP = Mid(txtIP, k + 1)
        dwIP = Val(szTmp)
        k = InStr(1, txtIP, ".", CompareMethod.Text)
        If k = 0 Then
            pubIPAddrToLong = 0
            Exit Function
        End If
        szTmp = Left(txtIP, k - 1)
        txtIP = Mid(txtIP, k + 1)
        dwIP = dwIP * 256 + Val(szTmp)
        k = InStr(1, txtIP, ".", CompareMethod.Text)
        If k = 0 Then
            pubIPAddrToLong = 0
            Exit Function
        End If
        szTmp = Left(txtIP, k - 1)
        txtIP = Mid(txtIP, k + 1)
        dwIP = dwIP * 256 + Val(szTmp)
        dwIP = dwIP * 256 + Val(txtIP)
        If dwIP > 2147483647 Then dwIP = dwIP - 4294967296.0#
        pubIPAddrToLong = dwIP
    End Function
End Module