// Using necessários para trabalhar com ASP.NET MVC
using Microsoft.AspNetCore.Mvc;

using School.Api.Application.DTOs.Students;

using School.Api.Application.Services.Interfaces;


namespace School.Api.Controllers
{
    /*
    Indica que esta classe é um Controller de API. Diz ao ASP.NET: “Essa classe responde requisições HTTP e retorna JSON.”
    Controller responsável apenas por:
        - Receber requisições HTTP
        - Chamar o serviço
        - Retornar resposta HTTP
        - Não acessa o banco diretamente, quem faz isso é o serviço (StudentService.cs).
    */
    [ApiController]

    // Define a rota base:  /api/students
    // O que ele faz é substituir [controller] pelo nome da classe sem a palavra “Controller”.
    [Route("api/[controller]")]

    // Controller responsável por gerenciar alunos (Students).
    public class StudentsController : BaseController
    {
         // Campo privado para armazenar o DbContext
        private readonly IStudentService _studentService;
        
        /*
            Injeção de Dependência do serviço.       
            O .NET cria o StudentService automaticamente porque registramos no Program.cs. 
        */
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /*
        =========================
        ENDPOINT: GET /api/students
        =========================
            Objetivo:
            - Buscar todos os alunos ativos no banco
            - Retornar uma lista em formato JSON
        ==> async:  Indica que o método é assíncrono e nos permite usar o "await" dentro do método.
            Ele pode “esperar” uma operação demorada (banco, rede, disco) sem travar o servidor.
            Em APIs reais, quase tudo que acessa banco deve ser async.
        ==> Task<...>:  Representa uma operação que começa agora e termina no futuro. O tipo dentro do Task indica o que será retornado quando a operação terminar.
        ==> ActionResult: Ele é um tipo especial que permite retornar um objeto do tipo T ou qualquer resultado HTTP (Ok, NotFound, etc.)
                Ele combina os dois mundos:  Dados e Status HTTP
                Para endpoints que retornam dados → use ActionResult<T>
                Para endpoints que não retornam dados (ex: DELETE) → use IActionResult
        ==> IEnumerable<StudentResponseDto>:  Uma coleção de StudentResponseDto, podendo ser lista, array, etc.
        */
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentService.GetAllAsync();

            if (!result.Success)
            {
                return HandleFailure(result);
            }

            return Ok(result);
        }
        
        /*
        =========================
        ENDPOINT: GET api/students/{id}
        =========================
            Objetivo:
            - Buscar um aluno específico pelo ID
            - Retornar os dados do aluno em formato JSON
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {   
            var result = await _studentService.GetByIdAsync(id);

            if (!result.Success)
                return HandleFailure(result);

            return Ok(result);
        }
        
        /* 
        =========================
        ENDPOINT: POST api/students
        =========================
            Objetivo:
            - Receber os dados de um novo aluno via JSON (pelo CreateStudentDto)
            - Invocar o StudentService.CreateAsync para criar o aluno no banco
            - Retornar o resultado da criação, incluindo os dados do aluno criado, ou um erro se o email já existir.
            - Se a criação for bem-sucedida, retorna HTTP 201 (Created) com os dados do aluno criado (usando o StudentResponseDto)
        */
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentDto dto)
        {           
            //result recebe o resultado do método CreateAsync do StudentService, que é do tipo Result<StudentResponseDto>.
            var result = await _studentService.CreateAsync(dto);

            // StudentService filtra para saber se já existe um aluno com o mesmo email. Se existir, ele retorna 
            // um Result<StudentResponseDto> com Success = false e uma mensagem de erro, quais são armazenados na variável result.
            if (!result.Success)
                return HandleFailure(result); 
                
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Data!.Id },
                result
            );
        }

        /* 
        =========================
        ENDPOINT: PUT api/students
        =========================
            Objetivo: Receber os dados atualizados de um aluno via JSON (pelo UpdateStudentDto)
        */
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, UpdateStudentDto dto)
        {
            var result = await _studentService.UpdateAsync(id, dto);

            if (!result.Success)
                return HandleFailure(result);

            return NoContent(); // HTTP 204
        }

        /* 
        =========================
        ENDPOINT: SOFTDELETE api/students (desativar)
        =========================
            Objetivo:
            - Receber o ID de um aluno a ser desativado
            - Marcar o registro como inativo (Soft Delete)
        */
        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> DeactivateStudent(Guid id)
        {
            var result = await _studentService.DeactivateAsync(id);

            if (!result.Success)
                return HandleFailure(result);

            return NoContent();
        }

         /* 
        =========================
        ENDPOINT: REACTIVATE api/students
        =========================
            Objetivo: Receber o ID de um aluno inativo a ser reativado
        */        
        [HttpPatch("{id}/reactivate")]
        // A diferença entre usar o PUT e o PATCH é que o PUT é usado para atualizar um recurso inteiro, 
        // enquanto o PATCH é usado para atualizar parcialmente um recurso (ex.: o status do aluno)
        public async Task<IActionResult> ReactivateStudent(Guid id)
        {
            var result = await _studentService.ReactivateAsync(id);

            if (!result.Success)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(result), // HTTP 404 (Not Found) indicando que o aluno não existe
                    ErrorType.Conflict => Conflict(result), // HTTP 409 (Conflict) indicando que o aluno existe mas está inativo, com a possibilidade de reativação
                    ErrorType.Validation => BadRequest(result), // HTTP 400 (Bad Request) para outros erros
                    _ => StatusCode(500, result)
                };
            }
            return Ok(result); // Retorna HTTP 200 (OK) com os dados do aluno reativado
        }
                
        /* 
        =========================
        ENDPOINT: DELETE api/students (hard delete)
        =========================
            Objetivo:
            - Receber o ID de um aluno a ser excluído
            - Excluir o registro do banco  (Hard Delete)
        */
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var result = await _studentService.DeleteAsync(id);

            if (!result.Success)
                return HandleFailure(result);

            return NoContent();
        }
    }
}
