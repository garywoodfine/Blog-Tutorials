using AutoMapper;
using Boleyn.Database.Entities.Authors;

namespace Cms.Endpoints.Salutations.GetAll
{
    public class GetAllSalutationMappingProfile : Profile
    {
        public GetAllSalutationMappingProfile()
        {
            CreateMap<Salutation, GetAllResponse>()
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.FullWord, opt => opt.MapFrom(src => src.FullWord));
        }
    }
}