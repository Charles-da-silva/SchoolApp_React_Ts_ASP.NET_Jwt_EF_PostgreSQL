using School.Api.Domain.Enums;

namespace School.Api.Domain.Entities
{
  public class Enrollment
  {
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }

    public Student Student { get; set; } = null!;

    public Guid ClassId { get; set; }

    public SchoolClass Class { get; set; } = null!;

    public EnrollmentShift Shift { get; set; }

    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;

    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public DateOnly? DeactivatedAt { get; set; }

    public string? FinalReport { get; set; }

    public ICollection<PendingAction> PendingActions { get; set; } = [];
  }
}
