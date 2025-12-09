Imports Microsoft.Data.SqlClient

Module Data_User
    Public Function DaftarPengguna(nama As String, email As String, kprodi As String, profesi As String, krumpun As String) As Integer
        Dim idPengguna As Integer
        Try
            ' sanitize inputs
            Dim kodeRumpun As String = If(krumpun, "").Trim()
            Dim kodeProfesiNama As String = If(profesi, "").Trim()

            Using conn As SqlConnection = GetConnection()
                conn.Open()

                ' If kode rumpun is empty, try to derive from profesi table
                If String.IsNullOrEmpty(kodeRumpun) AndAlso kodeProfesiNama <> "" Then
                    Dim qFind As String = "SELECT [kode rumpun] FROM Profesi WHERE [nama profesi] = @nama"
                    Using cmdFind As New SqlCommand(qFind, conn)
                        cmdFind.Parameters.AddWithValue("@nama", kodeProfesiNama)
                        Dim res = cmdFind.ExecuteScalar()
                        If res IsNot Nothing AndAlso Not IsDBNull(res) Then
                            kodeRumpun = res.ToString().Trim()
                        End If
                    End Using
                End If

                ' If still empty or not a valid rumpun, fallback to default 'CP'
                If String.IsNullOrEmpty(kodeRumpun) Then
                    kodeRumpun = "CP"
                Else
                    ' ensure the rumpun actually exists in Rumpun table; otherwise fallback to CP
                    Dim qCheck As String = "SELECT COUNT(1) FROM Rumpun WHERE [kode rumpun] = @kr"
                    Using cmdCheck As New SqlCommand(qCheck, conn)
                        cmdCheck.Parameters.AddWithValue("@kr", kodeRumpun)
                        Dim cnt = Convert.ToInt32(cmdCheck.ExecuteScalar())
                        If cnt = 0 Then
                            kodeRumpun = "CP"
                        End If
                    End Using
                End If

                Dim q = "INSERT INTO [dbo].[Data User] ([nama], [email], [kode prodi], [profesi], [kode rumpun])" &
                        " OUTPUT INSERTED.[Id user]" &
                        " VALUES (@nama, @email, @kprodi, @profesi, @krumpun)"

                Using cmd As SqlCommand = New SqlCommand(q, conn)
                    cmd.Parameters.AddWithValue("@nama", nama)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@kprodi", kprodi)
                    cmd.Parameters.AddWithValue("@profesi", profesi)
                    cmd.Parameters.AddWithValue("@krumpun", kodeRumpun)

                    idPengguna = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            ' keep original behaviour but write debug info to help diagnosis
            System.Diagnostics.Debug.WriteLine("DaftarPengguna error: " & ex.Message)
            idPengguna = -1
        End Try
        Return idPengguna
    End Function
End Module
