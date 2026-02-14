using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Data;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeminjamanController : ControllerBase
{
    private readonly AppDbContext _context;

    public PeminjamanController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Peminjaman>>> GetPeminjamans(
        [FromQuery] string? status,
        [FromQuery] int? ruanganId)
    {
        var query = _context.Peminjamans
            .Include(p => p.User)
            .Include(p => p.Ruangan)
            .AsQueryable();
        
        if (!string.IsNullOrEmpty(status))
            query = query.Where(p => p.Status == status);

        if (ruanganId.HasValue)
            query = query.Where(p => p.RuanganId == ruanganId.Value);

        return await query.OrderByDescending(p => p.TanggalPinjam).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Peminjaman>> GetPeminjaman(int id)
    {
        var peminjaman = await _context.Peminjamans
            .Include(p => p.User)
            .Include(p => p.Ruangan)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (peminjaman == null)
        {
            return NotFound();
        }

        return peminjaman;
    }

    [HttpPost]
    public async Task<ActionResult<Peminjaman>> PostPeminjaman(Peminjaman peminjaman)
    {
        var isConflict = await _context.Peminjamans
            .AnyAsync(p =>
                p.RuanganId == peminjaman.RuanganId &&
                p.Status != "Rejected" &&
                p.Status != "Canceled" &&
                peminjaman.TanggalPinjam < p.TanggalSelesai &&
                peminjaman.TanggalSelesai > p.TanggalPinjam);
        
        if (isConflict)
        {
            return Conflict(new {message = "Ruangan sudah dipinjam pada rentang waktu tersebut"});
        }

        peminjaman.Status = "Pending";
        _context.Peminjamans.Add(peminjaman);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPeminjaman), new { id = peminjaman.Id }, peminjaman);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPeminjaman(int id, Peminjaman peminjaman)
    {
        if (id != peminjaman.Id)
        {
            return BadRequest(new {message = "ID tidak sesuai"});
        }

        var isConflict = await _context.Peminjamans
            .AnyAsync(p =>
                p.Id != id &&
                p.RuanganId == peminjaman.RuanganId &&
                p.Status != "Rejected" &&
                p.Status != "Canceled" &&
                peminjaman.TanggalPinjam < p.TanggalSelesai &&
                peminjaman.TanggalSelesai > p.TanggalPinjam);

        if (isConflict)
        {
            return Conflict(new {message = "Ruangan sudah dipinjam pada rentang waktu tersebut"});
        }

        peminjaman.Status = "Pending";
        _context.Entry(peminjaman).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
            if (!_context.Peminjamans.Any(e => e.Id == id)) return NotFound();
            throw;
        }

        return Ok(new {message = "Data peminjaman berhasil diperbarui dan status kembali Pending"});
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
    {
        var peminjaman = await _context.Peminjamans.FindAsync(id);
        if (peminjaman == null) return NotFound();

        var validStatus = new[] {"Pending", "Approved", "Rejected", "Canceled"};
        if (!validStatus.Contains(newStatus)) return BadRequest("Status tidak valid");

        peminjaman.Status = newStatus;
        await _context.SaveChangesAsync();

        return Ok(new {message = "Status peminjaman berhasil diperbarui"});
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePeminjaman(int id)
    {
        var peminjaman = await _context.Peminjamans.FindAsync(id);
        if (peminjaman == null) return NotFound();

        _context.Peminjamans.Remove(peminjaman);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}