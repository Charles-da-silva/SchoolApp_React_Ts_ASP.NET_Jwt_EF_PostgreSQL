namespace School.Api.Application.DTOs.Students
{
    /// DTO usado para CRIAR um aluno.
    /// DTO = Data Transfer Object. É um padrão para transportar dados entre camadas.
    /// É como um formulário controlado, definindo o que o frontend pode enviar para criar um aluno.
    public class CreateStudentDto
    {
        /// Nome completo do aluno. Obrigatório.
        public string FullName { get; set; } = string.Empty;

        /// Email do aluno. Obrigatório.
        public string Email { get; set; } = string.Empty;

        /// Data de nascimento do aluno. Obrigatório.
        public DateTime DateOfBirth { get; set; } // Valor padrão para evitar nulls
    }
}