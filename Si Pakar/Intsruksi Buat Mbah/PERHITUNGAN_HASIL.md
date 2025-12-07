# DOKUMENTASI SISTEM PERHITUNGAN HASIL

## Flow Sistem

### 1. Input Data (FMain)
- User input: Nama, Email, Program Studi, Minat Karir
- Data disimpan ke tabel `Data User`
- Session ujian dibuat di tabel `Sesi Ujian`

### 2. Fase 1 - Pertanyaan Umum (FPertanyaan, phase=1)
- 10 pertanyaan umum (2 soal per rumpun)
- Jawaban user disimpan ke `Exam Details`
- Sistem menghitung skor mentah per rumpun
- **? Skor rumpun disimpan ke `Exam Rumpun Scores`**
- Menentukan skenario untuk Fase 2:
  - **Dominant**: Gap > 20%, fokus 1 rumpun (20 soal, 4 per sub-karir)
  - **Hybrid**: Gap ? 20% dan Top > 40% (10 soal dari 2 rumpun teratas)
  - **Beginner**: Top < 40% tapi ada minat (soal dari rumpun minat)
  - **Null**: Top < 40% tanpa minat (soal dari Creative & Product)

### 3. Fase 2 - Pertanyaan Khusus (FPertanyaan, phase=2)
- 20 pertanyaan spesifik berdasarkan skenario
- Setiap pertanyaan terkait dengan profesi spesifik
- Jawaban disimpan ke `Exam Details`

### 4. Perhitungan Hasil (FHasil)

#### A. Skor DST (Dempster-Shafer Theory - Simplified)

**Sumber Data:**
1. **`Exam Details`**: Jawaban user untuk setiap pertanyaan (fase 1 & 2)
2. **`Exam Rumpun Scores`**: Skor agregat per rumpun dari fase 1

**Formula Perhitungan:**
```
Skor DST per Profesi = ?(user_confidence × expert_weight) [dari Exam Details fase 2]
                     + (Skor Rumpun × 0.3) [dari Exam Rumpun Scores]
```

**Alasan Bobot 0.3:**
- Skor rumpun fase 1 memberikan "baseline" kemampuan umum
- Skor profesi fase 2 adalah kemampuan spesifik yang lebih dominan
- Kombinasi keduanya memberikan gambaran lengkap

**Normalisasi**:
```
Skor DST Normalized = Skor DST / Max(Skor DST dari semua profesi)
```

#### B. Interest Boosting
```
Variabel Minat = 1.0 jika profesi sesuai minat user
               = 0.0 jika tidak sesuai

Skor Minat = 0.3 × Variabel Minat
```

#### C. Skor Akhir
```
Skor Akhir = Skor DST Normalized + Skor Minat
```

### 5. Zona Skill vs Minat

| Kondisi | Zona | Deskripsi |
|---------|------|-----------|
| Skill ? 0.5 & Minat = 1.0 | **Golden Match** | Sesuai skill dan minat |
| Skill ? 0.5 & Minat = 0.0 | **Reality Check** | Bagus skillnya tapi bukan minat |
| Skill < 0.5 & Minat = 1.0 | **Hidden Gem** | Minat tinggi tapi skill perlu diasah |
| Skill < 0.5 & Minat = 0.0 | **Explorer** | Butuh eksplorasi lebih lanjut |

### 6. Analisis Linearitas

Berdasarkan Knowledge Base:

| Rumpun Hasil | TI | TMJ | TMD |
|--------------|----|----|-----|
| Software Eng | Linear | Related | Related |
| Data & AI | Linear | Pivot | Pivot |
| Infrastructure | Related | Linear | Pivot |
| Cyber Security | Linear | Linear | Pivot |
| Creative | Related | Pivot | Linear |

**Interpretasi**:
- **Linear**: Sejalan dengan program studi
- **Related**: Masih terkait dengan program studi
- **Pivot**: Tidak relevan, perlu upskilling/pivot karir

### 7. Output Final (FHasil)

**Tampilan**:
1. **Top 3 Karir** dengan skor akhir
2. **4 Zona Karir** (terbaik dari masing-masing zona)
3. **Analisis Linearitas** (MessageBox popup)

**Data Tersimpan ke Database** (JSON):
- `phase1 score json`: Skor per rumpun dari `Exam Rumpun Scores`
- `final result json`: Top 10 profesi dengan detail skor

**Format JSON**:
```json
{
  "kode_profesi": "SE-BE",
  "nama_profesi": "Backend Developer",
  "skor_dst": 0.8523,
  "skor_minat": 0.3,
  "skor_akhir": 1.1523,
  "zona": "Golden Match"
}
```

