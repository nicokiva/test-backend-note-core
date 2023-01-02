using AutoMapper;

namespace Test.Application.DTOs.Employee
{
    public class EmployeeAutoMapper : Profile
    {
        public EmployeeAutoMapper()
        {
            CreateMap<Domain.Entities.Employee, EmployeeDTO>();
        }
    }
}

