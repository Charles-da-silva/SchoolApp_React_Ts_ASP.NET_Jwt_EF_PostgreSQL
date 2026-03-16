
using School.Api.Application.Common;
using School.Api.Application.DTOs.Students;

namespace School.Api.Application.Services.Interfaces
{
    public interface IStudentAnamnesisService
    {
        Task<Result<StudentAnamnesisResponseDto>> GetByStudentIdAsync(
            Guid studentId);

        Task<Result<StudentAnamnesisResponseDto>> CreateAsync(
            Guid studentId,
            StudentAnamnesisCreateDto dto);

        Task<Result<StudentAnamnesisResponseDto>> UpdateAsync(
            Guid studentId,
            StudentAnamnesisUpdateDto dto);
    }
}