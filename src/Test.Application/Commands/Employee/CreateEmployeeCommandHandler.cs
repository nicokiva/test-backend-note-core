using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.DTOs.Employee;
using Test.Repository;

namespace Test.Application.Commands.Employee;

public class CreateEmployeeCommandHandler: IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateEmployeeCommandHandler> _logger;

    public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateEmployeeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateEmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = Domain.Entities.Employee.New(request.fullName, request.idNumber, request.dateOfBirth, request.company);
        if (request.dateOfBirth >= DateTime.UtcNow)
            throw new Exception("Invalid Date Of Birth");

        _logger.LogInformation("----- Creating Employee - Name: {@FullName} - IdNumber: {@IdNumber}", request.fullName, request.idNumber);
        
        employee = await _unitOfWork.EmployeeRepository.CreateAsync(employee, cancellationToken);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        return new CreateEmployeeResponse
        {
            Result = _mapper.Map<EmployeeDTO>(employee)
        };
    }
}