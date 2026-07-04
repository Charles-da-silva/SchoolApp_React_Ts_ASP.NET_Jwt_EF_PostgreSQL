

// Repository específico para Dapper

using School.Api.Application.DTOs.Students;

namespace School.Api.Application.Interfaces.Repositories;

public interface IStudentReadRepository
{

    Task<IEnumerable<StudentResponseDto>> GetActivedStudentsAsync();
    Task<IEnumerable<StudentResponseDto>> GetAllAsync (StudentFilterDto filter);

    Task<StudentResponseDetailedDto?> GetDetailedByIdAsync(Guid id);
}