-- ==========================================
-- SAMPLE DATA UNTUK TESTING SISTEM
-- ==========================================

-- 1. DATA RUMPUN
DELETE FROM Rumpun;
INSERT INTO Rumpun ([kode rumpun], [nama rumpun]) VALUES 
('SE', 'Software Engineering'),
('DA', 'Data & Intelligence'),
('IF', 'Infrastructure'),
('CS', 'Cyber Security'),
('CP', 'Creative & Product');

-- 2. DATA PRODI
DELETE FROM Prodi;
INSERT INTO Prodi ([kode prodi], [nama prodi]) VALUES 
('TI', 'Teknik Informatika'),
('TMJ', 'Teknik Multimedia Jaringan'),
('TMD', 'Teknik Multimedia Digital');

-- 3. DATA PROFESI
DELETE FROM Profesi;

-- Software Engineering (SE)
INSERT INTO Profesi ([kode profesi], [kode rumpun], [nama profesi], [deskripsi]) VALUES 
('SE-BE', 'SE', 'Backend Developer', 'Mengembangkan server-side logic dan database'),
('SE-FE', 'SE', 'Frontend Developer', 'Mengembangkan user interface website'),
('SE-MB', 'SE', 'Mobile Developer', 'Mengembangkan aplikasi Android/iOS'),
('SE-GM', 'SE', 'Game Programmer', 'Mengembangkan game dan engine'),
('SE-QA', 'SE', 'Quality Assurance', 'Testing dan quality control software');

-- Data & Intelligence (DA)
INSERT INTO Profesi ([kode profesi], [kode rumpun], [nama profesi], [deskripsi]) VALUES 
('DA-DS', 'DA', 'Data Scientist', 'Analisis data dan machine learning'),
('DA-AI', 'DA', 'AI Engineer', 'Mengembangkan sistem artificial intelligence'),
('DA-AN', 'DA', 'Data Analyst', 'Analisis dan visualisasi data bisnis'),
('DA-DE', 'DA', 'Data Engineer', 'Membangun data pipeline dan warehouse'),
('DA-BI', 'DA', 'BI Analyst', 'Business intelligence dan reporting');

-- Infrastructure (IF)
INSERT INTO Profesi ([kode profesi], [kode rumpun], [nama profesi], [deskripsi]) VALUES 
('IF-NE', 'IF', 'Network Engineer', 'Mengelola jaringan komputer'),
('IF-SA', 'IF', 'System Administrator', 'Mengelola server dan sistem'),
('IF-DO', 'IF', 'DevOps Engineer', 'CI/CD dan automation'),
('IF-IO', 'IF', 'IoT Engineer', 'Internet of Things development'),
('IF-TS', 'IF', 'Tech Support', 'Technical support dan troubleshooting');

-- Cyber Security (CS)
INSERT INTO Profesi ([kode profesi], [kode rumpun], [nama profesi], [deskripsi]) VALUES 
('CS-PT', 'CS', 'Penetration Tester', 'Ethical hacking dan security testing'),
('CS-SA', 'CS', 'Security Analyst', 'Monitoring dan analisis keamanan'),
('CS-SD', 'CS', 'SecDevOps', 'Security dalam development lifecycle'),
('CS-FR', 'CS', 'Digital Forensics', 'Investigasi kejahatan digital'),
('CS-GR', 'CS', 'GRC Specialist', 'Governance, Risk, dan Compliance');

-- Creative & Product (CP)
INSERT INTO Profesi ([kode profesi], [kode rumpun], [nama profesi], [deskripsi]) VALUES 
('CP-UI', 'CP', 'UI Designer', 'Desain user interface'),
('CP-UX', 'CP', 'UX Researcher', 'User experience research'),
('CP-GD', 'CP', 'Graphic Designer', 'Desain grafis dan visual'),
('CP-MM', 'CP', 'Multimedia Specialist', 'Video editing dan multimedia'),
('CP-PM', 'CP', 'Product Manager', 'Manajemen produk digital');

-- 4. SAMPLE PERTANYAAN FASE 1 (2 soal per rumpun = 10 soal)
DELETE FROM Pertanyaan WHERE phase = 1;

-- Software Engineering
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('SE-F1-01', 1, 'SE', NULL, 'Seberapa yakin Anda bisa membuat program sederhana dengan bahasa pemrograman (Python/Java/C++)?', 1.0),
('SE-F1-02', 1, 'SE', NULL, 'Seberapa yakin Anda memahami konsep algoritma dan struktur data?', 1.0);

