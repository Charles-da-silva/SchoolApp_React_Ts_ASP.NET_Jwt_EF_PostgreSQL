using System.ComponentModel.DataAnnotations;

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
    
        public string? Cpf { get; set; } = string.Empty;

        // Número da certidão de nascimento
        public string? BirthCertificateNumber { get; set; }

        // Email do aluno. 
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; } = string.Empty;
    }
}