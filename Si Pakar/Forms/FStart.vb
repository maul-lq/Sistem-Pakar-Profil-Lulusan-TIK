Public Class FStart
    ' Koleksi quote inspiratif untuk Sistem Pakar Profil Lulusan TIK
    Private quotes As String() = {
        "Sukses adalah perjalanan, bukan destinasi",
        "Teknologi membuka peluang tanpa batas",
        "Kreativitas dan inovasi adalah kunci masa depan",
        "Belajar adalah investasi terbaik untuk diri sendiri",
        "Setiap tantangan adalah peluang untuk tumbuh",
        "Dedikasi dan kerja keras menghasilkan kesuksesan",
        "Teknologi Informasi mengubah dunia",
        "Passion dan profesionalisme membuat perbedaan",
        "Masa depan milik mereka yang berani bermimpi",
        "Kualitas adalah prioritas utama kami"
    }

    Private subtitles As String() = {
        "Temukan profil karir terbaik Anda",
        "Identifikasi kekuatan dan potensi Anda",
        "Panduan cerdas untuk memilih karir TIK",
        "Raih kesempatan emas di dunia teknologi",
        "Wujudkan impian karir Anda sekarang",
        "Jelajahi peluang tanpa batas di TIK",
        "Pilih jalur karir yang tepat untuk Anda",
        "Sistem pakar untuk kesuksesan karir Anda",
        "Ayo mulai perjalanan karir Anda",
        "Jadilah bagian dari generasi teknologi"
    }

    Private Sub ButtonLakukanTes_Click(sender As Object, e As EventArgs) Handles ButtonLakukanTes.Click
        Dim fmain As New FMain
        fmain.Show()
        Me.Hide()
    End Sub

    Private Sub FStart_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()
    End Sub

    Private Sub FStart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
#If DEBUG Then
        Me.Text &= " (DEBUG)"
#End If
        ' Random quote dan subtitle generator
        Dim random As New Random()
        Dim quoteIndex As Integer = random.Next(quotes.Length)
        Dim subtitleIndex As Integer = random.Next(subtitles.Length)

        LabelQuote.Text = quotes(quoteIndex)
        LabelSubtitle.Text = subtitles(subtitleIndex)

        ' Atur MaximumSize untuk memungkinkan text wrap
        LabelQuote.MaximumSize = New Size(500, 0)
        LabelSubtitle.MaximumSize = New Size(500, 0)

        ' AutoSize akan menyesuaikan ukuran label berdasarkan text
        LabelQuote.AutoSize = True
        LabelSubtitle.AutoSize = True

        Dim centerX As Integer = Convert.ToInt32((Panel1.Size.Width - LabelQuote.Width) \ 2)
        Dim centerY As Integer = Convert.ToInt32((Panel1.Size.Height - LabelQuote.Height) \ 2) - 68

        ' Jangan beri nilai negatif jika label lebih besar dari panel
        centerX = Math.Max(0, centerX)
        centerY = Math.Max(0, centerY)

        LabelQuote.Location = New Point(centerX, centerY)

        centerX = Convert.ToInt32((Panel1.Size.Width - LabelSubtitle.Width) \ 2)
        centerY = Convert.ToInt32((Panel1.Size.Height - LabelSubtitle.Height) \ 2) + 88

        ' Jangan beri nilai negatif jika label lebih besar dari panel
        centerX = Math.Max(0, centerX)
        centerY = Math.Max(0, centerY)

        LabelSubtitle.Location = New Point(centerX, centerY)
    End Sub
End Class