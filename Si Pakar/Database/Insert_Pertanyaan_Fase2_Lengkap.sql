-- ==========================================
-- PERTANYAAN FASE 2 UNTUK PROFESI LAINNYA
-- ==========================================
-- Jalankan setelah Insert_Sample_Data.sql

-- ==========================================
-- INFRASTRUCTURE (IF)
-- ==========================================

-- Network Engineer (IF-NE)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('IF-NE-01', 2, 'IF', 'IF-NE', 'Seberapa yakin Anda bisa mengkonfigurasi router dan switch?', 1.2),
('IF-NE-02', 2, 'IF', 'IF-NE', 'Seberapa yakin Anda memahami TCP/IP dan OSI model?', 1.3),
('IF-NE-03', 2, 'IF', 'IF-NE', 'Seberapa yakin Anda bisa melakukan network troubleshooting?', 1.1),
('IF-NE-04', 2, 'IF', 'IF-NE', 'Seberapa yakin Anda memahami VLAN dan subnetting?', 1.4);

-- System Administrator (IF-SA)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('IF-SA-01', 2, 'IF', 'IF-SA', 'Seberapa yakin Anda bisa mengelola server Linux/Windows?', 1.2),
('IF-SA-02', 2, 'IF', 'IF-SA', 'Seberapa yakin Anda memahami virtualization (VMware/Hyper-V)?', 1.3),
('IF-SA-03', 2, 'IF', 'IF-SA', 'Seberapa yakin Anda bisa melakukan backup dan disaster recovery?', 1.1),
('IF-SA-04', 2, 'IF', 'IF-SA', 'Seberapa yakin Anda memahami scripting (Bash/PowerShell)?', 1.2);

-- DevOps Engineer (IF-DO)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('IF-DO-01', 2, 'IF', 'IF-DO', 'Seberapa yakin Anda menguasai CI/CD tools (Jenkins/GitLab CI)?', 1.4),
('IF-DO-02', 2, 'IF', 'IF-DO', 'Seberapa yakin Anda bisa menggunakan Docker dan Kubernetes?', 1.5),
('IF-DO-03', 2, 'IF', 'IF-DO', 'Seberapa yakin Anda memahami Infrastructure as Code (Terraform)?', 1.3),
('IF-DO-04', 2, 'IF', 'IF-DO', 'Seberapa yakin Anda bisa melakukan monitoring (Prometheus/Grafana)?', 1.2);

-- IoT Engineer (IF-IO)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('IF-IO-01', 2, 'IF', 'IF-IO', 'Seberapa yakin Anda bisa programming microcontroller (Arduino/ESP32)?', 1.3),
('IF-IO-02', 2, 'IF', 'IF-IO', 'Seberapa yakin Anda memahami sensor dan actuator?', 1.2),
('IF-IO-03', 2, 'IF', 'IF-IO', 'Seberapa yakin Anda bisa integrate IoT dengan cloud?', 1.4),
('IF-IO-04', 2, 'IF', 'IF-IO', 'Seberapa yakin Anda memahami protokol IoT (MQTT/CoAP)?', 1.3);

-- Tech Support (IF-TS)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('IF-TS-01', 2, 'IF', 'IF-TS', 'Seberapa yakin Anda bisa troubleshooting masalah hardware/software?', 1.1),
('IF-TS-02', 2, 'IF', 'IF-TS', 'Seberapa yakin Anda memiliki komunikasi yang baik dengan user?', 1.0),
('IF-TS-03', 2, 'IF', 'IF-TS', 'Seberapa yakin Anda bisa membuat dokumentasi teknis?', 1.2),
('IF-TS-04', 2, 'IF', 'IF-TS', 'Seberapa yakin Anda memahami ticketing system?', 1.1);

-- ==========================================
-- CYBER SECURITY (CS)
-- ==========================================

-- Penetration Tester (CS-PT)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CS-PT-01', 2, 'CS', 'CS-PT', 'Seberapa yakin Anda bisa melakukan vulnerability assessment?', 1.4),
('CS-PT-02', 2, 'CS', 'CS-PT', 'Seberapa yakin Anda menguasai tools hacking (Metasploit/Burp Suite)?', 1.5),
('CS-PT-03', 2, 'CS', 'CS-PT', 'Seberapa yakin Anda memahami OWASP Top 10?', 1.3),
('CS-PT-04', 2, 'CS', 'CS-PT', 'Seberapa yakin Anda bisa membuat penetration testing report?', 1.2);

-- Security Analyst (CS-SA)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CS-SA-01', 2, 'CS', 'CS-SA', 'Seberapa yakin Anda bisa monitoring security event (SIEM)?', 1.3),
('CS-SA-02', 2, 'CS', 'CS-SA', 'Seberapa yakin Anda memahami threat intelligence?', 1.2),
('CS-SA-03', 2, 'CS', 'CS-SA', 'Seberapa yakin Anda bisa melakukan incident response?', 1.4),
('CS-SA-04', 2, 'CS', 'CS-SA', 'Seberapa yakin Anda memahami log analysis?', 1.1);

-- SecDevOps (CS-SD)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CS-SD-01', 2, 'CS', 'CS-SD', 'Seberapa yakin Anda bisa integrate security di CI/CD pipeline?', 1.4),
('CS-SD-02', 2, 'CS', 'CS-SD', 'Seberapa yakin Anda memahami secure coding practices?', 1.3),
('CS-SD-03', 2, 'CS', 'CS-SD', 'Seberapa yakin Anda bisa melakukan SAST/DAST?', 1.5),
('CS-SD-04', 2, 'CS', 'CS-SD', 'Seberapa yakin Anda memahami container security?', 1.2);

