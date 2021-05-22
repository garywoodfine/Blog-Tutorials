using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Article.Get
{
    public class GetArticleQuery : IRequest<GetArticleResponse>
    {
        [FromRoute(Name = "id")] public Guid Id { get; set; }
    }
}