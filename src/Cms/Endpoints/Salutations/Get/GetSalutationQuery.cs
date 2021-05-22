using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Salutations.Get
{
    public class GetSalutationQuery : IRequest<GetSalutationResponse>
    {
        [FromRoute(Name = "id")] public int Id { get; set; }
    }
}