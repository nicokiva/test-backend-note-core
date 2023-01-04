using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Test.API.DTOs.Errors;
using Test.Application.Commands.Employee;
using Test.Application.DTOs.Employee;
using Test.Application.Queries.Company;
using Test.Application.Queries.Employee;

namespace Test.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICompanyQueries _companyQueries;

        private readonly IEmployeeQueries _employeeQueries;

        public EmployeeController(IMediator mediator, ICompanyQueries companyQueries, IEmployeeQueries employeeQueries)
        {
            _mediator = mediator;
            _companyQueries = companyQueries;
            _employeeQueries = employeeQueries;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CreateEmployeeResponse>> Create(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send<CreateEmployeeResponse>(command, cancellationToken));
        }
    }
}

