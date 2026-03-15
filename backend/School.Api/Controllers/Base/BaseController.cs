using Microsoft.AspNetCore.Mvc;
using School.Api.Application.Common;

namespace School.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        // ===============================
    // FAILURE (GENERIC)
    // ===============================
    protected ActionResult HandleFailure<T>(Result<T> result)
        => HandleFailure((Result)result);


    // ===============================
    // FAILURE (NON GENERIC)
    // ===============================
    protected ActionResult HandleFailure(Result result)
    {
        return result.ErrorType switch
       {
                ErrorType.NotFound => NotFound(result), // HTTP 404
                ErrorType.Conflict => Conflict(result), // HTTP 409
                ErrorType.Validation => BadRequest(result), // HTTP 400
                _ => StatusCode(500, result) // HTTP 500 para erros inesperados
       };
    }


    // ===============================
    // SUCCESS HANDLER
    // ===============================
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (!result.Success)
            return HandleFailure(result);

        if (result.Data is null)
            return NoContent();

        return Ok(result.Data);
    }

        /*
            Fluxo do HandleFailure:

            StudentService
                ↓
            Result<StudentResponseDto>
                ↓
            StudentsController
                ↓
            HandleResult()
                ↓
            HTTP 200 + StudentResponseDto
        */
    }
}