using School.Api.Domain.Entities;

public class StudentAnamnesis
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }
    
    public Student Student { get; set; } = null!;

    /// Texto completo da ficha
    public string Content { get; set; } = string.Empty;

    public DateOnly CreatedAt { get; set; }
        = DateOnly.FromDateTime(DateTime.UtcNow);

    public DateOnly? UpdatedAt { get; set; }
}