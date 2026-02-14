using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Ruangan
{   
    public int Id { get; set; }

    [Required(ErrorMessage = "Nama ruangan harus diisi.")]
    [StringLength(100, ErrorMessage = "Nama ruangan tidak boleh lebih dari 100 karakter.")]
    public string Nama { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lokasi gedung harus diisi.")]
    public string Gedung { get; set; } = string.Empty;

    [Range(1, 500, ErrorMessage = "Kapasitas harus antara 1 hingga 500.")]
    public int Kapasitas { get; set; }

    public bool IsDeleted { get; set; } = false;
}
