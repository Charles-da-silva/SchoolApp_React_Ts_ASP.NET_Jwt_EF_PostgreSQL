// Using necessários para trabalhar com ASP.NET MVC
using Microsoft.AspNetCore.Mvc;

// Using para trabalhar com EF Core (LINQ, DbContext, etc.)
using Microsoft.EntityFrameworkCore;

// Using para fazer a importação do DbContext da aplicação
using School.Api.Infrastructure.Data;

// Using para fazer a importação da entidade Student
using School.Api.Domain.Entities;
using School.Api.Application.DTOs.Students;

namespace School.Api.Controllers
{
    // Indica que esta classe é um Controller de API. Diz ao ASP.NET: “Essa classe responde requisições HTTP e retorna JSON.”
    // Controller responsável por gerenciar alunos (Students).
    [ApiController]

    // Define a rota base:  /api/students
    // O que ele faz é substituir [controller] pelo nome da classe sem a palavra “Controller”.
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
         // Campo privado para armazenar o DbContext
        private readonly SchoolDbContext _context;
        
        /*
            CONSTRUTOR COM DEPENDENCY INJECTION (DI)

            O ASP.NET Core cria automaticamente uma instância de SchoolDbContext e injeta aqui para nós.

            Ou seja:
            - NÃO criamos o DbContext manualmente
            - NÃO usamos "new SchoolDbContext(...)"
            - O framework cuida do ciclo de vida
        */
        public StudentsController(SchoolDbContext context)
        {
            _context = context;
        }

        /*
            ==> ENDPOINT: GET /api/students
            Objetivo:
            - Buscar todos os alunos ativos no banco
            - Retornar uma lista em formato JSON

            ==> async:  Indica que o método é assíncrono e nos permite usar o "await" dentro do método.
                Ele pode “esperar” uma operação demorada (banco, rede, disco) sem travar o servidor.
                Em APIs reais, quase tudo que acessa banco deve ser async.

            ==> Task<...>:  Representa uma operação que começa agora e termina no futuro. O tipo dentro do Task indica o que será retornado quando a operação terminar.

            ==> ActionResult: Permite retornar Ok(...), NotFound(), BadRequest(), etc.

            ==> IEnumerable<Student>:  Uma coleção de alunos, podendo ser lista, array, etc.
        */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            /* _context.Students
                significa:
                    - Acessar a tabela Students;
                    - Trabalhar com objetos C#
                    - Sem escrever SQL diretamente */
                    
            // Busca todos os alunos ativos (IsActive = true)
            var students = await _context.Students
                .Where(s => s.IsActive)
                .ToListAsync();

                /* 
                    .ToListAsync() - Aqui o EF Core:
                        - Traduz LINQ → SQL
                        - Abre conexão
                        - Executa o SELECT
                        - Converte linhas → objetos Student
                        - Retorna uma lista
                    Por isso precisa do await.
                */

            // Retorna HTTP 200 (OK) com os dados
            return Ok(students);
        }

        // Busca um aluno pelo ID.
        // GET: api/students/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {   
            /*
                _context é o SchoolDbContext injetado no controller. Representa a conexão com o banco.

                FirstOrDefault é um método do LINQ que retorna o primeiro elemento que satisfaz a condição ou null se não encontrar.
            
                s => s.Id == id é uma expressão lambda que compara o Id do aluno com o id passado na URL.
                “Pegue cada Student (apelidado de s) e verifique se o Id dele é igual ao id recebido na URL”
            */
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);  

            if (student == null)
                return NotFound();

            // Converter Entity → DTO de resposta
            var response = new StudentResponseDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                IsActive = student.IsActive
            };
            
            // Retorna HTTP 200 (OK) com os dados
            return Ok(response);
        }

        /// Cria um novo aluno.
        /// POST: api/students
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentDto dto)
        {
            // Converter DTO → Entity
            var student = new Student
            {
                Id = Guid.NewGuid(),          // Backend gera o ID
                FullName = dto.FullName,              // Vem do DTO
                Email = dto.Email,            // Vem do DTO
                DateOfBirth = dto.DateOfBirth, // Vem do DTO
                IsActive = true,              // Regra de negócio
                CreatedAt = DateTime.UtcNow   // Backend controla
            };

            // Salvar no banco
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // Converter Entity → DTO de resposta
            var response = new StudentResponseDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                IsActive = student.IsActive
            };

            // Retornar resposta HTTP 201 (Created)
            return CreatedAtAction(
                nameof(GetById),
                new { id = student.Id },
                response
            );
        }

        // Busca um aluno pelo ID.
        // PUT: api/students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, UpdateStudentDto dto)
        {
            // Buscar aluno no banco
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            // Se não existir → retornar 404
            if (student == null)
                return NotFound();

            // Estamos modificando a entidade rastreada pelo EF
            student.FullName = dto.FullName;
            student.Email = dto.Email;
            student.DateOfBirth = dto.DateOfBirth;

            // Salvar alterações no banco
            await _context.SaveChangesAsync();

            // Retornar 204 (Sucesso sem conteúdo)
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            // Buscar aluno no banco
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            // Se não existir ou já tiver sido desativado → retornar 404
            if (student == null || !student.IsActive)
            return NotFound();

            // Soft Delete: Em vez de remover o registro, marcamos como inativo
            student.IsActive = false;

            // Salvar alterações no banco
            await _context.SaveChangesAsync();

            // DELETE padrão REST retorna 204
            return NoContent();
        }
    }
}
