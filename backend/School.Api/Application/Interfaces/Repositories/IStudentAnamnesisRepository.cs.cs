

// Repository usado pelo EF

namespace School.Api.Application.Interfaces.Repositories;

public interface IStudentAnamnesisRepository
{
    Task<StudentAnamnesis?> GetByStudentIdAsync(Guid studentId);
    // Esse Get é usado para rastrear a entity no método Update da Service

    Task AddAsync(StudentAnamnesis anamnesis);

    Task UpdateAsync (StudentAnamnesis anamnesis);

    Task DeleteAsync (StudentAnamnesis anamnesis);

    Task SaveChangesAsync();    
}