# Sistem Peminjaman Ruangan PENS (Backend)

RESTful API untuk sistem peminjaman ruangan, dibangun menggunakan **ASP.NET 10**.

## Fitur Utama
- **Authentication:** JWT Login (Admin & User)
- **Authorization:** Role-based access (Admin & User).
- **Smart Validation:** Mencegah peminjaman ganda di waktu yang sama.
- **Soft Delete & Restore:** Keamanan data user agar tidak hilang permanen.

## Tech Stack
- ASP.NET 10
- **SQLite** (Database)

## Cara Menjalankan

1.  Pastikan .NET SDK sudah terinstall
2.  Clone repository ini.
3.  Setup konfigurasi:
    - Copy file `.env.example` menjadi `.env` (atau atur di User Secrets).
    - Pastikan `Jwt__Key` diisi.
4.  Jalankan migrasi database (File `.db` akan otomatis dibuat):
    ```bash
    dotnet ef database update
    ```
4.  Jalankan server:
    ```bash
    dotnet run
    ```
5.  Pengetesan dapat dilakukan melalui POSTMAN