-- Digital Forensics (CS-FR)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CS-FR-01', 2, 'CS', 'CS-FR', 'Seberapa yakin Anda bisa melakukan data recovery?', 1.3),
('CS-FR-02', 2, 'CS', 'CS-FR', 'Seberapa yakin Anda memahami forensic tools (Autopsy/FTK)?', 1.4),
('CS-FR-03', 2, 'CS', 'CS-FR', 'Seberapa yakin Anda bisa menganalisis malware?', 1.5),
('CS-FR-04', 2, 'CS', 'CS-FR', 'Seberapa yakin Anda memahami chain of custody?', 1.2);

-- GRC Specialist (CS-GR)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CS-GR-01', 2, 'CS', 'CS-GR', 'Seberapa yakin Anda memahami compliance standards (ISO 27001)?', 1.3),
('CS-GR-02', 2, 'CS', 'CS-GR', 'Seberapa yakin Anda bisa melakukan risk assessment?', 1.2),
('CS-GR-03', 2, 'CS', 'CS-GR', 'Seberapa yakin Anda memahami security policies dan procedures?', 1.1),
('CS-GR-04', 2, 'CS', 'CS-GR', 'Seberapa yakin Anda bisa melakukan security audit?', 1.4);

-- ==========================================
-- CREATIVE & PRODUCT (CP)
-- ==========================================

-- UI Designer (CP-UI)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CP-UI-01', 2, 'CP', 'CP-UI', 'Seberapa yakin Anda menguasai design tools (Figma/Adobe XD)?', 1.3),
('CP-UI-02', 2, 'CP', 'CP-UI', 'Seberapa yakin Anda memahami design principles?', 1.2),
('CP-UI-03', 2, 'CP', 'CP-UI', 'Seberapa yakin Anda bisa membuat design system?', 1.4),
('CP-UI-04', 2, 'CP', 'CP-UI', 'Seberapa yakin Anda memahami typography dan color theory?', 1.1);

-- UX Researcher (CP-UX)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CP-UX-01', 2, 'CP', 'CP-UX', 'Seberapa yakin Anda bisa melakukan user research?', 1.3),
('CP-UX-02', 2, 'CP', 'CP-UX', 'Seberapa yakin Anda memahami usability testing?', 1.2),
('CP-UX-03', 2, 'CP', 'CP-UX', 'Seberapa yakin Anda bisa membuat user persona dan journey map?', 1.4),
('CP-UX-04', 2, 'CP', 'CP-UX', 'Seberapa yakin Anda memahami information architecture?', 1.1);

-- Graphic Designer (CP-GD)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CP-GD-01', 2, 'CP', 'CP-GD', 'Seberapa yakin Anda menguasai Adobe Creative Suite?', 1.3),
('CP-GD-02', 2, 'CP', 'CP-GD', 'Seberapa yakin Anda bisa membuat branding dan identity?', 1.2),
('CP-GD-03', 2, 'CP', 'CP-GD', 'Seberapa yakin Anda memahami layout dan composition?', 1.1),
('CP-GD-04', 2, 'CP', 'CP-GD', 'Seberapa yakin Anda bisa membuat ilustrasi digital?', 1.4);

-- Multimedia Specialist (CP-MM)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CP-MM-01', 2, 'CP', 'CP-MM', 'Seberapa yakin Anda bisa melakukan video editing?', 1.3),
('CP-MM-02', 2, 'CP', 'CP-MM', 'Seberapa yakin Anda menguasai After Effects atau Premiere Pro?', 1.4),
('CP-MM-03', 2, 'CP', 'CP-MM', 'Seberapa yakin Anda memahami audio production?', 1.2),
('CP-MM-04', 2, 'CP', 'CP-MM', 'Seberapa yakin Anda bisa membuat motion graphics?', 1.5);

-- Product Manager (CP-PM)
INSERT INTO Pertanyaan ([Id Pertanyaan], [phase], [kode rumpun], [kode profesi], [teks pertanyaan], [expert weight]) VALUES 
('CP-PM-01', 2, 'CP', 'CP-PM', 'Seberapa yakin Anda memahami product lifecycle?', 1.3),
('CP-PM-02', 2, 'CP', 'CP-PM', 'Seberapa yakin Anda bisa melakukan market research?', 1.2),
('CP-PM-03', 2, 'CP', 'CP-PM', 'Seberapa yakin Anda memahami agile/scrum methodology?', 1.1),
('CP-PM-04', 2, 'CP', 'CP-PM', 'Seberapa yakin Anda bisa membuat product roadmap?', 1.4);

-- Verifikasi total pertanyaan fase 2
SELECT 
    [kode rumpun],
    COUNT(*) as total_soal
FROM Pertanyaan 
WHERE phase = 2
GROUP BY [kode rumpun]
ORDER BY [kode rumpun];

-- Harus menghasilkan:
-- SE: 20 soal (5 profesi × 4 soal)
-- DA: 20 soal (5 profesi × 4 soal)
-- IF: 20 soal (5 profesi × 4 soal)
-- CS: 20 soal (5 profesi × 4 soal)
-- CP: 20 soal (5 profesi × 4 soal)
-- TOTAL: 100 soal
