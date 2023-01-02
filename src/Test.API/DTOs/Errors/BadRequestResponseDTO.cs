using System.Net;
using Microsoft.AspNetCore.Mvc;
using Test.API.Helpers;

namespace Test.API.DTOs.Errors;

public class BadRequestResponseDTO
{
    public BadRequestResponseDTO(string message)
    {
        Status = (int) HttpStatusCode.BadRequest;
        Message = message;
    }
    public BadRequestResponseDTO(ValidationProblemDetails validationProblemDetails)
    {
        Status = validationProblemDetails.Status;
        Message = validationProblemDetails.Title;
        Errors = FormatValidationErrors(validationProblemDetails);
    }
    
    public string? Message { get; set; }
    public int? Status { get; set; }
    public IDictionary<string, List<string>>? Errors { get; set; }
    
    private static Dictionary<string, List<string>>? FormatValidationErrors(ValidationProblemDetails validationProblemDetails)
    {
        if (!validationProblemDetails.Errors.Any())
            return null;
        
        var result = new Dictionary<string, List<string>>();
        foreach (var group in validationProblemDetails.Errors)
        {
            var key = group.Key.FirstCharToLower();
            result.Add(key, new List<string>());
            foreach (var failure in validationProblemDetails.Errors[group.Key])
            {
                result[key].Add(failure);
            }
        }

        return result;
    }
}