
// Using para fazer a importação da interface IStudentService, que define o contrato do serviço de alunos.
using School.Api.Application.Interfaces.Services;

// Using para fazer a importação das entidades, como a Student
using School.Api.Domain.Entities;

using School.Api.Application.DTOs.Students;

// Importante Exception personalizada para regras de negócio
using School.Api.Application.Common;
using School.Api.Domain.Enums;

using School.Api.Application.Interfaces.Repositories;

namespace School.Api.Application.Services
{
    /*
        Implementação do serviço de alunos.

        Aqui estamos criando a classe StudentService que implementa a interface IStudentService. 
        A classe fica obrigada a implementar todos os métodos definidos na interface, garantindo que o 
        contrato seja cumprido. 

        Aqui ficam as regras de negócio.
        O Controller NÃO deve acessar o DbContext diretamente.               
        Essa classe é a implementação concreta do contrato definido por IStudentService.
        Ao seguir a interface IStudentService, garantimos que o Controller possa depender apenas da 
        abstração (interface) e não da implementação concreta, promovendo um design mais flexível e 
        testável.
        Além disso, ao centralizar a lógica de negócios nesta classe, mantemos o Controller mais limpo e 
        focado apenas em lidar com as requisições HTTP e respostas, enquanto toda a lógica relacionada 
        aos alunos fica encapsulada ns Service.
        Essa abordagem segue os princípios de SOLID, especialmente o DIP (Dependency Inversion Principle),
        e facilita a manutenção e evolução do código ao longo do tempo.
        Essa classe Service aciona a classe Repository correspondente e a Repository é responsável manipular 
        o banco, utilizando o SchoolDbContext.
    */

    public class StudentService : IStudentService
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IStudentReadRepository _studentReadRepository;

        // Construtor:
        public StudentService(
            IStudentRepository studentRepository,
            IStudentReadRepository studentReadRepository)
        {
            _studentRepository = studentRepository;
            _studentReadRepository = studentReadRepository;
        }

        public async Task<Result<IEnumerable<StudentResponseDto>>> GetAllAsync(
            StudentFilterDto filter)
        {
            if (filter.MinAge.HasValue &&
                filter.MaxAge.HasValue &&
                filter.MinAge > filter.MaxAge)
            {
                return Result<IEnumerable<StudentResponseDto>>.Fail(
                    "MinAge não pode ser maior que MaxAge.",
                    ErrorType.Validation
                );
            }

            // Repository acessa banco

            var students = await _studentReadRepository.GetAllAsync(filter);

            return Result<IEnumerable<StudentResponseDto>>.Ok(students);
        }

        public async Task<Result<StudentResponseDetailedDto>> GetByIdAsync(Guid id)
        {
            var student =
                await _studentReadRepository.GetDetailedByIdAsync(id);

            if (student == null)
            {
                return Result<StudentResponseDetailedDto>.Fail(
                    "Aluno não encontrado.",
                    ErrorType.NotFound
                );
            }

            return Result<StudentResponseDetailedDto>.Ok(student);
        }

        public async Task<Result<StudentResponseDto>> CreateAsync(StudentCreateDto dto)
        {
            if (!Enum.IsDefined(typeof(DocumentType), dto.DocumentType) ||
                string.IsNullOrWhiteSpace(dto.DocumentNumber))
            {
                return Result<StudentResponseDto>.Fail(
                    "Tipo e número do documento são obrigatórios.",
                    ErrorType.Validation
                );
            }

            var existingStudent =
                await _studentRepository.GetByDocumentAsync(
                    dto.DocumentNumber
                );

            if (existingStudent != null)
            {
                var existingDto = new StudentResponseDto
                {
                    Id = existingStudent.Id,
                    FullName = existingStudent.FullName,
                    DocumentType = existingStudent.DocumentType.ToString(),
                    DocumentNumber = existingStudent.DocumentNumber,
                    DateOfBirth = existingStudent.DateOfBirth
                };

                return Result<StudentResponseDto>.Fail(
                    "Já existe um aluno com este documento.",
                    ErrorType.Conflict,
                    existingDto
                );
            }

            var student = new Student
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                DocumentType = dto.DocumentType,
                DocumentNumber = dto.DocumentNumber,
                DateOfBirth = dto.DateOfBirth,
                IsActive = true,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            await _studentRepository.AddAsync(student);

            await _studentRepository.SaveChangesAsync();

            var response = new StudentResponseDto
            {
                Id = student.Id,
                FullName = student.FullName,
                DocumentType = student.DocumentType.ToString(),
                DocumentNumber = student.DocumentNumber,
                DateOfBirth = student.DateOfBirth,
                IsActive = student.IsActive
            };

            return Result<StudentResponseDto>.Ok(response);
        }

