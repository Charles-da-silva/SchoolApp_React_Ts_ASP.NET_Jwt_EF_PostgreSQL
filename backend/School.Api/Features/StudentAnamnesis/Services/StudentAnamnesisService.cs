/*   Regra profissional (IMPORTANTE)
-------------------------------------------
    ==> Controller agrupa por RECURSO DA API
    ==> Service separa por RESPONSABILIDADE DE NEGÓCIO

Isso explica porque temos uma única controller StudentController que 
agrega endpoints de Student e StudentAnamnesis, mas temos duas Services,
uma para Student e outra para StudentAnamnesis.
*/

// Using para fazer a importação da interface IStudentService, que define o contrato do serviço de alunos.
using School.Api.Application.Services.Interfaces;

// Using para trabalhar com EF Core (LINQ, DbContext, etc.)
using Microsoft.EntityFrameworkCore;

// Using para fazer a importação do DbContext da aplicação
using School.Api.Infrastructure.Data;

using School.Api.Application.DTOs.Students;

// Importante Exception personalizada para regras de negócio
using School.Api.Application.Common;

using School.Api.Application.Exceptions;


namespace School.Api.Application.Services
{
    public class StudentAnamnesisService : IStudentAnamnesisService
    {
        private readonly SchoolDbContext _context;

        /// Injeção de dependência do DbContext - DEPENDENCY INJECTION (DI)
       
        /* O .NET cria automaticamente essa instância porque o SchoolDbContext foi registrado como 
        serviço no Program.cs. O framework cuida de criar e fornecer a instância correta quando o 
        StudentService for criado. Isso é o que chamamos de "Inversão de Controle" - a classe não 
        precisa se preocupar em criar suas dependências, elas são fornecidas pelo framework. Assim, 
        o código fica mais limpo, testável e desacoplado. */
        public StudentAnamnesisService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Result<StudentAnamnesisResponseDto>> GetByStudentIdAsync(
            Guid studentId)
        {
            var anamnesis = await _context.StudentAnamneses
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.StudentId == studentId);

            if (anamnesis == null)
                return Result<StudentAnamnesisResponseDto>
                    .Fail("Ficha anamnese não encontrada.", ErrorType.NotFound);

            var response = new StudentAnamnesisResponseDto
            {
                Id = anamnesis.Id,
                StudentId = anamnesis.StudentId,
                Content = anamnesis.Content
            };

            return Result<StudentAnamnesisResponseDto>.Ok(response);
        }

        public async Task<Result<StudentAnamnesisResponseDto>> CreateAsync(
            Guid studentId,
            StudentAnamnesisCreateDto dto)
        {
            var student = await _context.Students
            .AnyAsync(s => s.Id == studentId);

            if (!await _context.Students.AnyAsync(s => s.Id == studentId))
                return Result<StudentAnamnesisResponseDto>
                    .Fail("Aluno não encontrado.", ErrorType.NotFound);

            var existingAnamneses = await _context.StudentAnamneses
                .AnyAsync(a => a.StudentId == studentId);

            if (existingAnamneses)
                return Result<StudentAnamnesisResponseDto>
                    .Fail(
                        "Este aluno já possui ficha de anamnese cadastrada.",
                        ErrorType.Conflict);

            var newAnamnesis = new StudentAnamnesis
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                Content = dto.Content,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            _context.StudentAnamneses.Add(newAnamnesis);

            await _context.SaveChangesAsync();

            var response = new StudentAnamnesisResponseDto
            {
                Id = newAnamnesis.Id,
                StudentId = newAnamnesis.StudentId,
                Content = newAnamnesis.Content
            };

            return Result<StudentAnamnesisResponseDto>.Ok(response);
        }

        public async Task<Result<StudentAnamnesisResponseDto>>UpdateAsync(
            Guid studentId,
            StudentAnamnesisUpdateDto dto)
        {
            var anamnesis = await _context.StudentAnamneses
            .FirstOrDefaultAsync(a => a.StudentId == studentId);

            if (anamnesis == null)
                throw new BusinessException("Ficha de anamnese não encontrada.");

            anamnesis.Content = dto.Content;
            anamnesis.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

            await _context.SaveChangesAsync();

            var response = new StudentAnamnesisResponseDto
            {
                Id = anamnesis.Id,
                StudentId = anamnesis.StudentId,
                Content = anamnesis.Content,
                UpdatedAt = anamnesis.UpdatedAt
            };

            return Result<StudentAnamnesisResponseDto>.Ok(response);
        }
    }
}