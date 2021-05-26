using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Cms.Endpoints.Article.Get
{
    [Route(EndPointRouteNames.Article)]
    public class Get : BaseAsyncEndpoint
        .WithRequest<GetArticleQuery>
        .WithResponse<GetArticleResponse>
    {
        private readonly IMediator _mediator;

        public Get(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        [SwaggerOperation(
            Summary = "Retrieve an article by id ",
            Description = "Retrieves a full articles ",
            OperationId = "EF0A3653-153F-4E73-8D20-621C9F9FFDC9",
            Tags = new[] {"Article"})
        ]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetArticleResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [Produces("application/json")]
        public override async  Task<ActionResult<GetArticleResponse>> HandleAsync([FromRoute] GetArticleQuery query,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var article = await _mediator.Send(query, cancellationToken);
            if (article == null) return new NotFoundResult();
            return new OkObjectResult(article);
        }
    }
}
