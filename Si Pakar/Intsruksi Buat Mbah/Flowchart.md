1. Analisis Flowchart
Tujuan Sistem: Flowchart ini menggambarkan sebuah Sistem Asesmen Adaptif (kemungkinan besar untuk penentuan jurusan kuliah atau karier). Sistem ini tidak memberikan soal yang sama kepada semua orang, melainkan menyesuaikan jenis soal tahap kedua berdasarkan performa pengguna di tahap pertama.

Komponen Utama:

Input Data Diri: Mengambil nama, prodi, dan minat awal.

Tes Awal (General): 10 pertanyaan umum untuk memetakan kemampuan dasar.

Logika Percabangan (Decision Logic): Ini adalah inti dari sistem. Sistem mengevaluasi "Skor Tertinggi" (Top Score) dari tes awal dan membandingkannya dengan ambang batas (40%). Sistem juga melihat "Gap" (selisih skor) antara peringkat 1 dan 2, serta kesesuaian dengan "Minat" yang diinput di awal.

Tes Tahap Kedua (Specific): Berdasarkan evaluasi, pengguna diberikan 20 soal tambahan yang lebih spesifik (bisa dari rumpun minat, rumpun "Creative", atau campuran antara juara 1 dan 2).

Kalkulasi Akhir: Menggunakan metode DST (kemungkinan Dempster-Shafer Theory), konstanta boost, dan variabel minat.

Output: Analisis potensi kelulusan dan linearitas.

2. Representasi Flowchart dalam Markdown
Berikut adalah algoritma flowchart tersebut yang diubah ke dalam bentuk teks terstruktur:

Alur Logika Sistem Asesmen Potensi & Minat
I. Inisiasi
Start

Input: User memasukkan Nama, Program Studi, dan Minat.

Output Tahap 1: Sistem memberikan 10 Pertanyaan Umum.

Input: User memasukkan Jawaban.

II. Evaluasi & Penentuan Soal Tahap 2
Sistem mengevaluasi jawaban tahap 1 untuk menentukan jenis soal berikutnya (Total 20 soal).

Kondisi A: Performa Rendah
Cek: Apakah Skor Tertinggi < 40%?

YA:

Cek: Apakah Ada pilihan minat?

Ya: Output 20 soal dari rumpun minat.

Tidak: Output 20 soal dari rumpun Creative.

Kondisi B: Performa Cukup/Tinggi (Skor >= 40%)
Jika Kondisi A bernilai TIDAK (artinya skor >= 40%), maka sistem mengecek kriteria berikut secara berurutan:

Cek Ketidaksesuaian Minat:

Apakah Minat TIDAK SAMA dengan Juara 1 atau Juara 2?

YA: Output 10 soal rumpun Juara 1 DAN 10 soal rumpun Minat.

TIDAK: Lanjut ke pengecekan berikutnya.

Cek Dominasi Skor (Gap Tinggi):

Apakah Gap > 20% (selisih skor jauh) DAN Minat SAMA dengan Juara 1 atau 2?

YA: Output 20 soal dari rumpun Juara 1.

TIDAK: Lanjut ke pengecekan berikutnya.

Cek Persaingan Skor (Gap Rendah):

Apakah Gap <= 20% (selisih skor tipis) DAN Minat SAMA dengan Juara 1 atau 2?

YA: Output 10 soal rumpun Juara 1 DAN 10 soal rumpun Juara 2.

TIDAK: Kembali ke evaluasi awal (Loop/Error Handling).

III. Finalisasi
Input: User memasukkan Jawaban (dari 20 soal tahap 2).

Proses Hitung:

Menghitung skor akhir menggunakan metode DST (Dempster-Shafer Theory).

Menerapkan Konstanta Boost.

Memperhitungkan Variabel Minat.

Output Akhir: Menampilkan Potensi lulusan dengan linearity analysis.

End