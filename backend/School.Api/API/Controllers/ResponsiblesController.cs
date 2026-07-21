using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Api.Infrastructure.Data;
using School.Api.Domain.Entities;
using School.Api.Application.DTOs;

namespace School.Api.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResponsiblesController : ControllerBase
    {
        private readonly SchoolDbContext _db;

        public ResponsiblesController(SchoolDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? query = null)
        {
            var q = _db.Responsibles.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                q = q.Where(r => r.FullName.Contains(query) || (r.Cpf != null && r.Cpf.Contains(query)));
            }
            var list = await q.OrderBy(r => r.FullName).Take(50).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var r = await _db.Responsibles.FindAsync(id);
            if (r == null) return NotFound();
            return Ok(r);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResponsibleCreateDto dto)
        {
            // ModelState validation is automatic because of [ApiController]
            var r = new Responsible
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Cpf = dto.Cpf,
                Email = dto.Email,
                Phone = dto.Phone,
                Profession = dto.Profession,
                Address = dto.Address,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _db.Responsibles.Add(r);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ResponsibleUpdateDto dto)
        {
            var r = await _db.Responsibles.FindAsync(id);
            if (r == null) return NotFound();

            r.FullName = dto.FullName;
            r.Cpf = dto.Cpf;
            r.Email = dto.Email;
            r.Phone = dto.Phone;
            r.Profession = dto.Profession;
            r.Address = dto.Address;
            r.IsActive = dto.IsActive;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var r = await _db.Responsibles.FindAsync(id);
            if (r == null) return NotFound();

            _db.Responsibles.Remove(r);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}