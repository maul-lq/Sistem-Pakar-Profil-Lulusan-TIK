Imports Microsoft.Data.SqlClient

Module Profesi
    Public Function GetProfesiList() As List(Of String)
        Dim profesiList As New List(Of String)()
        Using conn As SqlConnection = GetConnection()
            Using cmd As SqlCommand = New SqlCommand("SELECT [nama profesi] FROM [dbo].[Profesi]", conn)
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    profesiList.Add(reader("nama profesi").ToString())
                End While
                conn.Close()
            End Using
        End Using
        Return profesiList
    End Function

    Public Function GetKodeProfesi(profesi As String) As String
        Dim kodeProfesi As String = ""
        Dim q = "select p.[kode profesi] from [dbo].[Profesi] p where p.[nama profesi] = @profesi"
        Using conn = GetConnection()
            Using cmd As SqlCommand = New SqlCommand(q, conn)
                conn.Open()
                cmd.Parameters.AddWithValue("@profesi", profesi)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    kodeProfesi = reader("kode profesi").ToString()
                End If
            End Using
        End Using
        Return kodeProfesi
    End Function

    Public Function GetKodeRumpun(profesi As String) As String
        Dim kodeRumpun As String = ""
        Dim q = "select p.[kode rumpun] from [dbo].[Profesi] p left join [dbo].[Rumpun] r on p.[kode rumpun] = r.[kode rumpun] where p.[nama profesi] = @profesi"
        Using conn = GetConnection()
            Using cmd As SqlCommand = New SqlCommand(q, conn)
                conn.Open()
                cmd.Parameters.AddWithValue("@profesi", profesi)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    kodeRumpun = reader("kode rumpun").ToString()
                End If
            End Using
        End Using
        Return kodeRumpun
    End Function
End Module
