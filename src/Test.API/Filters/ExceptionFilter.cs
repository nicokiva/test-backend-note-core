using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Test.API.DTOs.Errors;
using Test.API.Helpers;

namespace Test.API.Filters;

public class ExceptionFilter: ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result == null && context.Exception != null)
        {
            context.Result = new ObjectResult(new InternalServerErrorDTO("The operation cannot be processed.")
            ) {StatusCode = (int) HttpStatusCode.InternalServerError};

            context.ExceptionHandled = true;
        }
    }
}