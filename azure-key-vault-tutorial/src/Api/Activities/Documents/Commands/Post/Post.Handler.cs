using Azure.Storage.Blobs;
using MediatR;
using Threenine.ApiResponse;

namespace Threenine.Activities.Documents.Commands.Post;

public class Handler : IRequestHandler<Command, SingleResponse<Response>>
{
    private readonly BlobServiceClient _defaultClient;

    public Handler(   BlobServiceClient defaultClient)
    {
        _defaultClient = defaultClient;
    }

    public async Task<SingleResponse<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var containerClient = _defaultClient.GetBlobContainerClient("documents");
        var blobClient = containerClient.GetBlobClient(request.File.FileName);
     var result=   await blobClient.UploadAsync(request.File.OpenReadStream(), true, cancellationToken);
        
        return new SingleResponse<Response>(new Response
        {
            Tag = result.Value.ETag.ToString(),
            Created = result.Value.LastModified.DateTime,
            Reason = "Created",
            StatusCode = StatusCodes.Status201Created.ToString(),
            
        });
    }
}
