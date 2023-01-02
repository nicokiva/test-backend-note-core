namespace Test.API.DTOs.Errors;

public record InternalServerErrorDTO(string Message, int Status=500);