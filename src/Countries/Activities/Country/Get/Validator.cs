using FluentValidation;

namespace Boleyn.Countries.Activities.Sample.Get
{
    public class Validator : AbstractValidator<Query>
    {
        private const string IncorrectIsoCode = "Incorrect ISO country code value provided";
        public Validator()
        {
            RuleFor(c => c.CountryCode).NotEmpty().Length(2,3).Matches(@"^[a-zA-Z ]+$").WithMessage(IncorrectIsoCode);;
        }
    }
}