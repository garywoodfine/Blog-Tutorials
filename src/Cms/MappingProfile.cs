using System.Collections.Generic;
using AutoMapper;
using Boleyn.Database.Entities.Authors;
using Boleyn.Database.Entities.Contents;
using Cms.Endpoints.Article.Get;
using Cms.Endpoints.Salutations.Get;
using Cms.Endpoints.Salutations.GetAll;
using Cms.Endpoints.Salutations.Post;

namespace Cms
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Content, GetArticleResponse>()
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

            CreateMap<Salutation, GetSalutationResponse>()
                .ForMember(x => x.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.FullWord, opt => opt.MapFrom(src => src.FullWord));

            CreateMap<CreateSalutationCommand, Salutation>()
                .ForMember(x => x.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.FullWord, opt => opt.MapFrom(src => src.FullWord));

            CreateMap<Salutation, GetAllResponse>()
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.FullWord, opt => opt.MapFrom(src => src.FullWord));
        }
    }
}