

// Repository usado pelo EF

using School.Api.Domain.Entities;

namespace School.Api.Application.Interfaces.Repositories;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync (Guid id);

    Task<Student?> GetByDocumentAsync(string documentNumber);

    Task<Student?> GetByEmailAsync(string email);

    Task AddAsync(Student student);

    Task UpdateAsync (Student student);

    Task DeleteAsync (Student student);

    Task<bool> DocumentExistsAsync(string documentNumber,
        Guid studentId
    );

    Task SaveChangesAsync();
    
}