using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Test.API.DTOs.Errors;
using Test.API.Helpers;
using Test.Application.Commands.Base;

namespace Test.API.Filters;
public class BadRequestResponseMappingFilter : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is BadRequestObjectResult badRequestObjectResult)
        {
            if (badRequestObjectResult.Value is ValidationProblemDetails problemDetails)
            {
                badRequestObjectResult.Value = new BadRequestResponseDTO(problemDetails);
            }
            if (badRequestObjectResult.Value is string problemDetailsString)
            {
                badRequestObjectResult.Value = new BadRequestResponseDTO(problemDetailsString);
            }
        }

    }
}