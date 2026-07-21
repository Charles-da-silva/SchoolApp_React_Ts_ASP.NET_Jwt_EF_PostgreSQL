using System;

namespace School.Api.Domain.Entities
{
    public enum RelationshipType
    {
        Father,
        Mother,
        Grandfather,
        Grandmother,
        Brother,
        Sister,
        Guardian,
        Other
    }

    public class StudentResponsible
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public Student? Student { get; set; }

        public Guid ResponsibleId { get; set; }
        public Responsible? Responsible { get; set; }

        public RelationshipType RelationshipType { get; set; }
        public bool CanPickUpChild { get; set; }
        public bool IsFinanceContact { get; set; }
        public bool IsLegalResponsable { get; set; }
        public string? Observation { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}