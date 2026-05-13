

// Repository usado pelo EF

using Microsoft.EntityFrameworkCore;
using School.Api.Application.Interfaces.Repositories;
using School.Api.Infrastructure.Data;

namespace School.Api.Infrastructure.Repositories;

public class StudentAnamnesisRepository : IStudentAnamnesisRepository
{
    /// Injeção de dependência do DbContext - DEPENDENCY INJECTION (DI)

    /* O .NET cria automaticamente essa instância porque o SchoolDbContext foi registrado como 
    serviço no Program.cs. O framework cuida de criar e fornecer a instância correta quando o 
    StudentService for criado. Isso é o que chamamos de "Inversão de Controle" - a classe não 
    precisa se preocupar em criar suas dependências, elas são fornecidas pelo framework. Assim, 
    o código fica mais limpo, testável e desacoplado. */
    private readonly SchoolDbContext _context;

    public StudentAnamnesisRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<StudentAnamnesis?> GetByStudentIdAsync(Guid studentId)
    {
        return await _context.StudentAnamneses
            .FirstOrDefaultAsync(a => a.StudentId == studentId);
    }

    public async Task AddAsync(StudentAnamnesis anamnesis)
    {
        await _context.AddAsync(anamnesis);
    }

    public Task UpdateAsync (StudentAnamnesis anamnesis)
    {
        _context.StudentAnamneses.Update(anamnesis);

        return Task.CompletedTask;
    }

    public Task DeleteAsync (StudentAnamnesis anamnesis)
    {
        _context.StudentAnamneses.Remove(anamnesis);

        return Task.CompletedTask;
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