using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boleyn.Countries.Activities.Sample.Get
{
    public class Query : IRequest<Response>
    {
        [FromRoute(Name = "isoCode")] public string CountryCode { get; set; }
    }
}