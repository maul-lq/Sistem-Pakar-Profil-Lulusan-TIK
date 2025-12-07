Imports Microsoft.Data.SqlClient

Public Class FHasil
    Public sessionId As Integer
    Private hasilList As List(Of HasilProfesi)

    Private Sub FHasil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' 1. Hitung hasil akhir
            hasilList = HitungHasilAkhir(sessionId)

            ' 2. Simpan ke database
            SimpanHasilKeDatabase(sessionId, hasilList)

            ' 3. Tampilkan di form
            TampilkanHasil()

        Catch ex As Exception
            MessageBox.Show("Error saat memproses hasil: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TampilkanHasil()
        ' Sort berdasarkan skor akhir
        Dim sortedList = hasilList.OrderByDescending(Function(x) x.SkorAkhir).ToList()

        ' Top 3 Karir
        If sortedList.Count >= 1 Then
            LabelKarir1st.Text = $"{sortedList(0).NamaProfesi} ({Math.Round(sortedList(0).SkorAkhir, 2)})"
        End If
        If sortedList.Count >= 2 Then
            LabelKarir2nd.Text = $"{sortedList(1).NamaProfesi} ({Math.Round(sortedList(1).SkorAkhir, 2)})"
        End If
        If sortedList.Count >= 3 Then
            LabelKarir3Rd.Text = $"{sortedList(2).NamaProfesi} ({Math.Round(sortedList(2).SkorAkhir, 2)})"
        End If

        ' Zona Karir (ambil yang terbaik dari setiap zona)
        Dim goldenMatch = sortedList.Where(Function(x) x.Zona = "Golden Match").FirstOrDefault()
        Dim hiddenGem = sortedList.Where(Function(x) x.Zona = "Hidden Gem").FirstOrDefault()
        Dim realityCheck = sortedList.Where(Function(x) x.Zona = "Reality Check").FirstOrDefault()
        Dim explorer = sortedList.Where(Function(x) x.Zona = "Explorer").FirstOrDefault()

        LabelGoldeMatch.Text = If(goldenMatch IsNot Nothing, goldenMatch.NamaProfesi, "Tidak Ada")
        LabelHiddenGem.Text = If(hiddenGem IsNot Nothing, hiddenGem.NamaProfesi, "Tidak Ada")
        LabelRealityCheck.Text = If(realityCheck IsNot Nothing, realityCheck.NamaProfesi, "Tidak Ada")
        LabelExplorer.Text = If(explorer IsNot Nothing, explorer.NamaProfesi, "Tidak Ada")

        ' Tampilkan info linearitas di MessageBox (atau bisa ditambah label baru)
        Dim userData = GetUserDataForSession(sessionId)
        Dim top1Rumpun = sortedList(0).KodeRumpun
        Dim statusLinear = AnalisisLinearitas(userData("kode_prodi"), top1Rumpun)

        Dim pesanLinear As String = ""
        Select Case statusLinear
            Case "Linear"
                pesanLinear = "✅ Karir terbaik Anda SEJALAN dengan jurusan Anda!"
            Case "Related"
                pesanLinear = "⚠️ Karir terbaik Anda masih TERKAIT dengan jurusan Anda."
            Case "Pivot"
                pesanLinear = "🔄 Karir terbaik Anda TIDAK RELEVAN dengan jurusan Anda. Pertimbangkan upskilling!"
            Case Else
                pesanLinear = "❓ Status linearitas tidak dapat ditentukan."
        End Select

        MessageBox.Show($"Analisis Linearitas:{vbCrLf}{vbCrLf}{pesanLinear}{vbCrLf}{vbCrLf}Jurusan: {GetNamaProdi(userData("kode_prodi"))}{vbCrLf}Karir Terbaik: {sortedList(0).NamaProfesi}",
                       "Hasil Analisis",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information)
    End Sub

    Private Function GetUserDataForSession(sid As Integer) As Dictionary(Of String, String)
        Dim data As New Dictionary(Of String, String)
        Using conn = GetConnection()
            conn.Open()
            Dim q As String = "SELECT u.[kode prodi], u.[nama] FROM [Data User] u JOIN [Sesi Ujian] s ON u.[Id user] = s.[Id user] WHERE s.[Id sesi] = @sid"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@sid", sid)
            Dim reader = cmd.ExecuteReader()
            If reader.Read() Then
                data("kode_prodi") = reader("kode prodi").ToString()
                data("nama") = reader("nama").ToString()
            End If
        End Using
        Return data
    End Function

    Private Function GetNamaProdi(kodeProdi As String) As String
        Using conn = GetConnection()
            conn.Open()
            Dim q As String = "SELECT [nama prodi] FROM Prodi WHERE [kode prodi] = @kp"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@kp", kodeProdi)
            Dim result = cmd.ExecuteScalar()
            Return If(result IsNot Nothing, result.ToString(), kodeProdi)
        End Using
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonKembaliKeAwal.Click
        FStart.Show()
        Hide()
    End Sub

    Private Sub FHasil_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub

    Private Sub ButtonPrint_Click(sender As Object, e As EventArgs) Handles ButtonPrint.Click
        ' TODO: Implementasi print/export PDF hasil
        MessageBox.Show("Fitur print akan segera hadir!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class