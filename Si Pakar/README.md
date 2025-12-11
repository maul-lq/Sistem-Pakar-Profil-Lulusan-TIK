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

### 🎭 Landing Page dengan Quote Dinamis

- **Random Inspirational Quotes** - 10 kutipan motivasi dipilih secara acak
- **Dynamic Subtitles** - 10 subtitle berbeda untuk variasi tampilan
- **Centered Layout** - Quote dan subtitle diposisikan otomatis di tengah panel

### 📝 Form Input Data (FMain)

- **Nama Lengkap** (Required) - Validasi tidak boleh kosong
- **Email** (Required) - Validasi format email dengan regex pattern
- **Program Studi** (Required) - Dropdown selection (TI/TMJ/TMD)
- **Minat Karir** (Optional) - **SINGLE selection** dari 25 profesi atau KOSONG
- **Real-time Validation** - Error provider dengan feedback langsung
- **Smart Button State** - Tombol "Lanjut" disabled sampai input valid

### 🧪 Sistem Testing Adaptif Dua Fase

#### **Fase 1: Pertanyaan Umum (10 Soal)**

- 2 soal per rumpun karir (5 rumpun × 2)
- Mengidentifikasi kemampuan dasar pengguna
- Menentukan arah pertanyaan fase 2

#### **Fase 2: Pertanyaan Khusus (20 Soal)**

Sistem secara otomatis memilih 1 dari **5 skenario**:

| Skenario | Kondisi | Distribusi Soal |
|----------|---------|-----------------|
| **Dominant** | Gap > 20% & Top Score > 40% & Interest matches Top 2 | 20 soal dari rumpun terkuat (4 soal/profesi) |
| **Hybrid** | Gap ≤ 20% & Top Score > 40% & Interest matches Top 2 | 10 soal juara 1 + 10 soal juara 2 (2 soal/profesi) |
| **Special Hybrid** | Top Score > 40% & Interest NOT match Top 2 | 10 soal juara 1 + 10 soal minat (2 soal/profesi) |
| **Beginner** | Top Score < 40% & Ada Minat | 20 soal dari rumpun minat (4 soal/profesi) |
| **Null** | Top Score < 40% & Tanpa Minat | 20 soal Creative & Product (4 soal/profesi) |

