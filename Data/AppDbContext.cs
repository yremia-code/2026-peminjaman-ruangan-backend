using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Ruangan> Ruangans { get; set; }
    public DbSet<Peminjaman> Peminjamans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ruangan>().HasData(
            new Ruangan { Id = 1, Nama = "C.102", Gedung = "D4", Kapasitas = 30 },
            new Ruangan { Id = 2, Nama = "HH.201", Gedung = "D3", Kapasitas = 40 },
            new Ruangan { Id = 3, Nama = "PS.2.10", Gedung = "Pasca Sarjana", Kapasitas = 20 },
            new Ruangan { Id = 4, Nama = "SAW.10.11", Gedung = "SAW", Kapasitas = 15 }
        );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Nama = "Admin", Email = "admin@example.com", Password = "admin123", Role = "Admin" },
            new User { Id = 2, Nama = "Yere", Email = "yere@example.com", Password = "yere123", Role = "Mahasiswa" }
        );

        modelBuilder.Entity<Peminjaman>().HasData(
            new Peminjaman
            {
                Id = 1,
                UserId = 2,
                RuanganId = 1,
                TanggalPinjam = DateTime.Parse("2024-07-01"),
                TanggalSelesai =DateTime.Parse("2024-07-02"),
                Status = "Approved"
            }
        );
    }
}