using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Cms.Endpoints.Salutations.Get;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Swashbuckle.AspNetCore.Annotations;

namespace Cms.Endpoints.Salutations.GetAll
{
    [Route(EndPointRouteNames.Salutations)]
    public class Get : BaseAsyncEndpoint.WithRequest<GetAllSalutationQuery>.WithResponse<IEnumerable<GetAllResponse>>
    {
        private readonly IMediator _mediator;

        public Get(IMediator mediator)
        {
            _mediator = mediator;
        }   
        
        [HttpGet("{filter}/{value}")]
        [SwaggerOperation(
            Summary = "Retrieve an salutation by ",
            Description = "Retrieves a salutation object by the ID ",
            OperationId = "F3A12CFE-F288-456F-96E5-4D0AFDE508EB",
            Tags = new[] {EndPointRouteNames.Salutations})
        ]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [Produces("application/json")]
        public override async Task<ActionResult<IEnumerable<GetAllResponse>>> HandleAsync([FromRoute] GetAllSalutationQuery request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (result == null) return new NotFoundResult();
            return new OkObjectResult(result);
        }
    }
}