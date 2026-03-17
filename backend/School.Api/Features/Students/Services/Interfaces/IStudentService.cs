
using School.Api.Application.Common;
using School.Api.Application.DTOs.Students;

namespace School.Api.Application.Services.Interfaces
{
    /// Interface responsável por definir o contrato do serviço de alunos.
    
    /// POR QUE usar interface?
    /// - Permite Injeção de Dependência
    /// - Permite Testes Unitários com Mock
    /// - Desacopla Controller da implementação concreta
    /// - Segue princípio DIP (Dependency Inversion Principle - SOLID)
    public interface IStudentService
    {
        Task<Result<IEnumerable<StudentResponseDto>>> GetAllAsync(StudentFilterDto filter);

        Task<Result<StudentResponseDetailedDto>> GetByIdAsync(Guid id);

        Task<Result<StudentResponseDto>> CreateAsync(StudentCreateDto dto);

        Task<Result<StudentResponseDto>> UpdateAsync(Guid id, StudentUpdateDto dto);

        Task<Result> DeactivateAsync(Guid id);

        Task<Result> ReactivateAsync(Guid id);

        Task<Result> DeleteAsync(Guid id);
    }
}