---

## ?? **Tabel Database yang Digunakan**

### Tabel Input
1. **Data User**: Nama, email, prodi, minat awal
2. **Sesi Ujian**: Session tracking dengan scenario_used

### Tabel Pertanyaan & Jawaban
3. **Pertanyaan**: Bank soal fase 1 dan 2
4. **Exam Details**: Jawaban user per pertanyaan (fase 1 & 2)

### ? Tabel Skor (PENTING!)
5. **Exam Rumpun Scores**: Skor agregat per rumpun dari fase 1
   - Diisi setelah fase 1 selesai
   - Digunakan untuk menghitung skor baseline profesi
   
6. **Nilai Ujian Rumpun**: *(Opsional - bisa digunakan untuk tracking history)*
   - Alternatif/duplikat dari Exam Rumpun Scores
   - Bisa digunakan untuk laporan atau analisis tambahan

### Tabel Referensi
7. **Rumpun**: 5 kelompok karir
8. **Profesi**: 25 profesi
9. **Prodi**: 3 program studi
10. **Linearity Matrix**: Knowledge base kesesuaian

---

## Contoh Perhitungan

### Skenario User:
- Nama: Budi Santoso
- Prodi: TI (Teknik Informatika)
- Minat: Backend Developer
- Fase 1: Juara Software Engineering (60%), Data & AI (35%)
- Skenario: **Hybrid** (gap 25%, jadi ambil 10 soal SE + 10 soal DA)

### Fase 1 - Skor Rumpun (tersimpan di Exam Rumpun Scores):
```
SE: 1.6 (dari 2 soal, rata-rata 0.8)
DA: 1.0 (dari 2 soal, rata-rata 0.5)
IF: 0.4
CS: 1.0
CP: 0.2
```

### Fase 2:
- Menjawab 5 soal Backend (rata-rata 0.8, expert weight 1.2)
- Menjawab 3 soal Data Scientist (rata-rata 0.6, expert weight 1.3)

### Perhitungan Backend Developer:
```
Skor dari Exam Details (fase 2):
= (0.8 × 1.2) × 5 soal = 4.8

Skor Rumpun (dari Exam Rumpun Scores):
= 1.6 × 0.3 = 0.48

Total Skor DST:
= 4.8 + 0.48 = 5.28

Normalized (jika max skor adalah 5.28):
= 5.28 / 5.28 = 1.0

Variabel Minat = 1.0 (sesuai minat)
Skor Minat = 0.3 × 1.0 = 0.3

Skor Akhir = 1.0 + 0.3 = 1.3
Zona = Golden Match
```

### Analisis Linearitas:
- Karir terbaik: Backend Developer (Rumpun: SE)
- Prodi: TI
- Status: **Linear** ?
- Kesimpulan: "Karir terbaik Anda SEJALAN dengan jurusan Anda!"

---

## ?? **Perbedaan Tabel Exam Rumpun Scores vs Nilai Ujian Rumpun**

| Aspek | Exam Rumpun Scores | Nilai Ujian Rumpun |
|-------|-------------------|-------------------|
| Fungsi Utama | **Skor fase 1 untuk perhitungan DST** | Backup/History tracking |
| Diisi Kapan | Setelah fase 1 selesai | Setelah fase 1 selesai |
| Digunakan Oleh | `Hasil Analisis.vb` | (Opsional) Reporting |
| Foreign Key | `id_sesi`, `kode_rumpun` | `id_sesi`, `kode_rumpun` |

**Rekomendasi**: Gunakan **Exam Rumpun Scores** sebagai tabel utama, **Nilai Ujian Rumpun** sebagai backup.

---

## File-File Penting

1. **Model/Hasil Analisis.vb**: Logic perhitungan DST, boosting, zona
2. **Forms/FHasil.vb**: Tampilan hasil akhir
3. **Forms/FPertanyaan.vb**: Adaptive testing & simpan skor rumpun
4. **Database/Insert_Linearity_Matrix.sql**: Data knowledge base linearitas
5. **Database/DataSetProgram.xsd**: Skema database typed dataset

---

## Catatan Penting

- Semua perhitungan skor ternormalisasi 0-1
- Expert weight dari database menentukan bobot setiap pertanyaan
- User confidence dari jawaban Likert (0.0, 0.2, 0.5, 0.8, 0.95)
- Konstanta boost tetap 0.3 (bisa diubah jika diperlukan)
- **Skor rumpun fase 1 memberikan baseline 30% untuk skor profesi**
