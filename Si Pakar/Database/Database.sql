USE [Database Sistem Pakar]
GO

/* =============================================================
   BAGIAN 1: PEMBUATAN TABEL (CREATE TABLE)
   Diurutkan berdasarkan ketergantungan Foreign Key
=============================================================
*/

-- 1. Tabel Master: Rumpun
CREATE TABLE [dbo].[Rumpun] (
    [kode rumpun] NVARCHAR (10)  NOT NULL,
    [nama rumpun] NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([kode rumpun] ASC)
);

-- 2. Tabel Master: Prodi
CREATE TABLE [dbo].[Prodi] (
    [kode prodi] NVARCHAR (10)  NOT NULL,
    [nama prodi] NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([kode prodi] ASC)
);

-- 3. Tabel: Profesi (Depends on Rumpun)
CREATE TABLE [dbo].[Profesi] (
    [kode profesi] NVARCHAR (20)  NOT NULL,
    [kode rumpun]  NVARCHAR (10)  NOT NULL,
    [nama profesi] NVARCHAR (100) NOT NULL,
    [deskripsi]    NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([kode profesi] ASC),
    CONSTRAINT [FK_Profesi_ToRumpun] FOREIGN KEY ([kode rumpun]) REFERENCES [dbo].[Rumpun] ([kode rumpun])
);

-- 4. Tabel: Pertanyaan (Depends on Rumpun, Profesi)
CREATE TABLE [dbo].[Pertanyaan] (
    [Id Pertanyaan]   NVARCHAR (10)  NOT NULL,
    [phase]           INT            NOT NULL,
    [kode rumpun]     NVARCHAR (10)  NOT NULL,
    [kode profesi]    NVARCHAR (20)  DEFAULT ('NULL') NULL,
    [teks pertanyaan] NVARCHAR (MAX) NOT NULL,
    [expert weight]   FLOAT (53)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id Pertanyaan] ASC),
    CONSTRAINT [FK_Pertanyaan_ToProfiesi] FOREIGN KEY ([kode profesi]) REFERENCES [dbo].[Profesi] ([kode profesi]),
    CONSTRAINT [FK_Pertanyaan_ToRumpun] FOREIGN KEY ([kode rumpun]) REFERENCES [dbo].[Rumpun] ([kode rumpun])
);

-- 5. Tabel: Map Bukti Pertanyaan (Depends on Pertanyaan, Profesi)
CREATE TABLE [dbo].[Map Bukti Pertanyaan] (
    [map id]              INT            IDENTITY (1, 1) NOT NULL,
    [id pertanyaan]       NVARCHAR (10)  NOT NULL,
    [kode profesi target] NVARCHAR (20)  NOT NULL,
    CONSTRAINT [PK__Map Bukt__6255B816FC2D2C49] PRIMARY KEY CLUSTERED ([map id] ASC),
    CONSTRAINT [FK_Map Bukti Pertanyaan_pertanyan] FOREIGN KEY ([id pertanyaan]) REFERENCES [dbo].[Pertanyaan] ([Id Pertanyaan]),
    CONSTRAINT [FK_Map Bukti Pertanyaan_profesi] FOREIGN KEY ([kode profesi target]) REFERENCES [dbo].[Profesi] ([kode profesi])
);

-- 6. Tabel: Linearity Matrix (Depends on Prodi, Rumpun)
CREATE TABLE [dbo].[Linearity Matrix] (
    [matrix id]         INT           IDENTITY (1, 1) NOT NULL,
    [kode prodi]        NVARCHAR (10) NOT NULL,
    [kode rumpun]       NVARCHAR (10) NOT NULL,
    [status linearitas] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([matrix id] ASC),
    CONSTRAINT [FK_Table_ToProdi] FOREIGN KEY ([kode prodi]) REFERENCES [dbo].[Prodi] ([kode prodi]),
    CONSTRAINT [FK_Table_ToRumpun] FOREIGN KEY ([kode rumpun]) REFERENCES [dbo].[Rumpun] ([kode rumpun])
);

-- 7. Tabel: Data User (Depends on Prodi, Rumpun)
CREATE TABLE [dbo].[Data User] (
    [Id user]     INT            IDENTITY (1, 1) NOT NULL,
    [nama]        NVARCHAR (150) NOT NULL,
    [email]       NVARCHAR (150) NOT NULL,
    [kode prodi]  NVARCHAR (10)  NOT NULL,
    [profesi]     NVARCHAR (150) NOT NULL,
    [kode rumpun] NVARCHAR (10)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id user] ASC),
    CONSTRAINT [FK_Data User_ToProdi] FOREIGN KEY ([kode prodi]) REFERENCES [dbo].[Prodi] ([kode prodi]),
    CONSTRAINT [FK_Data User_ToRumpun] FOREIGN KEY ([kode rumpun]) REFERENCES [dbo].[Rumpun] ([kode rumpun])
);

