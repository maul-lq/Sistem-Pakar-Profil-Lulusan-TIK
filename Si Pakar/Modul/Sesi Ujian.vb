Imports Microsoft.Data.SqlClient

Module Sesi_Ujian
    Function BuatSesiUjian(kodePengguna As Integer) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Dim q As String = "INSERT INTO [dbo].[Sesi Ujian] ([Id user], [waktu mulai], [waktu selesai], [scenario used], [phase1 score json], [final result json]) VALUES (@userId, @waktuMulai, @waktuSelesai, @scenario, @phase1, @final)"
                Using cmd As New SqlCommand(q, conn)
                    cmd.Parameters.AddWithValue("@userId", kodePengguna)
                    cmd.Parameters.AddWithValue("@waktuMulai", DateTime.Now)
                    cmd.Parameters.AddWithValue("@waktuSelesai", DBNull.Value)
                    cmd.Parameters.AddWithValue("@scenario", "Umum")
                    cmd.Parameters.AddWithValue("@phase1", "{}")
                    cmd.Parameters.AddWithValue("@final", "{}")
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show("Error membuat sesi ujian: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Sub PerbaruiSeknarioUjian(idSesi As Integer, seknario As String)
        Try
            Using conn = GetConnection()
                conn.Open()
                Dim q As String = "UPDATE [dbo].[Sesi Ujian] SET [scenario used] = @scenario WHERE [Id sesi] = @idSesi"
                Using cmd As New SqlCommand(q, conn)
                    cmd.Parameters.AddWithValue("@scenario", seknario)
                    cmd.Parameters.AddWithValue("@idSesi", idSesi)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memperbarui skenario: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Function GetIdSesiUjian(kodePengguna As Integer) As Integer
        Try
            Using conn = GetConnection()
                conn.Open()
                Dim q As String = "SELECT TOP 1 [Id sesi] FROM [dbo].[Sesi Ujian] WHERE [Id user] = @userId ORDER BY [Id sesi] DESC"
                Using cmd As New SqlCommand(q, conn)
                    cmd.Parameters.AddWithValue("@userId", kodePengguna)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing Then
                        Return Convert.ToInt32(result)
                    Else
                        Return -1
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error mendapatkan ID sesi: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return -1
        End Try
    End Function
End Module
