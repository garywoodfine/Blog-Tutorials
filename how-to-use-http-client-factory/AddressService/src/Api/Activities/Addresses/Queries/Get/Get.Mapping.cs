using AutoMapper;
using Domain;


namespace Threenine.Activities.Addresses.Queries.Get;

public class Mapping: Profile
{
    public Mapping()
    {

        CreateMap<Item, Address>()
            .ForMember(dest => dest.Postcode, opt => opt.MapFrom(src => src.Postcode))
            .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.Locality))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Town))
            .ForMember(dest => dest.County, opt => opt.MapFrom(src => src.OptionalCounty))
            .ForMember(dest => dest.Organisation, opt => opt.MapFrom(src => src.Organisation))
            .ForMember(dest => dest.Property, opt => opt.MapFrom(src => src.Property));
    }
}