-- 8. Tabel: Sesi Ujian (Depends on Data User)
CREATE TABLE [dbo].[Sesi Ujian] (
    [Id sesi]           INT            IDENTITY (1, 1) NOT NULL,
    [Id user]           INT            NOT NULL,
    [waktu mulai]       DATETIME       NOT NULL,
    [waktu selesai]     DATETIME       NULL,
    [scenario used]     NVARCHAR (50)  NOT NULL,
    [phase1 score json] NVARCHAR (MAX) NOT NULL,
    [final result json] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK__Sesi Uji__C7D700E853B269D6] PRIMARY KEY CLUSTERED ([Id sesi] ASC),
    CONSTRAINT [FK_Sesi Ujian_Data User] FOREIGN KEY ([Id user]) REFERENCES [dbo].[Data User] ([Id user])
);

-- 9. Tabel: Exam Details (Depends on Sesi Ujian, Pertanyaan)
CREATE TABLE [dbo].[Exam Details] (
    [detail id]       INT            IDENTITY (1, 1) NOT NULL,
    [id sesi]         INT            NOT NULL,
    [id pertanyaan]   NVARCHAR (10)  NOT NULL,
    [user confidence] FLOAT (53)     NOT NULL,
    CONSTRAINT [PK__Exam Det__4FBDAC515000CE94] PRIMARY KEY CLUSTERED ([detail id] ASC),
    CONSTRAINT [FK_Exam Details_Pertanyaan] FOREIGN KEY ([id pertanyaan]) REFERENCES [dbo].[Pertanyaan] ([Id Pertanyaan]),
    CONSTRAINT [FK_Exam Details_Sesi Ujan] FOREIGN KEY ([id sesi]) REFERENCES [dbo].[Sesi Ujian] ([Id sesi])
);

-- 10. Tabel: Exam Rumpun Scores (Depends on Sesi Ujian, Rumpun)
CREATE TABLE [dbo].[Exam Rumpun Scores] (
    [score id]    INT           IDENTITY (1, 1) NOT NULL,
    [id sesi]     INT           NULL,
    [kode rumpun] NVARCHAR (10) NULL,
    [score value] FLOAT (53)    NULL,
    CONSTRAINT [PK__Exam Rum__9D8B673DEFE364DC] PRIMARY KEY CLUSTERED ([score id] ASC),
    CONSTRAINT [FK_Exam Rumpun Scores_Rumpun] FOREIGN KEY ([kode rumpun]) REFERENCES [dbo].[Rumpun] ([kode rumpun]),
    CONSTRAINT [FK_Exam Rumpun Scores_Sesi] FOREIGN KEY ([id sesi]) REFERENCES [dbo].[Sesi Ujian] ([Id sesi])
);

GO

/* =============================================================
   BAGIAN 2: INSERT DATA (DML)
   Data diambil dari file CSV yang diupload
=============================================================
*/

-- A. Insert Rumpun
INSERT INTO [dbo].[Rumpun] ([kode rumpun], [nama rumpun]) VALUES
('CP', 'Creative & Product'),
('CS', 'Cyber Security'),
('DI', 'Data & Intellegence'),
('IN', 'Infrastructure'),
('SE', 'Software Engineering');

-- B. Insert Prodi
INSERT INTO [dbo].[Prodi] ([kode prodi], [nama prodi]) VALUES
('TI', 'Teknik Informatika'),
('TMD', 'Teknik Mutlimedia Digital'),
('TMJ', 'Teknik Multimedia Jaringan');

