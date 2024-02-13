using AutoMapper;

namespace BreweryAPI.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Beer, BeerRequest>().ReverseMap();
    }
}
