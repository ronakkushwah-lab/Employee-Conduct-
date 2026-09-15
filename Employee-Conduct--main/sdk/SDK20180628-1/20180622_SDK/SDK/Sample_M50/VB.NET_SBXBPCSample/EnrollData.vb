Public Class EnrollData
    Dim mDataPath As String

    Public Shared DataModule As EnrollData

    Private Function GetConnection() As OleDb.OleDbConnection
        Return New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" _
         & "Data Source=" & mDataPath & "\datEnrollDat.mdb")
    End Function

    Public Overloads Function GetEnrollDatas() As DataSet
        Dim conn As OleDb.OleDbConnection = GetConnection()
        Try
            Dim ds As New DataSet()
            Dim sql As String = "select * from tblEnroll order by EnrollNumber"
            Dim da As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(sql, conn)

            Try
                da.Fill(ds, "tblEnroll")
            Finally
                da.Dispose()
            End Try

            Return ds
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Function

    Public Sub SaveEnrolls(ByVal ds As DataSet)

        Dim conn As OleDb.OleDbConnection = GetConnection()

        Try
            Dim sql As String = "select * from tblEnroll"
            Dim da As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(sql, conn)

            Try
                Dim cb As OleDb.OleDbCommandBuilder = New OleDb.OleDbCommandBuilder(da)

                If ds.HasChanges Then
                    da.Update(ds, "tblEnroll")
                    ds.AcceptChanges()
                End If
            Finally
                ds.Dispose()
                da.Dispose()
            End Try

        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub
    Public Sub DeleteDB()
        Dim conn As OleDb.OleDbConnection
        Dim cmd As OleDb.OleDbCommand

        conn = GetConnection()
        conn.Open()

        cmd = New OleDb.OleDbCommand("delete from tblEnroll", conn)
        cmd.ExecuteNonQuery()

        conn.Close()
    End Sub

    Public Sub New(ByVal sDatapath As String)
        MyBase.new()
        Me.mDataPath = sDatapath
        EnrollData.DataModule = Me
    End Sub
End Class