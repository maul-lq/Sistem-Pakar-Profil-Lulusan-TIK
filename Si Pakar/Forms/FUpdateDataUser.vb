Public Class FUpdateDataUser
    Public nama, email As String
    Public idSesi As Integer

    Private Sub FUpdateDataUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxNama.Text = nama
        TextBoxEmail.Text = email

        AddHandler TextBoxNama.TextChanged, AddressOf Inputs_Changed
        AddHandler TextBoxEmail.TextChanged, AddressOf Inputs_Changed
    End Sub

    Private Sub Inputs_Changed(sender As Object, e As EventArgs)
        ValidateInputs()
    End Sub

    Private Sub ButtonSimpan_Click(sender As Object, e As EventArgs) Handles ButtonSimpan.Click
        ' Clear previous errors
        ErrorProviderDataUser.SetError(TextBoxNama, "")
        ErrorProviderDataUser.SetError(TextBoxEmail, "")

        Dim newName = TextBoxNama.Text.Trim()
        Dim newEmail = TextBoxEmail.Text.Trim()
        ' Update Data User in database for the user associated with this session
        Try
            Using conn = GetConnection()
                conn.Open()
                Dim qGet As String = "SELECT [Id user] FROM [Sesi Ujian] WHERE [Id sesi] = @sid"
                Using cmdGet As New Microsoft.Data.SqlClient.SqlCommand(qGet, conn)
                    cmdGet.Parameters.AddWithValue("@sid", idSesi)
                    Dim res = cmdGet.ExecuteScalar()
                    If res Is Nothing Then
                        MessageBox.Show("Gagal menemukan sesi pengguna.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    Dim idUser As Integer = Convert.ToInt32(res)

                    Dim qUpdate As String = "UPDATE [Data User] SET [nama] = @nama, [email] = @email WHERE [Id user] = @iduser"
                    Using cmdUp As New Microsoft.Data.SqlClient.SqlCommand(qUpdate, conn)
                        cmdUp.Parameters.AddWithValue("@nama", newName)
                        cmdUp.Parameters.AddWithValue("@email", newEmail)
                        cmdUp.Parameters.AddWithValue("@iduser", idUser)
                        cmdUp.ExecuteNonQuery()
                    End Using
                End Using
            End Using

            MessageBox.Show("Data berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ValidateInputs()
        Dim isValid As Boolean = True

        If String.IsNullOrEmpty(TextBoxNama.Text) Then
            ErrorProviderDataUser.SetError(TextBoxNama, "Nama tidak boleh kosong")
            isValid = False
        End If

        ' Simple email pattern: contains @ and a domain part
        Dim pattern As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        If Not System.Text.RegularExpressions.Regex.IsMatch(TextBoxEmail.Text, pattern) Then
            ErrorProviderDataUser.SetError(TextBoxEmail, "Email tidak valid (format: user@domain.tld)")
            isValid = False
        End If

        ButtonSimpan.Enabled = isValid
    End Sub

    Private Sub ButtonBatal_Click(sender As Object, e As EventArgs) Handles ButtonBatal.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class