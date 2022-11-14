using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SyncNode.DTOs;
using SyncNode.Service;

namespace SyncNode.Endpoints
{
    [HttpPost("/api/sync-data")]
    [AllowAnonymous]
    public class SyncEndpoint : Endpoint<SyncRequestDto, SyncResponseDto>
    {
        private readonly ApiNodesService _apiNodesService;

        public SyncEndpoint(ApiNodesService apiNodesService)
        {
            _apiNodesService = apiNodesService;
        }

        public override async Task HandleAsync(SyncRequestDto request, CancellationToken cancellationToken)
        {
            var nextNode = _apiNodesService.GetNextNode(request.IpAddress);

            await SendOkAsync(new SyncResponseDto
            {
                IsLast = nextNode.IpAddress == request.RequestSource,
                NextNodeIpAddress = nextNode.IpAddress
            });
        }
    }
}
