using Cms.Endpoints.Salutations.GetAll;
using FluentValidation;

namespace Cms.Endpoints.Salutations.Get
{
    public class GetSalutationQueryValidator : AbstractValidator<GetSalutationQuery>
    {
        public GetSalutationQueryValidator()
        {
            RuleFor(c => c.Id).NotNull();
        }
    }
}