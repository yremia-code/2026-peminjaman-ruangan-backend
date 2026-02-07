using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Data;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]

public class RuanganController : ControllerBase
{
    private readonly AppDbContext _context;

    public RuanganController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ruangan>>> GetRuangans()
    {
        return await _context.Ruangans
            .Where(r => !r.IsDeleted)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ruangan>> GetRuangan(int id)
    {
        var ruangan = await _context.Ruangans.FindAsync(id);

        if (ruangan == null || ruangan.IsDeleted)
        {
            return NotFound(new {message = "Ruangan tidak ditemukan"});
        }

        return ruangan;
    }

    [HttpPost]
    public async Task<ActionResult<Ruangan>> PostRuangan(Ruangan ruangan)
    {
        _context.Ruangans.Add(ruangan);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRuangan), new { id = ruangan.Id }, ruangan);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRuangan(int id, Ruangan ruangan)
    {
        if (id != ruangan.Id)
        {
            return BadRequest(new {message = "ID tidak sesuai"});
        }

        _context.Entry(ruangan).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
            if (!_context.Ruangans.Any(e => e.Id == id)) return NotFound();
            throw;
        }

        return Ok(new {message = "Data ruangan berhasil diperbarui"});
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRuangan(int id)
    {
        var ruangan = await _context.Ruangans.FindAsync(id);

        if (ruangan == null) return NotFound();

        ruangan.IsDeleted = true;

        await _context.SaveChangesAsync();

        return Ok(new {message = "Data ruangan berhasil dihapus"});
    }
}