-- Data & Intelligence
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('DA-F1-01', 1, 'DA', NULL, 'Seberapa yakin Anda bisa melakukan analisis data menggunakan Excel/Python?', 1.0),
('DA-F1-02', 1, 'DA', NULL, 'Seberapa yakin Anda memahami konsep machine learning dasar?', 1.0);

-- Infrastructure
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('IF-F1-01', 1, 'IF', NULL, 'Seberapa yakin Anda bisa mengkonfigurasi jaringan komputer dasar?', 1.0),
('IF-F1-02', 1, 'IF', NULL, 'Seberapa yakin Anda memahami sistem operasi Linux/Windows Server?', 1.0);

-- Cyber Security
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CS-F1-01', 1, 'CS', NULL, 'Seberapa yakin Anda memahami konsep keamanan siber dasar?', 1.0),
('CS-F1-02', 1, 'CS', NULL, 'Seberapa yakin Anda bisa mengidentifikasi celah keamanan sederhana?', 1.0);

-- Creative & Product
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CP-F1-01', 1, 'CP', NULL, 'Seberapa yakin Anda bisa membuat desain visual yang menarik?', 1.0),
('CP-F1-02', 1, 'CP', NULL, 'Seberapa yakin Anda memahami prinsip user experience (UX)?', 1.0);

-- 5. SAMPLE PERTANYAAN FASE 2 (4 soal per profesi, total banyak)
DELETE FROM Pertanyaan WHERE phase = 2;

-- Backend Developer (SE-BE)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('SE-BE-01', 2, 'SE', 'SE-BE', 'Seberapa yakin Anda bisa membuat RESTful API?', 1.2),
('SE-BE-02', 2, 'SE', 'SE-BE', 'Seberapa yakin Anda memahami database SQL dan NoSQL?', 1.0),
('SE-BE-03', 2, 'SE', 'SE-BE', 'Seberapa yakin Anda bisa melakukan optimasi query database?', 1.3),
('SE-BE-04', 2, 'SE', 'SE-BE', 'Seberapa yakin Anda memahami microservices architecture?', 1.1);

-- Frontend Developer (SE-FE)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('SE-FE-01', 2, 'SE', 'SE-FE', 'Seberapa yakin Anda menguasai HTML, CSS, JavaScript?', 1.0),
('SE-FE-02', 2, 'SE', 'SE-FE', 'Seberapa yakin Anda bisa menggunakan framework React/Vue/Angular?', 1.2),
('SE-FE-03', 2, 'SE', 'SE-FE', 'Seberapa yakin Anda memahami responsive web design?', 1.0),
('SE-FE-04', 2, 'SE', 'SE-FE', 'Seberapa yakin Anda bisa mengoptimasi performa website?', 1.3);

-- Mobile Developer (SE-MB)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('SE-MB-01', 2, 'SE', 'SE-MB', 'Seberapa yakin Anda bisa membuat aplikasi Android/iOS?', 1.2),
('SE-MB-02', 2, 'SE', 'SE-MB', 'Seberapa yakin Anda menguasai Flutter/React Native/Kotlin?', 1.3),
('SE-MB-03', 2, 'SE', 'SE-MB', 'Seberapa yakin Anda memahami mobile UI/UX patterns?', 1.0),
('SE-MB-04', 2, 'SE', 'SE-MB', 'Seberapa yakin Anda bisa integrate API ke mobile app?', 1.1);

-- Game Programmer (SE-GM)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('SE-GM-01', 2, 'SE', 'SE-GM', 'Seberapa yakin Anda bisa menggunakan Unity/Unreal Engine?', 1.3),
('SE-GM-02', 2, 'SE', 'SE-GM', 'Seberapa yakin Anda memahami game physics dan mechanics?', 1.2),
('SE-GM-03', 2, 'SE', 'SE-GM', 'Seberapa yakin Anda bisa membuat game AI sederhana?', 1.1),
('SE-GM-04', 2, 'SE', 'SE-GM', 'Seberapa yakin Anda memahami 2D/3D graphics programming?', 1.4);

-- QA Engineer (SE-QA)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('SE-QA-01', 2, 'SE', 'SE-QA', 'Seberapa yakin Anda bisa membuat test case yang efektif?', 1.0),
('SE-QA-02', 2, 'SE', 'SE-QA', 'Seberapa yakin Anda menguasai automation testing tools (Selenium)?', 1.2),
('SE-QA-03', 2, 'SE', 'SE-QA', 'Seberapa yakin Anda memahami API testing (Postman)?', 1.1),
('SE-QA-04', 2, 'SE', 'SE-QA', 'Seberapa yakin Anda bisa melakukan performance testing?', 1.3);

