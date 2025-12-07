Public Class FMain
    Private Sub FMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'hapus data combobox
        ComboBoxProgramStudi.Items.Clear()
        ComboBoxProfesi.Items.Clear()

        'isi data combobox dari database
        For Each prodi As String In GetProfesiList()
            ComboBoxProfesi.Items.Add(prodi)
        Next
        For Each prodi As String In GetProdiList()
            ComboBoxProgramStudi.Items.Add(prodi)
        Next
    End Sub

    Private Sub ButtonLanjut_Click(sender As Object, e As EventArgs) Handles ButtonLanjut.Click
        Dim id = DaftarPengguna(TextBoxNama.Text,
                                TextBoxEmail.Text,
                                GetKodeProdi(ComboBoxProgramStudi.SelectedItem.ToString()),
                                ComboBoxProfesi.SelectedItem.ToString(),
                                GetKodeRumpun(ComboBoxProfesi.SelectedItem.ToString()))
        If Not id = -1 Then
            MessageBox.Show("Pendaftaran Berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            BuatSesiUjian(id)
        Else
            'MessageBox.Show("Pendaftaran Gagal. Silakan coba lagi.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim qForm As New FPertanyaan
        qForm.phase = 1
        qForm.sessionId = GetIdSesiUjian(id)
        qForm.Show()
        Hide()
    End Sub

    Private Sub FMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()
    End Sub
End Class