**Catatan Penting**:
- User hanya bisa memilih **SATU minat** atau **TIDAK MEMILIH** sama sekali
- Jika user tidak memilih minat, sistem akan pilih skenario Dominant/Hybrid (jika skill > 40%) atau Null (jika skill < 40%)
- Special Hybrid muncul ketika user punya skill tinggi tapi minatnya berbeda dari top 2 cluster

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
│ Skill ⬆ + Minat⬇│ Hidden Gem       │ 💎 Potensi Tersembunyi│
│ Skill ⬇ + Minat⬆│ Reality Check    │ ⚠️ Butuh Kerja Keras│
│ Skill ⬇ + Minat⬇│ Explorer         │ 🔍 Perlu Eksplorasi│
└─────────────────┴──────────────────┴───────────────────┘
```

**Threshold**: Skill ≥ 0.5 = Tinggi, Interest = 1.0 (dipilih) atau 0.0 (tidak dipilih)

**Penjelasan Zona:**

1. **Golden Match** ⭐⭐⭐ (Skill Tinggi + Minat Tinggi)
   > "Ini DESTINY-mu! Kamu JAGO dan SUKA bidang ini"
   - **Rekomendasi**: KEJAR dengan maksimal! Ini jalur karir terbaik untukmu
   - **Contoh**: Mahasiswa TI yang jago coding (skill 0.85) dan memilih Backend Developer sebagai minat

2. **Hidden Gem** 💎 (Skill Tinggi + Minat Rendah)
   > "Berlian tersembunyi - kamu BERBAKAT tapi belum SADAR/EKSPLORASI"
   - **Rekomendasi**: Pertimbangkan untuk dicoba! Kamu punya bakat alami di sini
   - **Contoh**: Mahasiswa TI yang jago Backend (skill 0.85) tapi minatnya UI Designer - sistem deteksi: "Kamu sebenarnya bagus di Backend, yakin ga mau coba?"

3. **Reality Check** ⚠️ (Skill Rendah + Minat Tinggi)
   > "Hadapi kenyataan - kamu SANGAT tertarik tapi skill BELUM cukup"
   - **Rekomendasi**: Kerja keras! Passion sudah ada, sekarang bangun skill-nya
   - **Contoh**: Mahasiswa TI yang suka UI Design (minat) tapi skill masih 0.35 - butuh belajar intensif untuk achieve dream career

4. **Explorer** 🔍 (Skill Rendah + Minat Rendah)
   > "Belum nemu passion dan skill-mu di bidang ini"
   - **Rekomendasi**: Keep exploring! Coba bidang-bidang lain
   - **Contoh**: Profesi yang tidak dipilih dan hasil tes juga rendah

**Mengapa Top 3 Bisa Masuk Hidden Gem?**
- **Hidden Gem** = Skill tinggi (≥0.5) TAPI user TIDAK memilih profesi ini sebagai minat
- **Contoh**: User jago Software Engineering (score 0.85) tapi minatnya UI Designer (Creative)
- **Rekomendasi Sistem**: "Kamu berbakat di sini, mungkin ini potensi yang overlooked! Worth considering?"

**Kapan Hidden Gem Kosong?**
1. ✅ Semua profesi dengan skill tinggi JUGA diminati (masuk Golden Match)
2. ✅ Semua profesi punya skill rendah (<0.5) - tidak ada yang masuk Hidden Gem
3. ✅ User pilih minat yang TIDAK match dengan skill terbaik mereka (semua skill tinggi diminati)

**Kapan Reality Check Kosong?**
1. ✅ User TIDAK memilih minat sama sekali (semua profesi minat = 0.0)
2. ✅ Semua profesi yang diminati JUGA punya skill tinggi (masuk Golden Match)
3. ✅ User pilih minat yang match dengan skill terbaik mereka

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
IF skenario = "Special Hybrid" THEN ambil_soal_rumpun_tertinggi + rumpun_minat
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

### Interest Boosting Algorithm

**Formula**:
```vb
Final_Score = DST_Score + (KONSTANTA_BOOST × Interest_Variable)

Where:
- KONSTANTA_BOOST = 0.3 (30% boost)
- Interest_Variable = 1.0 if profession matches user interest
                    = 0.0 if profession does NOT match user interest
```

**Example**:
- Profession: Backend Developer
- DST Score: 0.75
- User selected "Backend Developer" as interest → Interest_Variable = 1.0
- **Raw Score** = 0.75 + (0.3 × 1.0) = 1.05
- **After Re-normalization** = 1.05 / 1.05 = **1.00** (capped to [0-1] range)

**Re-normalization Process**:
After interest boosting is applied, the system performs a **second normalization** on professions tested in Phase 2:
```vb
' Find max score among tested professions
maxSkorAkhir = max(all final scores for tested professions)

