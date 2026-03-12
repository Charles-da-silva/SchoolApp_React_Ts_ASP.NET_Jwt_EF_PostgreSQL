using System.ComponentModel.DataAnnotations;
using School.Api.Domain.Enums;

namespace School.Api.Application.DTOs.Students
{
    // DTO usado para criar um aluno.
    // DTO (Data Transfer Object) é um padrão para transportar dados entre camadas.
    // É como um formulário controlado, definindo o que o frontend pode enviar para criar um aluno.
    public class CreateStudentDto
    {
        // Nome completo do aluno. Obrigatório.
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(5, ErrorMessage = "O nome deve ter pelo menos 5 caracteres.")]
        public string FullName { get; set; } = string.Empty;

        // Data de nascimento do aluno. Obrigatório.
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateOnly DateOfBirth { get; set; } // Valor padrão para evitar nulls

        [Required(ErrorMessage = "O tipo de documento é obrigatório.")]
        public DocumentType DocumentType { get; set; }

        [Required(ErrorMessage = "O número do documento é obrigatório.")]
        public string DocumentNumber { get; set; } = string.Empty;
    }
}