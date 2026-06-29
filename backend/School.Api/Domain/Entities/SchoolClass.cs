namespace School.Api.Domain.Entities
{
  public class SchoolClass
  {
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Year { get; set; }

    public Guid EmployeeId { get; set; }

    public string Room { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public string? Observations { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = [];
  }
}
