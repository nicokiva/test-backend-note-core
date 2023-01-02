using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.DTOs.Company;
using Test.Repository;

namespace Test.Application.Commands.Company;

public class CreateCompanyCommandHandler: IRequestHandler<CreateCompanyCommand, CreateCompanyResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCompanyCommandHandler> _logger;

    public CreateCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCompanyCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateCompanyResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = Domain.Entities.Company.New(request.Name, request.Code);
        if (request.Code == "1")
            throw new Exception("Va;od cpde");

        _logger.LogInformation("----- Creating Company - Name: {@Name} - Code: {@Code}", request.Name, request.Code);
        
        company = await _unitOfWork.CompanyRepository.CreateAsync(company, cancellationToken);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        return new CreateCompanyResponse
        {
            Result = _mapper.Map<CompanyDTO>(company)
        };
    }
}