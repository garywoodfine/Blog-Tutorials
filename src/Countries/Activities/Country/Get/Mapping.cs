using AutoMapper;
using Threenine.ApiResponse;

namespace Boleyn.Countries.Activities.Country.Get
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<WorldBank.Models.Country, Response>()
                .ForMember(x => x.Capital, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(x => x.Longitude, opt => opt.MapFrom(src => src.Longitude))
                ;

        }
    }
}