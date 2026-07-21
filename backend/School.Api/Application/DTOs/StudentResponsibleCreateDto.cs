using System.ComponentModel.DataAnnotations;
using School.Api.Domain.Entities;

namespace School.Api.Application.DTOs
{
    public class StudentResponsibleCreateDto
    {
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid ResponsibleId { get; set; }

        [Required]
        public RelationshipType RelationshipType { get; set; }

        public bool CanPickUpChild { get; set; }
        public bool IsFinanceContact { get; set; }
        public bool IsLegalResponsable { get; set; }

        [MaxLength(1000)]
        public string? Observation { get; set; }
    }
}