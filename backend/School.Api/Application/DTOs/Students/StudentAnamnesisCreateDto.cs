using System.ComponentModel.DataAnnotations;

namespace School.Api.Application.DTOs.Students
{
    // DTO usado para criar a ficha médica/anamnese de um aluno.
    // DTO (Data Transfer Object) é um padrão para transportar dados entre camadas.
    // É como um formulário controlado, definindo o que o frontend pode enviar para criar a ficha médica de um aluno.
    public class CreateStudentAnamnesisDto
    {
        // Id do aluno. Obrigatório.
        [Required(ErrorMessage = "O ID do aluno é obrigatório.")]
        public Guid StudentId { get; set; } = Guid.Empty;

        // Texto completo da ficha médica/anamnese. Obrigatório.
        [Required(ErrorMessage = "O conteúdo da ficha médica é obrigatório.")]
        public string Content { get; set; } = string.Empty;
    }
}