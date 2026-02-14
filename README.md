# Sistem Peminjaman Ruangan PENS (Backend)

RESTful API untuk sistem peminjaman ruangan, dibangun menggunakan **ASP.NET Core 8**.

## Fitur Utama
- **Authentication:** JWT Bearer Token.
- **Authorization:** Role-based access (Admin & User).
- **Smart Validation:** Mencegah peminjaman ganda di waktu yang sama.
- **Soft Delete & Restore:** Keamanan data user agar tidak hilang permanen.

## Tech Stack
- ASP.NET Core 8 Web API
- Entity Framework Core
- **SQLite** (Database)

## Cara Menjalankan

1.  Clone repository ini.
2.  Setup konfigurasi:
    - Copy file `.env.example` menjadi `.env` (atau atur di User Secrets).
    - Pastikan `Jwt__Key` diisi.
3.  Jalankan migrasi database (File `.db` akan otomatis dibuat):
    ```bash
    dotnet ef database update
    ```
4.  Jalankan server:
    ```bash
    dotnet run
    ```
5.  Pengetesan dapat dilakukan melalui POSTMAN