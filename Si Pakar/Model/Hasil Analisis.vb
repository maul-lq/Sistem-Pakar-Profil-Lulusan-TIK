Imports Microsoft.Data.SqlClient
Imports System.Text.Json
Imports Si_Pakar.DataSetProgramTableAdapters

' Model untuk menyimpan hasil perhitungan
Public Class HasilProfesi
    Public Property KodeProfesi As String
    Public Property NamaProfesi As String
    Public Property KodeRumpun As String
    Public Property SkorDST As Double
    Public Property SkorMinat As Double
    Public Property SkorAkhir As Double
    Public Property Zona As String
End Class

Module Hasil_Analisis
    Private Const KONSTANTA_BOOST As Double = 0.3

    ' ==========================================
    ' FUNGSI UTAMA: Hitung Hasil Akhir
    ' ==========================================
    Public Function HitungHasilAkhir(sessionId As Integer) As List(Of HasilProfesi)
        ' 1. Ambil data user dan minat
        Dim userData = GetUserData(sessionId)
        Dim kodeProdi As String = userData("kode_prodi")
        Dim minatRumpun As String = userData("kode_rumpun")
        Dim profesiMinat As String = userData("profesi")

        ' 2. Hitung Skor DST per Profesi (dari Exam Details + Skor Rumpun)
        Dim skorDST As Dictionary(Of String, Double) = HitungSkorDSTProfesi(sessionId)

        ' 3. Ambil semua profesi dan hitung skor akhir
        Dim hasilList As New List(Of HasilProfesi)

        Using conn = GetConnection()
            conn.Open()
            Dim q As String = "SELECT [kode profesi], [nama profesi], [kode rumpun] FROM Profesi"
            Dim cmd As New SqlCommand(q, conn)
            Dim reader = cmd.ExecuteReader()

            While reader.Read()
                Dim kodeProfesi As String = reader("kode profesi").ToString()
                Dim namaProfesi As String = reader("nama profesi").ToString()
                Dim kodeRumpun As String = reader("kode rumpun").ToString()

                ' Skor DST (jika tidak ada, default 0)
                Dim skorDSTVal As Double = If(skorDST.ContainsKey(kodeProfesi), skorDST(kodeProfesi), 0)

                ' Variabel Minat (1.0 jika sesuai minat user, 0.0 jika tidak)
                Dim variabelMinat As Double = If(namaProfesi = profesiMinat, 1.0, 0.0)

                ' Skor Minat
                Dim skorMinat As Double = KONSTANTA_BOOST * variabelMinat

                ' Skor Akhir
                Dim skorAkhir As Double = skorDSTVal + skorMinat

                ' Tentukan Zona
                Dim zona As String = TentukanZona(skorDSTVal, variabelMinat)

                hasilList.Add(New HasilProfesi With {
                    .KodeProfesi = kodeProfesi,
                    .NamaProfesi = namaProfesi,
                    .KodeRumpun = kodeRumpun,
                    .SkorDST = skorDSTVal,
                    .SkorMinat = skorMinat,
                    .SkorAkhir = skorAkhir,
                    .Zona = zona
                })
            End While
        End Using

        Return hasilList
    End Function

    ' ==========================================
    ' HITUNG SKOR DST PER PROFESI
    ' ==========================================
    Private Function HitungSkorDSTProfesi(sessionId As Integer) As Dictionary(Of String, Double)
        Dim hasil As New Dictionary(Of String, Double)

        Using conn = GetConnection()
            conn.Open()
            
            ' Hitung skor per profesi dari Exam Details (untuk pertanyaan fase 2 yang punya kode profesi)
            Dim q As String = "
                SELECT 
                    p.[kode profesi],
                    SUM(ed.[user confidence] * p.[expert weight]) as total_skor
                FROM [Exam Details] ed
                JOIN Pertanyaan p ON ed.[id pertanyaan] = p.[Id Pertanyaan]
                WHERE ed.[id sesi] = @sid AND p.[kode profesi] IS NOT NULL
                GROUP BY p.[kode profesi]"

            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@sid", sessionId)
            Dim reader = cmd.ExecuteReader()

            While reader.Read()
                Dim kodeProfesi As String = reader("kode profesi").ToString()
                Dim totalSkor As Double = Convert.ToDouble(reader("total_skor"))
                hasil(kodeProfesi) = totalSkor
            End While
            reader.Close()

            ' Ambil skor rumpun dari Exam Rumpun Scores untuk profesi yang belum ada skornya
            ' atau untuk menambah bobot dari fase 1
            Dim qRumpun As String = "
                SELECT 
                    p.[kode profesi],
                    p.[kode rumpun],
                    ISNULL(ers.[score value], 0) as skor_rumpun
                FROM Profesi p
                LEFT JOIN [Exam Rumpun Scores] ers ON p.[kode rumpun] = ers.[kode rumpun] AND ers.[id sesi] = @sid"
            
            Dim cmdRumpun As New SqlCommand(qRumpun, conn)
            cmdRumpun.Parameters.AddWithValue("@sid", sessionId)
            Dim readerRumpun = cmdRumpun.ExecuteReader()

            While readerRumpun.Read()
                Dim kodeProfesi As String = readerRumpun("kode profesi").ToString()
                Dim skorRumpun As Double = Convert.ToDouble(readerRumpun("skor_rumpun"))
                
                ' Tambahkan skor rumpun ke skor profesi (weighted)
                If hasil.ContainsKey(kodeProfesi) Then
                    hasil(kodeProfesi) += skorRumpun * 0.3 ' Bobot 30% dari skor rumpun fase 1
                Else
                    hasil(kodeProfesi) = skorRumpun * 0.5 ' Jika tidak ada skor fase 2, gunakan skor rumpun
                End If
            End While
            readerRumpun.Close()
        End Using

        ' Normalisasi skor (0-1) berdasarkan max skor
        If hasil.Count > 0 Then
            Dim maxSkor As Double = hasil.Values.Max()
            If maxSkor > 0 Then
                Dim keys = hasil.Keys.ToList()
                For Each k In keys
                    hasil(k) = hasil(k) / maxSkor
                Next
            End If
        End If

        Return hasil
    End Function

    ' ==========================================
    ' SIMPAN SKOR RUMPUN KE EXAM RUMPUN SCORES
    ' ==========================================
    Public Sub SimpanSkorRumpun(sessionId As Integer, skorRumpun As Dictionary(Of String, Double))
        Dim adapter As New Exam_Rumpun_ScoresTableAdapter()
        
        For Each kvp In skorRumpun
            Try
                adapter.Insert(sessionId, kvp.Key, kvp.Value)
            Catch ex As Exception
                ' Jika sudah ada, update
                Using conn = GetConnection()
                    conn.Open()
                    Dim q As String = "UPDATE [Exam Rumpun Scores] SET [score value] = @sv WHERE [id sesi] = @sid AND [kode rumpun] = @kr"
                    Dim cmd As New SqlCommand(q, conn)
                    cmd.Parameters.AddWithValue("@sv", kvp.Value)
                    cmd.Parameters.AddWithValue("@sid", sessionId)
                    cmd.Parameters.AddWithValue("@kr", kvp.Key)
                    cmd.ExecuteNonQuery()
                End Using
            End Try
        Next
    End Sub

    ' ==========================================
    ' TENTUKAN ZONA (Skill vs Minat)
    ' ==========================================
    Private Function TentukanZona(skorDST As Double, variabelMinat As Double) As String
        Dim skillTinggi As Boolean = skorDST >= 0.5
        Dim minatTinggi As Boolean = variabelMinat >= 0.5

        If skillTinggi And minatTinggi Then
            Return "Golden Match"
        ElseIf skillTinggi And Not minatTinggi Then
            Return "Reality Check"
        ElseIf Not skillTinggi And minatTinggi Then
            Return "Hidden Gem"
        Else
            Return "Explorer"
        End If
    End Function

    ' ==========================================
    ' ANALISIS LINEARITAS
    ' ==========================================
    Public Function AnalisisLinearitas(kodeProdi As String, kodeRumpun As String) As String
        Using conn = GetConnection()
            conn.Open()
            Dim q As String = "SELECT [status linearitas] FROM [Linearity Matrix] WHERE [kode prodi] = @kp AND [kode rumpun] = @kr"
            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@kp", kodeProdi)
            cmd.Parameters.AddWithValue("@kr", kodeRumpun)
            Dim result = cmd.ExecuteScalar()

            If result IsNot Nothing Then
                Return result.ToString()
            Else
                Return "Unknown"
            End If
        End Using
    End Function

    ' ==========================================
    ' GET USER DATA
    ' ==========================================
    Private Function GetUserData(sessionId As Integer) As Dictionary(Of String, String)
        Dim data As New Dictionary(Of String, String)

        Using conn = GetConnection()
            conn.Open()
            Dim q As String = "
                SELECT u.[kode prodi], u.[kode rumpun], u.[profesi]
                FROM [Data User] u
                JOIN [Sesi Ujian] s ON u.[Id user] = s.[Id user]
                WHERE s.[Id sesi] = @sid"

            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@sid", sessionId)
            Dim reader = cmd.ExecuteReader()

            If reader.Read() Then
                data("kode_prodi") = reader("kode prodi").ToString()
                data("kode_rumpun") = reader("kode rumpun").ToString()
                data("profesi") = reader("profesi").ToString()
            End If
        End Using

        Return data
    End Function

    ' ==========================================
    ' SIMPAN HASIL KE DATABASE (JSON)
    ' ==========================================
    Public Sub SimpanHasilKeDatabase(sessionId As Integer, hasilList As List(Of HasilProfesi))
        ' Buat JSON untuk phase1 score (skor per rumpun dari Exam Rumpun Scores)
        Dim phase1Json = BuatPhase1Json(sessionId)

        ' Buat JSON untuk final result
        Dim finalJson = BuatFinalResultJson(hasilList)

        ' Update ke database
        Using conn = GetConnection()
            conn.Open()
            Dim q As String = "
                UPDATE [Sesi Ujian] 
                SET [phase1 score json] = @p1, 
                    [final result json] = @fr,
                    [waktu selesai] = @ws
                WHERE [Id sesi] = @sid"

            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@p1", phase1Json)
            cmd.Parameters.AddWithValue("@fr", finalJson)
            cmd.Parameters.AddWithValue("@ws", DateTime.Now)
            cmd.Parameters.AddWithValue("@sid", sessionId)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function BuatPhase1Json(sessionId As Integer) As String
        Dim skorRumpun As New Dictionary(Of String, Double)

        Using conn = GetConnection()
            conn.Open()
            ' Ambil dari Exam Rumpun Scores
            Dim q As String = "
                SELECT [kode rumpun], [score value]
                FROM [Exam Rumpun Scores]
                WHERE [id sesi] = @sid"

            Dim cmd As New SqlCommand(q, conn)
            cmd.Parameters.AddWithValue("@sid", sessionId)
            Dim reader = cmd.ExecuteReader()

            While reader.Read()
                Dim rumpun As String = reader("kode rumpun").ToString()
                Dim skor As Double = Convert.ToDouble(reader("score value"))
                skorRumpun(rumpun) = skor
            End While
        End Using

        Return JsonSerializer.Serialize(skorRumpun)
    End Function

    Private Function BuatFinalResultJson(hasilList As List(Of HasilProfesi)) As String
        Dim hasil = hasilList.OrderByDescending(Function(x) x.SkorAkhir).Take(10).Select(Function(h) New With {
            .kode_profesi = h.KodeProfesi,
            .nama_profesi = h.NamaProfesi,
            .skor_dst = Math.Round(h.SkorDST, 4),
            .skor_minat = Math.Round(h.SkorMinat, 4),
            .skor_akhir = Math.Round(h.SkorAkhir, 4),
            .zona = h.Zona
        }).ToList()

        Return JsonSerializer.Serialize(hasil)
    End Function
End Module
