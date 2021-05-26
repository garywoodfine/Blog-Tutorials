using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Cms.Endpoints.Article.Get;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Swashbuckle.AspNetCore.Annotations;

namespace Cms.Endpoints.Salutations.Get
{
    [Route(EndPointRouteNames.Salutations)]
    public class Get : BaseAsyncEndpoint.WithRequest<GetSalutationQuery>.WithResponse<GetSalutationResponse>
    {
        private readonly IMediator _mediator;

        public Get(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{id:int}")]
        [SwaggerOperation(
            Summary = "Retrieve an salutation by id ",
            Description = "Retrieves a salutation object by the ID ",
            OperationId = "E7EBC9D3-8B3C-4A59-9294-63F2B22007D6",
            Tags = new[] {EndPointRouteNames.Salutations})
        ]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSalutationResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [Produces("application/json")]
        public override async Task<ActionResult<GetSalutationResponse>> HandleAsync([FromRoute] GetSalutationQuery request, CancellationToken cancellationToken = new CancellationToken())
        {
            var response = await _mediator.Send(request, cancellationToken);
            if (response == null) return new NotFoundResult();
            return new OkObjectResult(response);
        }
    }
}