Imports Microsoft.Data.SqlClient

Module Data_User
    Public Function DaftarPengguna(nama As String, email As String, kprodi As String, profesi As String, krumpun As String) As Integer
        Dim idPengguna As Integer
        Try
            Using conn As SqlConnection = GetConnection()
                Dim q = "INSERT INTO [dbo].[Data User] ([nama], [email], [kode prodi], [profesi], [kode rumpun])
                    OUTPUT INSERTED.[Id user]
                    VALUES (@nama, @email, @kprodi, @profesi, @krumpun)"
                Using cmd As SqlCommand = New SqlCommand(q, conn)
                    cmd.Parameters.AddWithValue("@nama", nama)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@kprodi", kprodi)
                    cmd.Parameters.AddWithValue("@profesi", profesi)
                    cmd.Parameters.AddWithValue("@krumpun", krumpun)
                    conn.Open()
                    idPengguna = cmd.ExecuteScalar()
                End Using
            End Using
        Catch ex As Exception
            idPengguna = -1
        End Try
        Return idPengguna
    End Function
End Module
