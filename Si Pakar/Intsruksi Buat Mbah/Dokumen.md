	Topik
Kelompok kami ditugaskan untuk membuat Sistem Pakar Profil Lulusan TIK. Untuk membedakan dengan kelompok lain, kami menggunakan metode Forward Chaining dan Dempster-Shafer Theory, dengan tambahan-tambahan lainnya seperti Interest Boosting (Menambah point untuk lulusan yang diminati), Linearity Analysis (Melihat apakah output sesuai dengan jurusannya dan memberikan insight, serta membuat pertanyaan dengan model Adaptive Testing (Menyesuaikan dengan kemampuannya).
	Mekanisme Sistem
Berikut penjelasan dari mekanismenya:
	Alur Sistem
Terdapat lima langkat pada alur sistem kami:
	Input Data: Pengguna memasukkan nama, jurusan, dan memilih minat karir (checkbox, maks 3)
	Input Pertanyaan: Pengguna akan menjawab pertanyaan yang diberikan oleh sistem. Pertanyaan akan dibagi dalam 2 Fase:
	Pertanyaan Umum (10 Soal): Pertanyaan ini berguna untuk mengklasifikasi kemampuan pengguna secara umum, yang digunakan untuk menentukan soal pada fase ke-2.
	Pertanyaan Khusus (20 Soal): Pertanyaan ini berguna untuk mengetahui kemampuan pengguna pada bidang atau rumpun yang telah ditentukan pada saat fase ke-1.
	Perhitungan DST: Sistem akan menghitung massa keyakinan, irisan himpunan, dan konflik.
	Perhitungan Booster: Sistem akan menghitung atau menambahkan poin minat dari pilihan minat pengguna ke skor atau hasil perhitungan DST sebelumnya.
	Output Final: Menampilkan prediksi karir (Skill + Minat) VS Analisis Linieritas (Kesesuaian Jurusan)
	Struktur Data dan Variabel
	Standar Input Jawaban: Setiap soal dijawab menggunakan skala Likert yang dikonversi menjadi probabilitas. Nilai yang dikonversi berada pada rentang 0 sampai 0,95.
Pilihan Pengguna	Keterangan	Nilai
Sangat Tidak Mampu	Tidak punya pengetahuan sama sekali	0,0
Kurang Mampu	Pernah dengar tapi lupa/tidak paham	0,2
Cukup Mampu	Paham teori dasar	0,5
Mampu	Sering mempraktekkan	0,8
Sangat Mampu	Sangat menguasai/Ahli	0,95

m(Himpunan)=BobotPakar×F_Pengguna

	Struktur Rumpun Karir: Karir dibagi menjadi 5 rumpun, masing-masing mempunyai 5 sub-karir:
	Software Engineering	: Backend, Frontend, Mobile, Game Programming, Quality Assurance
	Data & Intellegence	: Data Scientist, AI Engineer, Data Analyst, Data Engineer, BI Analyst
	Infrastructure		: Network Engineer, System Administrator, DevOps, IoT Engineer, Tech Support
	Cyber Security	: Pentester, Security Analyst, SecDevOps, Forensics, GRC
	Creative & Product	: UI Designer, UX Researcher, Graphic Designer, Multmedia, Product Manager
	Variabel Minat & Konstanta Booster: Variabel Minat akan disesuaikan dengan minat yang dipilih. Jika diminati, maka nilai variabelnya 1,0. Jika sebaliknya, maka nilainya 0,0. Variabel Minat akan dikalikan dengan Konstanta Booster bernilai 0,3.
	Mekanisme Adaptif
	Fase 1 - Pertanyaan Umum (10 Soal): Pengguna wajib mengerjakan 2 soal setiap Rumpun untuk menentukan soal pada fase ke-2.
	Fase 2 - Pertanyaan Khusus (20 Soal): Sistem menghitung skor pada Fase 1 dan menentukan paket soal Fase 2 menggunakan logika berikut:
	Skenario Dominan (Gap frekuensi Rumpun Juara 1 dan Juara 2 > 20%):
	Status: Pengguna fokus atau ahli terhadap satu bidang
	Aksi: Ambil 20 Soal dari rumpun
	Distribusi: 4 Soal per Sub-Karir
	Skenario Hybrid (Gap frekuensi Rumpun Juara 1 dan Juara 2 =< 20% dan Juara 1 > 40%):
	Status: Pengguna fokus atau ahli terhadap dua bidang atau lebih
	Aksi: Ambil 10 Soal dari Rumpun Juara 1 dan 10 Soal dari Rumpun Juara 2
	Distribusi: 2 Soal per Sub-Karir
	Skenario Pemula (Frekuensi Rumpun Juara 1 < 40%):
	Status: Pengguna kurang bisa terhadap semua bidang yang ada
	Aksi: Ambil frekuensi minat dan hubungannya dengan rumpun. Jika frekuensi minat pada satu atau dua rumpun lebih besar dibandingkan rumpun lain, maka gunakan rumpun tersebut.
	Distribusi: 2 atau 4 Soal per Sub-Karir (Sesuai dengan jumlah rumpun)
	Skenario Null (Frekuensi Rumpun Juara 1 < 40% dan tidak ada minat):
	Status: Pengguna tidak bisa terhadap semua bidang yang ada
	Aksi: Ambil 20 Soal dari rumpun Creative & Product
	Distribusi 4 Soal per Sub-Karir
	Skor Akhir
Rumus perhitungan skor akhir terdiri dari: Skor Kemampuan (DST), Konstanta Boost, dan Variabel Minat. Lalu, berikut merupakan tabel yang berkaitan dengan kolerasi antara skill dan minat.
Kondisi	Zona
Skill Tinggi, Minat Tinggi	Zona Emas
Skill Tinggi, Minat Rendah	Zona Jebakan
Skill Rendah, Minat Tinggi	Zona Potensi
Skill Rendah, Minat Rendah	Zona Abaikan

	Output
Output yang diberikan adalah potensial lulusan profil pengguna. Disinilah Linearity Analysis dipakai. Berikut merupakan table Knowledge Base untuk Linearity Analysis.
Rumpun Hasil Tes	TI	TMJ	TMD
Software Eng	Linear	Related	Related
Data & AI	Linear	Pivot	Pivot
Infrastructure	Related	Linear	Pivot
Cyber Security	Linear	Linear	Pivot
Creative	Related	Pivot	Linear

Linear berarti sejalan dengan program studinya. Related masih ada dalam program studinya namun separuhnya saja. Pivot tidak relevan dengan program studinya.
