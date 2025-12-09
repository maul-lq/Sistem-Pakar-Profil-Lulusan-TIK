Imports Microsoft.Data.SqlClient

Public Class FHasil
    Public sessionId As Integer
    Private hasilList As List(Of HasilProfesi)

    Private Sub FHasil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
#If DEBUG Then
        Me.Text &= " (DEBUG)"
#End If
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

        ' Set user labels (nama, email)
        Dim userData = GetUserDataForSession(sessionId)
        If userData.ContainsKey("nama") Then
            LabelUsername.Text = "Nama: " & userData("nama")
        End If
        If userData.ContainsKey("email") Then
            LabelEmail.Text = "Email: " & userData("email")
        End If

#If DEBUG Then
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
#Else
        ' Top 3 Karir
        If sortedList.Count >= 1 Then
            LabelKarir1st.Text = $"{sortedList(0).NamaProfesi}"
        End If
        If sortedList.Count >= 2 Then
            LabelKarir2nd.Text = $"{sortedList(1).NamaProfesi}"
        End If
        If sortedList.Count >= 3 Then
            LabelKarir3Rd.Text = $"{sortedList(2).NamaProfesi}"
        End If
#End If


        Dim top3 = sortedList.Take(3).ToList()

        Dim goldenMatch = top3.FirstOrDefault(Function(x) x.Zona = "Golden Match")
        If goldenMatch Is Nothing Then goldenMatch = sortedList.Where(Function(x) x.Zona = "Golden Match").FirstOrDefault()

        Dim hiddenGem = top3.FirstOrDefault(Function(x) x.Zona = "Hidden Gem")
        If hiddenGem Is Nothing Then hiddenGem = sortedList.Where(Function(x) x.Zona = "Hidden Gem").FirstOrDefault()

        Dim realityCheck = top3.FirstOrDefault(Function(x) x.Zona = "Reality Check")
        If realityCheck Is Nothing Then realityCheck = sortedList.Where(Function(x) x.Zona = "Reality Check").FirstOrDefault()

        Dim explorer = top3.FirstOrDefault(Function(x) x.Zona = "Explorer")
        If explorer Is Nothing Then explorer = sortedList.Where(Function(x) x.Zona = "Explorer").FirstOrDefault()

        LabelGoldeMatch.Text = If(goldenMatch IsNot Nothing, goldenMatch.NamaProfesi, "Tidak Ada")
        LabelHiddenGem.Text = If(hiddenGem IsNot Nothing, hiddenGem.NamaProfesi, "Tidak Ada")
        LabelRealityCheck.Text = If(realityCheck IsNot Nothing, realityCheck.NamaProfesi, "Tidak Ada")
        LabelExplorer.Text = If(explorer IsNot Nothing, explorer.NamaProfesi, "Tidak Ada")

        ' Tampilkan info linearitas in MessageBox
        Dim pesanLinear As New System.Text.StringBuilder()

        pesanLinear.AppendLine("🎓 ANALISIS LINEARITAS TOP 3 KARIR")
        pesanLinear.AppendLine("=" & New String("="c, 15))
        pesanLinear.AppendLine($"📚 Program Studi: {GetNamaProdi(userData("kode_prodi"))}")
        pesanLinear.AppendLine("")

        For i As Integer = 0 To Math.Min(2, sortedList.Count - 1)
            Dim profesi = sortedList(i)
            Dim statusLinear = AnalisisLinearitas(userData("kode_prodi"), profesi.KodeRumpun)
            Dim icon As String = ""
            Dim keterangan As String = ""

            Select Case statusLinear
                Case "Linear"
                    icon = "✅"
                    keterangan = "SEJALAN dengan jurusan Anda"
                Case "Related"
                    icon = "⚠️"
                    keterangan = "TERKAIT dengan jurusan Anda"
                Case "Pivot"
                    icon = "🔄"
                    keterangan = "TIDAK RELEVAN - perlu upskilling"
                Case Else
                    icon = "❓"
                    keterangan = "Status tidak diketahui"
            End Select

            pesanLinear.AppendLine($"#{i + 1}. {profesi.NamaProfesi}")
            pesanLinear.AppendLine($"    {icon} {statusLinear} - {keterangan}")
            pesanLinear.AppendLine("")
        Next

        pesanLinear.AppendLine("=" & New String("="c, 15))
        pesanLinear.AppendLine("💡 Rekomendasi:")
        pesanLinear.AppendLine("   ✅ Linear   : Fokus ke jalur ini!")
        pesanLinear.AppendLine("   ⚠️ Related : Pertimbangkan dengan matang")
        pesanLinear.AppendLine("   🔄 Pivot   : Perlu pembelajaran ekstra")

        MessageBox.Show(pesanLinear.ToString(),
                       "Analisis Linearitas",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information)
    End Sub

    Private Function GetUserDataForSession(sid As Integer) As Dictionary(Of String, String)
        Dim data As New Dictionary(Of String, String)
        Using conn = GetConnection()
            conn.Open()
            Dim q As String = "SELECT u.[kode prodi], u.[nama], u.[email] FROM [Data User] u JOIN [Sesi Ujian] s ON u.[Id user] = s.[Id user] WHERE s.[Id sesi] = @sid"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@sid", sid)
            Dim reader = cmd.ExecuteReader()
            If reader.Read() Then
                data("kode_prodi") = reader("kode prodi").ToString()
                data("nama") = reader("nama").ToString()
                data("email") = If(IsDBNull(reader("email")), "", reader("email").ToString())
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

    Private Sub FHasil_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
        hapusHasilTesIni(sessionId, CheckBoxDonSev.Checked)
    End Sub

    Private Sub ButtonPrint_Click(sender As Object, e As EventArgs) Handles ButtonPrint.Click
        ' TODO: Implementasi print/export PDF hasil
        MessageBox.Show("Fitur print akan segera hadir!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ButtonKembaliKeAwal_Click(sender As Object, e As EventArgs) Handles ButtonKembaliKeAwal.Click
        hapusHasilTesIni(sessionId, CheckBoxDonSev.Checked)
        FStart.Show()
        Hide()
    End Sub

    Private Sub ButtonGantiDataUser_Click(sender As Object, e As EventArgs) Handles ButtonGantiDataUser.Click
        ' Open update dialog with current values fetched from DB (ensure accurate)
        Dim userData = GetUserDataForSession(sessionId)
        Dim FDataUser As New FUpdateDataUser()
        If userData.ContainsKey("nama") Then FDataUser.nama = userData("nama")
        If userData.ContainsKey("email") Then FDataUser.email = userData("email")
        FDataUser.idSesi = sessionId
        Dim dr = FDataUser.ShowDialog()
        If dr = DialogResult.OK Then
            ' Refresh displayed name/email after successful update
            Dim refreshed = GetUserDataForSession(sessionId)
            If refreshed.ContainsKey("nama") Then LabelUsername.Text = $"Nama: {refreshed("nama")}"
            If refreshed.ContainsKey("email") Then LabelEmail.Text = $"Email: {refreshed("email")}"
        End If
    End Sub
End Class