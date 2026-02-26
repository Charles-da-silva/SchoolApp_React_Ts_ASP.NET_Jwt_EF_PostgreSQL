// Using para fazer a importação da entidade Student
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
        Task<Result<IEnumerable<StudentResponseDto>>> GetAllAsync();

        Task<Result<StudentResponseDto>> GetByIdAsync(Guid id);

        Task<Result<StudentResponseDto>> CreateAsync(CreateStudentDto dto);

        Task<Result<StudentResponseDto>> UpdateAsync(Guid id, UpdateStudentDto dto);

        Task<Result<bool>> DeactivateAsync(Guid id);

        Task<Result<StudentResponseDto>> ReactivateAsync(Guid id);

        Task<Result<bool>> DeleteAsync(Guid id);
    }
}