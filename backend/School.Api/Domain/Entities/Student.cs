using Microsoft.EntityFrameworkCore;

namespace School.Api.Domain.Entities
{
    /*
        Uma Entity: 
            - Representa algo do mundo real (Aluno, Professor, Turma…)
            - Vira uma tabela no banco de dados
            - É usada pelo Entity Framework Core para persistência
    
        Neste caso aqui, esta Entity (Student.cs) representa um aluno da escola.
        Esta classe será mapeada para a tabela Students no banco de dados.
    */

    [Index(nameof(Email), IsUnique = true)]
    /* O que isso faz?
    Diz ao EF Core que Email deve ser único no banco.
    Quando gerar migration → cria UNIQUE INDEX no banco
    Agora o banco garante integridade de não existir dois Alunos com o mesmo email.
    Já havíamos inserido um controle de regra de negódio no StudentService.cs (metódo CreateAsync), 
    mas o índice no banco é uma camada adicional de segurança. */

    public class Student
    {
        /// Identificador único do aluno.
        /// Chave primária da tabela.
        /// "Guid Id" melhor que int em APIs modernas. Evita conflitos. Muito usado em sistemas distribuídos.
        public Guid Id { get; set; }
        
        /// Nome completo do aluno.
        public string FullName { get; set; } = string.Empty;

        /// Data de nascimento do aluno.
        public DateTime DateOfBirth { get; set; }

        /// Email do aluno (pode ser usado futuramente para login).
        public string Email { get; set; } = string.Empty;

        /// "IsActive { get; set; } = true" - Indica se o aluno está ativo no sistema.
        /*  Em vez de excluir o aluno do banco, podemos desativar (IsActive = false), fazendo que 
            não apareça nas listagens, mas ainda esteja no banco em caso de auditorias por exemplo. */
        public bool IsActive { get; set; } = true;

        /// Data de criação do registro.
        /// Boa prática para auditoria.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
