using AutoMapper;
using Entities.Models;
using Entities.DTO;

namespace CompanyEmployees.AutoMapper;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        CreateMap<Company, CompanyDTO>()
            .ForMember(c => c.FullAddress,
                opt => opt.MapFrom(x => x.Address + " " + x.Country));
    }
}