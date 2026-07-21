using System.ComponentModel.DataAnnotations;

namespace School.Api.Application.DTOs
{
    public class ResponsibleCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(30)]
        public string? Cpf { get; set; }

        [EmailAddress]
        [MaxLength(200)]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(150)]
        public string? Profession { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }
    }
}