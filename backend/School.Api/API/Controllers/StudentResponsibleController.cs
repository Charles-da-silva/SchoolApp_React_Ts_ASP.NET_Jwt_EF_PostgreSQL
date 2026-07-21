using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Api.Infrastructure.Data;
using School.Api.Domain.Entities;
using School.Api.Application.DTOs;

namespace School.Api.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentResponsibleController : ControllerBase
    {
        private readonly SchoolDbContext _db;

        public StudentResponsibleController(SchoolDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid? studentId, [FromQuery] Guid? responsibleId)
        {
            var q = _db.StudentResponsibles
                       .Include(sr => sr.Responsible)
                       .Include(sr => sr.Student)
                       .AsQueryable();

            if (studentId.HasValue) q = q.Where(x => x.StudentId == studentId.Value);
            if (responsibleId.HasValue) q = q.Where(x => x.ResponsibleId == responsibleId.Value);

            var list = await q.OrderByDescending(x => x.CreatedAt).ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentResponsibleCreateDto dto)
        {
            // validate
            var student = await _db.Students.FindAsync(dto.StudentId);
            if (student == null) return BadRequest("Student not found");

            var responsible = await _db.Responsibles.FindAsync(dto.ResponsibleId);
            if (responsible == null) return BadRequest("Responsible not found");

            var exists = await _db.StudentResponsibles.AnyAsync(x => x.StudentId == dto.StudentId && x.ResponsibleId == dto.ResponsibleId);
            if (exists) return Conflict("Vínculo já existe");

            var sr = new StudentResponsible
            {
                Id = Guid.NewGuid(),
                StudentId = dto.StudentId,
                ResponsibleId = dto.ResponsibleId,
                RelationshipType = dto.RelationshipType,
                CanPickUpChild = dto.CanPickUpChild,
                IsFinanceContact = dto.IsFinanceContact,
                IsLegalResponsable = dto.IsLegalResponsable,
                Observation = dto.Observation
            };

            _db.StudentResponsibles.Add(sr);
            await _db.SaveChangesAsync();

            var result = await _db.StudentResponsibles.Include(x => x.Responsible).Include(x => x.Student).FirstOrDefaultAsync(x => x.Id == sr.Id);
            return CreatedAtAction(nameof(Get), new { studentId = sr.StudentId }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] StudentResponsibleUpdateDto dto)
        {
            var sr = await _db.StudentResponsibles.FindAsync(id);
            if (sr == null) return NotFound();

            sr.RelationshipType = dto.RelationshipType;
            sr.CanPickUpChild = dto.CanPickUpChild;
            sr.IsFinanceContact = dto.IsFinanceContact;
            sr.IsLegalResponsable = dto.IsLegalResponsable;
            sr.Observation = dto.Observation;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sr = await _db.StudentResponsibles.FindAsync(id);
            if (sr == null) return NotFound();

            _db.StudentResponsibles.Remove(sr);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}