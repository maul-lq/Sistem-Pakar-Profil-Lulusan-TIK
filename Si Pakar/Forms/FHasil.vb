Imports System.Drawing.Printing
Imports System.Drawing.Drawing2D
Imports Microsoft.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting
Imports Si_Pakar.My.Resources.ImageRes

Public Class FHasil
    Public sessionId As Integer
    Private hasilList As List(Of HasilProfesi)

    ' ==========================================
    ' KONFIGURASI PRINT LAYOUT
    ' ==========================================
    Private Const MARGIN_LEFT As Integer = 50
    Private Const MARGIN_TOP As Integer = 50
    Private Const MARGIN_RIGHT As Integer = 50
    Private Const MARGIN_BOTTOM As Integer = 50
    Private Const LOGO_SIZE As Integer = 80
    Private Const CHART_WIDTH As Integer = 350
    Private Const CHART_HEIGHT As Integer = 250

    ' Warna tema (ubah sesuai keinginan)
    Private ReadOnly COLOR_PRIMARY As Color = Color.FromArgb(51, 102, 153)
    Private ReadOnly COLOR_SECONDARY As Color = Color.FromArgb(102, 153, 204)
    Private ReadOnly COLOR_ACCENT As Color = Color.FromArgb(255, 153, 51)
    Private ReadOnly COLOR_HIGHLIGHT As Color = Color.FromArgb(46, 204, 113) ' Hijau untuk tertinggi

    ' Font settings
    Private ReadOnly FONT_TITLE As New Font("Arial", 16, FontStyle.Bold)
    Private ReadOnly FONT_SUBTITLE As New Font("Arial", 12, FontStyle.Bold)
    Private ReadOnly FONT_BODY As New Font("Arial", 10, FontStyle.Regular)
    Private ReadOnly FONT_SMALL As New Font("Arial", 8, FontStyle.Regular)

    ' Print state management
    Private currentPage As Integer = 0
    Private totalPages As Integer = 4 ' Tambah 1 halaman untuk split content

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
        ' Top 3 Karir - DEBUG: Tampilkan sebagai desimal (0.00 - 1.00)
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
        ' Top 3 Karir - RELEASE: Tampilkan sebagai persentase (0% - 100%)
        If sortedList.Count >= 1 Then
            LabelKarir1st.Text = $"{sortedList(0).NamaProfesi} ({Math.Round(sortedList(0).SkorAkhir * 100, 1)}%)"
        End If
        If sortedList.Count >= 2 Then
            LabelKarir2nd.Text = $"{sortedList(1).NamaProfesi} ({Math.Round(sortedList(1).SkorAkhir * 100, 1)}%)"
        End If
        If sortedList.Count >= 3 Then
            LabelKarir3Rd.Text = $"{sortedList(2).NamaProfesi} ({Math.Round(sortedList(2).SkorAkhir * 100, 1)}%)"
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
        Try
            ' Set document untuk print
            DokumenHasilTes.DocumentName = $"Laporan_Hasil_{DateTime.Now:yyyyMMdd_HHmmss}"

            ' Setup print dialog dengan preview
            PrintPreviewDialogTesDoc.Document = DokumenHasilTes

            ' Tampilkan print preview dialog
            If PrintPreviewDialogTesDoc.ShowDialog() = DialogResult.OK Then
                ' Jika user klik print dari preview, document sudah ter-handle
                ' Tidak perlu action tambahan
            End If

        Catch ex As Exception
            MessageBox.Show("Error saat print: " & ex.Message & vbCrLf & ex.StackTrace,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ButtonKembaliKeAwal_Click(sender As Object, e As EventArgs) Handles ButtonKembaliKeAwal.Click
        hapusHasilTesIni(sessionId, CheckBoxDonSev.Checked)
        FStart.Show()
        Hide()
    End Sub

    Private Sub ButtonGantiDataUser_Click(sender As Object, e As EventArgs) Handles ButtonGantiDataUser.Click
        Dim userData = GetUserDataForSession(sessionId)
        Dim FDataUser As New FUpdateDataUser()
        If userData.ContainsKey("nama") Then FDataUser.nama = userData("nama")
        If userData.ContainsKey("email") Then FDataUser.email = userData("email")
        FDataUser.idSesi = sessionId
        Dim dr = FDataUser.ShowDialog()
        If dr = DialogResult.OK Then
            Dim refreshed = GetUserDataForSession(sessionId)
            If refreshed.ContainsKey("nama") Then LabelUsername.Text = $"Nama: {refreshed("nama")}"

            If refreshed.ContainsKey("email") Then LabelEmail.Text = $"Email: {refreshed("email")}"
        End If
    End Sub

    ' ==========================================
    ' PRINT DOCUMENT HANDLER
    ' ==========================================
    Private Sub DokumenHasilTes_PrintPage(sender As PrintDocument, e As PrintPageEventArgs) Handles DokumenHasilTes.PrintPage
        Try
            ' Set high quality rendering
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            e.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

            currentPage += 1

            Select Case currentPage
                Case 1
                    PrintPage1_HasilDeteksi(e)
                Case 2
                    PrintPage2_Fase1(e)
                Case 3
                    PrintPage3_Fase2Part1(e)
                Case 4
                    PrintPage4_Fase2Part2(e)
            End Select

            ' Ada halaman berikutnya?
            e.HasMorePages = (currentPage < totalPages)

        Catch ex As Exception
            MessageBox.Show("Error saat print halaman " & currentPage & ": " & ex.Message & vbCrLf & ex.StackTrace,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            e.HasMorePages = False
        End Try
    End Sub

    ' ==========================================
    ' HALAMAN 1: HASIL DETEKSI (FIXED CHART DISPOSAL)
    ' ==========================================
    Private Sub PrintPage1_HasilDeteksi(e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim pageWidth As Integer = e.PageBounds.Width
        Dim pageHeight As Integer = e.PageBounds.Height
        Dim y As Integer = MARGIN_TOP

        ' Header dengan logo dan judul
        DrawHeader(g, pageWidth, y, "LAPORAN HASIL DETEKSI PROFIL LULUSAN TIK")
        y += 120

        ' Info user
        Dim userData = GetUserDataForSession(sessionId)
        g.DrawString($"Nama: {userData("nama")}", FONT_BODY, Brushes.Black, MARGIN_LEFT, y)
        y += 20
        g.DrawString($"Program Studi: {GetNamaProdi(userData("kode_prodi"))}", FONT_BODY, Brushes.Black, MARGIN_LEFT, y)
        y += 20
        g.DrawString($"Email: {userData("email")}", FONT_BODY, Brushes.Black, MARGIN_LEFT, y)
        y += 40

        ' SPLIT VERTICAL LAYOUT: Bagi jadi 2 kolom
        Dim colWidth = (pageWidth - MARGIN_LEFT - MARGIN_RIGHT - 30) \ 2
        Dim col1X = MARGIN_LEFT
        Dim col2X = MARGIN_LEFT + colWidth + 30

        ' Section: Hasil Deteksi
        g.DrawString("HASIL DETEKSI", FONT_SUBTITLE, New SolidBrush(COLOR_PRIMARY), col1X, y)
        y += 35

        ' KOLOM 1: Top 3 Profesi Chart (FIXED: Proper disposal)
        Dim yCol1 = y
        Dim chartTop3 As Chart = Nothing
        Dim chartBmp As Bitmap = Nothing

        Try
            chartTop3 = BuatPieChartTop3()
            chartTop3.Size = New Size(colWidth, 220)
            chartBmp = New Bitmap(colWidth, 220)
            chartTop3.DrawToBitmap(chartBmp, New Rectangle(0, 0, colWidth, 220))
            g.DrawImage(chartBmp, col1X, yCol1)
        Finally
            ' PENTING: Dispose chart dan bitmap
            If chartBmp IsNot Nothing Then chartBmp.Dispose()
            If chartTop3 IsNot Nothing Then chartTop3.Dispose()
        End Try

        ' KOLOM 2: Deskripsi Top 3 Profesi
        Dim yCol2 = y
        g.DrawString("Deskripsi Profesi:", FONT_BODY, New SolidBrush(COLOR_PRIMARY), col2X, yCol2)
        yCol2 += 25

        Dim sortedList = hasilList.OrderByDescending(Function(x) x.SkorAkhir).ToList()
        For i As Integer = 0 To Math.Min(2, sortedList.Count - 1)
            Dim profesi = sortedList(i)
            Dim deskripsi = GetDeskripsiProfesi(profesi.KodeProfesi)

            ' Medal icon
            Dim medal = If(i = 0, "🥇", If(i = 1, "🥈", "🥉"))
            g.DrawString($"{medal} #{i + 1}. {profesi.NamaProfesi}",
                        New Font("Arial", 9, FontStyle.Bold), Brushes.Black, col2X, yCol2)
            yCol2 += 18

            g.DrawString($"Skor: {Math.Round(profesi.SkorAkhir * 100, 1)}%",
                        FONT_SMALL, New SolidBrush(COLOR_ACCENT), col2X + 10, yCol2)
            yCol2 += 15

            ' Wrap deskripsi dengan lebar kolom
            Dim deskLines = WrapText(deskripsi, colWidth - 20, g, FONT_SMALL)
            For j = 0 To Math.Min(2, deskLines.Count - 1) ' Max 3 baris per profesi
                g.DrawString(deskLines(j), FONT_SMALL, Brushes.DarkGray, col2X + 10, yCol2)
                yCol2 += 13
            Next
            yCol2 += 10

            ' Stop jika overflow
            If yCol2 > pageHeight - MARGIN_BOTTOM - 100 Then Exit For
        Next

        ' Footer
        DrawFooter(g, pageWidth, e.PageBounds.Height, 1, totalPages)
    End Sub

    ' ==========================================
    ' HALAMAN 2: FASE 1 (FIXED CHART DISPOSAL)
    ' ==========================================
    Private Sub PrintPage2_Fase1(e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim pageWidth As Integer = e.PageBounds.Width
        Dim pageHeight As Integer = e.PageBounds.Height
        Dim y As Integer = MARGIN_TOP

        DrawHeader(g, pageWidth, y, "RESPON PENGGUNA - FASE 1")
        y += 120

        ' SPLIT VERTICAL: Kolom 1 = Chart, Kolom 2 = Tabel (5 pertanyaan pertama)
        Dim colWidth = (pageWidth - MARGIN_LEFT - MARGIN_RIGHT - 30) \ 2
        Dim col1X = MARGIN_LEFT
        Dim col2X = MARGIN_LEFT + colWidth + 30

        ' KOLOM 1: Pie Chart Rumpun (FIXED: Proper disposal)
        g.DrawString("Diagram Skor Per Rumpun", FONT_SUBTITLE, New SolidBrush(COLOR_PRIMARY), col1X, y)
        Dim yCol1 = y + 30

        Dim chartFase1 As Chart = Nothing
        Dim chartBmp As Bitmap = Nothing

        Try
            chartFase1 = BuatPieChartFase1()
            chartFase1.Size = New Size(colWidth, 250)
            chartBmp = New Bitmap(colWidth, 250)
            chartFase1.DrawToBitmap(chartBmp, New Rectangle(0, 0, colWidth, 250))
            g.DrawImage(chartBmp, col1X, yCol1)
        Finally
            If chartBmp IsNot Nothing Then chartBmp.Dispose()
            If chartFase1 IsNot Nothing Then chartFase1.Dispose()
        End Try

        ' KOLOM 2: Tabel Pertanyaan (5 pertama) - MULAI DARI 1
        g.DrawString("Pertanyaan (1-5)", FONT_SUBTITLE, New SolidBrush(COLOR_PRIMARY), col2X, y)
        Dim yCol2 = y + 30

        Dim pertanyaanFase1 = GetPertanyaanDenganJawaban(sessionId, 1)
        Dim pertanyaan5Pertama = pertanyaanFase1.Take(5).ToList()
        yCol2 = DrawTabelPertanyaanCompact(g, col2X, colWidth - 20, yCol2, pertanyaan5Pertama, 5, 1)

        ' Bawah: 5 pertanyaan terakhir (full width) - LANJUT DARI 6
        y = Math.Max(yCol1 + 260, yCol2) + 20
        g.DrawString("Pertanyaan (6-10)", FONT_SUBTITLE, New SolidBrush(COLOR_PRIMARY), MARGIN_LEFT, y)
        y += 30

        Dim pertanyaan5Terakhir = pertanyaanFase1.Skip(5).Take(5).ToList()
        y = DrawTabelPertanyaanCompact(g, MARGIN_LEFT, pageWidth - MARGIN_LEFT - MARGIN_RIGHT, y, pertanyaan5Terakhir, 5, 6)

        DrawFooter(g, pageWidth, e.PageBounds.Height, 2, totalPages)
    End Sub

    ' ==========================================
    ' HALAMAN 3: FASE 2 BAR CHART (FIXED CHART DISPOSAL)
    ' ==========================================
    Private Sub PrintPage3_Fase2Part1(e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim pageWidth As Integer = e.PageBounds.Width
        Dim y As Integer = MARGIN_TOP

        DrawHeader(g, pageWidth, y, "RESPON PENGGUNA - FASE 2 (Part 1)")
        y += 120

        ' Bar Chart dengan fix: hanya profesi yang diujikan (FIXED: Proper disposal)
        g.DrawString("Diagram Skor Per Profesi (Fase 2)", FONT_SUBTITLE, New SolidBrush(COLOR_PRIMARY), MARGIN_LEFT, y)
        y += 35

        Dim chartFase2 As Chart = Nothing
        Dim chartBmp As Bitmap = Nothing

        Try
            chartFase2 = BuatBarChartFase2Fixed()
            Dim chartWidth = pageWidth - MARGIN_LEFT - MARGIN_RIGHT
            chartFase2.Size = New Size(chartWidth, 350)
            chartBmp = New Bitmap(chartWidth, 350)
            chartFase2.DrawToBitmap(chartBmp, New Rectangle(0, 0, chartWidth, 350))
            g.DrawImage(chartBmp, MARGIN_LEFT, y)
        Finally
            If chartBmp IsNot Nothing Then chartBmp.Dispose()
            If chartFase2 IsNot Nothing Then chartFase2.Dispose()
        End Try

        y += 370

        ' Tabel 10 pertanyaan pertama (split 2 kolom)
        g.DrawString("Pertanyaan Fase 2 (1-10)", FONT_SUBTITLE, New SolidBrush(COLOR_PRIMARY), MARGIN_LEFT, y)
        y += 30

        Dim colWidth = (pageWidth - MARGIN_LEFT - MARGIN_RIGHT - 30) \ 2
        Dim col1X = MARGIN_LEFT
        Dim col2X = MARGIN_LEFT + colWidth + 30

        Dim pertanyaanFase2 = GetPertanyaanDenganJawaban(sessionId, 2)
        Dim pertanyaan5A = pertanyaanFase2.Take(5).ToList()
        Dim pertanyaan5B = pertanyaanFase2.Skip(5).Take(5).ToList()

        ' KOLOM KIRI: Nomor 1-5
        DrawTabelPertanyaanCompact(g, col1X, colWidth - 10, y, pertanyaan5A, 5, 1)
        ' KOLOM KANAN: Nomor 6-10
        DrawTabelPertanyaanCompact(g, col2X, colWidth - 10, y, pertanyaan5B, 5, 6)

        DrawFooter(g, pageWidth, e.PageBounds.Height, 3, totalPages)
    End Sub

    ' ==========================================
    ' HALAMAN 4: FASE 2 PART 2 (FIXED)
    ' ==========================================
    Private Sub PrintPage4_Fase2Part2(e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim pageWidth As Integer = e.PageBounds.Width
        Dim y As Integer = MARGIN_TOP

        DrawHeader(g, pageWidth, y, "RESPON PENGGUNA - FASE 2 (Part 2)")
        y += 120

        g.DrawString("Pertanyaan Fase 2 (11-20)", FONT_SUBTITLE, New SolidBrush(COLOR_PRIMARY), MARGIN_LEFT, y)
        y += 30

        ' Split 2 kolom
        Dim colWidth = (pageWidth - MARGIN_LEFT - MARGIN_RIGHT - 30) \ 2
        Dim col1X = MARGIN_LEFT
        Dim col2X = MARGIN_LEFT + colWidth + 30

        Dim pertanyaanFase2 = GetPertanyaanDenganJawaban(sessionId, 2)
        Dim pertanyaan5A = pertanyaanFase2.Skip(10).Take(5).ToList()
        Dim pertanyaan5B = pertanyaanFase2.Skip(15).Take(5).ToList()

        ' KOLOM KIRI: Nomor 11-15
        DrawTabelPertanyaanCompact(g, col1X, colWidth - 10, y, pertanyaan5A, 5, 11)
        ' KOLOM KANAN: Nomor 16-20
        DrawTabelPertanyaanCompact(g, col2X, colWidth - 10, y, pertanyaan5B, 5, 16)

        DrawFooter(g, pageWidth, e.PageBounds.Height, 4, totalPages)
    End Sub

    ' ==========================================
    ' FIX BAR CHART: VERTICAL BARS YANG BENAR
    ' ==========================================
    Private Function BuatBarChartFase2Fixed() As Chart
        Dim chart As New Chart()
        chart.Size = New Size(700, 350)

        ' Setup Chart Area
        Dim chartArea As New ChartArea("MainArea")
        chartArea.BackColor = Color.White

        ' AxisX (horizontal - untuk nama profesi)
        chartArea.AxisX.Title = "Profesi"
        chartArea.AxisX.TitleFont = New Font("Arial", 8, FontStyle.Bold)
        chartArea.AxisX.Interval = 1
        chartArea.AxisX.IntervalType = DateTimeIntervalType.Number
        chartArea.AxisX.LabelStyle.Angle = -45
        chartArea.AxisX.LabelStyle.Font = New Font("Arial", 6)
        chartArea.AxisX.MajorGrid.Enabled = False
        chartArea.AxisX.IsLabelAutoFit = True

        ' AxisY (vertical - untuk skor)
        chartArea.AxisY.Title = "Skor (%)"
        chartArea.AxisY.TitleFont = New Font("Arial", 9, FontStyle.Bold)
        chartArea.AxisY.Minimum = 0
        chartArea.AxisY.Maximum = 100
        chartArea.AxisY.Interval = 10
        chartArea.AxisY.LabelStyle.Font = New Font("Arial", 7)
        chartArea.AxisY.MajorGrid.LineColor = Color.LightGray
        chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot

        chart.ChartAreas.Add(chartArea)

        ' Setup Series
        Dim series As New Series("Skor Profesi")
        series.ChartArea = "MainArea"
        series.ChartType = SeriesChartType.Column ' VERTICAL BARS
        series.IsValueShownAsLabel = True
        series.LabelFormat = "0.0"
        series.Font = New Font("Arial", 6, FontStyle.Bold)

        ' FIX: Ambil hanya profesi yang DIUJIKAN di Phase 2
        Dim profesiDiujikan = GetProfesiDiujikanFase2(sessionId)

        ' Gabungkan dengan skor dari hasilList
        Dim profesiDenganSkor As New List(Of HasilProfesi)()
        For Each kodeProfesi In profesiDiujikan
            Dim profesi = hasilList.FirstOrDefault(Function(x) x.KodeProfesi = kodeProfesi)
            If profesi IsNot Nothing Then
                profesiDenganSkor.Add(profesi)
            End If
        Next

        ' Urutkan dari skor tertinggi ke terendah
        profesiDenganSkor = profesiDenganSkor.OrderByDescending(Function(x) x.SkorAkhir).ToList()

        ' Cari skor tertinggi untuk highlight
        Dim skorTertinggi = If(profesiDenganSkor.Count > 0, profesiDenganSkor(0).SkorAkhir, 0)

        ' Tambahkan data points dengan EXPLICIT X dan Y values
        For i As Integer = 0 To profesiDenganSkor.Count - 1
            Dim profesi = profesiDenganSkor(i)
            Dim skorPersen = profesi.SkorAkhir * 100

            ' KUNCI: Tambahkan X (index) dan Y (skor) secara eksplisit
            Dim pointIndex = series.Points.AddXY(i, skorPersen)

            ' Set axis label untuk X
            series.Points(pointIndex).AxisLabel = profesi.NamaProfesi

            ' Set warna: hijau untuk tertinggi, biru untuk lainnya
            If profesi.SkorAkhir = skorTertinggi Then
                series.Points(pointIndex).Color = COLOR_HIGHLIGHT
                series.Points(pointIndex).Label = $"★ {Math.Round(skorPersen, 1)}%"
                series.Points(pointIndex).LabelForeColor = Color.DarkGreen
                series.Points(pointIndex).Font = New Font("Arial", 7, FontStyle.Bold)
            Else
                series.Points(pointIndex).Color = COLOR_SECONDARY
                series.Points(pointIndex).Label = $"{Math.Round(skorPersen, 1)}%"
                series.Points(pointIndex).LabelForeColor = Color.Black
            End If

            ' Set tooltip
            series.Points(pointIndex).ToolTip = $"{profesi.NamaProfesi}: {Math.Round(skorPersen, 1)}%"
        Next

        ' Set bar width (0.8 = 80% of space)
        series("PointWidth") = "0.8"

        chart.Series.Add(series)

        ' Optional: Add title
        Dim title As New Title()
        title.Text = "Skor Per Profesi (Phase 2)"
        title.Font = New Font("Arial", 9, FontStyle.Bold)
        title.ForeColor = COLOR_PRIMARY
        chart.Titles.Add(title)

        Return chart
    End Function

    ' ==========================================
    ' GET PROFESI YANG DIUJIKAN DI FASE 2
    ' ==========================================
    Private Function GetProfesiDiujikanFase2(sid As Integer) As List(Of String)
        Dim profesiList As New List(Of String)

        Using conn = GetConnection()
            conn.Open()
            Dim q = "SELECT DISTINCT p.[kode profesi] " &
                   "FROM [Exam Details] ed " &
                   "JOIN Pertanyaan p ON ed.[id pertanyaan] = p.[Id Pertanyaan] " &
                   "WHERE ed.[id sesi] = @sid AND p.[phase] = 2 AND p.[kode profesi] IS NOT NULL " &
                   "ORDER BY p.[kode profesi]"

            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@sid", sid)
            Dim reader = cmd.ExecuteReader()

            While reader.Read()
                profesiList.Add(reader("kode profesi").ToString())
            End While
        End Using

        Return profesiList
    End Function

    ' ==========================================
    ' HELPER: TABEL COMPACT (FIXED NUMBERING)
    ' ==========================================
    Private Function DrawTabelPertanyaanCompact(g As Graphics, startX As Integer, width As Integer, startY As Integer, pertanyaan As List(Of PertanyaanJawaban), maxRows As Integer, Optional startNum As Integer = 1) As Integer
        Dim y = startY
        Dim rowHeight = 45

        ' Header (lebih compact)
        g.FillRectangle(New SolidBrush(COLOR_PRIMARY), startX, y, 20, 18)
        g.FillRectangle(New SolidBrush(COLOR_PRIMARY), startX + 20, y, width - 120, 18)
        g.FillRectangle(New SolidBrush(COLOR_PRIMARY), startX + width - 100, y, 100, 18)

        g.DrawString("No", New Font("Arial", 7, FontStyle.Bold), Brushes.White, startX + 3, y + 3)
        g.DrawString("Pertanyaan", New Font("Arial", 7, FontStyle.Bold), Brushes.White, startX + 23, y + 3)
        g.DrawString("Confidence", New Font("Arial", 7, FontStyle.Bold), Brushes.White, startX + width - 95, y + 3)

        y += 18

        ' Baris data - FIX: Gunakan startNum untuk nomor awal
        For i = 0 To Math.Min(maxRows - 1, pertanyaan.Count - 1)
            Dim p = pertanyaan(i)

            ' Wrap text dengan lebar kolom yang lebih kecil
            Dim wrappedText = WrapText(p.TeksPertanyaan, width - 130, g, New Font("Arial", 7))
            Dim cellHeight = Math.Max(rowHeight, wrappedText.Count * 12 + 8)

            ' Border
            g.DrawRectangle(Pens.Gray, startX, y, 20, cellHeight)
            g.DrawRectangle(Pens.Gray, startX + 20, y, width - 120, cellHeight)
            g.DrawRectangle(Pens.Gray, startX + width - 100, y, 100, cellHeight)

            ' No - FIX: Gunakan startNum + i untuk nomor yang berlanjut
            g.DrawString((startNum + i).ToString(), New Font("Arial", 7), Brushes.Black, startX + 6, y + 5)

            ' Pertanyaan (wrapped, max 3 baris)
            Dim yText = y + 5
            For j = 0 To Math.Min(2, wrappedText.Count - 1)
                g.DrawString(wrappedText(j), New Font("Arial", 7), Brushes.Black, startX + 23, yText)
                yText += 12
            Next

            ' Confidence (lebih compact)
            Dim confidenceText = FormatConfidenceShort(p.UserConfidence)
            g.DrawString(confidenceText, New Font("Arial", 7), Brushes.Black, startX + width - 95, y + 5)

            y += cellHeight
        Next

        Return y + 10
    End Function

    ' ==========================================
    ' HELPER: FORMAT CONFIDENCE SHORT
    ' ==========================================
    Private Function FormatConfidenceShort(val As Double) As String
        Select Case val
            Case 0.0
                Return "STM (0%)"
            Case 0.2
                Return "KM (20%)"
            Case 0.5
                Return "CM (50%)"
            Case 0.8
                Return "M (80%)"
            Case 0.95
                Return "SM (95%)"
            Case Else
                Return "N/A"
        End Select
    End Function

    ' ==========================================
    ' HELPER METHODS
    ' ==========================================

    Private Sub DrawHeader(g As Graphics, pageWidth As Integer, y As Integer, title As String)
        ' Logo di kiri atas
        Try
            Dim logo As Image = Logo_Program
            g.DrawImage(logo, MARGIN_LEFT, y, LOGO_SIZE, LOGO_SIZE)
        Catch
            ' Jika logo tidak ada, skip
        End Try

        ' Judul di tengah
        Dim titleSize = g.MeasureString(title, FONT_TITLE)
        Dim titleX = (pageWidth - titleSize.Width) / 2
        g.DrawString(title, FONT_TITLE, New SolidBrush(COLOR_PRIMARY), titleX, y + 20)

        ' Garis pemisah
        g.DrawLine(New Pen(COLOR_PRIMARY, 2), MARGIN_LEFT, y + 100, pageWidth - MARGIN_RIGHT, y + 100)
    End Sub

    Private Sub DrawFooter(g As Graphics, pageWidth As Integer, pageHeight As Integer, currPage As Integer, totPage As Integer)
        Dim footerY = pageHeight - MARGIN_BOTTOM + 10
        g.DrawLine(Pens.Gray, MARGIN_LEFT, footerY, pageWidth - MARGIN_RIGHT, footerY)

        Dim footerText = $"Halaman {currPage} dari {totPage}"
        Dim footerSize = g.MeasureString(footerText, FONT_SMALL)
        g.DrawString(footerText, FONT_SMALL, Brushes.Gray, (pageWidth - footerSize.Width) / 2, footerY + 5)

        g.DrawString($"© {DateTime.Now.Year} Sistem Pakar Profil Lulusan TIK", FONT_SMALL, Brushes.Gray, MARGIN_LEFT, footerY + 5)
    End Sub

    Private Function BuatPieChartTop3() As Chart
        Dim chart As New Chart()
        chart.Size = New Size(CHART_WIDTH, CHART_HEIGHT)

        Dim chartArea As New ChartArea()
        chartArea.BackColor = Color.White
        chart.ChartAreas.Add(chartArea)

        Dim series As New Series()
        series.ChartType = SeriesChartType.Pie

        ' FIX OVERLAP: Disable label pada pie slice, gunakan legend saja
        series("PieLabelStyle") = "Disabled"

        Dim sortedList = hasilList.OrderByDescending(Function(x) x.SkorAkhir).Take(3).ToList()
        For Each profesi In sortedList
            Dim poin = series.Points.Add(profesi.SkorAkhir * 100)
            ' Tidak tampilkan label di slice untuk menghindari overlap
            poin.Label = ""
            poin.LegendText = $"{profesi.NamaProfesi} ({Math.Round(profesi.SkorAkhir * 100, 1)}%)"
        Next

        series.Font = New Font("Arial", 9)
        chart.Series.Add(series)

        Dim legend As New Legend()
        legend.Docking = Docking.Bottom
        legend.Font = New Font("Arial", 8)
        chart.Legends.Add(legend)

        Return chart
    End Function

    Private Function BuatPieChartFase1() As Chart
        Dim chart As New Chart()
        chart.Size = New Size(CHART_WIDTH, CHART_HEIGHT)

        Dim chartArea As New ChartArea()
        chartArea.BackColor = Color.White
        chart.ChartAreas.Add(chartArea)

        Dim series As New Series()
        series.ChartType = SeriesChartType.Pie

        ' FIX OVERLAP: Disable label pada pie slice
        series("PieLabelStyle") = "Disabled"

        Dim skorRumpun = GetSkorRumpunFase1(sessionId)
        For Each kvp In skorRumpun.OrderByDescending(Function(x) x.Value)
            Dim poin = series.Points.Add(kvp.Value * 100)
            poin.Label = ""
            poin.LegendText = $"{GetNamaRumpun(kvp.Key)} ({Math.Round(kvp.Value * 100, 1)}%)"
        Next

        series.Font = New Font("Arial", 9)
        chart.Series.Add(series)

        Dim legend As New Legend()
        legend.Docking = Docking.Bottom
        legend.Font = New Font("Arial", 8)
        chart.Legends.Add(legend)

        Return chart
    End Function

    Private Function BuatBarChartFase2() As Chart
        Dim chart As New Chart()
        chart.Size = New Size(700, 300)

        Dim chartArea As New ChartArea()
        chartArea.BackColor = Color.White
        chartArea.AxisX.LabelStyle.Angle = -45
        chartArea.AxisX.Interval = 1
        chartArea.AxisX.LabelStyle.Font = New Font("Arial", 6)
        chartArea.AxisY.Title = "Skor (%)"
        chartArea.AxisY.TitleFont = New Font("Arial", 8, FontStyle.Bold)
        chart.ChartAreas.Add(chartArea)

        Dim series As New Series()
        series.ChartType = SeriesChartType.Column ' Ganti ke Column agar lebih jelas
        series.Color = COLOR_SECONDARY

        ' FIX BAR CHART: Ambil SEMUA 25 profesi dan tampilkan dengan urutan skor tertinggi
        ' Filter hanya profesi yang skornya > 0 untuk kejelasan (opsional)
        Dim profesiDenganSkor = hasilList.Where(Function(x) x.SkorAkhir > 0).OrderByDescending(Function(x) x.SkorAkhir).ToList()

        ' Jika mau tampilkan SEMUA (termasuk skor 0), uncomment baris ini:
        ' Dim profesiDenganSkor = hasilList.OrderByDescending(Function(x) x.SkorAkhir).ToList()

        For Each profesi In profesiDenganSkor
            Dim point = series.Points.AddXY(profesi.NamaProfesi, profesi.SkorAkhir * 100)
            series.Points(series.Points.Count - 1).Label = Math.Round(profesi.SkorAkhir * 100, 1).ToString() & "%"
            series.Points(series.Points.Count - 1).LabelForeColor = Color.Black
        Next

        series.Font = New Font("Arial", 6)
        series.IsValueShownAsLabel = True
        chart.Series.Add(series)

        Return chart
    End Function

    Private Function DrawTabelPertanyaan(g As Graphics, pageWidth As Integer, y As Integer, pertanyaan As List(Of PertanyaanJawaban), maxRows As Integer) As Integer
        Dim colWidth = (pageWidth - MARGIN_LEFT - MARGIN_RIGHT - 50) \ 2
        Dim rowHeight = 60

        ' Header tabel
        g.FillRectangle(New SolidBrush(COLOR_PRIMARY), MARGIN_LEFT, y, 30, 20)
        g.FillRectangle(New SolidBrush(COLOR_PRIMARY), MARGIN_LEFT + 30, y, colWidth, 20)
        g.FillRectangle(New SolidBrush(COLOR_PRIMARY), MARGIN_LEFT + 30 + colWidth, y, 170, 20)

        g.DrawString("No", New Font("Arial", 9, FontStyle.Bold), Brushes.White, MARGIN_LEFT + 5, y + 3)
        g.DrawString("Pertanyaan", New Font("Arial", 9, FontStyle.Bold), Brushes.White, MARGIN_LEFT + 35, y + 3)
        g.DrawString("Confidence", New Font("Arial", 9, FontStyle.Bold), Brushes.White, MARGIN_LEFT + 35 + colWidth, y + 3)

        y += 20

        ' Baris data
        For i = 0 To Math.Min(maxRows - 1, pertanyaan.Count - 1)
            Dim p = pertanyaan(i)

            ' Wrap text pertanyaan
            Dim wrappedText = WrapText(p.TeksPertanyaan, colWidth - 10, g, FONT_SMALL)
            Dim cellHeight = Math.Max(rowHeight, wrappedText.Count * 15 + 10)

            ' Gambar border
            g.DrawRectangle(Pens.Gray, MARGIN_LEFT, y, 30, cellHeight)
            g.DrawRectangle(Pens.Gray, MARGIN_LEFT + 30, y, colWidth, cellHeight)
            g.DrawRectangle(Pens.Gray, MARGIN_LEFT + 30 + colWidth, y, 170, cellHeight)

            ' No
            g.DrawString((i + 1).ToString(), FONT_SMALL, Brushes.Black, MARGIN_LEFT + 10, y + 5)

            ' Pertanyaan (wrapped)
            Dim yText = y + 5
            For Each line In wrappedText
                g.DrawString(line, FONT_SMALL, Brushes.Black, MARGIN_LEFT + 35, yText)
                yText += 15
            Next

            ' Confidence
            Dim confidenceText = FormatConfidence(p.UserConfidence)
            g.DrawString(confidenceText, FONT_SMALL, Brushes.Black, MARGIN_LEFT + 35 + colWidth + 5, y + 5)

            y += cellHeight
        Next

        Return y + 20
    End Function

    Private Function WrapText(text As String, maxWidth As Integer, g As Graphics, font As Font) As List(Of String)
        Dim words = text.Split(" "c)
        Dim lines As New List(Of String)()
        Dim currentLine As String = ""

        For Each word In words
            Dim testLine = If(String.IsNullOrEmpty(currentLine), word, currentLine & " " & word)
            Dim size = g.MeasureString(testLine, font)

            If size.Width > maxWidth Then
                If Not String.IsNullOrEmpty(currentLine) Then
                    lines.Add(currentLine)
                    currentLine = word
                Else
                    lines.Add(word)
                    currentLine = ""
                End If
            Else
                currentLine = testLine
            End If
        Next

        If Not String.IsNullOrEmpty(currentLine) Then
            lines.Add(currentLine)
        End If

        Return lines
    End Function

    Private Function FormatConfidence(val As Double) As String
        Dim persen = Math.Round(val * 100, 0)
        Dim text = ""

        Select Case val
            Case 0.0
                text = "Sangat Tidak Mampu"
            Case 0.2
                text = "Kurang Mampu"
            Case 0.5
                text = "Cukup Mampu"
            Case 0.8
                text = "Mampu"
            Case 0.95
                text = "Sangat Mampu"
            Case Else
                text = "N/A"
        End Select

        Return $"{text} ({persen}%)"
    End Function

    ' ==========================================
    ' DATABASE HELPER METHODS
    ' ==========================================

    Private Function GetDeskripsiProfesi(kodeProfesi As String) As String
        Using conn = GetConnection()
            conn.Open()
            Dim q = "SELECT [deskripsi] FROM Profesi WHERE [kode profesi] = @kp"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@kp", kodeProfesi)
            Dim result = cmd.ExecuteScalar()
            Return If(result IsNot Nothing AndAlso Not IsDBNull(result), result.ToString(), "Tidak ada deskripsi")
        End Using
    End Function

    Private Function GetNamaRumpun(kodeRumpun As String) As String
        Using conn = GetConnection()
            conn.Open()
            Dim q = "SELECT [nama rumpun] FROM Rumpun WHERE [kode rumpun] = @kr"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@kr", kodeRumpun)
            Dim result = cmd.ExecuteScalar()
            Return If(result IsNot Nothing, result.ToString(), kodeRumpun)
        End Using
    End Function

    Private Function GetSkorRumpunFase1(sid As Integer) As Dictionary(Of String, Double)
        Dim skor As New Dictionary(Of String, Double)

        Using conn = GetConnection()
            conn.Open()
            Dim q = "SELECT [kode rumpun], [score value] FROM [Exam Rumpun Scores] WHERE [id sesi] = @sid"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@sid", sid)
            Dim reader = cmd.ExecuteReader()

            While reader.Read()
                skor(reader("kode rumpun").ToString()) = Convert.ToDouble(reader("score value"))
            End While
        End Using

        Return skor
    End Function

    Private Class PertanyaanJawaban
        Public Property IdPertanyaan As String
        Public Property TeksPertanyaan As String
        Public Property UserConfidence As Double
    End Class

    Private Function GetPertanyaanDenganJawaban(sid As Integer, phase As Integer) As List(Of PertanyaanJawaban)
        Dim list As New List(Of PertanyaanJawaban)()

        Using conn = GetConnection()
            conn.Open()
            Dim q = "SELECT p.[Id Pertanyaan], p.[teks pertanyaan], ed.[user confidence] " &
                   "FROM [Exam Details] ed " &
                   "JOIN Pertanyaan p ON ed.[id pertanyaan] = p.[Id Pertanyaan] " &
                   "WHERE ed.[id sesi] = @sid AND p.[phase] = @phase " &
                   "ORDER BY p.[Id Pertanyaan]"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@sid", sid)
            cmd.Parameters.AddWithValue("@phase", phase)
            Dim reader = cmd.ExecuteReader()

            While reader.Read()
                Dim pj As New PertanyaanJawaban()
                pj.IdPertanyaan = reader("Id Pertanyaan").ToString()
                pj.TeksPertanyaan = reader("teks pertanyaan").ToString()
                pj.UserConfidence = Convert.ToDouble(reader("user confidence"))
                list.Add(pj)
            End While
        End Using

        Return list
    End Function

    Private Sub DokumenHasilTes_BeginPrint(sender As Object, e As PrintEventArgs) Handles DokumenHasilTes.BeginPrint
        currentPage = 0
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class