using AutoMapper;
using Boleyn.Database.Entities.Authors;

namespace Cms.Endpoints.Salutations.Get
{
    public class GetSalutationMappingProfile : Profile
    {
        public GetSalutationMappingProfile()
        {
            CreateMap<Salutation, GetSalutationResponse>()
                .ForMember(x => x.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.FullWord, opt => opt.MapFrom(src => src.FullWord));
        }
    }
}