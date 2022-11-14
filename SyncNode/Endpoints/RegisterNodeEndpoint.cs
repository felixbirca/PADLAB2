using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SyncNode.DTOs;
using SyncNode.Models;
using SyncNode.Service;

namespace SyncNode.Endpoints
{
    [HttpPost("/api/register-node")]
    [AllowAnonymous]
    public class RegisterNodeEndpoint : Endpoint<NodeInfoDto, SyncResponseDto>
    {
        private readonly ApiNodesService _apiNodesService;

        public RegisterNodeEndpoint(ApiNodesService apiNodesService)
        {
            _apiNodesService = apiNodesService;
        }

        public override async Task HandleAsync(NodeInfoDto request, CancellationToken cancellationToken)
        {
            var existingNode = _apiNodesService.FindNode(request.IpAddress);

            if (existingNode)
                AddError("Node with this ip address already exists");

            _apiNodesService.AddNode(new NodeInfo { IpAddress = request.IpAddress });
            Console.WriteLine($"{request.IpAddress} node registered");

            ThrowIfAnyErrors();

            await SendOkAsync();
        }
    }
}
