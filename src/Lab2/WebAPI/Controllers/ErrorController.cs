using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OOP_WebApp.Application.Common.Exceptions;
using OOP_WebApp.Domain.Exceptions;

namespace OOP_WebApp.Lab2.WebAPI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public ActionResult<object> HandleError()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var responseCode = exception switch
        {
            BadRequestException or DomainArgumentException => 400,
            NotFoundException => 404,
            ConflictException => 409,
            _ => 500
        };

        return StatusCode(responseCode, new {exception?.Message, exception?.StackTrace});
    }
}
