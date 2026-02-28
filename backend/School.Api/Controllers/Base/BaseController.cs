using Microsoft.AspNetCore.Mvc;
using School.Api.Application.Common;

namespace School.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult HandleFailure<T>(Result<T> result)
        {
            return result.ErrorType switch
            {
                ErrorType.NotFound => NotFound(result), // HTTP 404
                ErrorType.Conflict => Conflict(result), // HTTP 409
                ErrorType.Validation => BadRequest(result), // HTTP 400
                _ => StatusCode(500, result) // HTTP 500 para erros inesperados
            };
        }
    }
}