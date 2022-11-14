Imports System.Data.SQLite

Module Module1

    Public cn As SQLiteConnection
    Public cmd As SQLiteCommand
    Public dr As SQLiteDataReader
    Public sql As String

    Public Sub openCon()
        cn = New SQLiteConnection("Data Source=" & Application.StartupPath & "/CostumerDB.db")

        If cn.State = ConnectionState.Closed Then
            cn.Open()
        End If
    End Sub

End Module