' Re-normalize each profession
normalized_score = final_score / maxSkorAkhir
```

This ensures:
- ✅ All scores remain in [0-1] range (never exceed 100%)
- ✅ Relative ranking is preserved
- ✅ Interest boost still provides advantage without breaking the scale

---

## 🏗️ Arsitektur Sistem

### Alur Sistem (Flowchart)

```
┌─────────────────┐
│  FStart         │
│  (Landing Page) │
│  Random Quote   │
└────────┬────────┘
         │
    ┌────▼────────┐
    │  FMain      │
    │  Input:     │
    │  - Nama     │
    │  - Email    │
    │  - Prodi    │
    │  - Minat (1)│
    └────┬────────┘
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
    └─┬─┬─┬─┬─┬─┘
      │ │ │ │ │
  ┌───▼┐▼┐▼┐▼┐▼───┐
  │Dom││Hy││SH││Be││Null│
  └───┬┘└┬┘└┬┘└┬┘└┬──┘
      └──┴──┴──┴──┘
         │
    ┌────▼──────────┐
    │ FASE 2:       │
    │ 20 Soal Adaptif│
    └────────┬──────┘
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
│   ├── FStart.vb                  # Landing page (random quotes)
│   ├── FMain.vb                   # Input data (nama, email, prodi, 1 minat)
│   ├── FPertanyaan.vb             # Form testing (Fase 1 & 2)
│   ├── FHasil.vb                  # Form hasil akhir
│   └── FUpdateDataUser.vb         # Form update data user
├── Modul/                         # ⚠️ BUKAN "Model/"
│   ├── Koneksi.vb                 # Database connection
│   ├── Data User.vb               # CRUD user
│   ├── Sesi Ujian.vb              # Manajemen sesi ujian
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

Proyek ini menyediakan **DUA opsi** setup database:

##### **Opsi A: Menggunakan LocalDB File (Recommended - Termudah)** ✅

Database file `.mdf` sudah disertakan dalam repository dan siap pakai:

- **File lokasi**: `Si Pakar\Database\Database Sistem Pakar.mdf`
- **Isi**: 10 tabel + 110 pertanyaan + 25 profesi
- **Keuntungan**: Tidak perlu setup manual, langsung jalan

**Connection String** sudah dikonfigurasi otomatis di `Koneksi.vb`:

```vb
Module Koneksi
    Public Function GetConnection()
#If PUBLISH Then
        ' PRODUCTION MODE: Path relatif (otomatis)
        Dim path = AppDomain.CurrentDomain.BaseDirectory
        Dim dbPath = "Database\Database Sistem Pakar.mdf"
        Dim fullPath As String = IO.Path.Combine(path, dbPath)
        Return New SqlConnection($"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={fullPath};Integrated Security=True")
#Else
        ' DEBUG MODE: Path absolut (developer-specific)
        Return New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rizlrad Fz\source\repos\Sistem Pakar Profil Lulusan TIK\Si Pakar\Database\Database Sistem Pakar.mdf;Integrated Security=True")
#End If
    End Function
End Module
```

**⚠️ PENTING untuk Development:**
Jika lokasi project Anda **BERBEDA** dari `C:\Users\Rizlrad Fz\source\repos\...`, Anda perlu **update path di mode DEBUG**:

1. Buka file `Si Pakar\Modul\Koneksi.vb`
2. Edit bagian `#Else` (DEBUG mode):
   ```vb
   #Else
       ' Ganti path ini sesuai lokasi project Anda
       Return New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\NAMA_USER_ANDA\source\repos\Sistem Pakar Profil Lulusan TIK\Si Pakar\Database\Database Sistem Pakar.mdf;Integrated Security=True")
   #End If
   ```

**Cara cepat mendapatkan path yang benar**:
1. Klik kanan pada file `Database Sistem Pakar.mdf` di Solution Explorer
2. Pilih **"Copy Full Path"**
3. Paste ke connection string bagian `AttachDbFilename=...`

**✅ Untuk Production/Publish**: Mode `PUBLISH` akan otomatis menggunakan path relatif, jadi **tidak perlu edit manual**.

---

##### **Opsi B: Menggunakan SQL Script (Manual Setup)** 🔧

Jika Anda ingin setup database dari awal atau menggunakan SQL Server instance (bukan LocalDB):

1. **Buka SQL Server Management Studio (SSMS)** atau **Azure Data Studio**

2. **Buat database baru**:
   ```sql
   CREATE DATABASE [Database Sistem Pakar];
   GO
   USE [Database Sistem Pakar];
   GO
   ```

3. **Jalankan script SQL**:
   - File: `Si Pakar\Database\Database.sql`
   - Script ini akan:
     - Membuat **10 tabel** dengan relasi Foreign Key
     - Insert **110 pertanyaan** (10 fase 1 + 100 fase 2)
     - Insert **25 profesi** (5 per rumpun)
     - Insert data master (Rumpun, Prodi)

