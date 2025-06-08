Dokumen Perancangan: Aplikasi Proyek & Tagihan Freelancer
Dokumen ini berfungsi sebagai cetak biru (blueprint) untuk membangun aplikasi manajemen proyek dan tagihan. Tujuannya adalah untuk memberikan panduan yang jelas dan terstruktur dari konsep hingga implementasi.

1. Visi & Pengguna (User Persona)
Visi Aplikasi: Menyediakan alat yang sederhana, efisien, dan self-hosted bagi freelancer dan UMKM untuk mengelola siklus hidup proyek, dari penawaran hingga pembayaran, tanpa biaya langganan bulanan.

Target Pengguna Utama (Persona):

Nama: Budi

Pekerjaan: Freelance Desainer Grafis / Web Developer

Kebutuhan:

Mencatat daftar klien agar tidak tercecer.

Melacak proyek mana yang sedang aktif, dalam revisi, atau sudah selesai.

Memecah proyek besar (misal: "Branding Usaha Kopi") menjadi tugas-tugas kecil (Logo, Kemasan, Menu).

Membuat tagihan profesional dalam format PDF dengan cepat berdasarkan pekerjaan yang selesai.

Melihat ringkasan pendapatan bulanan.

2. Fitur Utama (Core Features)
Aplikasi akan dibagi menjadi beberapa modul utama untuk memudahkan pengembangan dan penggunaan.

Manajemen Klien (Client Management)

[ ] Tambah, Edit, Hapus data klien (Nama, Perusahaan, Email, No. Telepon).

[ ] Lihat daftar semua klien dengan fitur pencarian.

[ ] Lihat riwayat proyek untuk setiap klien.

Manajemen Proyek (Project Management)

[ ] Buat proyek baru yang terhubung ke seorang klien.

[ ] Atur detail proyek (Nama Proyek, Deskripsi, Deadline, Nominal Proyek / Tarif per Jam).

[ ] Ubah status proyek (Contoh: Baru, Aktif, Selesai, Dibatalkan).

Manajemen Tugas (Task Management)

[ ] Tambah, Edit, Hapus tugas di dalam sebuah proyek.

[ ] Setiap tugas memiliki deskripsi dan status (Contoh: To-do, In Progress, Done).

[ ] (Opsional) Tetapkan nilai nominal untuk setiap tugas jika proyek tidak berbasis harga tetap.

Manajemen Tagihan (Invoice Management)

[ ] Buat tagihan baru dari sebuah proyek yang telah berstatus Selesai.

[ ] Aplikasi secara otomatis menarik item-item dari tugas yang Done atau nominal proyek.

[ ] Kustomisasi tagihan (Nomor Invoice, Tanggal Jatuh Tempo, Catatan, Info Pajak/PPN jika ada).

[ ] Ubah status tagihan (Draft, Terkirim, Lunas, Terlambat).

[ ] Generate tagihan ke format PDF untuk diunduh atau dikirim.

Dashboard

[ ] Halaman utama yang menampilkan ringkasan visual:

Jumlah proyek aktif.

Tagihan yang belum dibayar dan yang akan jatuh tempo.

Grafik sederhana pendapatan bulanan (dari tagihan yang Lunas).

3. Desain Database (Skema Tabel)
Ini adalah fondasi aplikasi Anda. Menggunakan PostgreSQL dan Entity Framework Core akan sangat mempermudah proses ini.

// Tabel Pengguna (jika ingin sistem multi-user di masa depan, untuk MVP bisa diskip)
Users
-----
Id (PK, Guid)
FullName
Email (Unique)
PasswordHash

// Tabel Klien
Clients
-------
Id (PK, Guid)
UserId (FK to Users) // Milik siapa klien ini
Name
Company
Email
PhoneNumber
CreatedAt (datetime)

// Tabel Proyek
Projects
--------
Id (PK, Guid)
ClientId (FK to Clients) // Proyek ini milik klien mana
Name
Description
Status (int/enum: Baru=0, Aktif=1, Selesai=2, Dibatalkan=3)
TotalValue (decimal) // Harga total proyek
DueDate (datetime)
CreatedAt (datetime)

// Tabel Tugas
Tasks
-----
Id (PK, Guid)
ProjectId (FK to Projects) // Tugas ini bagian dari proyek mana
Description
Status (int/enum: ToDo=0, InProgress=1, Done=2)
Value (decimal) // Harga tugas ini (jika tidak flat)
CreatedAt (datetime)

// Tabel Tagihan
Invoices
--------
Id (PK, Guid)
ProjectId (FK to Projects) // Tagihan untuk proyek mana
InvoiceNumber (string, unique) // Contoh: INV/2025/06/001
Status (int/enum: Draft=0, Sent=1, Paid=2, Overdue=3)
IssueDate (datetime) // Tanggal diterbitkan
DueDate (datetime)   // Tanggal jatuh tempo
TotalAmount (decimal)
Notes (text)
CreatedAt (datetime)

