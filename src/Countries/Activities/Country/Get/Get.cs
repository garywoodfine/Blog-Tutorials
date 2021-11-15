using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boleyn.Countries.Activities.Country.Get
{
    [Route("country")]
    public class Get : BaseAsyncEndpoint.WithRequest<Query>.WithResponse<CountryDetail>
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
            Tags = new[] { "country" })
        ]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryDetail))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        [Produces("application/json")]
        public override async Task<ActionResult<CountryDetail>> HandleAsync([FromRoute] Query query,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(query, cancellationToken);
            return  result.IsValid ?  new OkObjectResult(result.Item) : new BadRequestObjectResult(result.Errors);
            
        }
    }
}