4. **Update Connection String di `Koneksi.vb`**:
   ```vb
   Module Koneksi
       Public Function GetConnection()
           ' Untuk SQL Server Instance (bukan LocalDB)
           Return New SqlConnection("Server=YOUR_SERVER_NAME;Database=Database Sistem Pakar;Integrated Security=True")
       End Function
   End Module
   ```

**Kapan menggunakan Opsi B?**
- ✅ Deploy ke server production
- ✅ Menggunakan SQL Server Express/Standard/Enterprise
- ✅ Perlu kontrol penuh atas database
- ✅ Tim development dengan shared database server

---

#### 3. Konfigurasi Build Mode

Proyek ini memiliki **2 build configuration**:

| Mode | Condition | Connection String | Use Case |
|------|-----------|-------------------|----------|
| **DEBUG** | `#Else` | Path absolut (hardcoded) | Development di local machine |
| **PUBLISH** | `#If PUBLISH Then` | Path relatif (dynamic) | Production/Deployment |

**Untuk Development** (DEBUG mode):
```bash
# Di Visual Studio:
# 1. Pastikan Configuration = "Debug"
# 2. Path di Koneksi.vb sudah benar
# 3. Press F5 untuk run
```

**Untuk Production** (PUBLISH mode):
```bash
# Di Visual Studio:
# 1. Klik Build > Publish Si Pakar
# 2. Pilih target folder
# 3. Click "Publish"
# 4. File .mdf akan di-copy otomatis ke folder output
# 5. Path database akan resolved secara relatif
```

---

#### 4. Build & Run

##### **Development Mode (F5 - Debug)**
```bash
# Dari Visual Studio:
1. Open Solution: Si Pakar.sln
2. Set 'Si Pakar' sebagai Startup Project (klik kanan project > Set as Startup Project)
3. Pastikan Configuration = "Debug"
4. Press F5 atau klik "Start"
```

##### **Production Mode (Publish)**
```bash
# Dari Visual Studio:
1. Klik kanan pada project "Si Pakar" > Publish
2. Pilih target: Folder
3. Atur lokasi output (misal: C:\Deploy\SistemPakar)
4. Klik "Publish"
5. Copy folder Database/ ke lokasi yang sama dengan .exe
6. Jalankan Si Pakar.exe
```

---

### Troubleshooting Instalasi

#### ❌ **Error: "Cannot open database 'Database Sistem Pakar'"**

**Solusi**:
1. Pastikan SQL Server LocalDB terinstal:
   ```powershell
   sqllocaldb info
   ```
   Jika tidak ada, install dari Visual Studio Installer:
   - Buka Visual Studio Installer
   - Modify > Individual Components
   - Cari "SQL Server Express 2019 LocalDB" ✓
   - Install

2. Pastikan instance MSSQLLocalDB berjalan:
   ```powershell
   sqllocaldb start MSSQLLocalDB
   ```

3. Periksa path di `Koneksi.vb` sudah benar

---

#### ❌ **Error: "System.Data.SqlClient not found"**

**Solusi**:
1. Install NuGet package:
   ```
   Tools > NuGet Package Manager > Manage NuGet Packages for Solution
   ```
2. Search: `Microsoft.Data.SqlClient`
3. Install untuk project "Si Pakar"
4. Rebuild solution

---

#### ❌ **Error: "Access Denied" saat akses .mdf file**

**Solusi**:
1. **Mode DEBUG**: Pastikan path mengarah ke folder project Anda
2. **Mode PUBLISH**: Pastikan folder Database/ berada di folder yang sama dengan .exe
3. Run Visual Studio sebagai Administrator (klik kanan > Run as Administrator)

---

#### ⚠️ **Database file terkunci (in use)**

