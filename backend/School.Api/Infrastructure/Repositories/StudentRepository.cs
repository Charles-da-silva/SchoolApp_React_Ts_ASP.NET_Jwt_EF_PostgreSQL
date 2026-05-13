
// esse repository será usado pelo EF Core

using Microsoft.EntityFrameworkCore;
using School.Api.Application.Interfaces.Repositories;
using School.Api.Domain.Entities;
using School.Api.Infrastructure.Data;

namespace School.Api.Infrastructure.Repositories;

public class StudentRepository: IStudentRepository
{
    /*
        private - Essa variável só pode ser usada dentro da classe. Boa prática: dependências devem 
        ser privadas.

        readonly - Significa que essa variável:
            Só pode ser atribuída no construtor
            Não pode ser alterada depois
            Isso evita bugs e mantém imutabilidade da dependência.
            Muito usado em injeção de dependência.

        SchoolDbContext - É o tipo da variável. Ou seja, _context é uma instância do seu DbContext. 
        Ele representa a conexão com o banco e as tabelas.

        _context - Nome da variável. Underline é convenção para campos privados.
    */
    private readonly SchoolDbContext _context;

    /// Injeção de dependência do DbContext - DEPENDENCY INJECTION (DI)

    /* O .NET cria automaticamente essa instância porque o SchoolDbContext foi registrado como 
    serviço no Program.cs. O framework cuida de criar e fornecer a instância correta quando o 
    StudentService for criado. Isso é o que chamamos de "Inversão de Controle" - a classe não 
    precisa se preocupar em criar suas dependências, elas são fornecidas pelo framework. Assim, 
    o código fica mais limpo, testável e desacoplado. */

    public StudentRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<Student?> GetByIdAsync (Guid id)
    {
        return await _context.Students
            .Include(s => s.Anamnesis)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student?> GetByDocumentAsync(string documentNumber)
    {
        return await _context.Students
        .FirstOrDefaultAsync(s => s.DocumentNumber == documentNumber);
    }

    public async Task<Student?> GetByEmailAsync(string email)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task AddAsync(Student student)
    {
        await _context.AddAsync(student);
    }

    public Task UpdateAsync (Student student)
    {
        _context.Students.Update(student);

        return Task.CompletedTask;
    }

    public Task DeleteAsync (Student student)
    {
        _context.Students.Remove(student);

        return Task.CompletedTask;
    }

    public async Task<bool> DocumentExistsAsync(string documentNumber,
        Guid studentId)
    {
        return await _context.Students.AnyAsync(
            s => s.DocumentNumber == documentNumber &&
                s.Id != studentId
        );
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();

        /* Retirei o SaveChanges de dentro dos métodos, criando um próprio
        para ele, assim todas as operações podem ser salvas juntas e 
        diminuimos o tamanho do código.
        Isso segue as melhores práticas de:

        - Repository Pattern;
        - Unit of Work;
        - Clean Architecture.
        */
    }
}