Imports System.Drawing.Drawing2D
Imports Microsoft.Data.SqlClient
Public Class FMain
    Public startpage As FStart

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
        If DaftarPengguna(TextBoxNama.Text, TextBoxEmail.Text, GetKodeProdi(ComboBoxProgramStudi.SelectedItem.ToString()), ComboBoxProfesi.SelectedItem.ToString(), GetKodeRumpun(ComboBoxProfesi.SelectedItem.ToString())) Then
            MessageBox.Show("Pendaftaran Berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Pendaftaran Gagal. Silakan coba lagi.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim qForm As New FPertanyaan
        qForm.startpage = startpage
        qForm.Show()
        Hide()
    End Sub
End Class