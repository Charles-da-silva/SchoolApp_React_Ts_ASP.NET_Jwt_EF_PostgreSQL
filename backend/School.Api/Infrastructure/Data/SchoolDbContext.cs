// Namespace do Entity Framework Core.
// Contém as classes necessárias para trabalhar com DbContext, DbSet, migrations, etc.
using Microsoft.EntityFrameworkCore;

// Importa a entidade Student (domínio da aplicação)
using School.Api.Domain.Entities;

// Namespace que indica que esta classe faz parte da camada de infraestrutura,
// mais especificamente da parte de acesso a dados (Data)
namespace School.Api.Infrastructure.Data
{
    // SchoolDbContext herda de DbContext.
    // DbContext é a classe central do Entity Framework Core.
    // Ela representa a conexão da aplicação com o banco de dados.
    public class SchoolDbContext : DbContext
    {
        // Construtor do DbContext.
        // O ASP.NET injeta automaticamente as configurações do banco
        // (string de conexão, provider MySQL/PostgreSQL, etc.)
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
            // Aqui normalmente não colocamos lógica.
            // Apenas repassamos as opções para a classe base (DbContext).
        }

        /*  No comando DbSet estamos informando que existe uma tabela chamada Students
            no banco de dados e que cada registro dessa tabela deve ser mapeado para um
            objeto do tipo Student.
            Resumo:
                - Cada registro da tabela (linha) vira um objeto Student
                - Cada propriedade do Student vira uma coluna da tabela
        */
        public DbSet<Student> Students { get; set; }

        public DbSet<StudentAnamnesis> StudentAnamneses { get; set; }

        // Aqui é onde configuramos as entidades via Fluent API, sobrescrevendo o método OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* Procura e aplica automaticamente todas as classes de configuração que 
            implementam IEntityTypeConfiguration.
            Ex.: ../Infrastructure/Configuration/StudentConfiguration.cs, TeacherConfiguration.cs, etc. */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext).Assembly);
        }
    }
}
