using AutoMapper;

namespace Boleyn.Countries.Activities.Sample.Get
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<WorldBank.Models.Country, Response>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Capital, opt => opt.MapFrom(src => src.CapitalCity))
                .ForMember(x => x.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(x => x.Longitude, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(x => x.Region, opt => opt.MapFrom(src => src.Region.Value));
        }
    }
}