using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    // Indica que esta classe é um Controller de API. Diz ao ASP.NET: “Essa classe responde requisições HTTP e retorna JSON.”
    [ApiController]

    // Define a rota base:  /api/students
    // O que ele faz é substituir [controller] pelo nome da classe sem a palavra “Controller”.
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        // Esse método responde a requisições GET (GET http://localhost:xxxx/api/students)
        [HttpGet]
        public IActionResult GetAll()
        {
            // Por enquanto, vamos retornar dados "fake"
            // Depois isso virá do banco de dados

            var students = new[]
            {
                new
                {
                    Id = 1,
                    Name = "Ana Clara",
                    Age = 4
                },
                new
                {
                    Id = 2,
                    Name = "João Pedro",
                    Age = 5
                }
            };

            // Retorna HTTP 200 (OK) + JSON
            return Ok(students);
        }
    }
}
