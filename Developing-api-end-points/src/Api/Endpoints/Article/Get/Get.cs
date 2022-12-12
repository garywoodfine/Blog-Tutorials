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
    [Route(Routes.Article)]
    public class Get : BaseAsyncEndpoint
        .WithRequest<Query>
        .WithResponse<Response>
    {
        private readonly IMediator _mediator;

        public Get(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{category}/{id}")]
        [SwaggerOperation(
            Summary = "Retrieve an article by id ",
            Description = "Retrieves a full articles ",
            OperationId = "EF0A3653-153F-4E73-8D20-621C9F9FFDC9",
            Tags = new[] {Routes.Article})
        ]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [Produces("application/json")]
        public override async  Task<ActionResult<Response>> HandleAsync([FromRoute] Query query,
            CancellationToken cancellationToken = new())
        {
            return await Task.Run(() => new OkObjectResult(new Response
            {
                Content = "blah blah blah",
                Description = "This is a Fine Description",
                Published = DateTime.Now.AddHours(-10),
                Summary = "this is a fine Summary",
                SubHeading = "This is a sub heading"
            }), cancellationToken);
        }
    }
}