        public async Task<Result<StudentResponseDto>> UpdateAsync(Guid id, StudentUpdateDto dto)
        {
            // Buscar aluno no banco filtrando por ID
            var student = await _studentRepository.GetByIdAsync(id);

            // Se não encontrar o aluno, retorna null
            if (student == null)
            {
                return Result<StudentResponseDto>.Fail("Aluno não existe.", ErrorType.NotFound);
            }

            if (student.IsActive == false)
            {
                return Result<StudentResponseDto>.Fail("Aluno está inativo. Reative para atualizar.", ErrorType.Conflict);
            }

            if (student.DocumentNumber != dto.DocumentNumber)
            {
                var documentExists =
                    await _studentRepository.DocumentExistsAsync(
                        dto.DocumentNumber,
                        id
                    );

                if (documentExists)
                {
                    return Result<StudentResponseDto>.Fail(
                        "Já existe outro aluno com este documento.",
                        ErrorType.Conflict
                    );
                }
            }

            // Se encontrou o aluno pelo ID e está ativo, a variável studente não será null, logo o método não executou o return acima, 
            // passando para o passo abaixo onde atualiza os campos do student, usando as informações recebidas no DTO de atualização (UpdateStudentDto).
            student.FullName = dto.FullName;
            student.DocumentType = dto.DocumentType;
            student.DocumentNumber = dto.DocumentNumber;
            student.DateOfBirth = dto.DateOfBirth;

            // Salva no banco e retorna true para indicar sucesso
            await _studentRepository.UpdateAsync(student);

            await _studentRepository.SaveChangesAsync();

            var response = new StudentResponseDto
            {
                Id = student.Id,
                FullName = student.FullName,
                DocumentType = student.DocumentType.ToString(),
                DocumentNumber = student.DocumentNumber,
                DateOfBirth = student.DateOfBirth,
                IsActive = student.IsActive
            };

            return Result<StudentResponseDto>.Ok(response);
        }

        public async Task<Result> DeactivateAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return Result.Fail("Aluno não encontrado.", ErrorType.NotFound);
            }

            if (!student.IsActive)
            {
                return Result.Fail("Aluno já está inativo.", ErrorType.Conflict);
            }

            // Soft Delete: Em vez de remover o registro, marcamos como inativo
            student.IsActive = false;
            student.DeactivatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

            await _studentRepository.SaveChangesAsync();

            return Result.Ok("Aluno desativado com sucesso.");
        }

        public async Task<Result> ReactivateAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
                return Result.Fail("Aluno não encontrado.", ErrorType.NotFound);

            if (student.IsActive)
                return Result.Fail("Aluno já está ativo.", ErrorType.Conflict);

            student.IsActive = true;
            student.DeactivatedAt = null;

            await _studentRepository.SaveChangesAsync();

            return Result.Ok("Aluno reativado com sucesso.");
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return Result.Fail(
                    "Aluno não encontrado.",
                    ErrorType.NotFound
                );
            }

            if (student.DeactivatedAt == null)
            {
                return Result.Fail(
                    "Aluno ainda está ativo. Antes de deletar, desative o aluno.",
                    ErrorType.Conflict
                );
            }

            if (student.DeactivatedAt > DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-5)))
            {
                return Result.Fail(
                    "Aluno foi desativado há menos de 5 anos. Aguarde o período de retenção.",
                    ErrorType.Conflict
                );
            }

            await _studentRepository.DeleteAsync(student);

            await _studentRepository.SaveChangesAsync();

            return Result.Ok("Aluno deletado com sucesso.");
        }
    }
}