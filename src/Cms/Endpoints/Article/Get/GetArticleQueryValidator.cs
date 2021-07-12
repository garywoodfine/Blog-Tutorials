using FluentValidation;

namespace Cms.Endpoints.Article.Get
{
    public class GetArticleQueryValidator : AbstractValidator<GetArticleQuery>
    {
        public GetArticleQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}