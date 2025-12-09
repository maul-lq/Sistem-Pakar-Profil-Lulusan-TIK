Imports Microsoft.Data.SqlClient
Imports System.Linq

Public Class FPertanyaan
    Public phase As Short
    Public sessionId As Integer

    Dim dataSoal As New List(Of PertanyaanModel)()
    Dim currNum As Integer = 0
    Dim scenarioName As String = ""

    Private Sub FPertanyaan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If phase = 1 Then
            GroupBoxTopik.Text = "Pertanyaan - Umum"
            MuatSoalFase1()
        End If

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
                Dim newModel As New PertanyaanModel()
                newModel.IdPertanyaan = reader("Id Pertanyaan").ToString()
                newModel.TeksPertanyaan = reader("teks pertanyaan").ToString()
                newModel.KodeRumpun = reader("kode rumpun").ToString()
                newModel.ExpertWeight = Convert.ToDouble(reader("expert weight"))
                newModel.JawabanUser = -1.0
                dataSoal.Add(newModel)
            End While
        End Using

        UpdateTampilanAwal()
    End Sub

    Private Sub ProsesLogikaFase2()
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
        Dim topScore As Double = juara1.Value

        Dim minatUser As List(Of String) = GetMinatUser(sessionId)
        Dim rumpunMinat As List(Of String) = GetRumpunDariMinat(minatUser)

        Dim kandidatList As New List(Of PertanyaanModel)()

        If topScore < 0.4 Then
            If rumpunMinat.Count > 0 Then
                scenarioName = "Beginner"
                PerbaruiSeknarioUjian(sessionId, scenarioName)

                If rumpunMinat.Count = 1 Then
                    kandidatList = AmbilKandidatSoal(rumpunMinat(0))
                    kandidatList = FilterSoalDistribusi(kandidatList, 4)
                ElseIf rumpunMinat.Count = 2 Then
                    Dim list1 = AmbilKandidatSoal(rumpunMinat(0))
                    list1 = FilterSoalDistribusi(list1, 2)
                    Dim list2 = AmbilKandidatSoal(rumpunMinat(1))
                    list2 = FilterSoalDistribusi(list2, 2)
                    kandidatList.AddRange(list1)
                    kandidatList.AddRange(list2)
                Else
                    ' If more than 2 interests, use first as primary
                    kandidatList = AmbilKandidatSoal(rumpunMinat(0))
                    kandidatList = FilterSoalDistribusi(kandidatList, 4)
                End If
            Else
                scenarioName = "Null"
                PerbaruiSeknarioUjian(sessionId, scenarioName)
                kandidatList = AmbilKandidatSoal("CP")
                kandidatList = FilterSoalDistribusi(kandidatList, 4)
            End If
        Else
            ' If user has no minat selected, allow Dominant/Hybrid based on gap/topScore alone
            Dim minatMatchTop As Boolean
            If rumpunMinat.Count = 0 Then
                minatMatchTop = True
            Else
                minatMatchTop = rumpunMinat.Contains(rumpunTarget1) Or rumpunMinat.Contains(rumpunTarget2)
            End If

            If gap > 0.2 And minatMatchTop Then
                scenarioName = "Dominant"
                PerbaruiSeknarioUjian(sessionId, scenarioName)
                kandidatList = AmbilKandidatSoal(rumpunTarget1)
                kandidatList = FilterSoalDistribusi(kandidatList, 4)
            ElseIf gap <= 0.2 And minatMatchTop Then
                scenarioName = "Hybrid"
                PerbaruiSeknarioUjian(sessionId, scenarioName)
                Dim list1 = AmbilKandidatSoal(rumpunTarget1)
                list1 = FilterSoalDistribusi(list1, 2)
                Dim list2 = AmbilKandidatSoal(rumpunTarget2)
                list2 = FilterSoalDistribusi(list2, 2)
                kandidatList.AddRange(list1)
                kandidatList.AddRange(list2)
            Else
                ' Merge Special Hybrid into Hybrid: always produce a 10+10 split
                scenarioName = "Hybrid"
                PerbaruiSeknarioUjian(sessionId, scenarioName)
                Dim list1 = AmbilKandidatSoal(rumpunTarget1)
                list1 = FilterSoalDistribusi(list1, 2)

                Dim list2 As List(Of PertanyaanModel)
                If rumpunMinat.Count > 0 Then
                    ' if user has interest but it didn't match top2, use that interest as second group
                    list2 = AmbilKandidatSoal(rumpunMinat(0))
                ElseIf rumpunTarget2 <> "" Then
                    list2 = AmbilKandidatSoal(rumpunTarget2)
                Else
                    ' fallback to top1 if no second rumpun available
                    list2 = AmbilKandidatSoal(rumpunTarget1)
                End If

                list2 = FilterSoalDistribusi(list2, 2)
                kandidatList.AddRange(list1)
                kandidatList.AddRange(list2)
            End If
        End If

        ' Ensure we have 20 questions. If FilterSoalDistribusi returned fewer (e.g., missing questions in DB), fill
        If kandidatList.Count < 20 Then
            Dim needed = 20 - kandidatList.Count
            ' Try to fill from top rumpun first, then second, then CP
            Dim filler As New List(Of PertanyaanModel)()
            filler.AddRange(AmbilKandidatSoal(rumpunTarget1))
            If rumpunTarget2 <> "" Then filler.AddRange(AmbilKandidatSoal(rumpunTarget2))
            filler.AddRange(AmbilKandidatSoal("CP"))

            ' Remove duplicates by IdPertanyaan
            Dim existingIds = New HashSet(Of String)(kandidatList.Select(Function(s) s.IdPertanyaan))
            For Each f In filler
                If kandidatList.Count >= 20 Then Exit For
                If Not existingIds.Contains(f.IdPertanyaan) Then
                    kandidatList.Add(f)
                    existingIds.Add(f.IdPertanyaan)
                End If
            Next
        End If

        dataSoal.Clear()
        dataSoal.AddRange(kandidatList.Take(20))

        UpdateTampilanAwal()
    End Sub

    Private Function AmbilKandidatSoal(kodeRumpun As String) As List(Of PertanyaanModel)
        Dim list As New List(Of PertanyaanModel)()

        Using conn = GetConnection()
            conn.Open()
            Dim q As String = "SELECT [Id Pertanyaan], [teks pertanyaan], [kode rumpun], [expert weight], [kode profesi] FROM Pertanyaan WHERE phase = 2 AND [kode rumpun] = @kr"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@kr", kodeRumpun)
            Dim r = cmd.ExecuteReader()

            While r.Read()
                Dim model As New PertanyaanModel()
                model.IdPertanyaan = r("Id Pertanyaan").ToString()
                model.TeksPertanyaan = r("teks pertanyaan").ToString()
                model.KodeRumpun = r("kode rumpun").ToString()
                model.ExpertWeight = Convert.ToDouble(r("expert weight"))
                model.JawabanUser = -1.0

                If Not IsDBNull(r("kode profesi")) Then
                    model.IdPertanyaan &= "|" & r("kode profesi").ToString()
                End If

                list.Add(model)
            End While
        End Using

        Return list
    End Function

    Private Function FilterSoalDistribusi(listSoal As List(Of PertanyaanModel), maxPerSub As Integer) As List(Of PertanyaanModel)
        Dim hasil As New List(Of PertanyaanModel)()
        Dim grouped As New Dictionary(Of String, List(Of PertanyaanModel))()

        For Each soal In listSoal
            Dim parts = soal.IdPertanyaan.Split("|"c)
            Dim idAsli = parts(0)
            Dim kodeProfesi = If(parts.Length > 1, parts(1), "UNKNOWN")

            soal.IdPertanyaan = idAsli

            If Not grouped.ContainsKey(kodeProfesi) Then
                grouped(kodeProfesi) = New List(Of PertanyaanModel)()
            End If
            grouped(kodeProfesi).Add(soal)
        Next

        For Each kvp In grouped
            hasil.AddRange(kvp.Value.Take(maxPerSub))
        Next

        Return hasil
    End Function

    Private Function HitungSkorMentah() As Dictionary(Of String, Double)
        Dim hasil As New Dictionary(Of String, Double)
        For Each row In dataSoal
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
        For Each row In dataSoal
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
            Dim q As String = "SELECT u.[profesi] FROM [Data User] u JOIN [Sesi Ujian] s ON u.[Id user] = s.[Id user] WHERE s.[Id sesi] = @ids"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@ids", idSesi)
            Dim res = cmd.ExecuteScalar()
            If res IsNot Nothing AndAlso Not IsDBNull(res) Then
                Dim profesiStr = res.ToString().Trim()
                If profesiStr <> "" Then
                    Dim profesiList = profesiStr.Split(","c)
                    For Each p In profesiList
                        Dim t = p.Trim()
                        If t <> "" Then
                            minat.Add(t)
                        End If
                    Next
                End If
            End If
        End Using
        Return minat
    End Function

    Private Function GetRumpunDariMinat(profesiList As List(Of String)) As List(Of String)
        Dim rumpunList As New List(Of String)

        Using conn = GetConnection()
            conn.Open()
            For Each profesi In profesiList
                Dim q As String = "SELECT [kode rumpun] FROM Profesi WHERE [nama profesi] = @profesi"
                Dim cmd As New SqlCommand(q, conn)
                cmd.Parameters.AddWithValue("@profesi", profesi)
                Dim res = cmd.ExecuteScalar()
                If res IsNot Nothing AndAlso Not rumpunList.Contains(res.ToString()) Then
                    rumpunList.Add(res.ToString())
                End If
            Next
        End Using

        Return rumpunList
    End Function

    Private Function CekIrisanMinat(minatUser As List(Of String), skorRumpun As Dictionary(Of String, Double)) As String
        If minatUser.Count > 0 Then Return minatUser(0)
        Return ""
    End Function

    Private Function CekSemuaSoalSudahDijawab() As Boolean
        For Each row In dataSoal
            If row.JawabanUser < 0 Then Return False
        Next
        Return True
    End Function

    Private Function HitungJumlahSoalTerjawab() As Integer
        Dim count As Integer = 0
        For Each row In dataSoal
            If row.JawabanUser >= 0 Then count += 1
        Next
        Return count
    End Function

    Private Sub UpdateStatusSubmitButton()
        ButtonSubmit.Enabled = CekSemuaSoalSudahDijawab()
    End Sub

    Private Sub UpdateWarnaButtonNomor()
        For i As Integer = 0 To FlowLayoutPanelNomorSoal.Controls.Count - 1
            Dim btn As Button = CType(FlowLayoutPanelNomorSoal.Controls(i), Button)
            Dim row = dataSoal(i)

            If row.JawabanUser >= 0 Then
                btn.BackColor = Color.LightGreen
            Else
                btn.BackColor = Color.White
            End If
        Next
    End Sub

    Private Sub UpdateTampilanAwal()
        BuatTombol(dataSoal.Count)
        UbahSoal(1)

        If phase = 2 Then
            GroupBoxTopik.Text = "Pertanyaan - Lanjutan"
