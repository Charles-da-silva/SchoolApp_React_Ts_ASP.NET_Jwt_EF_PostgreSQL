namespace School.Api.Application.DTOs.Students
{
    // DTO usado para criar a ficha médica/anamnese de um aluno.
    // DTO (Data Transfer Object) é um padrão para transportar dados entre camadas.
    // É como um formulário controlado, definindo o que o frontend pode enviar para criar a ficha médica de um aluno.
    public class StudentAnamnesisResponseDto
    {
        public Guid Id { get; set; } 
        
        public Guid StudentId { get; set; } 
     
        public string? Content { get; set; } 

        public DateOnly CreatedAt { get; set; }

        public DateOnly? UpdatedAt { get; set; }
    }
}