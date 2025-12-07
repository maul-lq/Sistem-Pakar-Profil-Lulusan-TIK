-- ==========================================
-- INSERT DATA LINEARITY MATRIX
-- ==========================================
-- Berdasarkan Knowledge Base dari Dokumen.md
-- 
-- Rumpun:
-- SE = Software Engineering
-- DA = Data & AI
-- IF = Infrastructure
-- CS = Cyber Security
-- CP = Creative & Product
--
-- Prodi:
-- TI = Teknik Informatika
-- TMJ = Teknik Multimedia Jaringan
-- TMD = Teknik Multimedia Digital
-- ==========================================

-- Hapus data lama jika ada
DELETE FROM [Linearity Matrix];

-- Software Engineering
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TI', 'SE', 'Linear');
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TMJ', 'SE', 'Related');
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TMD', 'SE', 'Related');

-- Data & AI
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TI', 'DA', 'Linear');
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TMJ', 'DA', 'Pivot');
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TMD', 'DA', 'Pivot');

-- Infrastructure
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TI', 'IF', 'Related');
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TMJ', 'IF', 'Linear');
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TMD', 'IF', 'Pivot');

-- Cyber Security
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TI', 'CS', 'Linear');
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TMJ', 'CS', 'Linear');
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TMD', 'CS', 'Pivot');

-- Creative & Product
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TI', 'CP', 'Related');
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TMJ', 'CP', 'Pivot');
INSERT INTO [Linearity Matrix] ([kode prodi], [kode rumpun], [status linearitas]) VALUES ('TMD', 'CP', 'Linear');

-- Verifikasi
SELECT * FROM [Linearity Matrix] ORDER BY [kode prodi], [kode rumpun];