**Solusi**:
1. Stop aplikasi yang sedang berjalan
2. Disconnect semua koneksi di SSMS:
   ```sql
   USE master;
   GO
   ALTER DATABASE [Database Sistem Pakar] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
   GO
   ALTER DATABASE [Database Sistem Pakar] SET MULTI_USER;
   GO
   ```
3. Atau restart SQL Server LocalDB:
   ```powershell
   sqllocaldb stop MSSQLLocalDB
   sqllocaldb start MSSQLLocalDB
   ```

---

### Verifikasi Instalasi

Setelah instalasi selesai, verifikasi dengan langkah berikut:

1. ✅ **Run aplikasi** - Form FStart muncul dengan random quote
2. ✅ **Input data** - Form FMain validasi email dan nama berhasil
3. ✅ **Phase 1** - 10 soal muncul dengan navigasi lancar
4. ✅ **Phase 2** - Scenario detection bekerja (check console DEBUG)
5. ✅ **Results** - Form FHasil menampilkan Top 3 + Zona + Linearity

Jika semua langkah di atas berhasil, instalasi Anda **SUKSES**! 🎉

---

### Tips Development

**1. Multi-developer Setup:**
```vb
' Tambahkan conditional per developer
#If PUBLISH Then
    ' Production
#ElseIf DEVELOPER_A Then
    Return New SqlConnection("...path developer A...")
#ElseIf DEVELOPER_B Then
    Return New SqlConnection("...path developer B...")
#Else
    ' Default path
#End If
```

**2. Gunakan Environment Variable:**
```vb
' Alternative: Gununakan variable dari system
Dim dbPath = Environment.GetEnvironmentVariable("SI_PAKAR_DB_PATH")
If String.IsNullOrEmpty(dbPath) Then
    dbPath = "C:\Default\Path\Database Sistem Pakar.mdf"
End If
```

**3. Config File (app.config):**
```xml
<!-- App.config -->
<connectionStrings>
  <add name="SistemPakar" 
       connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database Sistem Pakar.mdf;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

---

## 🚀 Contoh Penggunaan

Setelah user mengisi data diri dan menjalani tes, sistem akan memberikan rekomendasi karir. Berikut adalah contoh hasil analisis dan rekomendasi dari sistem:

### **Test 1: Skenario Dominant**
```
Input:
- Nama: Ahli Backend
- Email: ahli.backend@example.com
- Prodi: TI
- Minat: Backend Developer (SE cluster)
- Fase 1: SE (8/10) = 80%, DI (2/10) = 20%
- Gap: 60% > 20% ✓
- Interest matches Top 2 (SE, DI) ✗

Expected:
- Skenario: Dominant
- Fase 2: 20 soal SE (4 soal × 5 profesi)
- Top Result: Backend Developer, Frontend Developer

Zona Karir:
- Backend Developer: Skill 0.85, Minat 1.0 → Golden Match ⭐⭐⭐
- Frontend Developer: Skill 0.78, Minat 0.0 → Hidden Gem 💎

Skor Akhir:
- Backend Developer: 0.85 + 0.3 = 1.15 (dinormalisasi ke 1.0)
- Frontend Developer: 0.78 + 0.0 = 0.78

Interpretasi:
- Golden Match: "Ini DESTINY-mu! Kamu JAGO dan SUKA bidang ini"
- Hidden Gem: "Kamu punya bakat terpendam di Frontend, coba eksplor lebih dalam!"
```

---

### **Test 2: Skenario Special Hybrid**
```
Input:
- Nama: Budi Santoso
- Email: budi@example.com
- Prodi: TI
- Minat: UI Designer (CP cluster)
- Fase 1: SE (7/10) = 70%, DI (6/10) = 60%, CP (2/10) = 20%
- Gap: 10% < 20% ✓
- Interest does NOT match Top 2 (SE, DI) ✓

Expected:
- Skenario: Special Hybrid
- Fase 2: 10 soal SE + 10 soal CP (2 soal × 5 profesi)
- Top Result: Mix of Software & Creative

