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
        Task<IEnumerable<StudentResponseDto>> GetAllAsync();

        Task<StudentResponseDto?> GetByIdAsync(Guid id);

        Task<Result<StudentResponseDto>> CreateAsync(CreateStudentDto dto);

        Task<bool> UpdateAsync(Guid id, UpdateStudentDto dto);

        Task<bool> DeleteAsync(Guid id);
    }
}