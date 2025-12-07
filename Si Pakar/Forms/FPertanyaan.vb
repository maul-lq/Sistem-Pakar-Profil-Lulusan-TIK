Imports Microsoft.Data.SqlClient
Imports Si_Pakar.DataSetProgram
Imports System.Linq

Public Class FPertanyaan
    Public phase As Short
    Public sessionId As Integer

    Dim dataSoal As New DataPertanyaanDataTable()
    Dim currNum As Integer = 0
    Dim scenarioName As String = ""

    Private Sub FPertanyaan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If phase = 1 Then
            GroupBoxTopik.Text = "Pertanyaan - Umum"
            MuatSoalFase1()
        End If

        ' ✅ DISABLE BUTTON SUBMIT DI AWAL
        ButtonSubmit.Enabled = False
    End Sub

    Private Sub MuatSoalFase1()
        dataSoal.Clear()
        Using conn = GetConnection()
            conn.Open()
            Dim query As String = "SELECT [Id Pertanyaan], [teks pertanyaan], [kode rumpun], [expert weight] FROM Pertanyaan WHERE phase = 1"
            Dim cmd As New SqlCommand(query, conn)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim newRow As DataPertanyaanRow = dataSoal.NewDataPertanyaanRow()
                newRow.IdPertanyaan = reader("Id Pertanyaan").ToString()
                newRow.TeksPertanyaan = reader("teks pertanyaan").ToString()
                newRow.KodeRumpun = reader("kode rumpun").ToString()
                newRow.ExpertWeight = Convert.ToDouble(reader("expert weight"))
                newRow.JawabanUser = -1.0 ' ✅ UBAH DEFAULT JADI -1 (BELUM DIJAWAB)
                dataSoal.AddDataPertanyaanRow(newRow)
            End While
        End Using

        UpdateTampilanAwal()
    End Sub

    Private Sub ProsesLogikaFase2()
        ' ✅ FIX BUG: RESET currNum DAN RADIO BUTTONS SEBELUM CLEAR DATA
        currNum = 0
        ResetRadioButtons()

        Dim skorRumpun As Dictionary(Of String, Double) = HitungSkorMentah()
        Dim maxScoreRumpun As Dictionary(Of String, Double) = HitungMaxBobotPerRumpun()

        Dim percentRumpun As New Dictionary(Of String, Double)
        For Each k In skorRumpun.Keys
            If maxScoreRumpun.ContainsKey(k) AndAlso maxScoreRumpun(k) > 0 Then
                percentRumpun(k) = skorRumpun(k) / maxScoreRumpun(k)
            Else
                percentRumpun(k) = 0
            End If
        Next

        Dim sortedRumpun = percentRumpun.OrderByDescending(Function(x) x.Value).ToList()
        Dim juara1 = sortedRumpun(0)

        Dim juara2Val As Double = 0
        Dim rumpunTarget2 As String = ""
        If sortedRumpun.Count > 1 Then
            juara2Val = sortedRumpun(1).Value
            rumpunTarget2 = sortedRumpun(1).Key
        End If

        Dim gap As Double = juara1.Value - juara2Val
        Dim rumpunTarget1 As String = juara1.Key

        Dim dtKandidat As New DataTable()

        If gap > 0.2 Then
            scenarioName = "Dominant"
            PerbaruiSeknarioUjian(sessionId, scenarioName)
            dtKandidat = AmbilKandidatSoal(rumpunTarget1)
            FilterSoalDistribusi(dtKandidat, 4)

        ElseIf gap <= 0.2 And juara1.Value > 0.4 Then
            scenarioName = "Hybrid"
            PerbaruiSeknarioUjian(sessionId, scenarioName)
            Dim dt1 = AmbilKandidatSoal(rumpunTarget1)
            FilterSoalDistribusi(dt1, 2)

            Dim dt2 = AmbilKandidatSoal(rumpunTarget2)
            FilterSoalDistribusi(dt2, 2)

            dtKandidat.Merge(dt1)
            dtKandidat.Merge(dt2)

        Else
            Dim minatUser As List(Of String) = GetMinatUser(sessionId)
            Dim rumpunMinat As String = CekIrisanMinat(minatUser, skorRumpun)

            If rumpunMinat <> "" Then
                scenarioName = "Beginner"
                PerbaruiSeknarioUjian(sessionId, scenarioName)
                dtKandidat = AmbilKandidatSoal(rumpunMinat)
                FilterSoalDistribusi(dtKandidat, 2)
            Else
                scenarioName = "Null"
                PerbaruiSeknarioUjian(sessionId, scenarioName)
                dtKandidat = AmbilKandidatSoal("CP")
                FilterSoalDistribusi(dtKandidat, 4)
            End If
        End If

        dataSoal.Clear()
        For Each rowKandidat As DataRow In dtKandidat.Rows
            Dim newRow As DataPertanyaanRow = dataSoal.NewDataPertanyaanRow()
            newRow.IdPertanyaan = rowKandidat("Id Pertanyaan").ToString()
            newRow.TeksPertanyaan = rowKandidat("teks pertanyaan").ToString()
            newRow.KodeRumpun = rowKandidat("kode rumpun").ToString()
            newRow.ExpertWeight = Convert.ToDouble(rowKandidat("expert weight"))
            newRow.JawabanUser = -1.0
            dataSoal.AddDataPertanyaanRow(newRow)
        Next

        UpdateTampilanAwal()
    End Sub

    Private Function AmbilKandidatSoal(kodeRumpun As String) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Id Pertanyaan", GetType(String))
        dt.Columns.Add("teks pertanyaan", GetType(String))
        dt.Columns.Add("kode rumpun", GetType(String))
        dt.Columns.Add("expert weight", GetType(Double))
        dt.Columns.Add("kode profesi", GetType(String))

        Using conn = GetConnection()
            conn.Open()
            Dim q As String = "SELECT [Id Pertanyaan], [teks pertanyaan], [kode rumpun], [expert weight], [kode profesi] FROM Pertanyaan WHERE phase = 2 AND [kode rumpun] = @kr"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@kr", kodeRumpun)
            Dim r = cmd.ExecuteReader()
            While r.Read()
                dt.Rows.Add(r("Id Pertanyaan"), r("teks pertanyaan"), r("kode rumpun"), r("expert weight"), r("kode profesi"))
            End While
        End Using
        Return dt
    End Function

    Private Sub FilterSoalDistribusi(ByRef dtTarget As DataTable, maxPerSub As Integer)
        Dim rowsList = dtTarget.AsEnumerable().ToList()
        Dim groups = rowsList.GroupBy(Function(row) row.Field(Of String)("kode profesi"))

        Dim dtHasil As DataTable = dtTarget.Clone()

        For Each grp In groups
            Dim taken = grp.Take(maxPerSub)
            For Each row In taken
                dtHasil.ImportRow(row)
            Next
        Next
        dtTarget = dtHasil
    End Sub

    Private Function HitungSkorMentah() As Dictionary(Of String, Double)
        Dim hasil As New Dictionary(Of String, Double)
        For Each row As DataPertanyaanRow In dataSoal.Rows
            ' ✅ SKIP JIKA BELUM DIJAWAB
            If row.JawabanUser < 0 Then Continue For

            Dim kr As String = row.KodeRumpun
            Dim val As Double = row.JawabanUser
            Dim w As Double = row.ExpertWeight

            If Not hasil.ContainsKey(kr) Then hasil(kr) = 0
            hasil(kr) += (val * w)
        Next
        Return hasil
    End Function

    Private Function HitungMaxBobotPerRumpun() As Dictionary(Of String, Double)
        Dim hasil As New Dictionary(Of String, Double)
        For Each row As DataPertanyaanRow In dataSoal.Rows
            Dim kr As String = row.KodeRumpun
            Dim w As Double = row.ExpertWeight

            If Not hasil.ContainsKey(kr) Then hasil(kr) = 0
            hasil(kr) += (1.0 * w)
        Next
        Return hasil
    End Function

    Private Function GetMinatUser(idSesi As Integer) As List(Of String)
        Dim minat As New List(Of String)
        Using conn = GetConnection()
            conn.Open()
            Dim q As String = "SELECT u.[kode rumpun] FROM [Data User] u JOIN [Sesi Ujian] s ON u.[Id user] = s.[Id user] WHERE s.[Id sesi] = @ids"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@ids", idSesi)
            Dim res = cmd.ExecuteScalar()
            If res IsNot Nothing Then minat.Add(res.ToString())
        End Using
        Return minat
    End Function

    Private Function CekIrisanMinat(minatUser As List(Of String), skorRumpun As Dictionary(Of String, Double)) As String
        If minatUser.Count > 0 Then Return minatUser(0)
        Return ""
    End Function

    ' ==========================================
    ' ✅ VALIDASI & UPDATE STATUS
    ' ==========================================
    Private Function CekSemuaSoalSudahDijawab() As Boolean
        For Each row As DataPertanyaanRow In dataSoal.Rows
            If row.JawabanUser < 0 Then Return False ' Belum dijawab
        Next
        Return True
    End Function

    Private Function HitungJumlahSoalTerjawab() As Integer
        Dim count As Integer = 0
        For Each row As DataPertanyaanRow In dataSoal.Rows
            If row.JawabanUser >= 0 Then count += 1
        Next
        Return count
    End Function

    Private Sub UpdateStatusSubmitButton()
        ButtonSubmit.Enabled = CekSemuaSoalSudahDijawab()

        ' ✅ UPDATE PROGRESS LABEL (opsional - tambahkan Label di Designer jika diperlukan)
        ' LabelProgress.Text = $"{HitungJumlahSoalTerjawab()} / {dataSoal.Rows.Count} Soal Dijawab"
    End Sub

    Private Sub UpdateWarnaButtonNomor()
        For i As Integer = 0 To FlowLayoutPanelNomorSoal.Controls.Count - 1
            Dim btn As Button = CType(FlowLayoutPanelNomorSoal.Controls(i), Button)
            Dim row As DataPertanyaanRow = CType(dataSoal.Rows(i), DataPertanyaanRow)

            ' ✅ HIJAU JIKA SUDAH DIJAWAB, PUTIH JIKA BELUM
            If row.JawabanUser >= 0 Then
                btn.BackColor = Color.LightGreen
            Else
                btn.BackColor = Color.White
            End If
        Next
    End Sub

    Private Sub UpdateTampilanAwal()
        BuatTombol(dataSoal.Rows.Count)
        UbahSoal(1)

        If phase = 2 Then
            GroupBoxTopik.Text = "Pertanyaan - Lanjutan"
            MessageBox.Show("Skenario Terdeteksi: " & scenarioName, "Info")
            ButtonSubmit.Text = "LIHAT HASIL AKHIR"
        End If

        ' ✅ UPDATE STATUS AWAL
        UpdateStatusSubmitButton()
        UpdateWarnaButtonNomor()
    End Sub

    Private Sub BuatTombol(maksSoal As Integer)
        FlowLayoutPanelNomorSoal.Controls.Clear()
        For d As Integer = 1 To maksSoal
            Dim btn As New Button()
            btn.Text = d.ToString()
            btn.Size = New Size(35, 35)
            AddHandler btn.Click, Sub(s, ev) UbahSoal(CInt(btn.Text))
            FlowLayoutPanelNomorSoal.Controls.Add(btn)
        Next
    End Sub

    Private Sub UbahSoal(nomorUrut As Integer)
        ' ✅ FIX BUG: CEK currNum VALID SEBELUM SIMPAN JAWABAN
        If currNum >= 0 And currNum < dataSoal.Rows.Count Then
            SimpanJawabanSementara()
        End If

        currNum = nomorUrut - 1

        If currNum < 0 Or currNum >= dataSoal.Rows.Count Then Return

        Dim row As DataPertanyaanRow = CType(dataSoal.Rows(currNum), DataPertanyaanRow)

        LabelSoal.Text = row.TeksPertanyaan
        AturPosisiLabel()

        ResetRadioButtons()
        Dim nilaiTersimpan As Double = row.JawabanUser

        ' ✅ JIKA BELUM DIJAWAB, SEMUA RADIO TETAP FALSE
        If nilaiTersimpan >= 0 Then
            SetRadioButtonValue(nilaiTersimpan)
        End If

        AturStatusTombolNavigasi()
    End Sub

    Private Sub AturPosisiLabel()
        LabelSoal.AutoSize = True
        Dim centerX As Integer = Convert.ToInt32((PanelPertanyaann.Size.Width - LabelSoal.Width) \ 2)
        Dim centerY As Integer = Convert.ToInt32((PanelPertanyaann.Size.Height - LabelSoal.Height) \ 2) - 20
        LabelSoal.Location = New Point(Math.Max(0, centerX), Math.Max(0, centerY))
    End Sub

    Private Sub SetRadioButtonValue(val As Double)
        If val = 0.0 Then RadioButton1.Checked = True
        If val = 0.2 Then RadioButton2.Checked = True
        If val = 0.5 Then RadioButton3.Checked = True
        If val = 0.8 Then RadioButton4.Checked = True
        If val = 0.95 Then RadioButton5.Checked = True
    End Sub

    Private Sub ResetRadioButtons()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        RadioButton4.Checked = False
        RadioButton5.Checked = False
    End Sub

    Private Sub SimpanJawabanSementara()
        Dim nilai As Double = -1
        If RadioButton1.Checked Then nilai = 0.0
        If RadioButton2.Checked Then nilai = 0.2
        If RadioButton3.Checked Then nilai = 0.5
        If RadioButton4.Checked Then nilai = 0.8
        If RadioButton5.Checked Then nilai = 0.95

        ' ✅ FIX BUG: VALIDASI currNum >= 0 DAN < COUNT
        If nilai >= 0 AndAlso currNum >= 0 AndAlso currNum < dataSoal.Rows.Count Then
            Dim row As DataPertanyaanRow = CType(dataSoal.Rows(currNum), DataPertanyaanRow)
            row.JawabanUser = nilai

            ' ✅ UPDATE STATUS SETIAP KALI ADA JAWABAN BARU
            UpdateStatusSubmitButton()
            UpdateWarnaButtonNomor()
        End If
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged, RadioButton4.CheckedChanged, RadioButton5.CheckedChanged
        If sender.Checked Then SimpanJawabanSementara()
    End Sub

    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click
        SimpanJawabanSementara()

        ' ✅ VALIDASI AKHIR (DOUBLE CHECK)
        If Not CekSemuaSoalSudahDijawab() Then
            MessageBox.Show("Mohon jawab semua pertanyaan terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        SimpanKeDatabase()

        If phase = 1 Then
            ' ✅ SIMPAN SKOR RUMPUN SETELAH FASE 1
            Dim skorRumpun As Dictionary(Of String, Double) = HitungSkorMentah()
            SimpanSkorRumpun(sessionId, skorRumpun)

            phase = 2
            ' ✅ RESET BUTTON SUBMIT UNTUK FASE 2
            ButtonSubmit.Enabled = False
            ProsesLogikaFase2()
        Else
            Using conn = GetConnection()
                conn.Open()
                Dim q As String = "UPDATE [Sesi Ujian] SET [scenario used] = @sc WHERE [Id sesi] = @ids"
                Dim cmd As New SqlCommand(q, conn)
                cmd.Parameters.AddWithValue("@sc", scenarioName)
                cmd.Parameters.AddWithValue("@ids", sessionId)
                cmd.ExecuteNonQuery()
            End Using

            Dim fhasil As New FHasil
            fhasil.sessionId = sessionId
            fhasil.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub SimpanKeDatabase()
        Using conn = GetConnection()
            conn.Open()
            For Each row As DataPertanyaanRow In dataSoal.Rows
                Dim delQ As String = "DELETE FROM [Exam Details] WHERE [id sesi] = @sesi AND [id pertanyaan] = @idTanya"
                Dim cmdDel As New SqlCommand(delQ, conn)
                cmdDel.Parameters.AddWithValue("@sesi", sessionId)
                cmdDel.Parameters.AddWithValue("@idTanya", row.IdPertanyaan)
                cmdDel.ExecuteNonQuery()

                Dim query As String = "INSERT INTO [Exam Details] ([id sesi], [id pertanyaan], [user confidence]) VALUES (@sesi, @idTanya, @conf)"
                Dim cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@sesi", sessionId)
                cmd.Parameters.AddWithValue("@idTanya", row.IdPertanyaan)
                cmd.Parameters.AddWithValue("@conf", row.JawabanUser)
                cmd.ExecuteNonQuery()
            Next
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If currNum > 0 Then UbahSoal(currNum)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If currNum < dataSoal.Rows.Count - 1 Then UbahSoal(currNum + 2)
    End Sub

    Private Sub AturStatusTombolNavigasi()
        Button1.Enabled = (currNum < dataSoal.Rows.Count - 1)
        Button2.Enabled = (currNum > 0)
    End Sub

    Private Sub FPertanyaan_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub
End Class