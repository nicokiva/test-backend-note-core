using System.Collections.ObjectModel;
using Test.Application.Queries.Base;

namespace Test.Application.DTOs.Company;

public class CompaniesDTO: Collection<EmployeeDTO>, IQueryDataResponse
{
    public CompaniesDTO(IList<EmployeeDTO> companies): base(companies){}
}