using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Movies.API.DTOs;
using Movies.API.Helpers;
using Movies.API.Services;

namespace Movies.API.Endpoints
{
    [HttpPost("/api/movies/delete")]
    [AllowAnonymous]
    public class DeleteMovieEndpoint : Endpoint<DeleteMovieDto>
    {
        private readonly IMoviesService _moviesService;
        private SyncNodeClient _syncNodeClient;
        private readonly ApiClient _apiClient;

        public DeleteMovieEndpoint(IMoviesService moviesService)
        {
            _moviesService = moviesService;
            _syncNodeClient = new SyncNodeClient();
            _apiClient = new ApiClient();
        }

        public override async Task HandleAsync(DeleteMovieDto request, CancellationToken cancellationToken)
        {
            var existingMovie = await _moviesService.GetMovieById(request.Id);

            if (existingMovie == null)
                AddError("Movie with this Id does not exist.");

            ThrowIfAnyErrors();

            await _moviesService.DeleteMovie(request.Id);

            var response = await _syncNodeClient.GetNextNode(new Common.SyncRequestDto
            {
                IpAddress = Environment.GetEnvironmentVariable("CONTAINER_IP"),
                RequestSource = request.SourceIpAddress ?? Environment.GetEnvironmentVariable("CONTAINER_IP")
            });

            var newRequest  =new DeleteMovieDto
            {
                Id = request.Id,
            };

            newRequest.SourceIpAddress = request.SourceIpAddress ?? Environment.GetEnvironmentVariable("CONTAINER_IP");

            if (!response.IsLast)
            {
                _apiClient.DeleteMovie(newRequest, response.NextNodeIpAddress);
            }

            await SendOkAsync();
        }
    }
}
