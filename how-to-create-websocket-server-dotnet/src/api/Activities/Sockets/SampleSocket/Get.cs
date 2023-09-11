using System.Net;
using System.Net.WebSockets;
using System.Text;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Activities.Sockets.Queries.SampleSocket;

[Route(Routes.Sockets)]
public class WebSocketQuery : EndpointBaseAsync.WithoutRequest.WithoutResult
{
    private readonly ILogger<WebSocketQuery> _logger;
    public WebSocketQuery(ILogger<WebSocketQuery> logger)
    {
        _logger = logger;
    }
        
    [HttpGet]
    [SwaggerOperation(
        Summary = "Web Socket Upgrade Request to initiate a Web Socket Session",
        Description = "Web Socket Upgrade Request to initiate a Web Socket Session",
        OperationId = "1318F7F5-4FE0-4315-BBF3-0DD1A9CABB5C",
        Tags = new[] { Routes.Sockets})
    ]
    [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
    public override  async Task<IActionResult> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest) return BadRequest();
        
      
        using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        _logger.Log(LogLevel.Information, "WebSocket connection established");
        await Echo(webSocket);
        return new OkResult();
    }
    private async Task Echo(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        _logger.Log(LogLevel.Information, "Message received from Client");

        while (!result.CloseStatus.HasValue)
        {
            var serverMsg = Encoding.UTF8.GetBytes($"Server Response to: {Encoding.UTF8.GetString(buffer)}");
            await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
            _logger.Log(LogLevel.Information, "Sent Response back to client confirming message received");

            buffer = new byte[1024 * 4];
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            _logger.Log(LogLevel.Information, "Message received from Client");
        }

        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        _logger.Log(LogLevel.Information, "WebSocket connection closed");
    }
}