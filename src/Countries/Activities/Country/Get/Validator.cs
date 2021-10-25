using FluentValidation;
namespace Boleyn.Countries.Activities.Country.Get
{
    public class Validator : AbstractValidator<Query>
    {
        private const string IncorrectIsoCode = "Incorrect ISO country code value provided";
        public Validator()
        {
            RuleFor(c => c.IsoCode).NotNull().NotEmpty()
                .Length(2,3).WithMessage("must be between {MinLength} and {MaxLength} characters. {TotalLength} were provided")
                .Matches(@"^[a-zA-Z ]+$").WithMessage(IncorrectIsoCode);;
        }
    }
}