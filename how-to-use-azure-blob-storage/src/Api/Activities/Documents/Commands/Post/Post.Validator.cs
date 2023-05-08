using FluentValidation;

namespace Threenine.Activities.Documents.Commands.Post;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.File).NotNull().NotEmpty();
    }       
}
