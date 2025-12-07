Imports Microsoft.Data.SqlClient

Module Data_User
    Public Function DaftarPengguna(nama As String, email As String, kprodi As String, profesi As String, krumpun As String) As Boolean
        Dim success As Boolean = False
        Using conn As SqlConnection = GetConnection()
            Dim q = "INSERT INTO [dbo].[Data User] ([nama], [email], [kode prodi], [profesi], [kode rumpun]) VALUES 
                    (@nama, @email, @kprodi, @profesi, @krumpun)"
            Using cmd As SqlCommand = New SqlCommand(q, conn)
                cmd.Parameters.AddWithValue("@nama", nama)
                cmd.Parameters.AddWithValue("@email", email)
                cmd.Parameters.AddWithValue("@kprodi", kprodi)
                cmd.Parameters.AddWithValue("@profesi", profesi)
                cmd.Parameters.AddWithValue("@krumpun", krumpun)
                conn.Open()
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                success = (rowsAffected > 0)
            End Using
        End Using
        Return success
    End Function
End Module
