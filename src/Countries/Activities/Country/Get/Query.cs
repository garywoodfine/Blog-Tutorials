using MediatR;
using Microsoft.AspNetCore.Mvc;
using Threenine.ApiResponse;

namespace Boleyn.Countries.Activities.Country.Get
{
    public class Query : IRequest<SingleResponse<Response>>
    {
        [FromRoute(Name = "isoCode")] public string IsoCode { get; set; }
    }
}