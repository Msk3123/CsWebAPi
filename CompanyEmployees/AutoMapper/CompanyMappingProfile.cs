using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace CompanyEmployees.AutoMapper;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        CreateMap<Company, CompanyDTO>()
            .ForMember(c => c.FullAddress,
                opt => opt.MapFrom(x => x.Address + " " + x.Country));

        CreateMap<Employee, EmployeeDTO>();
        
        CreateMap<CompanyForCreationDto, Company>();
        
        CreateMap<EmployeeForCreationDto, Employee>();
    }
}