using AutoMapper;
using energo.customer.api.Dto;
using energo.domain.Models;

namespace energo.customer.api.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerDto, Customer>()
            .ForMember(d => d.Name, s => s.MapFrom(r => r.CustomerName));
    }
}
