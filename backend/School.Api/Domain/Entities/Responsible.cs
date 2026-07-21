using System;
using System.Collections.Generic;

namespace School.Api.Domain.Entities
{
    public class Responsible
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Profession { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeactivatedAt { get; set; }

        // Navigation
        public ICollection<StudentResponsible> StudentResponsibles { get; set; } = new List<StudentResponsible>();
    }
}