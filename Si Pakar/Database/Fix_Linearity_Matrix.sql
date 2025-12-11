/* =====================================================================
   FIX: LINEARITY MATRIX - MENAMBAHKAN DATA YANG HILANG
   
   Problem: Tabel Linearity Matrix hanya punya 9 record dari 15 kombinasi
   Solution: Insert 6 record yang missing (untuk rumpun DI dan IN)
   
   Author: AI Assistant
   Date: 2024
   ===================================================================== */

USE [Database Sistem Pakar]
GO

-- Backup data existing (opsional - untuk keamanan)
-- SELECT * INTO [Linearity Matrix_Backup] FROM [Linearity Matrix]

-- Insert data yang hilang
-- Berdasarkan dokumentasi di copilot-instructions.md

PRINT 'Starting Linearity Matrix fix...'

-- Check existing data count
DECLARE @ExistingCount INT
SELECT @ExistingCount = COUNT(*) FROM [Linearity Matrix]
PRINT 'Existing records: ' + CAST(@ExistingCount AS NVARCHAR(10))

-- Insert missing records
SET IDENTITY_INSERT [dbo].[Linearity Matrix] ON;

-- TI (Teknik Informatika) - Data & Intelligence (Linear)
IF NOT EXISTS (SELECT 1 FROM [Linearity Matrix] WHERE [kode prodi] = 'TI' AND [kode rumpun] = 'DI')
BEGIN
    INSERT INTO [dbo].[Linearity Matrix] ([matrix id], [kode prodi], [kode rumpun], [status linearitas])
    VALUES (4, 'TI', 'DI', 'Linear')
    PRINT '? Inserted: TI ? DI = Linear'
END
ELSE
    PRINT '? Already exists: TI ? DI'

-- TI (Teknik Informatika) - Infrastructure (Related)
IF NOT EXISTS (SELECT 1 FROM [Linearity Matrix] WHERE [kode prodi] = 'TI' AND [kode rumpun] = 'IN')
BEGIN
    INSERT INTO [dbo].[Linearity Matrix] ([matrix id], [kode prodi], [kode rumpun], [status linearitas])
    VALUES (5, 'TI', 'IN', 'Related')
    PRINT '? Inserted: TI ? IN = Related'
END
ELSE
    PRINT '? Already exists: TI ? IN'

-- TMJ (Teknik Multimedia Jaringan) - Data & Intelligence (Pivot)
IF NOT EXISTS (SELECT 1 FROM [Linearity Matrix] WHERE [kode prodi] = 'TMJ' AND [kode rumpun] = 'DI')
BEGIN
    INSERT INTO [dbo].[Linearity Matrix] ([matrix id], [kode prodi], [kode rumpun], [status linearitas])
    VALUES (6, 'TMJ', 'DI', 'Pivot')
    PRINT '? Inserted: TMJ ? DI = Pivot'
END
ELSE
    PRINT '? Already exists: TMJ ? DI'

-- TMJ (Teknik Multimedia Jaringan) - Infrastructure (Linear)
IF NOT EXISTS (SELECT 1 FROM [Linearity Matrix] WHERE [kode prodi] = 'TMJ' AND [kode rumpun] = 'IN')
BEGIN
    INSERT INTO [dbo].[Linearity Matrix] ([matrix id], [kode prodi], [kode rumpun], [status linearitas])
    VALUES (7, 'TMJ', 'IN', 'Linear')
    PRINT '? Inserted: TMJ ? IN = Linear'
END
ELSE
    PRINT '? Already exists: TMJ ? IN'

-- TMD (Teknik Multimedia Digital) - Data & Intelligence (Pivot)
IF NOT EXISTS (SELECT 1 FROM [Linearity Matrix] WHERE [kode prodi] = 'TMD' AND [kode rumpun] = 'DI')
BEGIN
    INSERT INTO [dbo].[Linearity Matrix] ([matrix id], [kode prodi], [kode rumpun], [status linearitas])
    VALUES (8, 'TMD', 'DI', 'Pivot')
    PRINT '? Inserted: TMD ? DI = Pivot'
END
ELSE
    PRINT '? Already exists: TMD ? DI'

-- TMD (Teknik Multimedia Digital) - Infrastructure (Pivot)
IF NOT EXISTS (SELECT 1 FROM [Linearity Matrix] WHERE [kode prodi] = 'TMD' AND [kode rumpun] = 'IN')
BEGIN
    INSERT INTO [dbo].[Linearity Matrix] ([matrix id], [kode prodi], [kode rumpun], [status linearitas])
    VALUES (9, 'TMD', 'IN', 'Pivot')
    PRINT '? Inserted: TMD ? IN = Pivot'
END
ELSE
    PRINT '? Already exists: TMD ? IN'

SET IDENTITY_INSERT [dbo].[Linearity Matrix] OFF;

-- Verify result
DECLARE @NewCount INT
SELECT @NewCount = COUNT(*) FROM [Linearity Matrix]
PRINT ''
PRINT 'Fix completed!'
PRINT 'Records before: ' + CAST(@ExistingCount AS NVARCHAR(10))
PRINT 'Records after: ' + CAST(@NewCount AS NVARCHAR(10))
PRINT 'Records added: ' + CAST(@NewCount - @ExistingCount AS NVARCHAR(10))

-- Display final complete matrix
PRINT ''
PRINT '==================================================================='
PRINT 'COMPLETE LINEARITY MATRIX (All 15 combinations)'
PRINT '==================================================================='
SELECT 
    [matrix id],
    [kode prodi] AS 'Prodi',
    [kode rumpun] AS 'Rumpun',
    [status linearitas] AS 'Status',
    CASE 
        WHEN [status linearitas] = 'Linear' THEN '? SEJALAN'
        WHEN [status linearitas] = 'Related' THEN '?? TERKAIT'
        WHEN [status linearitas] = 'Pivot' THEN '?? PERLU PIVOT'
        ELSE '? UNKNOWN'
    END AS 'Keterangan'
FROM [Linearity Matrix]
ORDER BY [kode prodi], [kode rumpun]

PRINT ''
PRINT '==================================================================='
PRINT 'VERIFICATION: Check for any missing combinations'
PRINT '==================================================================='

-- Generate expected combinations and check missing
DECLARE @Expected TABLE (prodi NVARCHAR(10), rumpun NVARCHAR(10))
INSERT INTO @Expected VALUES
('TI', 'SE'), ('TI', 'DI'), ('TI', 'IN'), ('TI', 'CS'), ('TI', 'CP'),
('TMJ', 'SE'), ('TMJ', 'DI'), ('TMJ', 'IN'), ('TMJ', 'CS'), ('TMJ', 'CP'),
('TMD', 'SE'), ('TMD', 'DI'), ('TMD', 'IN'), ('TMD', 'CS'), ('TMD', 'CP')

SELECT 
    e.prodi AS 'Missing Prodi',
    e.rumpun AS 'Missing Rumpun'
FROM @Expected e
LEFT JOIN [Linearity Matrix] lm ON e.prodi = lm.[kode prodi] AND e.rumpun = lm.[kode rumpun]
WHERE lm.[matrix id] IS NULL

IF @@ROWCOUNT = 0
    PRINT '? All combinations exist! No missing records.'
ELSE
    PRINT '? WARNING: Some combinations are still missing (see results above)'

GO
