using School.Api.Domain.Enums;

namespace School.Api.Domain.Entities
{
  public class PendingAction
  {
    public Guid Id { get; set; }

    public PendingActionType Type { get; set; }

    public PendingActionStatus Status { get; set; } = PendingActionStatus.Open;

    public string Description { get; set; } = string.Empty;

    public DateOnly DueDate { get; set; }

    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public Guid? StudentId { get; set; }

    public Student? Student { get; set; }

    public Guid? EnrollmentId { get; set; }

    public Enrollment? Enrollment { get; set; }

    public Guid? EmployeeId { get; set; }

    public Guid? FinancialTransactionId { get; set; }
  }
}
