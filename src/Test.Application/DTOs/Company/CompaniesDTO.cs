using System.Collections.ObjectModel;
using Test.Application.Queries.Base;

namespace Test.Application.DTOs.Company;

public class CompaniesDTO: Collection<CompanyDTO>, IQueryDataResponse
{
    public CompaniesDTO(IList<CompanyDTO> companies): base(companies){}
}