

// Repository usado pelo Dapper

using School.Api.Application.DTOs.StudentsAnamnesis;

namespace School.Api.Application.Interfaces.Repositories;

public interface IStudentAnamnesisReadRepository
{
    Task<StudentAnamnesisResponseDto?> GetByStudentIdAsync (Guid studentId);
    
}