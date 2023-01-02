using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Test.API.DTOs.Errors;
using Test.Application.Commands.Company;
using Test.Application.DTOs;
using Test.Application.DTOs.Company;
using Test.Application.Queries.Base;
using Test.Application.Queries.Company;

namespace Test.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CompanyController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICompanyQueries _companyQueries;
    
    public CompanyController(IMediator mediator, ICompanyQueries companyQueries)
    {
        _mediator = mediator;
        _companyQueries = companyQueries;
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CompanyDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(NotFoundDTO), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<QueryResponse<CompanyDTO>>> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _companyQueries.GetCompanyByIdAsync(id, cancellationToken);
    }
    
    [HttpGet()]
    [ProducesResponseType(typeof(PaginatedDataResponse<CompaniesDTO>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(NotFoundDTO), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<QueryPaginatedResponseDTO<CompaniesDTO>>> List(CancellationToken cancellationToken)
    {
        return await _companyQueries.ListCompanies(cancellationToken);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CompanyDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponseDTO), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CreateCompanyResponse>> Create(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send<CreateCompanyResponse>(command, cancellationToken));
    }
}