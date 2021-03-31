using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Attributes;
using Api.Endpoints.Article.Request;
using Api.Endpoints.Article.Response;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.Article
{
    [Route("/article")]
    public class Get : BaseAsyncEndpoint
        .WithRequest<ArticleRequest>
        .WithResponse<ArticleResponse>
    {
        public Get()
        {
        }

        [HttpGet("{category}/{id}")]
        [SwaggerOperation(
            Summary = "Retrieve an article by id ",
            Description = "Retrieves a full articles ",
            OperationId = "EF0A3653-153F-4E73-8D20-621C9F9FFDC9",
            Tags = new[] {"Article"})
        ]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleResponse))]
        [Produces("application/json")]
        [Telemetry(nameof(Get), nameof(Get.HandleAsync))]
        public async override  Task<ActionResult<ArticleResponse>> HandleAsync([FromRoute] ArticleRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await Task.Run(() =>
                new OkObjectResult(new ArticleResponse
                {
                    Content = "blah blah blah blah blah ",
                    Description = "This is a Fine Description",
                    Published = DateTime.Now,
                    Summary = "this is a fine Summary",
                    SubHeading = "This is a sub heading"

                }), cancellationToken);

        }
    }
}
