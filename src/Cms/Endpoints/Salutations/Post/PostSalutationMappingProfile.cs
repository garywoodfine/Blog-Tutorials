using AutoMapper;
using AutoMapper.Configuration;
using Boleyn.Database.Entities.Authors;

namespace Cms.Endpoints.Salutations.Post
{
    public class PostSalutationMappingProfile : Profile
    {
        public PostSalutationMappingProfile()
        {
            CreateMap<CreateSalutationCommand, Salutation>()
                .ForMember(x => x.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.FullWord, opt => opt.MapFrom(src => src.FullWord))
                .ForMember(x => x.GenderId, opt => opt.MapFrom(src => src.GenderId));
        }
    }
}