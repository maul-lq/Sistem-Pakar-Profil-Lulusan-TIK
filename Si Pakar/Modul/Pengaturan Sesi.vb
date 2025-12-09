Imports Microsoft.Data.SqlClient

Module Pengaturan_Sesi
    Public Sub hapusHasilTesIni(idSesi As Integer, Optional kondisi As Boolean = False)
        ' kondisi = True -> hapus user beserta semua sesi dan data terkait
        ' kondisi = False -> hapus hanya data sesi (Exam Details, Exam Rumpun Scores, Sesi Ujian)

        Using conn = GetConnection()
            conn.Open()
            Dim tran = conn.BeginTransaction()
            Try
                ' Ambil Id user dari sesi yang diberikan
                Dim qGetUser As String = "SELECT [Id user] FROM [Sesi Ujian] WHERE [Id sesi] = @ids"
                Dim cmdGet As New SqlCommand(qGetUser, conn, tran)
                cmdGet.Parameters.AddWithValue("@ids", idSesi)
                Dim userObj = cmdGet.ExecuteScalar()

                If userObj Is Nothing Then
                    ' Tidak ada sesi ditemukan, commit dan keluar
                    tran.Commit()
                    Return
                End If

                Dim idUser As Integer = Convert.ToInt32(userObj)

                If kondisi Then
                    ' Hapus semua sesi milik user beserta detail dan rumpun scores
                    Dim sessionIds As New List(Of Integer)()
                    Dim qSessions As String = "SELECT [Id sesi] FROM [Sesi Ujian] WHERE [Id user] = @uid"
                    Dim cmdSess As New SqlCommand(qSessions, conn, tran)
                    cmdSess.Parameters.AddWithValue("@uid", idUser)
                    Using r = cmdSess.ExecuteReader()
                        While r.Read()
                            sessionIds.Add(Convert.ToInt32(r("Id sesi")))
                        End While
                    End Using

                    For Each sid In sessionIds
                        Dim delDetails As New SqlCommand("DELETE FROM [Exam Details] WHERE [id sesi] = @s", conn, tran)
                        delDetails.Parameters.AddWithValue("@s", sid)
                        delDetails.ExecuteNonQuery()

                        Dim delRumpun As New SqlCommand("DELETE FROM [Exam Rumpun Scores] WHERE [id sesi] = @s", conn, tran)
                        delRumpun.Parameters.AddWithValue("@s", sid)
                        delRumpun.ExecuteNonQuery()
                    Next

                    ' Hapus semua sesi
                    Dim delSesiAll As New SqlCommand("DELETE FROM [Sesi Ujian] WHERE [Id user] = @uid", conn, tran)
                    delSesiAll.Parameters.AddWithValue("@uid", idUser)
                    delSesiAll.ExecuteNonQuery()

                    ' Hapus data user
                    Dim delUser As New SqlCommand("DELETE FROM [Data User] WHERE [Id user] = @uid", conn, tran)
                    delUser.Parameters.AddWithValue("@uid", idUser)
                    delUser.ExecuteNonQuery()
                Else
                    ' Hapus hanya data sesi saat ini
                    Dim delDetails As New SqlCommand("DELETE FROM [Exam Details] WHERE [id sesi] = @ids", conn, tran)
                    delDetails.Parameters.AddWithValue("@ids", idSesi)
                    delDetails.ExecuteNonQuery()

                    Dim delRumpun As New SqlCommand("DELETE FROM [Exam Rumpun Scores] WHERE [id sesi] = @ids", conn, tran)
                    delRumpun.Parameters.AddWithValue("@ids", idSesi)
                    delRumpun.ExecuteNonQuery()

                    Dim delSesi As New SqlCommand("DELETE FROM [Sesi Ujian] WHERE [Id sesi] = @ids", conn, tran)
                    delSesi.Parameters.AddWithValue("@ids", idSesi)
                    delSesi.ExecuteNonQuery()
                End If

                tran.Commit()
            Catch ex As Exception
                Try
                    tran.Rollback()
                Catch
                End Try
                MessageBox.Show("Gagal menghapus data sesi/user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub
End Module
