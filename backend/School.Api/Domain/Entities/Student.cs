
using School.Api.Domain.Enums;

namespace School.Api.Domain.Entities
{
    /*
        Uma Entity: 
            - Representa algo do mundo real (Aluno, Professor, Turma…)
            - Vira uma tabela no banco de dados
            - É usada pelo Entity Framework Core para persistência
            - Contém apenas regras estruturais do domínio
    
        Neste caso aqui, esta Entity (Student.cs) representa um aluno da escola.
        Esta classe será mapeada para a tabela Students no banco de dados.
    */

    public class Student
    {
        // Identificador único do aluno.
        // Chave primária da tabela.
        // "Guid Id" melhor que int em APIs modernas. Evita conflitos. Muito usado em sistemas distribuídos.
        public Guid Id { get; set; }
        
        /// Nome completo do aluno.
        public string FullName { get; set; } = string.Empty;

        /// Data de nascimento do aluno.
        public DateOnly DateOfBirth { get; set; }

        /// Tipo do documento (CPF ou Certidão)
        public DocumentType DocumentType { get; set; }

        /// Número do documento (CPF ou Certidão)
        public string DocumentNumber { get; set; } = string.Empty;

        /// Ficha médica/anamnese (vem da entidade StudentAnamnesis.cs)
        public StudentAnamnesis? Anamnesis { get; set; }

        /// "IsActive { get; set; } = true" - Indica se o aluno está ativo no sistema.
        /*  Em vez de excluir o aluno do banco, podemos desativar (IsActive = false), fazendo que 
            não apareça nas listagens, mas ainda esteja no banco em caso de auditorias por exemplo. */
        public bool IsActive { get; set; } = true;

        /// Data de criação do registro.
        /// Boa prática para auditoria.
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        /// Data de desativação do registro.
        public DateOnly? DeactivatedAt { get; set; }
    }
}
