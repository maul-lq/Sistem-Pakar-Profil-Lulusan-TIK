USE [Database Sistem Pakar]
GO
/****** Object:  Table [dbo].[Data User]    Script Date: 07/12/2025 14.16.17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data User](
	[Id user] [int] IDENTITY(1,1) NOT NULL,
	[nama] [nvarchar](150) NOT NULL,
	[email] [nvarchar](150) NOT NULL,
	[kode prodi] [nvarchar](10) NOT NULL,
	[profesi] [nvarchar](150) NOT NULL,
	[kode rumpun] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exam Details]    Script Date: 07/12/2025 14.16.17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exam Details](
	[detail id] [int] NOT NULL,
	[id sesi] [int] NOT NULL,
	[id pertanyaan] [nvarchar](10) NOT NULL,
	[user confidence] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[detail id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exam Rumpun Scores]    Script Date: 07/12/2025 14.16.17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exam Rumpun Scores](
	[score id] [int] NOT NULL,
	[id sesi] [int] NULL,
	[kode rumpun] [nvarchar](10) NULL,
	[score value] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[score id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Linearity Matrix]    Script Date: 07/12/2025 14.16.17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Linearity Matrix](
	[matrix id] [int] IDENTITY(1,1) NOT NULL,
	[kode prodi] [nvarchar](10) NOT NULL,
	[kode rumpun] [nvarchar](10) NOT NULL,
	[status linearitas] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[matrix id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Map Bukti Pertanyaan]    Script Date: 07/12/2025 14.16.17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map Bukti Pertanyaan](
	[map id] [int] NOT NULL,
	[id pertanyaan] [nvarchar](10) NOT NULL,
	[kode profesi target] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[map id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nilai Ujuan Rumpun]    Script Date: 07/12/2025 14.16.17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nilai Ujuan Rumpun](
	[Id skor] [int] NOT NULL,
	[id sesi] [int] NOT NULL,
	[kode rumpun] [nvarchar](10) NOT NULL,
	[nilai skor] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id skor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pertanyaan]    Script Date: 07/12/2025 14.16.17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pertanyaan](
	[Id Pertanyaan] [nvarchar](10) NOT NULL,
	[phase] [int] NOT NULL,
	[kode rumpun] [nvarchar](10) NOT NULL,
	[kode profesi] [nvarchar](20) NULL,
	[teks pertanyaan] [nvarchar](max) NOT NULL,
	[expert weight] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id Pertanyaan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prodi]    Script Date: 07/12/2025 14.16.17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prodi](
	[kode prodi] [nvarchar](10) NOT NULL,
	[nama prodi] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[kode prodi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profesi]    Script Date: 07/12/2025 14.16.17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profesi](
	[kode profesi] [nvarchar](20) NOT NULL,
	[kode rumpun] [nvarchar](10) NOT NULL,
	[nama profesi] [nvarchar](100) NOT NULL,
	[deskripsi] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[kode profesi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rumpun]    Script Date: 07/12/2025 14.16.17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rumpun](
	[kode rumpun] [nvarchar](10) NOT NULL,
	[nama rumpun] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[kode rumpun] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sesi Ujian]    Script Date: 07/12/2025 14.16.17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sesi Ujian](
	[Id sesi] [int] NOT NULL,
	[Id user] [int] NOT NULL,
	[waktu mulai] [datetime] NOT NULL,
	[waktu selesai] [datetime] NOT NULL,
	[scenario used] [nvarchar](50) NOT NULL,
	[phase1 score json] [nvarchar](max) NOT NULL,
	[final result json] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id sesi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pertanyaan] ADD  DEFAULT ('NULL') FOR [kode profesi]
GO
ALTER TABLE [dbo].[Data User]  WITH CHECK ADD  CONSTRAINT [FK_Data User_ToProdi] FOREIGN KEY([kode prodi])
REFERENCES [dbo].[Prodi] ([kode prodi])
GO
ALTER TABLE [dbo].[Data User] CHECK CONSTRAINT [FK_Data User_ToProdi]
GO
ALTER TABLE [dbo].[Data User]  WITH CHECK ADD  CONSTRAINT [FK_Data User_ToRumpun] FOREIGN KEY([kode rumpun])
REFERENCES [dbo].[Rumpun] ([kode rumpun])
GO
ALTER TABLE [dbo].[Data User] CHECK CONSTRAINT [FK_Data User_ToRumpun]
GO
ALTER TABLE [dbo].[Exam Details]  WITH CHECK ADD  CONSTRAINT [FK_Exam Details_Pertanyaan] FOREIGN KEY([id pertanyaan])
REFERENCES [dbo].[Pertanyaan] ([Id Pertanyaan])
GO
ALTER TABLE [dbo].[Exam Details] CHECK CONSTRAINT [FK_Exam Details_Pertanyaan]
GO
ALTER TABLE [dbo].[Exam Details]  WITH CHECK ADD  CONSTRAINT [FK_Exam Details_Sesi Ujan] FOREIGN KEY([id sesi])
REFERENCES [dbo].[Sesi Ujian] ([Id sesi])
GO
ALTER TABLE [dbo].[Exam Details] CHECK CONSTRAINT [FK_Exam Details_Sesi Ujan]
GO
ALTER TABLE [dbo].[Exam Rumpun Scores]  WITH CHECK ADD  CONSTRAINT [FK_Exam Rumpun Scores_Rumpun] FOREIGN KEY([kode rumpun])
REFERENCES [dbo].[Rumpun] ([kode rumpun])
GO
ALTER TABLE [dbo].[Exam Rumpun Scores] CHECK CONSTRAINT [FK_Exam Rumpun Scores_Rumpun]
GO
ALTER TABLE [dbo].[Exam Rumpun Scores]  WITH CHECK ADD  CONSTRAINT [FK_Exam Rumpun Scores_Sesi] FOREIGN KEY([id sesi])
REFERENCES [dbo].[Sesi Ujian] ([Id sesi])
GO
ALTER TABLE [dbo].[Exam Rumpun Scores] CHECK CONSTRAINT [FK_Exam Rumpun Scores_Sesi]
GO
ALTER TABLE [dbo].[Linearity Matrix]  WITH CHECK ADD  CONSTRAINT [FK_Table_ToProdi] FOREIGN KEY([kode prodi])
REFERENCES [dbo].[Prodi] ([kode prodi])
GO
ALTER TABLE [dbo].[Linearity Matrix] CHECK CONSTRAINT [FK_Table_ToProdi]
GO
ALTER TABLE [dbo].[Linearity Matrix]  WITH CHECK ADD  CONSTRAINT [FK_Table_ToRumpun] FOREIGN KEY([kode rumpun])
REFERENCES [dbo].[Rumpun] ([kode rumpun])
GO
ALTER TABLE [dbo].[Linearity Matrix] CHECK CONSTRAINT [FK_Table_ToRumpun]
GO
ALTER TABLE [dbo].[Map Bukti Pertanyaan]  WITH CHECK ADD  CONSTRAINT [FK_Map Bukti Pertanyaan_pertanyan] FOREIGN KEY([id pertanyaan])
REFERENCES [dbo].[Pertanyaan] ([Id Pertanyaan])
GO
ALTER TABLE [dbo].[Map Bukti Pertanyaan] CHECK CONSTRAINT [FK_Map Bukti Pertanyaan_pertanyan]
GO
ALTER TABLE [dbo].[Map Bukti Pertanyaan]  WITH CHECK ADD  CONSTRAINT [FK_Map Bukti Pertanyaan_profesi] FOREIGN KEY([kode profesi target])
REFERENCES [dbo].[Profesi] ([kode profesi])
GO
ALTER TABLE [dbo].[Map Bukti Pertanyaan] CHECK CONSTRAINT [FK_Map Bukti Pertanyaan_profesi]
GO
ALTER TABLE [dbo].[Nilai Ujuan Rumpun]  WITH CHECK ADD  CONSTRAINT [FK_Nilai Ujuan Rumpun_Sesi Ujian] FOREIGN KEY([id sesi])
REFERENCES [dbo].[Sesi Ujian] ([Id sesi])
GO
ALTER TABLE [dbo].[Nilai Ujuan Rumpun] CHECK CONSTRAINT [FK_Nilai Ujuan Rumpun_Sesi Ujian]
GO
ALTER TABLE [dbo].[Pertanyaan]  WITH CHECK ADD  CONSTRAINT [FK_Pertanyaan_ToProfiesi] FOREIGN KEY([kode profesi])
REFERENCES [dbo].[Profesi] ([kode profesi])
GO
ALTER TABLE [dbo].[Pertanyaan] CHECK CONSTRAINT [FK_Pertanyaan_ToProfiesi]
GO
ALTER TABLE [dbo].[Pertanyaan]  WITH CHECK ADD  CONSTRAINT [FK_Pertanyaan_ToRumpun] FOREIGN KEY([kode rumpun])
REFERENCES [dbo].[Rumpun] ([kode rumpun])
GO
ALTER TABLE [dbo].[Pertanyaan] CHECK CONSTRAINT [FK_Pertanyaan_ToRumpun]
GO
ALTER TABLE [dbo].[Profesi]  WITH CHECK ADD  CONSTRAINT [FK_Profesi_ToRumpun] FOREIGN KEY([kode rumpun])
REFERENCES [dbo].[Rumpun] ([kode rumpun])
GO
ALTER TABLE [dbo].[Profesi] CHECK CONSTRAINT [FK_Profesi_ToRumpun]
GO
ALTER TABLE [dbo].[Sesi Ujian]  WITH CHECK ADD  CONSTRAINT [FK_Sesi Ujian_Data User] FOREIGN KEY([Id user])
REFERENCES [dbo].[Data User] ([Id user])
GO
ALTER TABLE [dbo].[Sesi Ujian] CHECK CONSTRAINT [FK_Sesi Ujian_Data User]
GO
