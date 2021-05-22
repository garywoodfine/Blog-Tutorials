using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Cms.Endpoints.Salutations.Post
{   
    [Route(EndPointRouteNames.Salutations)]
    public class Post : BaseAsyncEndpoint.WithRequest<CreateSalutationCommand>.WithoutResponse
    {
        private readonly IMediator _mediator;
        
        public Post(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Retrieve an article by id ",
            Description = "Retrieves a full articles ",
            OperationId = "F08FB935-5EEE-479A-9138-18089B3390CD",
            Tags = new[] {EndPointRouteNames.Salutations})
        ]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public override async Task<ActionResult> HandleAsync([FromBody] CreateSalutationCommand request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request, cancellationToken);
            return new CreatedResult( new Uri(EndPointRouteNames.Salutations, UriKind.Relative), new { id = result });
        }
    }
}