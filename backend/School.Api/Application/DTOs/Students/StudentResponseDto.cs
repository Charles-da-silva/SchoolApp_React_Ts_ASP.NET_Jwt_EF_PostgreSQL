namespace School.Api.Application.DTOs.Students
{
    /// DTO usado para RETORNAR dados do aluno para o frontend.
    /// Nunca expomos a entidade (entity Student.cs) diretamente.
    public class StudentResponseDto
    {
        /// Identificador único do aluno.
        public Guid Id { get; set; }
       
        public string FullName { get; set; }     

        public DateOnly DateOfBirth { get; set; }

        public string DocumentType { get; set; }

        public string DocumentNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; }


    }
}