#If DEBUG Then
            MessageBox.Show("Skenario Terdeteksi: " & scenarioName, "Info")
#End If
            ButtonSubmit.Text = "LIHAT HASIL AKHIR"
        End If

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
        If currNum >= 0 And currNum < dataSoal.Count Then
            SimpanJawabanSementara()
        End If

        currNum = nomorUrut - 1

        If currNum < 0 Or currNum >= dataSoal.Count Then Return

        Dim row = dataSoal(currNum)

        LabelSoal.Text = row.TeksPertanyaan
        AturPosisiLabel()

        ResetRadioButtons()
        Dim nilaiTersimpan As Double = row.JawabanUser

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

        If nilai >= 0 AndAlso currNum >= 0 AndAlso currNum < dataSoal.Count Then
            Dim row = dataSoal(currNum)
            row.JawabanUser = nilai

            UpdateStatusSubmitButton()
            UpdateWarnaButtonNomor()
        End If
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged, RadioButton4.CheckedChanged, RadioButton5.CheckedChanged
        If sender.Checked Then SimpanJawabanSementara()
    End Sub

    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click
        SimpanJawabanSementara()

        If Not CekSemuaSoalSudahDijawab() Then
            MessageBox.Show("Mohon jawab semua pertanyaan terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        SimpanKeDatabase()

        If phase = 1 Then
            Dim skorRumpun As Dictionary(Of String, Double) = HitungSkorMentah()
            SimpanSkorRumpun(sessionId, skorRumpun)

            phase = 2
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
            For Each row In dataSoal
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
        If currNum < dataSoal.Count - 1 Then UbahSoal(currNum + 2)
    End Sub

    Private Sub AturStatusTombolNavigasi()
        Button1.Enabled = (currNum < dataSoal.Count - 1)
        Button2.Enabled = (currNum > 0)
    End Sub

    Private Sub FPertanyaan_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        hapusHasilTesIni(sessionId, True)
        Application.Exit()
    End Sub
End Class
