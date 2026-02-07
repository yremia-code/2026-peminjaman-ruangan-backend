using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Peminjaman
{
    public int Id { get; set; }
    
    // Relasi ke User
    [Required]
    public int UserId { get; set; }
    public User? User { get; set; } 
    
    // Relasi ke Ruangan
    [Required]
    public int RuanganId { get; set; }
    public Ruangan? Ruangan { get; set; } 
    
    [Required]
    public DateTime TanggalPinjam { get; set; }

    [Required]
    public DateTime TanggalSelesai { get; set; }
    
    public string Status { get; set; } = "Pending";
}