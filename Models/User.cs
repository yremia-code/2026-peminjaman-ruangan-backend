using System.ComponentModel.DataAnnotations;

namespace Backend.Models;
public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nama wajib diisi.")]
    public string Nama { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Format email tidak valid.")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6, ErrorMessage = "Password minimal 6 karakter.")]
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "Mahasiswa"; 
}