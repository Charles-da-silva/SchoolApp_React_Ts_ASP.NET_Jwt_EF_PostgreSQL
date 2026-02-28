// Using para fazer a importação da interface IStudentService, que define o contrato do serviço de alunos.
using School.Api.Application.Services.Interfaces;

// Using para trabalhar com EF Core (LINQ, DbContext, etc.)
using Microsoft.EntityFrameworkCore;

// Using para fazer a importação do DbContext da aplicação
using School.Api.Infrastructure.Data;

// Using para fazer a importação da entidade Student
using School.Api.Domain.Entities;

using School.Api.Application.DTOs.Students;

// Importante Exception personalizada para regras de negócio
using School.Api.Application.Common;

namespace School.Api.Application.Services
{
    /*
        Implementação do serviço de alunos.
     
        Aqui ficam as regras de negócio.
        O Controller NÃO deve acessar o DbContext diretamente.
        </summary>
        Essa classe é a implementação concreta do contrato definido por IStudentService.
        Ela é responsável por realizar as operações de CRUD (Create, Read, Update, Delete) para os alunos, utilizando o SchoolDbContext para acessar o banco de dados.
        Ao seguir a interface IStudentService, garantimos que o Controller possa depender apenas da abstração (interface) e não da implementação concreta, promovendo um design mais flexível e testável.
        Além disso, ao centralizar a lógica de negócios nesta classe, mantemos o Controller mais limpo e focado apenas em lidar com as requisições HTTP e respostas, enquanto toda a lógica relacionada aos alunos fica encapsulada no serviço.
        Essa abordagem segue os princípios de SOLID, especialmente o DIP (Dependency Inversion Principle), e facilita a manutenção e evolução do código ao longo do tempo.
    */

    // Aqui estamos criando a classe StudentService que implementa a interface IStudentService. A classe fica obrigada a implementar todos os métodos definidos na interface, garantindo que o contrato seja cumprido. 
    public class StudentService : IStudentService
    {
        /*
            private - Essa variável só pode ser usada dentro da classe. Boa prática: dependências devem ser privadas.

            readonly - Significa que essa variável:
                Só pode ser atribuída no construtor
                Não pode ser alterada depois
                Isso evita bugs e mantém imutabilidade da dependência.
                Muito usado em injeção de dependência.

            SchoolDbContext - É o tipo da variável. Ou seja, _context é uma instância do seu DbContext. Ele representa a conexão com o banco e as tabelas.

            _context - Nome da variável. Underline é convenção para campos privados.
        */
        private readonly SchoolDbContext _context;

        /// Injeção de dependência do DbContext - DEPENDENCY INJECTION (DI)
       
        /// O .NET cria automaticamente essa instância porque o SchoolDbContext foi registrado como serviço no Program.cs. O framework cuida de criar e fornecer a instância correta quando o StudentService for criado. Isso é o que chamamos de "Inversão de Controle" - a classe não precisa se preocupar em criar suas dependências, elas são fornecidas pelo framework. Assim, o código fica mais limpo, testável e desacoplado.
        public StudentService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<StudentResponseDto>>> GetAllAsync(StudentFilterDto filter)
        {
            /* _context.Students
                significa:
                    - Acessar a tabela Students;
                    - Trabalhar com objetos C#
                    - Sem escrever SQL diretamente */
            var query = _context.Students.AsQueryable();

            if (filter.IsActive.HasValue)
                query = query.Where(s => s.IsActive == filter.IsActive.Value);

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(s => s.FullName.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(s => s.Email.Contains(filter.Email));

            if (filter.CreatedAfter.HasValue)
                query = query.Where(s => s.CreatedAt >= filter.CreatedAfter.Value);

            // Busca apenas alunos ativos (Soft Delete aplicado)
            var students = await query.ToListAsync();
                /* 
                    .ToListAsync() - Aqui o EF Core:
                        - Traduz LINQ → SQL
                        - Abre conexão
                        - Executa o SELECT
                        - Converte linhas → objetos Student
                        - Retorna uma lista
                    Por isso precisa do await.
                */

            // Select é como um .map - transforma cada entidade Student em um StudentResponseDto
            var response = students.Select(s => new StudentResponseDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                DateOfBirth = s.DateOfBirth,
                IsActive = s.IsActive
            });

            return Result<IEnumerable<StudentResponseDto>>.Ok(response);
        }

        public async Task<Result<StudentResponseDto>> GetByIdAsync(Guid id)
        {
            /*
                _context é o SchoolDbContext injetado no service. Representa a conexão com o banco.

                FirstOrDefaultAsync é um método do LINQ que retorna o primeiro elemento que satisfaz a condição ou null se não encontrar.
            
                s => s.Id == id é uma expressão lambda que compara o Id do aluno com o id passado na URL.
                “Pegue cada Student (apelidado de s) e verifique se o Id dele é igual ao id recebido na URL”
            */
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return Result<StudentResponseDto>.Fail("Aluno não encontrado.", ErrorType.NotFound);

            var responde = new StudentResponseDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                IsActive = student.IsActive
            };

            return Result<StudentResponseDto>.Ok(responde);
        }

        /// Cria um novo aluno.
        public async Task<Result<StudentResponseDto>> CreateAsync(CreateStudentDto dto)
        {   
            // Verifica se já existe aluno com mesmo email no banco. 
            // Se não existir o banco responde com null
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.Email == dto.Email);

