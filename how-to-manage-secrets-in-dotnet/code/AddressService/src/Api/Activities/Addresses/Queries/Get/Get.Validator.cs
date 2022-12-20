using System;
using Common;
using FluentValidation;

namespace Threenine.Activities.Addresses.Queries.Get;

public class Validator : AbstractValidator<Query>
{
    public Validator()
    {
        RuleFor(x => x.PostCode).NotEmpty();

        RuleFor(x => x.PostCode).Matches(RegularExpressions.PostCodeValidator)
            .WithMessage("Valid UK postcode required");

    }       
}
