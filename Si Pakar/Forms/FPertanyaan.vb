Public Class FPertanyaan
    Public startpage As FStart

    Private Sub FPertanyaan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For d As Integer = 1 To 10
            Dim btn As New Button()
            btn.Text = d
            btn.Size = New Size(35, 35)
            AddHandler btn.Click, Sub(s, ev)
                                      ' Aksi ketika tombol nomor soal diklik
                                      MessageBox.Show("Tombol nomor soal " & btn.Text & " diklik.")
                                      UbahSoal(d)
                                  End Sub
            FlowLayoutPanelNomorSoal.Controls.Add(btn)
        Next
    End Sub

    Private Sub UbahSoal(id As Integer)
        ' Pastikan label menyesuaikan ukuran teks lalu posisikan di tengah panel pertanyaan
        LabelSoal.AutoSize = True
        LabelSoal.Text = "akdhvbgiewvbkryiefadsbjk\wvhbaewgviwfl"

        Dim centerX As Integer = Convert.ToInt32((PanelPertanyaann.Size.Width - LabelSoal.Width) \ 2)
        Dim centerY As Integer = Convert.ToInt32((PanelPertanyaann.Size.Height - LabelSoal.Height) \ 2) - 88

        ' Jangan beri nilai negatif jika label lebih besar dari panel
        centerX = Math.Max(0, centerX)
        centerY = Math.Max(0, centerY)

        LabelSoal.Location = New Point(centerX, centerY)
    End Sub

    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click
        Dim fhasil As New FHasil
        fhasil.Show()
    End Sub
End Class