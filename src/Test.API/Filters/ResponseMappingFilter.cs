using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Test.API.Helpers;
using Test.Application.Commands.Base;
using Test.Application.Queries.Base;
using Test.Application.Validators.Base;

namespace Test.API.Filters;

public class ResponseMappingFilter: ActionFilterAttribute
{
    private static string BadRequestMessage = "One or more validation errors occurred.";
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // Command Result Response handling
        if (context.Result is ObjectResult commandObjectResult && commandObjectResult.Value is ICommandResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                context.Result = new ObjectResult(response.Data)
                    { StatusCode = (int)HttpStatusCode.OK };
            else
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    if (response.FieldError != null)
                        context.Result = new BadRequestObjectResult(
                            new
                            {
                                message = BadRequestMessage,
                                status = HttpStatusCode.BadRequest,
                                errors = FormatValidationErrors(response.FieldError)
                            });
                    else
                        context.Result = new BadRequestObjectResult(
                            new
                            {
                                message = BadRequestMessage,
                                status = HttpStatusCode.BadRequest,
                                errors = string.IsNullOrEmpty(response.ErrorMessage) ? null : new { nonFieldError = new string[] { response.ErrorMessage} }
                            });
                }
            }
        }
        // Query Result Response handling
        else if (context.Result is ObjectResult queryObjectResult && queryObjectResult.Value is IQueryResponse queryResponse)
        {
            context.Result = new ObjectResult(queryResponse.Data)
                { StatusCode = queryResponse.Code };
                
        }
    }
    
    private Dictionary<string, List<string>> FormatValidationErrors(FieldError error)
    {
        var result = new Dictionary<string, List<string>>();
        var key = error.PropertyName.FirstCharToLower();
        result.Add(key, new List<string>());
        result[key].Add(error.ErrorMessage);
        return result;
    }
}