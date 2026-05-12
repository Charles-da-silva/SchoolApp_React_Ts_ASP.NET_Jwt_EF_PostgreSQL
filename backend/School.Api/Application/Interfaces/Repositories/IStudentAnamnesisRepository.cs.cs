

// Repository usado pelo EF

namespace School.Api.Application.Interfaces.Repositories;

public interface IStudentAnamnesisRepository
{
    Task<StudentAnamnesis?> GetByStudentIdAsync (Guid studentId);

    Task AddAsync(StudentAnamnesis anamnesis);

    Task UpdateAsync (StudentAnamnesis anamnesis);

    Task DeleteAsync (StudentAnamnesis anamnesis);

    Task SaveChangesAsync();
    
}