Imports Microsoft.Data.SqlClient

Module Prodi
    Public Function GetProdiList() As List(Of String)
        Dim prodiList As New List(Of String)()
        Using conn As SqlConnection = GetConnection()
            Using cmd As SqlCommand = New SqlCommand("SELECT [nama prodi] FROM [dbo].[Prodi]", conn)
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    prodiList.Add(reader("nama prodi").ToString())
                End While
                conn.Close()
            End Using
        End Using
        Return prodiList
    End Function

    Public Function GetKodeProdi(prodi As String) As String
        Dim kodeProdi As String = ""
        Dim q = "SELECT [kode prodi] FROM [dbo].[Prodi] WHERE [nama prodi] = @prodi"
        Using conn As SqlConnection = GetConnection()
            Using cmd As SqlCommand = New SqlCommand(q, conn)
                cmd.Parameters.AddWithValue("@prodi", prodi)
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    kodeProdi = reader("kode prodi").ToString()
                End If
            End Using
        End Using
        Return kodeProdi
    End Function
End Module
