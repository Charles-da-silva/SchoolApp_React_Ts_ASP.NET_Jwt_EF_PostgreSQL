namespace School.Api.Application.DTOs.Students
{
    /// DTO usado para RETORNAR dados do aluno para o frontend.
    /// Nunca expomos a entidade (entity Student.cs) diretamente.
    public class StudentResponseDto
    {
        /// Identificador único do aluno.
        public Guid Id { get; set; }
       
        public string FullName { get; set; } = string.Empty;
       
        public string Email { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}