# 🎓 Sistem Pakar Profil Lulusan TIK

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![VB.NET](https://img.shields.io/badge/VB.NET-Windows%20Forms-5C2D91)](https://docs.microsoft.com/en-us/dotnet/visual-basic/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-CC2927?logo=microsoftsqlserver)](https://www.microsoft.com/en-us/sql-server)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

> **Sistem Pakar berbasis Adaptive Testing dengan metode Forward Chaining dan Dempster-Shafer Theory untuk prediksi profil karir lulusan TIK**

---

## 📋 Daftar Isi

- [Tentang Proyek](#-tentang-proyek)
- [Fitur Utama](#-fitur-utama)
- [Metode yang Digunakan](#-metode-yang-digunakan)
- [Arsitektur Sistem](#-arsitektur-sistem)
- [Database Schema](#-database-schema)
- [Instalasi](#-instalasi)
- [Cara Penggunaan](#-cara-penggunaan)
- [Struktur Proyek](#-struktur-proyek)
- [Progress Implementasi](#-progress-implementasi)
- [Kontributor](#-kontributor)
- [Lisensi](#-lisensi)

---

## 🎯 Tentang Proyek

Sistem Pakar Profil Lulusan TIK adalah aplikasi desktop berbasis **VB.NET** yang dirancang untuk membantu mahasiswa Teknologi Informasi dan Komunikasi (TIK) dalam menentukan jalur karir yang sesuai dengan **kemampuan** dan **minat** mereka.

### 🔑 Keunikan Sistem

Sistem ini membedakan diri dari sistem pakar konvensional dengan menggunakan:

1. **Adaptive Testing** - Pertanyaan menyesuaikan dengan kemampuan responden
2. **Dempster-Shafer Theory (DST)** - Mengelola ketidakpastian dalam analisis
3. **Interest Boosting** - Meningkatkan skor untuk karir yang diminati (+30%)
4. **Linearity Analysis** - Menilai kesesuaian hasil dengan program studi
5. **Forward Chaining** - Inferensi berbasis aturan untuk rekomendasi karir

---

## ✨ Fitur Utama

### 🧪 Sistem Testing Adaptif Dua Fase

#### **Fase 1: Pertanyaan Umum (10 Soal)**
- 2 soal per rumpun karir (5 rumpun × 2)
- Mengidentifikasi kemampuan dasar pengguna
- Menentukan arah pertanyaan fase 2

#### **Fase 2: Pertanyaan Khusus (20 Soal)**
Sistem secara otomatis memilih 1 dari 4 skenario:

| Skenario | Kondisi | Distribusi Soal |
|----------|---------|-----------------|
| **Dominant** | Gap > 20% & Skor Tertinggi > 40% | 20 soal dari rumpun terkuat (4 soal/sub-karir) |
| **Hybrid** | Gap ≤ 20% & Skor Tertinggi > 40% | 10 soal juara 1 + 10 soal juara 2 (2 soal/sub-karir) |
| **Beginner** | Skor Tertinggi < 40% & Ada Minat | 20 soal dari rumpun minat (2/4 soal/sub-karir) |
| **Null** | Skor Tertinggi < 40% & Tanpa Minat | 20 soal Creative & Product (4 soal/sub-karir) |

**Catatan Penting**: 
- Jika user tidak memilih minat, sistem akan tetap memilih skenario Dominant atau Hybrid berdasarkan gap dan top score
- Skenario Special Hybrid telah diintegrasikan ke dalam Hybrid scenario untuk konsistensi

### 📊 Skala Likert 5 Poin

| Pilihan | Deskripsi | Nilai Probabilitas |
|---------|-----------|-------------------|
| Sangat Tidak Mampu | Tidak punya pengetahuan sama sekali | 0.0 |
| Kurang Mampu | Pernah dengar tapi lupa/tidak paham | 0.2 |
| Cukup Mampu | Paham teori dasar | 0.5 |
| Mampu | Sering mempraktekkan | 0.8 |
| Sangat Mampu | Sangat menguasai/Ahli | 0.95 |

**Catatan**: Nilai maksimum adalah 0.95 (bukan 1.0) untuk menjaga aspek ketidakpastian dalam perhitungan DST.

### 🎯 Analisis Hasil

#### **Zona Karir (Skill vs Interest)**

```
┌─────────────────┬──────────────────┬───────────────────┐
│     Kondisi     │       Zona       │    Rekomendasi    │
├─────────────────┼──────────────────┼───────────────────┤
│ Skill ⬆ + Minat⬆│ Golden Match     │ ⭐⭐⭐ Jalur Ideal │
│ Skill ⬆ + Minat⬇│ Reality Check    │ ⚠️ Pertimbangkan   │
│ Skill ⬇ + Minat⬆│ Hidden Gem       │ 💎 Potensi Besar  │
│ Skill ⬇ + Minat⬇│ Explorer         │ 🔍 Perlu Eksplorasi│
└─────────────────┴──────────────────┴───────────────────┘
```

**Threshold**: Skill ≥ 0.5 = Tinggi, Interest ≥ 0.5 (variabel minat = 1.0) = Tinggi

#### **Linearity Analysis**

| Status | Makna | Program Studi |
|--------|-------|---------------|
| **Linear** ✅ | Sejalan dengan prodi | TI (Software/Data/CyberSec), TMJ (Infra/CyberSec), TMD (Creative) |
| **Related** ⚠️ | Masih terkait dengan prodi | TI (Infra/Creative), TMJ (Software), TMD (Software) |
| **Pivot** 🔄 | Tidak relevan dengan prodi | TI (N/A), TMJ (Creative), TMD (Data/Infra/CyberSec) |

---

## 🧬 Metode yang Digunakan

### 1. **Forward Chaining**
```
IF jawaban_fase1 THEN tentukan_skenario
IF skenario = "Dominant" THEN ambil_soal_rumpun_tertinggi
IF skenario = "Hybrid" THEN ambil_soal_2_rumpun_tertinggi
IF skenario = "Beginner" THEN ambil_soal_rumpun_minat
IF skenario = "Null" THEN ambil_soal_creative_product
```

**Implementasi**: Lihat `FPertanyaan.ProsesLogikaFase2()` untuk logika lengkap.

### 2. **Dempster-Shafer Theory (DST)**

**Rumus Massa Keyakinan:**
```
m(H) = Bobot_Pakar × Confidence_User
```

**Contoh Perhitungan:**
- Pertanyaan: "Seberapa paham Anda REST API?"
- Bobot Pakar (expert weight): 0.8
- Jawaban User: "Mampu" → 0.8
- **m(Backend Developer)** = 0.8 × 0.8 = **0.64**

**Kombinasi Skor**:
- **Fase 2 (70%)**: SUM(user_confidence × expert_weight) per profesi
- **Fase 1 (30%)**: Skor rumpun × 0.3 (atau × 0.5 jika tidak ada skor fase 2)
- **Normalisasi**: Semua skor dinormalisasi ke rentang [0-1]

**Implementasi**: `Hasil Analisis.HitungSkorDSTProfesi()`

### 3. **Interest Boosting**

**Rumus Skor Akhir:**
```
Skor_Akhir = Skor_DST + (0.3 × Variabel_Minat)

dimana:
- Skor_DST: Hasil perhitungan Dempster-Shafer (normalized)
- Konstanta Boost: 0.3 (30% boost)
- Variabel_Minat: 1.0 (jika diminati) atau 0.0 (jika tidak)
```

**Contoh:**
- Profesi: Backend Developer
- Skor DST: 0.75
- User memilih Backend Developer sebagai minat → Variabel Minat = 1.0
- **Skor Akhir** = 0.75 + (0.3 × 1.0) = **1.05** (dinormalisasi ke 1.0)

---

## 🏗️ Arsitektur Sistem

### Alur Sistem (Flowchart)

```
┌─────────────────┐
│  START          │
└────────┬────────┘
         │
    ┌────▼────┐
    │ Input:  │
    │ - Nama  │
    │ - Prodi │
    │ - Minat │
    └────┬────┘
         │
┌────────▼────────┐
│ FASE 1:         │
│ 10 Soal Umum    │
└────────┬────────┘
         │
    ┌────▼────────────────┐
    │ Evaluasi Skor Fase 1│
    │ - Hitung % per Rumpun│
    │ - Tentukan Gap      │
    └────┬────────────────┘
         │
    ┌────▼─────┐
    │ Skenario │
    │ Decision │
    └─┬──┬──┬──┬─┘
      │  │  │  │
  ┌───▼┐┌▼┐┌▼┐┌▼───┐
  │Dom││Hy││Be││Null│
  └───┬┘└┬┘└┬┘└┬───┘
      └──┬┘  └──┬───┘
         │      │
    ┌────▼──────▼────┐
    │ FASE 2:         │
    │ 20 Soal Spesifik│
    └────────┬────────┘
             │
    ┌────────▼────────┐
    │ Perhitungan:    │
    │ - DST           │
    │ - Boost Interest│
    │ - Zona Karir    │
    └────────┬────────┘
             │
    ┌────────▼────────┐
    │ Output:         │
    │ - Top 3 Karir   │
    │ - Zona Analysis │
    │ - Linearity     │
    └─────────────────┘
             │
         ┌───▼───┐
         │  END  │
         └───────┘
```

### Struktur Folder

```
Si Pakar/
├── Database/
│   └── Database Sistem Pakar.mdf  # LocalDB database file
├── Forms/
│   ├── FStart.vb                  # Landing page + input data
│   ├── FMain.vb                   # Menu utama
│   ├── FPertanyaan.vb             # Fase 1 & 2 testing
│   ├── FHasil.vb                  # Tampilan hasil akhir
│   └── FUpdateDataUser.vb         # Update data user
├── Modul/                         # ⚠️ BUKAN "Model/"
│   ├── Koneksi.vb                 # Database connection
│   ├── Data User.vb               # CRUD user
│   ├── Sesi Ujian.vb              # Session management
│   ├── PertanyaanModel.vb         # Model pertanyaan
│   ├── Profesi.vb                 # CRUD profesi
│   ├── Prodi.vb                   # CRUD program studi
│   ├── Hasil Analisis.vb          # Logika DST + Scoring
│   └── Pengaturan Sesi.vb         # Session utilities
└── Intsruksi Buat Mbah/
    ├── Dokumen.md                 # Dokumentasi metode
    ├── Flowchart.md               # Alur logika
    ├── Pertanyaan.md              # Bank soal (110 soal)
    └── Data Tabel Rumpun.md       # Mapping rumpun-profesi
```

---

## 🗄️ Database Schema

### ERD (Entity Relationship Diagram)

```
┌─────────────┐
│   Prodi     │
└──────┬──────┘
       │1
       │
       │M
┌──────▼──────┐       ┌─────────────┐
│  Data User  │◄──┐   │   Rumpun    │
└──────┬──────┘   │1  └──────┬──────┘
       │1         │          │1
       │          │          │
       │M         │M         │M
┌──────▼──────┐   │   ┌──────▼──────┐
│ Sesi Ujian  │   │   │   Profesi   │
└──────┬──────┘   │   └──────┬──────┘
       │1         │          │1
       ├──────────┘          │
       │                     │
       │M                    │M
┌──────▼──────┐       ┌──────▼──────┐
│Exam Details │       │ Pertanyaan  │
└─────────────┘       └─────────────┘
       │                     │
       └──────────┬──────────┘
                  │M
         ┌────────▼────────┐
         │ Map Bukti       │
         │ Pertanyaan      │
         └─────────────────┘
         
         ┌─────────────────┐
         │ Exam Rumpun     │
         │ Scores          │
         └─────────────────┘
         
         ┌─────────────────┐
         │ Linearity       │
         │ Matrix          │
         └─────────────────┘
```

### Tabel Utama

#### **1. Rumpun (5 kategori)**
```sql
SE - Software Engineering
DI - Data & Intelligence  
IN - Infrastructure
CS - Cyber Security
CP - Creative & Product
```

#### **2. Profesi (25 profesi - 5 per rumpun)**
Contoh:
- **SE**: Backend Developer (BED), Frontend Developer (FED), Mobile Developer (MBD), Game Programmer (GMP), QA Engineer (QAE)
- **DI**: Data Scientist (DSC), AI Engineer (AIE), Data Analyst (DAN), Data Engineer (DEN), BI Analyst (BIA)
- **IN**: Network Engineer (NTE), System Administrator (SYA), DevOps Engineer (DOE), IoT Engineer (IOE), IT Support Specialist (ITS)
- **CS**: Pentester (PNT), Security Analyst (SCA), SecDevOps Engineer (SDO), Digital Forensics (DGF), GRC Specialist (GRC)
- **CP**: UI Designer (UID), UX Researcher (UXR), Graphic Designer (GRD), Multimedia Specialist (MMS), Product Manager (PMA)

#### **3. Pertanyaan (110 soal)**
- **Fase 1**: 10 soal general (G01-G10) - `kode profesi` = NULL
- **Fase 2**: 100 soal spesifik - `kode profesi` = SET
  - 20 soal SE (SE01-SE20)
  - 20 soal DI (DI01-DI20)
  - 20 soal IN (IN01-IN20)
  - 20 soal CS (CS01-CS20)
  - 20 soal CP (CP01-CP20)

---

## 🚀 Instalasi

### Prasyarat

- **Windows 10/11** (64-bit)
- **.NET 10.0 SDK** atau lebih baru
- **Visual Studio 2022** (Community/Professional/Enterprise)
- **SQL Server LocalDB** (biasanya sudah terinstal dengan Visual Studio)
- **Git** (untuk clone repository)

### Langkah Instalasi

#### 1. Clone Repository
```bash
git clone https://github.com/maul-lq/Sistem-Pakar-Profil-Lulusan-TIK.git
cd Sistem-Pakar-Profil-Lulusan-TIK
```

#### 2. Setup Database
Database menggunakan **LocalDB file** (`.mdf`) yang sudah disertakan dalam repository:
- File lokasi: `Si Pakar\Database\Database Sistem Pakar.mdf`
- Database sudah berisi 10 tabel + 110+ data soal
- **Tidak perlu menjalankan script SQL manual**

#### 3. Konfigurasi Connection String
Edit file `Si Pakar\Modul\Koneksi.vb`:
```vb
Public Function GetConnection()
    ' ⚠️ PENTING: Sesuaikan path dengan lokasi project Anda
    Return New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\NAMA_USER_ANDA\source\repos\Sistem Pakar Profil Lulusan TIK\Si Pakar\Database\Database Sistem Pakar.mdf;Integrated Security=True")
End Function
```

**Cara cepat mendapatkan path yang benar**:
1. Klik kanan pada file `Database Sistem Pakar.mdf` di Solution Explorer
2. Pilih "Copy Full Path"
3. Paste ke connection string bagian `AttachDbFilename=...`

#### 4. Build & Run
```bash
# Dari Visual Studio:
# 1. Open Solution: Si Pakar.sln
# 2. Set 'Si Pakar' sebagai Startup Project
# 3. Press F5 atau klik Start
```

---

## 📖 Cara Penggunaan

### 1. Halaman Start
- Masukkan **Nama Lengkap**
- Pilih **Program Studi** (TI/TMJ/TMD)
- Pilih **Minat Karir** (maksimal 3 dari 25 profesi) - **OPSIONAL**
- Klik **"MULAI TES"**

### 2. Fase 1 - Pertanyaan Umum
- Akan diberikan **10 soal** umum (2 soal per rumpun)
- Pilih jawaban sesuai kemampuan Anda (Skala Likert 1-5)
- Navigasi: Gunakan tombol nomor atau "Sebelumnya/Selanjutnya"
- **Submit** hanya aktif jika semua soal dijawab (button hijau = sudah dijawab)

### 3. Sistem Menentukan Skenario
Sistem akan menampilkan (dalam mode DEBUG):
- **"Skenario Terdeteksi: Dominant"** → Fokus pada 1 rumpun terkuat
- **"Skenario Terdeteksi: Hybrid"** → Menguji 2 rumpun teratas
- **"Skenario Terdeteksi: Beginner"** → Berbasis minat
- **"Skenario Terdeteksi: Null"** → Default Creative & Product

### 4. Fase 2 - Pertanyaan Khusus
- **20 soal** spesifik sesuai skenario
- Sama seperti Fase 1, jawab semua pertanyaan
- Klik **"LIHAT HASIL AKHIR"** setelah selesai

### 5. Halaman Hasil
Akan ditampilkan:
- **Top 3 Karir** (urut berdasarkan skor akhir dengan 2 desimal)
- **Zona Analisis** (4 zona):
  - Golden Match (Skill Tinggi + Minat Tinggi) - ⭐⭐⭐
  - Hidden Gem (Skill Rendah + Minat Tinggi) - 💎
  - Reality Check (Skill Tinggi + Minat Rendah) - ⚠️
  - Explorer (Skill Rendah + Minat Rendah) - 🔍
- **Linearity Analysis** (popup message):
  - ✅ Linear: Sesuai dengan jurusan
  - ⚠️ Related: Masih terkait
  - 🔄 Pivot: Perlu upskilling

---

## 📁 Struktur Proyek

```
Sistem-Pakar-Profil-Lulusan-TIK/
│
├── Si Pakar/                          # Main Project
│   ├── Database/
│   │   └── Database Sistem Pakar.mdf  # LocalDB database file (BUKAN .sql)
│   ├── Forms/
│   │   ├── FStart.vb                  # Form awal (input data user)
│   │   ├── FMain.vb                   # Form menu utama
│   │   ├── FPertanyaan.vb             # Form testing (Fase 1 & 2)
│   │   ├── FHasil.vb                  # Form hasil akhir
│   │   └── FUpdateDataUser.vb         # Form update data user
│   ├── Modul/                         # ⚠️ BUKAN "Model/"
│   │   ├── Koneksi.vb                 # Database connection
│   │   ├── Data User.vb               # CRUD user
│   │   ├── Sesi Ujian.vb              # Manajemen sesi ujian
│   │   ├── PertanyaanModel.vb         # Model pertanyaan
│   │   ├── Profesi.vb                 # CRUD profesi
│   │   ├── Prodi.vb                   # CRUD program studi
│   │   ├── Hasil Analisis.vb          # Logika DST, scoring, zona
│   │   └── Pengaturan Sesi.vb         # Session utilities
│   ├── Intsruksi Buat Mbah/
│   │   ├── Dokumen.md                 # Dokumentasi metode
│   │   ├── Flowchart.md               # Diagram alur sistem
│   │   ├── Pertanyaan.md              # Bank soal lengkap
│   │   ├── Data Tabel Rumpun.md       # Mapping data
│   │   └── FITUR_VALIDASI.md          # Dokumentasi fitur validasi
│   ├── My Project/
│   │   ├── Application.Designer.vb
│   │   └── Resources.Designer.vb
│   ├── ImageRes.Designer.vb
│   ├── ApplicationEvents.vb
│   └── Si Pakar.vbproj
├── README.md                          # Dokumentasi utama (file ini)
└── LICENSE                            # MIT License
```

---

## 📊 Progress Implementasi

### ✅ Selesai (100%)

| Fitur | Status | Keterangan |
|-------|--------|------------|
| **Database Schema** | ✅ Done | 10 tabel + relasi lengkap |
| **Input Data User** | ✅ Done | Form FStart - nama, prodi, minat (opsional) |
| **Fase 1 Testing** | ✅ Done | 10 soal umum + navigasi |
| **Validasi Jawaban** | ✅ Done | Button submit disabled sampai lengkap |
| **Visual Indicator** | ✅ Done | Button hijau = sudah dijawab |
| **Adaptive Logic** | ✅ Done | 4 skenario (Dominant/Hybrid/Beginner/Null) |
| **No Interest Handling** | ✅ Done | Sistem tetap berfungsi tanpa minat user |
| **Fase 2 Testing** | ✅ Done | 20 soal adaptif + distribusi per sub-karir |
| **DST Calculation** | ✅ Done | Perhitungan massa keyakinan + normalisasi |
| **Interest Boosting** | ✅ Done | +30% untuk profesi diminati |
| **Zona Karir** | ✅ Done | 4 zona (Golden/Hidden/Reality/Explorer) |
| **Linearity Analysis** | ✅ Done | Mapping prodi vs rumpun hasil |
| **Exam Rumpun Scores** | ✅ Done | Simpan skor per rumpun fase 1 |
| **Form Hasil** | ✅ Done | Top 3 + Zona + Linearitas |
| **Form Update User** | ✅ Done | Update data user (FUpdateDataUser) |
| **JSON Serialization** | ✅ Done | Phase1 score json + Final result json |

### 🚧 Dalam Pengembangan

| Fitur | Status | Target |
|-------|--------|--------|
| **Export PDF** | 🚧 Planned | Print/export hasil ke PDF |

---

## 🧪 Testing & Debugging

### Test Case Scenario

#### **Test 1: Skenario Dominant**
```
Input:
- User: Mahasiswa TI
- Minat: Backend Developer
- Fase 1: 
  - SE (8 poin) = 80%
  - DI (3 poin) = 30%
  - Gap: 50% > 20% ✓

Expected:
- Skenario: Dominant
- Fase 2: 20 soal SE (4 soal × 5 sub-karir)
- Top Result: Backend Developer (dengan interest boost)
```

#### **Test 2: Skenario Hybrid**
```
Input:
- User: Mahasiswa TMJ
- Minat: Network Engineer
- Fase 1:
  - IN (6 poin) = 60%
  - CS (5 poin) = 50%
  - Gap: 10% < 20% ✓
  
Expected:
- Skenario: Hybrid
- Fase 2: 10 soal IN + 10 soal CS (2 soal × 5 sub-karir)
- Linearity: Linear (TMJ matches IN & CS)
```

#### **Test 3: Skenario Null (No Interest)**
```
Input:
- User: Mahasiswa TI
- Minat: (tidak dipilih)
- Fase 1: Semua < 40%

Expected:
- Skenario: Null
- Fase 2: 20 soal CP (Creative & Product)
- No interest boost applied
```

### Bug Fixes

#### **Bug #1: Soal Fase 2 Otomatis Terisi** ✅ Fixed
- **Problem**: Satu soal fase 2 terisi otomatis dengan jawaban fase 1
- **Root Cause**: `currNum` tidak di-reset + RadioButton masih checked
- **Solution**: Reset `currNum = 0` dan `ResetRadioButtons()` di `ProsesLogikaFase2()`

#### **Bug #2: Index Out of Range** ✅ Fixed
- **Problem**: Error saat load fase 2
- **Root Cause**: Validasi `currNum` kurang ketat
- **Solution**: Triple validation di `SimpanJawabanSementara()`

#### **Bug #3: Question ID Pipe Delimiter** ✅ Fixed
- **Problem**: Kode profesi ter-append di ID pertanyaan (`SE01|BED`)
- **Root Cause**: Temporary workaround untuk mapping profession
- **Solution**: Split dan restore ID asli di `FilterSoalDistribusi()` sebelum save

---

## 📝 Lisensi

Proyek ini dilisensikan di bawah **MIT License** - lihat file [LICENSE](LICENSE) untuk detail.

```
MIT License

Copyright (c) 2024 [Nama Kelompok/Universitas]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction...
```

---

## 🙏 Ucapan Terima Kasih

- **Github Copilot** - Asisten coding yang luar biasa
- **Gemini AI** - Untuk brainstorming ide dan dokumentasi
- **ChatGPT** - Bantuan dalam penulisan dan debugging
- **Qwen AI** - Sumber referensi tambahan

---

## 📧 Kontak & Dukungan

- **GitHub Issues**: [Link ke Issues](https://github.com/maul-lq/Sistem-Pakar-Profil-Lulusan-TIK/issues)
- **Repository**: [GitHub](https://github.com/maul-lq/Sistem-Pakar-Profil-Lulusan-TIK)

---

## 📚 Referensi

1. Shafer, G. (1976). *A Mathematical Theory of Evidence*. Princeton University Press.
2. Russell, S., & Norvig, P. (2020). *Artificial Intelligence: A Modern Approach* (4th ed.). Pearson.
3. Microsoft. (2024). *.NET Documentation*. https://docs.microsoft.com/en-us/dotnet/
4. Microsoft. (2024). *VB.NET Documentation*. https://docs.microsoft.com/en-us/dotnet/visual-basic/

---

## 🔮 Roadmap

### Version 2.0 (Future Release)
- [ ] Export hasil ke PDF/Excel
- [ ] Analisis statistik global (semua user)
- [ ] Dashboard visualisasi data
- [ ] Integrasi Machine Learning untuk prediksi yang lebih akurat
- [ ] Web-based version (ASP.NET Core)
- [ ] Mobile App (Xamarin/MAUI)
- [ ] Multi-language support (English, Indonesian)

---

<div align="center">

**⭐ Jika proyek ini membantu Anda, berikan star di GitHub! ⭐**

Made with ❤️ by **[Bahlil]** | **[1920]**

[⬆ Kembali ke atas](#-sistem-pakar-profil-lulusan-tik)

</div>