-- C. Insert Profesi
INSERT INTO [dbo].[Profesi] ([kode profesi], [kode rumpun], [nama profesi], [deskripsi]) VALUES
('AIE', 'DI', 'AI Engineer', 'Deskripsi untuk AI Engineer'),
('BED', 'SE', 'Backend Developer', 'Deskripsi untuk Backend Developer'),
('BIA', 'DI', 'BI Analyst', 'Deskripsi untuk BI Analyst'),
('DAN', 'DI', 'Data Analyst', 'Deskripsi untuk Data Analyst'),
('DEN', 'DI', 'Data Engineer', 'Deskripsi untuk Data Engineer'),
('DGF', 'CS', 'Digital Forensics', 'Deskripsi untuk Digital Forensics'),
('DOE', 'IN', 'DevOps Engineer', 'Deskripsi untuk DevOps Engineer'),
('DSC', 'DI', 'Data Scientist', 'Deskripsi untuk Data Scientist'),
('FED', 'SE', 'Frontend Developer', 'Deskripsi untuk Frontend Developer'),
('GMP', 'SE', 'Game Programmer', 'Deskripsi untuk Game Programmer'),
('GRC', 'CS', 'GRC Specialist', 'Deskripsi untuk GRC Specialist'),
('GRD', 'CP', 'Graphic Designer', 'Deskripsi untuk Graphic Designer'),
('IOE', 'IN', 'IoT Engineer', 'Deskripsi untuk IoT Engineer'),
('ITS', 'IN', 'IT Support Specialist', 'Deskripsi untuk IT Support Specialist'),
('MBD', 'SE', 'Mobile Developer', 'Deskripsi untuk Mobile Developer'),
('MMS', 'CP', 'Multimedia Specialist', 'Deskripsi untuk Multimedia Specialist'),
('NTE', 'IN', 'Network Engineer', 'Deskripsi untuk Network Engineer'),
('PMA', 'CP', 'Product Manager', 'Deskripsi untuk Product Manager'),
('PNT', 'CS', 'Pentester', 'Deskripsi untuk Pentester'),
('QAE', 'SE', 'QA Engineer', 'Deskripsi untuk QA Engineer'),
('SCA', 'CS', 'Security Analyst', 'Deskripsi untuk Security Analyst'),
('SDO', 'CS', 'SecDevOps Engineer', 'Deskripsi untuk SecDevOps Engineer'),
('SYA', 'IN', 'System Administrator', 'Deskripsi untuk System Administrator',
    -- 9. Tabel: Exam Details (Depends on Sesi Ujian, Pertanyaan)
CREATE TABLE [dbo].[Exam Details] (
    [detail id]       INT            IDENTITY (1, 1) NOT NULL,
    [id sesi]         INT            NOT NULL,
    [id pertanyaan]   NVARCHAR (10)  NOT NULL,
    [user confidence] FLOAT (53)     NOT NULL,
    CONSTRAINT [PK__Exam Det__4FBDAC515000CE94] PRIMARY KEY CLUSTERED ([detail id] ASC),
    CONSTRAINT [FK_Exam Details_Pertanyaan] FOREIGN KEY ([id pertanyaan]) REFERENCES [dbo].[Pertanyaan] ([Id Pertanyaan]),
    CONSTRAINT [FK_Exam Details_Sesi Ujan] FOREIGN KEY ([id sesi]) REFERENCES [dbo].[Sesi Ujian] ([Id sesi])
);

-- 10. Tabel: Exam Rumpun Scores (Depends on Sesi Ujian, Rumpun)
CREATE TABLE [dbo].[Exam Rumpun Scores] (
    [score id]    INT           IDENTITY (1, 1) NOT NULL,
    [id sesi]     INT           NULL,
    [kode rumpun] NVARCHAR (10) NULL,
    [score value] FLOAT (53)    NULL,
    CONSTRAINT [PK__Exam Rum__9D8B673DEFE364DC] PRIMARY KEY CLUSTERED ([score id] ASC),
    CONSTRAINT [FK_Exam Rumpun Scores_Rumpun] FOREIGN KEY ([kode rumpun]) REFERENCES [dbo].[Rumpun] ([kode rumpun]),
    CONSTRAINT [FK_Exam Rumpun Scores_Sesi] FOREIGN KEY ([id sesi]) REFERENCES [dbo].[Sesi Ujian] ([Id sesi])
);

GO

/* =============================================================
   BAGIAN 2: INSERT DATA (DML)
   Data diambil dari file CSV yang diupload
=============================================================
*/

-- A. Insert Rumpun
INSERT INTO [dbo].[Rumpun] ([kode rumpun], [nama rumpun]) VALUES
('CP', 'Creative & Product'),
('CS', 'Cyber Security'),
('DI', 'Data & Intellegence'),
('IN', 'Infrastructure'),
('SE', 'Software Engineering');

-- B. Insert Prodi
INSERT INTO [dbo].[Prodi] ([kode prodi], [nama prodi]) VALUES
('TI', 'Teknik Informatika'),
('TMD', 'Teknik Mutlimedia Digital'),
('TMJ', 'Teknik Multimedia Jaringan');

-- C. Insert Profesi
INSERT INTO [dbo].[Profesi] ([kode profesi], [kode rumpun], [nama profesi], [deskripsi]) VALUES
('AIE', 'DI', 'AI Engineer', 'Deskripsi untuk AI Engineer'),
('BED', 'SE', 'Backend Developer', 'Deskripsi untuk Backend Developer'),
('BIA', 'DI', 'BI Analyst', 'Deskripsi untuk BI Analyst'),
('DAN', 'DI', 'Data Analyst', 'Deskripsi untuk Data Analyst'),
('DEN', 'DI', 'Data Engineer', 'Deskripsi untuk Data Engineer'),
('DGF', 'CS', 'Digital Forensics', 'Deskripsi untuk Digital Forensics'),
('DOE', 'IN', 'DevOps Engineer', 'Deskripsi untuk DevOps Engineer'),
('DSC', 'DI', 'Data Scientist', 'Deskripsi untuk Data Scientist'),
('FED', 'SE', 'Frontend Developer', 'Deskripsi untuk Frontend Developer'),
('GMP', 'SE', 'Game Programmer', 'Deskripsi untuk Game Programmer'),
('GRC', 'CS', 'GRC Specialist', 'Deskripsi untuk GRC Specialist'),
('GRD', 'CP', 'Graphic Designer', 'Deskripsi untuk Graphic Designer'),
('IOE', 'IN', 'IoT Engineer', 'Deskripsi untuk IoT Engineer'),
('ITS', 'IN', 'IT Support Specialist', 'Deskripsi untuk IT Support Specialist'),
('MBD', 'SE', 'Mobile Developer', 'Deskripsi untuk Mobile Developer'),
('MMS', 'CP', 'Multimedia Specialist', 'Deskripsi untuk Multimedia Specialist'),
('NTE', 'IN', 'Network Engineer', 'Deskripsi untuk Network Engineer'),
('PMA', 'CP', 'Product Manager', 'Deskripsi untuk Product Manager'),
('PNT', 'CS', 'Pentester', 'Deskripsi untuk Pentester'),
('QAE', 'SE', 'QA Engineer', 'Deskripsi untuk QA Engineer'),
('SCA', 'CS', 'Security Analyst', 'Deskripsi untuk Security Analyst'),
('SDO', 'CS', 'SecDevOps Engineer', 'Deskripsi untuk SecDevOps Engineer'),
('SYA', 'IN', 'System Administrator', 'Deskripsi untuk System Administrator'),
('UID', 'CP', 'UI Designer', 'Deskripsi untuk UI Designer'),
('UXR', 'CP', 'UX Researcher', 'Deskripsi untuk UX Researcher');

-- D. Insert Linearity Matrix
-- Karena ada kolom IDENTITY, kita harus ON-kan Identity Insert
SET IDENTITY_INSERT [dbo].[Linearity Matrix] ON;

INSERT INTO [dbo].[Linearity Matrix] ([matrix id], [kode prodi], [kode rumpun], [status linearitas]) VALUES
-- TI (Teknik Informatika) - Linear: SE, DI, CS | Related: IN, CP
(1, 'TI', 'SE', 'Linear'),
(4, 'TI', 'DI', 'Linear'),
(5, 'TI', 'IN', 'Related'),
(10, 'TI', 'CS', 'Linear'),
(13, 'TI', 'CP', 'Related'),

-- TMJ (Teknik Multimedia Jaringan) - Linear: IN, CS | Related: SE | Pivot: DI, CP
(2, 'TMJ', 'SE', 'Related'),
(6, 'TMJ', 'DI', 'Pivot'),
(7, 'TMJ', 'IN', 'Linear'),
(11, 'TMJ', 'CS', 'Linear'),
(14, 'TMJ', 'CP', 'Pivot'),

-- TMD (Teknik Multimedia Digital) - Linear: CP | Related: SE | Pivot: DI, IN, CS
(3, 'TMD', 'SE', 'Related'),
(8, 'TMD', 'DI', 'Pivot'),
(9, 'TMD', 'IN', 'Pivot'),
(12, 'TMD', 'CS', 'Pivot'),
(15, 'TMD', 'CP', 'Linear');

SET IDENTITY_INSERT [dbo].[Linearity Matrix] OFF;

INSERT INTO [dbo].[Pertanyaan] 
([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) 
VALUES
-- General (Phase 1)
('G01', 1, 'SE', NULL, 'Seberapa mampu Anda memecahkan logika algoritma (Looping, Array, If-Else) tanpa bantuan AI?', 0.8),
('G02', 1, 'SE', NULL, 'Seberapa paham Anda konsep Object Oriented Programming (Class, Object, Inheritance)?', 0.7),
('G03', 1, 'DI', NULL, 'Seberapa nyaman Anda bekerja dengan angka statistik, probabilitas, dan pola data?', 0.7),
('G04', 1, 'DI', NULL, 'Seberapa sering Anda menggunakan rumus Excel kompleks atau coding untuk mengolah data?', 0.6),
('G05', 1, 'IN', NULL, 'Seberapa paham Anda tentang konsep IP Address, Subnetting, dan cara kerja Router/Switch?', 0.7),
('G06', 1, 'IN', NULL, 'Seberapa tertarik Anda mengutak-atik hardware komputer, sensor elektronik, atau mikrokontroler?', 0.6),
('G07', 1, 'CS', NULL, 'Seberapa paham Anda tentang jenis serangan siber (Phishing, Malware, DDoS) dan cara mencegahnya?', 0.7),
('G08', 1, 'CS', NULL, 'Seberapa peduli Anda tentang enkripsi, privasi data, dan celah keamanan pada aplikasi?', 0.7),
('G09', 1, 'CP', NULL, 'Seberapa peka mata Anda terhadap kombinasi warna, tipografi, dan keseimbangan visual?', 0.7),
('G10', 1, 'CP', NULL, 'Seberapa sering Anda memikirkan kemudahan penggunaan (User Experience) saat memakai aplikasi?', 0.7),

-- Software Engineering (Phase 2)
('SE01', 2, 'SE', 'BED', 'Seberapa paham Anda konsep REST API, JSON, dan HTTP Methods (GET/POST)?', 0.8),
('SE02', 2, 'SE', 'BED', 'Seberapa mahir Anda membuat query SQL kompleks (JOIN, Indexing, Transaction)?', 0.85),
('SE03', 2, 'SE', 'BED', 'Seberapa mengerti Anda implementasi autentikasi user menggunakan JWT atau OAuth?', 0.9),
('SE04', 2, 'SE', 'BED', 'Seberapa bisa Anda melakukan deployment aplikasi ke server VPS (Linux/Ubuntu)?', 0.95),
('SE05', 2, 'SE', 'FED', 'Seberapa mahir Anda mengubah desain Figma menjadi kode CSS/Layout responsif?', 0.8),
('SE06', 2, 'SE', 'FED', 'Seberapa paham Anda manipulasi DOM HTML menggunakan Javascript murni?', 0.9),
('SE07', 2, 'SE', 'FED', 'Seberapa familiar Anda dengan Lifecycle Framework modern (React/Vue/Angular)?', 0.8),
('SE08', 2, 'SE', 'FED', 'Seberapa paham Anda tentang teknik optimasi loading website (Lazy load, Minify)?', 0.8),
('SE09', 2, 'SE', 'MBD', 'Seberapa paham Anda struktur project native Android (Gradle/Manifest) atau iOS?', 0.9),
('SE10', 2, 'SE', 'MBD', 'Seberapa mengerti Anda siklus hidup aplikasi (onCreate, onResume, onPause)?', 0.9),
('SE11', 2, 'SE', 'MBD', 'Seberapa bisa Anda menggunakan database lokal di HP (SQLite/Room/CoreData)?', 0.8),
('SE12', 2, 'SE', 'MBD', 'Seberapa bisa Anda proses upload aplikasi ke Play Store / App Store?', 0.6),
('SE13', 2, 'SE', 'GMP', 'Seberapa paham Anda logika fisika game (Collision, Gravity, RigidBody)?', 0.8),
('SE14', 2, 'SE', 'GMP', 'Seberapa mengerti Anda konsep Game Loop (Update frame per detik)?', 0.9),
('SE15', 2, 'SE', 'GMP', 'Seberapa paham Anda matematika vektor dan matriks untuk pergerakan objek 3D?', 0.8),
('SE16', 2, 'SE', 'GMP', 'Seberapa mahir Anda menggunakan Game Engine (Unity/Unreal/Godot)?', 0.8),
('SE17', 2, 'SE', 'QAE', 'Seberapa bisa Anda membuat Unit Testing otomatis pada kode program?', 0.9),
('SE18', 2, 'SE', 'QAE', 'Seberapa paham Anda tools automation testing seperti Selenium atau Appium?', 0.9),
('SE19', 2, 'SE', 'QAE', 'Seberapa teliti Anda mendokumentasikan langkah-langkah reproduksi bug?', 0.8),
('SE20', 2, 'SE', 'QAE', 'Seberapa paham Anda cara melakukan stress test/load test pada server?', 0.8),

-- Data & AI (Phase 2)
('DI01', 2, 'DI', 'DSC', 'Seberapa mahir Anda menggunakan Python (Pandas/NumPy) untuk manipulasi data?', 0.8),
('DI02', 2, 'DI', 'DSC', 'Seberapa paham Anda statistik inferensial (Hipotesis, P-Value, Distribusi)?', 0.9),
('DI03', 2, 'DI', 'DSC', 'Seberapa mengerti Anda algoritma dasar ML (Regresi, Clustering, Decision Tree)?', 0.8),
('DI04', 2, 'DI', 'DSC', 'Seberapa bisa Anda membersihkan data kotor (Missing values, Outliers)?', 0.8),
('DI05', 2, 'DI', 'AIE', 'Seberapa paham Anda arsitektur Neural Networks dan Deep Learning?', 0.95),
('DI06', 2, 'DI', 'AIE', 'Seberapa bisa Anda men-deploy model AI menjadi API yang bisa diakses user?', 0.8),
('DI07', 2, 'DI', 'AIE', 'Seberapa familiar Anda dengan Computer Vision (OpenCV) atau NLP?', 0.8),
('DI08', 2, 'DI', 'AIE', 'Seberapa paham Anda teknik Hyperparameter Tuning untuk akurasi model?', 0.95),
('DI09', 2, 'DI', 'DAN', 'Seberapa mahir Anda menggunakan Tableau/PowerBI untuk visualisasi?', 0.8),
('DI10', 2, 'DI', 'DAN', 'Seberapa mampu Anda menerjemahkan data menjadi cerita/insight bisnis?', 0.8),
('DI11', 2, 'DI', 'DAN', 'Seberapa mahir Anda menggunakan SQL untuk reporting (Group By, Window Func)?', 0.8),
('DI12', 2, 'DI', 'DAN', 'Seberapa menguasai Anda fitur Advanced Excel (Pivot, VLOOKUP, Macro)?', 0.6),
('DI13', 2, 'DI', 'DEN', 'Seberapa paham Anda konsep ETL (Extract, Transform, Load) pipeline?', 0.9),
('DI14', 2, 'DI', 'DEN', 'Seberapa familiar Anda dengan ekosistem Big Data (Hadoop/Spark/Kafka)?', 0.9),
('DI15', 2, 'DI', 'DEN', 'Seberapa paham Anda konsep Data Warehouse vs Data Lake?', 0.8),
('DI16', 2, 'DI', 'DEN', 'Seberapa bisa Anda mengelola database cloud (BigQuery/Redshift)?', 0.8),
('DI17', 2, 'DI', 'BIA', 'Seberapa paham Anda mendefinisikan metrik KPI bisnis yang tepat?', 0.8),
('DI18', 2, 'DI', 'BIA', 'Seberapa bisa Anda merancang dashboard monitoring kinerja perusahaan?', 0.8),
('DI19', 2, 'DI', 'BIA', 'Seberapa paham Anda pemodelan data dimensional (Star Schema)?', 0.8),
('DI20', 2, 'DI', 'BIA', 'Seberapa bisa Anda melakukan forecasting trend bisnis sederhana?', 0.8),

-- Infrastructure & Network (Phase 2)
('IN01', 2, 'IN', 'NTE', 'Seberapa lancar Anda menghitung Subnetting (CIDR) dan VLSM?', 0.8),
('IN02', 2, 'IN', 'NTE', 'Seberapa paham Anda konfigurasi Routing Protocol (OSPF/BGP/EIGRP)?', 0.95),
('IN03', 2, 'IN', 'NTE', 'Seberapa menguasai Anda konfigurasi VLAN, Trunking, dan STP pada Switch?', 0.8),
('IN04', 2, 'IN', 'NTE', 'Seberapa hafal dan paham Anda fungsi setiap layer pada OSI 7 Layer?', 0.5),
('IN05', 2, 'IN', 'SYA', 'Seberapa mahir Anda menggunakan Linux CLI (Permission, Process, Service)?', 0.9),
('IN06', 2, 'IN', 'SYA', 'Seberapa bisa Anda konfigurasi Web Server (Nginx/Apache) dan Reverse Proxy?', 0.8),
('IN07', 2, 'IN', 'SYA', 'Seberapa paham Anda manajemen DNS Server dan propagasi domain?', 0.8),
('IN08', 2, 'IN', 'SYA', 'Seberapa bisa Anda mengelola User Management (LDAP/Active Directory)?', 0.8),
('IN09', 2, 'IN', 'DOE', 'Seberapa paham Anda konsep Containerization dan membuat Dockerfile?', 0.9),
('IN10', 2, 'IN', 'DOE', 'Seberapa bisa Anda membuat pipeline CI/CD (Jenkins/GitLab/Github Actions)?', 0.8),
('IN11', 2, 'IN', 'DOE', 'Seberapa paham Anda Infrastructure as Code (Terraform/Ansible)?', 0.9),
('IN12', 2, 'IN', 'DOE', 'Seberapa menguasai Anda layanan Cloud (AWS EC2, S3, RDS)?', 0.8),
('IN13', 2, 'IN', 'IOE', 'Seberapa bisa Anda memprogram mikrokontroler (Arduino/ESP32)?', 0.8),
('IN14', 2, 'IN', 'IOE', 'Seberapa paham Anda rangkaian elektronika dasar (Resistor, Kapasitor)?', 0.8),
('IN15', 2, 'IN', 'IOE', 'Seberapa paham Anda protokol komunikasi ringan (MQTT/CoAP/HTTP)?', 0.8),
('IN16', 2, 'IN', 'IOE', 'Seberapa mampu Anda mengintegrasikan berbagai sensor analog/digital?', 0.8),
('IN17', 2, 'IN', 'ITS', 'Seberapa mahir Anda mendiagnosa kerusakan hardware PC/Laptop?', 0.8),
('IN18', 2, 'IN', 'ITS', 'Seberapa bisa Anda melakukan instalasi dan troubleshooting Windows/Mac?', 0.8),
('IN19', 2, 'IN', 'ITS', 'Seberapa bisa Anda melakukan crimping kabel UTP dengan urutan warna benar?', 0.6),
('IN20', 2, 'IN', 'ITS', 'Seberapa paham Anda troubleshooting printer, scanner, dan proyektor?', 0.6),

-- Cyber Security (Phase 2)
('CS01', 2, 'CS', 'PNT', 'Seberapa mahir Anda menggunakan tools scanning (Nmap/Wireshark/Burp)?', 0.85),
('CS02', 2, 'CS', 'PNT', 'Seberapa paham Anda teknik eksploitasi web (SQL Injection, XSS)?', 0.85),
('CS03', 2, 'CS', 'PNT', 'Seberapa bisa Anda menggunakan framework eksploitasi (Metasploit)?', 0.9),
('CS04', 2, 'CS', 'PNT', 'Seberapa mampu Anda menulis laporan kerentanan teknis (POC)?', 0.85),
('CS05', 2, 'CS', 'SCA', 'Seberapa paham Anda monitoring log keamanan menggunakan SIEM (Splunk/Wazuh)?', 0.85),
('CS06', 2, 'CS', 'SCA', 'Seberapa paham Anda cara kerja dan penyebaran Malware/Ransomware?', 0.85),
('CS07', 2, 'CS', 'SCA', 'Seberapa update Anda dengan Threat Intelligence dan IOC terbaru?', 0.85),
('CS08', 2, 'CS', 'SCA', 'Seberapa paham Anda prosedur Incident Response (Containment, Eradication)?', 0.85),
('CS09', 2, 'CS', 'SDO', 'Seberapa bisa Anda mengintegrasikan security scan (SAST/DAST) di pipeline?', 0.85),
('CS10', 2, 'CS', 'SDO', 'Seberapa mampu Anda melakukan Secure Code Review manual?', 0.85),
('CS11', 2, 'CS', 'SDO', 'Seberapa paham teknik Hardening Server dan Container?', 0.85),
('CS12', 2, 'CS', 'SDO', 'Seberapa paham Anda manajemen akses (IAM) dan security group di Cloud?', 0.85),
('CS13', 2, 'CS', 'DGF', 'Seberapa bisa Anda melacak jejak digital yang dihapus (Data Recovery)?', 0.85),
('CS14', 2, 'CS', 'DGF', 'Seberapa paham Anda analisis memori (RAM Dump) untuk mencari malware?', 0.9),
('CS15', 2, 'CS', 'DGF', 'Seberapa bisa Anda menganalisis paket jaringan (PCAP) pasca insiden?', 0.9),
('CS16', 2, 'CS', 'DGF', 'Seberapa paham Anda prinsip Chain of Custody dalam barang bukti digital?', 0.85),
('CS17', 2, 'CS', 'GRC', 'Seberapa paham Anda standar keamanan informasi (ISO 27001/NIST)?', 0.9),
('CS18', 2, 'CS', 'GRC', 'Seberapa mampu Anda melakukan audit kepatuhan IT internal?', 0.85),
('CS19', 2, 'CS', 'GRC', 'Seberapa bisa Anda menyusun dokumen manajemen risiko IT?', 0.85),
('CS20', 2, 'CS', 'GRC', 'Seberapa mampu Anda menyusun kebijakan keamanan (Security Policy) perusahaan?', 0.7),

-- Creative & Product (Phase 2)
('CP01', 2, 'CP', 'UID', 'Seberapa mahir Anda menggunakan Figma/Adobe XD untuk desain antarmuka?', 0.8),
('CP02', 2, 'CP', 'UID', 'Seberapa paham Anda membuat Design System (Component, Token, Style Guide)?', 0.9),
('CP03', 2, 'CP', 'UID', 'Seberapa bisa Anda membuat High-Fidelity Prototype yang interaktif?', 0.8),
('CP04', 2, 'CP', 'UID', 'Seberapa paham Anda standar aksesibilitas desain (Contrast ratio, Size)?', 0.8),
('CP05', 2, 'CP', 'UXR', 'Seberapa mahir Anda melakukan User Interview dan membuat User Persona?', 0.8),
('CP06', 2, 'CP', 'UXR', 'Seberapa bisa Anda melakukan Usability Testing dan A/B Testing?', 0.8),
('CP07', 2, 'CP', 'UXR', 'Seberapa mampu Anda merancang User Journey Map dan Wireframe?', 0.8),
('CP08', 2, 'CP', 'UXR', 'Seberapa bisa Anda mengambil keputusan desain berdasarkan data analitik?', 0.8),
('CP09', 2, 'CP', 'GRD', 'Seberapa mahir Anda menggunakan Illustrator/Corel untuk ilustrasi vektor?', 0.7),
('CP10', 2, 'CP', 'GRD', 'Seberapa paham Anda persiapan file cetak (CMYK, Bleed, Margin)?', 0.8),
('CP11', 2, 'CP', 'GRD', 'Seberapa bisa Anda merancang Identitas Visual (Logo, Branding) konsisten?', 0.8),
('CP12', 2, 'CP', 'GRD', 'Seberapa peka Anda terhadap Grid System dan Hierarki Tipografi?', 0.8),
('CP13', 2, 'CP', 'MMS', 'Seberapa mahir Anda editing video (Premiere/DaVinci) - Cut, Color, Sound?', 0.8),
('CP14', 2, 'CP', 'MMS', 'Seberapa menguasai Anda Motion Graphic dan VFX (After Effects)?', 0.9),
('CP15', 2, 'CP', 'MMS', 'Seberapa bisa Anda membuat model 3D dan animasi dasar (Blender)?', 0.9),
('CP16', 2, 'CP', 'MMS', 'Seberapa paham Anda teknik mixing audio dan sound design?', 0.7),
('CP17', 2, 'CP', 'PMA', 'Seberapa paham Anda metodologi Agile/Scrum dalam pengembangan produk?', 0.8),
('CP18', 2, 'CP', 'PMA', 'Seberapa bisa Anda menentukan prioritas fitur berdasarkan impact bisnis?', 0.8),
('CP19', 2, 'CP', 'PMA', 'Seberapa mampu Anda menyusun Product Roadmap jangka panjang?', 0.8),
('CP20', 2, 'CP', 'PMA', 'Seberapa baik kemampuan Anda komunikasi antara tim teknis dan bisnis?', 0.8);

-- F. Insert Map Bukti Pertanyaan
-- Mengaktifkan Identity Insert
SET IDENTITY_INSERT [dbo].[Map Bukti Pertanyaan] ON;

INSERT INTO [dbo].[Map Bukti Pertanyaan] ([map id], [id pertanyaan], [kode profesi target]) VALUES
(1, 'SE01', 'BED'),
(2, 'SE02', 'BED'),
(3, 'SE03', 'BED'),
(4, 'SE04', 'BED'),
(5, 'SE05', 'FED'),
(6, 'SE06', 'FED'),
(7, 'SE07', 'FED'),
(8, 'SE08', 'FED'),
(9, 'SE09', 'MBD'),
(10, 'SE10', 'MBD'),
(11, 'SE11', 'MBD'),
(12, 'SE12', 'MBD'),
(13, 'SE13', 'GMP'),
(14, 'SE14', 'GMP'),
(15, 'SE15', 'GMP'),
(16, 'SE16', 'GMP'),
(17, 'SE17', 'QAE'),
(18, 'SE18', 'QAE'),
(19, 'SE19', 'QAE'),
(20, 'SE20', 'QAE'),
(21, 'DI01', 'DSC'),
(22, 'DI02', 'DSC'),
(23, 'DI03', 'DSC'),
(24, 'DI04', 'DSC'),
(25, 'DI05', 'AIE'),
(26, 'DI06', 'AIE'),
(27, 'DI07', 'AIE'),
(28, 'DI08', 'AIE'),
(29, 'DI09', 'DAN'),
(30, 'DI10', 'DAN'),
(31, 'DI11', 'DAN'),
(32, 'DI12', 'DAN'),
(33, 'DI13', 'DEN'),
(34, 'DI14', 'DEN'),
(35, 'DI15', 'DEN'),
(36, 'DI16', 'DEN'),
(37, 'DI17', 'BIA'),
(38, 'DI18', 'BIA');

SET IDENTITY_INSERT [dbo].[Map Bukti Pertanyaan] OFF;

PRINT 'Semua tabel berhasil dibuat dan data berhasil diimport.';