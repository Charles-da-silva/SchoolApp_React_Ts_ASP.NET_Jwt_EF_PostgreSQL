
using School.Api.Application.DTOs.Students;

public class StudentResponseDetailedDto
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string DocumentType { get; set; } = string.Empty;

    public string DocumentNumber { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public bool IsActive { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? DeactivatedAt { get; set; }

    public StudentAnamnesisResponseDto? Anamnesis { get; set; }
}