-- Data Scientist (DA-DS)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('DA-DS-01', 2, 'DA', 'DA-DS', 'Seberapa yakin Anda menguasai Python untuk data science?', 1.2),
('DA-DS-02', 2, 'DA', 'DA-DS', 'Seberapa yakin Anda memahami statistik dan probability?', 1.3),
('DA-DS-03', 2, 'DA', 'DA-DS', 'Seberapa yakin Anda bisa membuat machine learning model?', 1.4),
('DA-DS-04', 2, 'DA', 'DA-DS', 'Seberapa yakin Anda memahami data visualization?', 1.0);

-- AI Engineer (DA-AI)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('DA-AI-01', 2, 'DA', 'DA-AI', 'Seberapa yakin Anda menguasai deep learning frameworks (TensorFlow/PyTorch)?', 1.4),
('DA-AI-02', 2, 'DA', 'DA-AI', 'Seberapa yakin Anda memahami neural network architecture?', 1.3),
('DA-AI-03', 2, 'DA', 'DA-AI', 'Seberapa yakin Anda bisa melakukan NLP atau Computer Vision?', 1.5),
('DA-AI-04', 2, 'DA', 'DA-AI', 'Seberapa yakin Anda memahami model deployment?', 1.2);

-- Data Analyst (DA-AN)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('DA-AN-01', 2, 'DA', 'DA-AN', 'Seberapa yakin Anda menguasai SQL untuk data analysis?', 1.1),
('DA-AN-02', 2, 'DA', 'DA-AN', 'Seberapa yakin Anda bisa menggunakan Excel/Google Sheets advanced?', 1.0),
('DA-AN-03', 2, 'DA', 'DA-AN', 'Seberapa yakin Anda memahami business intelligence tools (Tableau/PowerBI)?', 1.2),
('DA-AN-04', 2, 'DA', 'DA-AN', 'Seberapa yakin Anda bisa membuat dashboard reporting?', 1.3);

-- Data Engineer (DA-DE)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('DA-DE-01', 2, 'DA', 'DA-DE', 'Seberapa yakin Anda memahami data pipeline architecture?', 1.3),
('DA-DE-02', 2, 'DA', 'DA-DE', 'Seberapa yakin Anda menguasai ETL processes?', 1.2),
('DA-DE-03', 2, 'DA', 'DA-DE', 'Seberapa yakin Anda bisa menggunakan Apache Spark/Hadoop?', 1.4),
('DA-DE-04', 2, 'DA', 'DA-DE', 'Seberapa yakin Anda memahami data warehouse design?', 1.3);

-- BI Analyst (DA-BI)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('DA-BI-01', 2, 'DA', 'DA-BI', 'Seberapa yakin Anda bisa membuat KPI dashboard?', 1.2),
('DA-BI-02', 2, 'DA', 'DA-BI', 'Seberapa yakin Anda memahami business metrics?', 1.1),
('DA-BI-03', 2, 'DA', 'DA-BI', 'Seberapa yakin Anda menguasai data storytelling?', 1.3),
('DA-BI-04', 2, 'DA', 'DA-BI', 'Seberapa yakin Anda bisa melakukan trend analysis?', 1.0);

-- (Lanjutkan untuk profesi lainnya dengan pola yang sama...)
-- Note: Untuk hemat tempat, saya hanya berikan sample beberapa profesi.
-- Anda bisa menambahkan 15 profesi lainnya dengan pola serupa.

-- 6. LINEARITY MATRIX (sudah ada di Insert_Linearity_Matrix.sql)

-- Verifikasi
SELECT 'Rumpun' as Tabel, COUNT(*) as Jumlah FROM Rumpun
UNION ALL
SELECT 'Prodi', COUNT(*) FROM Prodi
UNION ALL
SELECT 'Profesi', COUNT(*) FROM Profesi
UNION ALL
SELECT 'Pertanyaan Fase 1', COUNT(*) FROM Pertanyaan WHERE phase = 1
UNION ALL
SELECT 'Pertanyaan Fase 2', COUNT(*) FROM Pertanyaan WHERE phase = 2
UNION ALL
SELECT 'Linearity Matrix', COUNT(*) FROM [Linearity Matrix];
