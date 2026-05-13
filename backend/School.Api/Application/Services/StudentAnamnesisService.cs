/*   Regra profissional (IMPORTANTE)
-------------------------------------------
    ==> Controller agrupa por RECURSO DA API
    ==> Service separa por RESPONSABILIDADE DE NEGÓCIO

Isso explica porque temos uma única controller StudentController que 
agrega endpoints de Student e StudentAnamnesis, mas temos duas Services,
uma para Student e outra para StudentAnamnesis.
*/

// Using para fazer a importação da interface IStudentService, que define o contrato do serviço de alunos.
using School.Api.Application.Interfaces.Services;

using School.Api.Application.DTOs.StudentsAnamnesis;

// Importante Exception personalizada para regras de negócio
using School.Api.Application.Common;

using School.Api.Application.Exceptions;
using School.Api.Application.Interfaces.Repositories;


namespace School.Api.Application.Services
{
    public class StudentAnamnesisService : IStudentAnamnesisService
    {
        private readonly IStudentAnamnesisReadRepository _studentAnamnesisReadRepository;
        private readonly IStudentAnamnesisRepository _studentAnamnesisRepository;

        private readonly IStudentRepository _studentRepository;

        public StudentAnamnesisService(
            IStudentAnamnesisReadRepository studentAnamnesisReadRepository,
            IStudentAnamnesisRepository studentAnamnesisRepository,
            IStudentRepository studentRepository)
        {
            _studentAnamnesisReadRepository = studentAnamnesisReadRepository;
            _studentAnamnesisRepository = studentAnamnesisRepository;
            _studentRepository = studentRepository;
        }



        public async Task<Result<StudentAnamnesisResponseDto>> GetByStudentIdAsync(
            Guid studentId)
        {
            var anamnesis = await _studentAnamnesisReadRepository.GetByStudentIdAsync(studentId);

            if (anamnesis == null)
            {
                return Result<StudentAnamnesisResponseDto>
                    .Fail("Ficha anamnese não encontrada.", ErrorType.NotFound);
            }

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
            var student = await _studentRepository.GetByIdAsync(studentId);

            if (student == null)
            {
                return Result<StudentAnamnesisResponseDto>
                    .Fail("O ID do aluno não foi encontrado.", ErrorType.NotFound);
            }

            var existingAnamneses = await _studentAnamnesisReadRepository.GetByStudentIdAsync(studentId);
            
            if (existingAnamneses != null)
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

            await _studentAnamnesisRepository.AddAsync(newAnamnesis);

            await _studentAnamnesisRepository.SaveChangesAsync();

            var response = new StudentAnamnesisResponseDto
            {
                Id = newAnamnesis.Id,
                StudentId = newAnamnesis.StudentId,
                Content = newAnamnesis.Content
            };

            return Result<StudentAnamnesisResponseDto>.Ok(response);
        }

        public async Task<Result<StudentAnamnesisResponseDto>> UpdateAsync(
            Guid studentId,
            StudentAnamnesisUpdateDto dto)
        {

            var student = await _studentRepository.GetByIdAsync(studentId);

            if (student == null)
                return Result<StudentAnamnesisResponseDto>
                    .Fail("O ID do aluno não foi encontrado.", ErrorType.NotFound);

            var anamnesis = await _studentAnamnesisRepository.GetByStudentIdAsync(studentId);

            if (anamnesis == null)
                throw new BusinessException("Ficha de anamnese não encontrada.");

            anamnesis.Content = dto.Content;
            anamnesis.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

            await _studentAnamnesisRepository.UpdateAsync(anamnesis);
            await _studentAnamnesisRepository.SaveChangesAsync();

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