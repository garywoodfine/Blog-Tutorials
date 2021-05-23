using System.Data;
using FluentValidation;

namespace Cms.Endpoints.Salutations.Post
{
    public class CreateSalutationCommandValidator : AbstractValidator<CreateSalutationCommand>
    {
        public CreateSalutationCommandValidator()
        {
            RuleFor(x => x.Abbreviation).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.FullWord).NotEmpty();
        }
    }
}