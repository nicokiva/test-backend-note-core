using MediatR;
using Test.Application.Commands.Base;

namespace Test.Application.Commands.Company;

public record CreateCompanyCommand(string Name, string Code): BaseCommand, IRequest<CreateCompanyResponse>;