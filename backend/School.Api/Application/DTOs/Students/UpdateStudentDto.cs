using System.ComponentModel.DataAnnotations;

namespace School.Api.Application.DTOs.Students
{
    // DTO usado para atualizar um aluno.
    // DTO (Data Transfer Object) é um padrão para transportar dados entre camadas.
    // É como um formulário controlado, definindo o que o frontend pode enviar para atualizar um aluno.
    public class UpdateStudentDto
    {
        // Nome completo do aluno. Obrigatório.
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(5, ErrorMessage = "O nome deve ter pelo menos 5 caracteres.")]
        public string FullName { get; set; } = string.Empty;

        // Email do aluno. Obrigatório.
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; } = string.Empty;

        // Data de nascimento do aluno. Obrigatório.
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DateOfBirth { get; set; } // Valor padrão para evitar nulls
    }
}