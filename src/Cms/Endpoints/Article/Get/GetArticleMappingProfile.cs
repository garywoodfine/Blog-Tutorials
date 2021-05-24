using AutoMapper;
using Boleyn.Database.Entities.Contents;

namespace Cms.Endpoints.Article.Get
{
    public class GetArticleMappingProfile : Profile
    {
        public GetArticleMappingProfile()
        {
            CreateMap<Content, GetArticleResponse>()
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        }
    }
}