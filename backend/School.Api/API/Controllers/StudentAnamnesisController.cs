

using Microsoft.AspNetCore.Mvc;
using School.Api.Application.DTOs.StudentsAnamnesis;
using School.Api.Application.Interfaces.Services;

namespace School.Api.API.Controllers
{
    /*
    [ApiController] Indica que esta classe é um Controller de API. Diz ao ASP.NET: “Essa classe responde 
    requisições HTTP e retorna JSON.”
    Controller responsável apenas por:
        - Receber requisições HTTP
        - Chamar o Service
        - Retornar resposta HTTP
        - Não acessa o banco diretamente, quem faz isso é o serviço (StudentService.cs).
    */
    [ApiController]

    // Define a rota base:  /api/students
    // O que ele faz é substituir [controller] pelo nome da classe sem a palavra “Controller”
    [Route("api/[controller]")]

    public class StudentAnamnesisController : BaseController
    {
        private readonly IStudentAnamnesisService _studentAnamnesisService;
        
        public StudentAnamnesisController(IStudentAnamnesisService studentAnamnesisService)
        {
            _studentAnamnesisService = studentAnamnesisService;
        }

        [HttpPost("{studentId}/anamnesis")]
        public async Task<IActionResult> CreateAnamnesis(
            Guid studentId,
            StudentAnamnesisCreateDto dto)
        {
            var result = await _studentAnamnesisService.CreateAsync(studentId, dto);

            return HandleResult(result);
        }

        [HttpGet("{studentId}/anamnesis")]
        public async Task<IActionResult> GetAnamnesis(Guid studentId)
        {
            var result = await _studentAnamnesisService.GetByStudentIdAsync(studentId);

            return HandleResult(result);
        }

        [HttpPut("{studentId}/anamnesis")]
        public async Task<IActionResult> UpdateAnamnesis(
            Guid studentId,
            StudentAnamnesisUpdateDto dto)
        {
            var result = await _studentAnamnesisService.UpdateAsync(studentId, dto);

            return HandleResult(result);
        }
    }

}