// Tabel Item Tagihan (Detail dari setiap tagihan)
InvoiceItems
------------
Id (PK, Guid)
InvoiceId (FK to Invoices)
Description (string) // Deskripsi item, misal: "Desain Logo V1"
Quantity (int)
UnitPrice (decimal)
Total (decimal) // Dihitung dari Quantity * UnitPrice

4. Alur Proses Utama (Flowchart)
Berikut adalah dua alur kerja paling krusial dalam aplikasi ini.

A. Flowchart Alur Kerja Proyek (Dari Awal Hingga Selesai)
graph TD
    A[Mulai] --> B{Punya Klien?};
    B -- Tidak --> C[Tambah Klien Baru];
    B -- Ya --> D[Pilih Klien dari Daftar];
    C --> D;
    D --> E[Buat Proyek Baru];
    E --> F[Isi Detail Proyek (Nama, Deskripsi, Nilai)];
    F --> G[Proyek Dibuat dengan Status 'Baru'];
    G --> H[Tambah Tugas-Tugas ke Proyek];
    H --> I{Kerjakan Proyek};
    I --> J[Update Status Tugas menjadi 'In Progress' atau 'Done'];
    J --> K{Semua Tugas Selesai?};
    K -- Belum --> I;
    K -- Sudah --> L[Update Status Proyek menjadi 'Selesai'];
    L --> M[Proyek Siap untuk Ditagih];
    M --> Z[Selesai];

B. Flowchart Alur Pembuatan Tagihan (Invoicing)
graph TD
    A[Mulai dari Dashboard/Halaman Proyek] --> B[Pilih Proyek yang Berstatus 'Selesai'];
    B --> C[Klik Tombol 'Buat Tagihan'];
    C --> D[Sistem Membuat Tagihan 'Draft'];
    D --> E[Tarik Otomatis Detail Proyek & Tugas Selesai ke Item Tagihan];
    E --> F{Perlu Edit?};
    F -- Ya --> G[Edit Item, Tambah Diskon/Pajak, Ubah Catatan];
    F -- Tidak --> H[Konfirmasi Detail Tagihan];
    G --> H;
    H --> I[Pilih 'Generate PDF'];
    I --> J[Sistem Membuat File PDF Tagihan];
    J --> K[Unduh PDF];
    K --> L[Update Status Tagihan menjadi 'Terkirim'];
    L --> M{Klien Bayar?};
    M -- Nanti --> N[Pantau di Dashboard];
    M -- Sudah --> O[Update Status Tagihan menjadi 'Lunas'];
    O --> Z[Selesai];

5. Arsitektur & Tumpukan Teknologi (Rekomendasi)
Backend: ASP.NET Core Web API (.NET 8+)

Gunakan arsitektur yang bersih, misalnya memisahkan proyek ke dalam layer Domain, Application, dan Infrastructure.

Database: PostgreSQL

ORM: Entity Framework Core untuk memetakan kelas C# ke tabel database.

Frontend: Blazor Server

Sangat ideal untuk aplikasi internal/dashboard seperti ini. Kamu bisa menulis UI dengan C# sehingga pengembangan menjadi lebih cepat.

Library PDF: QuestPDF

Library modern, gratis, dan open-source untuk membuat PDF menggunakan kode C#. Sangat intuitif.

Autentikasi: ASP.NET Core Identity

Sudah terintegrasi untuk menangani login, registrasi, dan manajemen pengguna.

Deployment (di VPS): Docker & Nginx

Bungkus aplikasi Blazor Server dan database PostgreSQL dalam satu file docker-compose.yml.

Gunakan Nginx sebagai reverse proxy untuk menangani domain dan sertifikat SSL (HTTPS) dari Let's Encrypt.

6. Rencana Pengembangan Bertahap (Development Roadmap)
Jangan buat semuanya sekaligus! Ikuti fase ini agar proyek tetap berjalan dan termotivasi.

Fase 1: MVP (Minimum Viable Product) - Fondasi Utama

Tujuan: Aplikasi bisa digunakan secara fungsional untuk mencatat proyek dan tugas.

Fitur:

CRUD penuh untuk Klien.

CRUD penuh untuk Proyek.

CRUD penuh untuk Tugas di dalam proyek.

Ubah status Proyek & Tugas secara manual.

(Tanpa login dulu, fokus ke fungsionalitas inti).

Fase 2: Versi 1.1 - Fitur Inti (Billing)

Tujuan: Pengguna bisa membuat tagihan.

Fitur:

Implementasi modul Manajemen Tagihan.

Generate PDF menggunakan QuestPDF.

Buat Dashboard sederhana yang menampilkan 2-3 metrik utama.

Fase 3: Versi 1.2 - Peningkatan Kualitas & Keamanan

Tujuan: Membuat aplikasi lebih aman dan nyaman digunakan.

Fitur:

Implementasi Login & Registrasi menggunakan ASP.NET Core Identity.

Filter dan pencarian pada daftar klien, proyek, dan tagihan.

Perbaikan UI/UX berdasarkan pemakaian.