Zone Classification (AFTER SWAP):
- Backend Developer: Skill 0.85, Minat 0.0 → Hidden Gem 💎 (NEW!)
- Frontend Developer: Skill 0.78, Minat 0.0 → Hidden Gem 💎 (NEW!)
- UI Designer: Skill 0.35, Minat 1.0 → Reality Check ⚠️ (NEW!)

Skor Akhir:
- Backend Developer: 0.85 + 0.0 = 0.85
- Frontend Developer: 0.78 + 0.0 = 0.78
- UI Designer: 0.35 + 0.3 = 0.65 (interest boost)

Interpretasi:
- Hidden Gem: "Kamu jago Backend/Frontend tapi belum tertarik - potensi tersembunyi!"
- Reality Check: "Kamu suka UI Design tapi skill masih kurang - kerja keras untuk achieve it!"

```

---

### 🚧 Dalam Pengembangan

| Fitur | Status | Target |
|-------|--------|--------|
| **Export PDF** | 🚧 Planned | Print/export hasil ke PDF |

---

## ⚠️ Known Issues & Fixes

### **Issue #1: Tanda "?" pada Analisis Linearitas** ✅ FIXED

**Problem**: 
- Info linearitas menampilkan "❓ Status tidak diketahui" untuk kombinasi tertentu
- Terjadi saat Top 3 hasil dari rumpun DI atau IN

**Root Cause**:
- Tabel `Linearity Matrix` hanya berisi **9 dari 15 kombinasi** yang seharusnya
- Missing records: TI→DI, TI→IN, TMJ→DI, TMJ→IN, TMD→DI, TMD→IN

**Solution**:
Jalankan script perbaikan database:
```sql
-- File: Si Pakar/Database/Fix_Linearity_Matrix.sql
-- Menambahkan 6 record yang hilang

-- Atau jalankan langsung:
USE [Database Sistem Pakar]

SET IDENTITY_INSERT [dbo].[Linearity Matrix] ON;

INSERT INTO [dbo].[Linearity Matrix] ([matrix id], [kode prodi], [kode rumpun], [status linearitas]) VALUES
(4, 'TI', 'DI', 'Linear'),
(5, 'TI', 'IN', 'Related'),
(6, 'TMJ', 'DI', 'Pivot'),
(7, 'TMJ', 'IN', 'Linear'),
(8, 'TMD', 'DI', 'Pivot'),
(9, 'TMD', 'IN', 'Pivot');

SET IDENTITY_INSERT [dbo].[Linearity Matrix] OFF;
```

**Verification**:
```sql
-- Check total records (should be 15)
SELECT COUNT(*) FROM [Linearity Matrix]

-- Display complete matrix
SELECT * FROM [Linearity Matrix] 
ORDER BY [kode prodi], [kode rumpun]
```

**Complete Matrix After Fix**:
| Prodi | Rumpun | Status | Keterangan |
|-------|--------|--------|------------|
| TI | SE | Linear | ✅ Sejalan dengan jurusan |
| TI | DI | Linear | ✅ Sejalan dengan jurusan |
| TI | IN | Related | ⚠️ Masih terkait |
| TI | CS | Linear | ✅ Sejalan dengan jurusan |
| TI | CP | Related | ⚠️ Masih terkait |
| TMJ | SE | Related | ⚠️ Masih terkait |
| TMJ | DI | Pivot | 🔄 Perlu upskilling |
| TMJ | IN | Linear | ✅ Sejalan dengan jurusan |
| TMJ | CS | Linear | ✅ Sejalan dengan jurusan |
| TMJ | CP | Pivot | 🔄 Perlu upskilling |
| TMD | SE | Related | ⚠️ Masih terkait |
| TMD | DI | Pivot | 🔄 Perlu upskilling |
| TMD | IN | Pivot | 🔄 Perlu upskilling |
| TMD | CS | Pivot | 🔄 Perlu upskilling |
| TMD | CP | Linear | ✅ Sejalan dengan jurusan |
