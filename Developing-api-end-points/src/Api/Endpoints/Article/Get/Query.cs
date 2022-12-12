using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Article.Get
{
    public class Query : IRequest<Response>
    {
        [FromRoute(Name = "id")] public Guid Id { get; set; }
    }
}