            if (existingStudent != null)
            {
                // Se já existe um aluno com o mesmo email, criamos uma variável existingDto para retornar os dados do aluno
                var existingDto = new StudentResponseDto
                {
                    Id = existingStudent.Id,
                    FullName = existingStudent.FullName,
                    Email = existingStudent.Email,
                    DateOfBirth = existingStudent.DateOfBirth
                };

                // Invocamos o método Result<StudentResponseDto>.Fail para retornar um resultado de falha, 
                // passando a mensagem de erro, os dados do aluno existente (variável existingDto) e indicando se é possível reativar o aluno. 
                // Isso permite que o Controller trate essa situação de forma adequada, retornando um status HTTP 409 Conflict com informações úteis para o cliente.
                return Result<StudentResponseDto>.Fail(
                    "Já existe um aluno com este email.",
                    ErrorType.Conflict,
                    existingDto
                );
            }

            // Se o email ainda não existe, o método continua para criar um novo aluno normalmente, 
            // convertendo o DTO de criação (CreateStudentDto) para a entidade Student, salvando no banco e 
            var student = new Student
            {
                Id = Guid.NewGuid(),           // Gera identificador único. Evita dependência do banco para gerar ID. Boa prática para sistemas distribuídos.
                FullName = dto.FullName,       // Vem do DTO
                Email = dto.Email,             // Vem do DTO
                DateOfBirth = dto.DateOfBirth, // Vem do DTO
                IsActive = true,               // Regra de negócio
                CreatedAt = DateTime.UtcNow    // Backend controla
            };

            // Salvar no banco
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // Convertendo Entity → DTO de resposta
            var response = new StudentResponseDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth
            };

            // Retornando um resultado de sucesso com os dados do aluno criado (StudentResponseDto armazendo dentro da variável response).
            return Result<StudentResponseDto>.Ok(response);
        }

        // Tipo de retorno bool para indicar sucesso ou falha da operação, podendo ser tratado no Controller para retornar o status HTTP adequado (200, 404, etc.)
        public async Task<Result<StudentResponseDto>> UpdateAsync(Guid id, UpdateStudentDto dto)
        {   
            // Buscar aluno no banco filtrando por ID
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            // Se não encontrar o aluno, retorna null
            if (student == null)
            {
                return Result<StudentResponseDto>.Fail("Aluno não existe.", ErrorType.NotFound);
            }

            if (student.IsActive == false)
            {
                return Result<StudentResponseDto>.Fail("Aluno está inativo. Reative para atualizar.", ErrorType.Conflict);
            }

            if (student.Email != dto.Email)
            {
                // Verificar se o novo email já existe para outro aluno
                var emailExists = await _context.Students.AnyAsync(s => s.Email == dto.Email && s.Id != id);

                if (emailExists)
                {
                    return Result<StudentResponseDto>.Fail("Já existe outro aluno com este email.", ErrorType.Conflict);
                }
            }

            // Se encontrou o aluno pelo ID e está ativo, a variável studente não será null, logo o método não executou o return acima, 
            // passando para o passo abaixo onde atualiza os campos do student, usando as informações recebidas no DTO de atualização (UpdateStudentDto).
            student.FullName = dto.FullName;
            student.Email = dto.Email;
            student.DateOfBirth = dto.DateOfBirth;

            // Salva no banco e retorna true para indicar sucesso
            await _context.SaveChangesAsync();

            var response = new StudentResponseDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                IsActive = student.IsActive
            };

            return Result<StudentResponseDto>.Ok(response);
        }

        public async Task<Result<bool>> DeactivateAsync(Guid id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return Result<bool>.Fail(
                    "Aluno não encontrado.",
                    ErrorType.NotFound
                );
            }

            if (!student.IsActive)
            {
                return Result<bool>.Fail(
                    "Aluno já está inativo.",
                    ErrorType.Conflict
                );
            }

            // Soft Delete: Em vez de remover o registro, marcamos como inativo
            student.IsActive = false;
            student.DeactivatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);        
        }

        public async Task<Result<StudentResponseDto>> ReactivateAsync(Guid id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return Result<StudentResponseDto>.Fail("Aluno não encontrado.", ErrorType.NotFound);

            if (student.IsActive)
                return Result<StudentResponseDto>.Fail("Aluno já está ativo.", ErrorType.Conflict);

            student.IsActive = true;
            student.DeactivatedAt = null;
            
            await _context.SaveChangesAsync();

            var response = new StudentResponseDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                IsActive = student.IsActive
            };

            return Result<StudentResponseDto>.Ok(response);
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return Result<bool>.Fail(
                    "Aluno não encontrado.",
                    ErrorType.NotFound
                );
            }

            if (student.DeactivatedAt == null)
            {
                return Result<bool>.Fail(
                    "Aluno ainda está ativo. Antes de deletar, desative o aluno.",
                    ErrorType.Conflict
                );
            }

            if (student.DeactivatedAt > DateTime.UtcNow.AddYears(-5))
            {
                return Result<bool>.Fail(
                    "Aluno foi desativado há menos de 5 anos. Aguarde o período de retenção.",
                    ErrorType.Conflict
                );
            }

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);            
        }
    }
}