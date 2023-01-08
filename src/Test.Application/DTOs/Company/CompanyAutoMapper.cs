using AutoMapper;

namespace Test.Application.DTOs.Company;

public class CompanyAutoMapper: Profile
{
    public CompanyAutoMapper()
    {
        CreateMap<Domain.Entities.Company, EmployeeDTO>().ReverseMap();
    }
}