using MediatR;
using Microsoft.AspNetCore.Mvc;
using Threenine.ApiResponse;

namespace Threenine.Activities.Documents.Commands.Post;

public class Command : IRequest<SingleResponse<Response>>
{
        [FromBody] public IFormFile  File { get; set; }
}


