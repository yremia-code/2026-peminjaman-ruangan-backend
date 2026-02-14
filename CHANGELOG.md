# Changelog

Semua perubahan penting pada proyek ini akan didokumentasikan dalam file ini.

## [1.0.0] - 2026-02-14

### Added
- **Core API:** CRUD untuk User, Ruangan, dan Peminjaman.
- **Auth:** Implementasi Login dengan JWT Token.
- **Soft Delete:** Fitur hapus sementara untuk User dan Ruangan.
- **Restore Account:** Admin dapat mengaktifkan kembali user yang sudah dihapus.
- **Validation Logic:** Validasi bentrok jadwal ruangan (Conflict Checking).
- **Booking Status:** Support status Pending, Approved, Rejected, dan Canceled.

### Fixed
- **Conflict Check Bug:** Memastikan peminjaman yang berstatus 'Canceled' atau 'Rejected' tidak memblokir jadwal ruangan.