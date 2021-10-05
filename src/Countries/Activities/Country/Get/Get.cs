using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Boleyn.Countries.Activities.Sample.Get;
using Boleyn.Countries.Content.Activities;
using Boleyn.Countries.Content.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boleyn.Countries.Activities.Country.Get
{
    [Route(Routes.Country)]
    public class Get : BaseAsyncEndpoint.WithRequest<Query>.WithResponse<Response>
    {
        private readonly IMediator _mediator;

        public Get(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{isoCode}")]
        [SwaggerOperation(
            Summary = "Retrieve a sample response by id ",
            Description = "Retrieves a sample response ",
            OperationId = "EF0A3653-153F-4E73-8D20-621C9F9FFDC9",
            Tags = new[] { Routes.Country })
        ]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        [Produces("application/json")]
        public override async Task<ActionResult<Response>> HandleAsync([FromRoute] Query query,
            CancellationToken cancellationToken = new())
        {
            try
            {
                var result = await _mediator.Send(query, cancellationToken);
                if (result == null) return new NotFoundResult();
                return new OkObjectResult(result);
            }
            catch( CountryValidationException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}