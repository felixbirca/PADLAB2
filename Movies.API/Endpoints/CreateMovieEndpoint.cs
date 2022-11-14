using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Movies.API.DTOs;
using Movies.API.Helpers;
using Movies.API.Services;

namespace Movies.API.Endpoints
{
    [HttpPost("/api/movies/create")]
    [AllowAnonymous]
    public class CreateMovieEndpoint : Endpoint<CreateMovieDto>
    {
        private readonly IMoviesService _moviesService;
        private SyncNodeClient _syncNodeClient;
        private readonly ApiClient _apiClient;

        public CreateMovieEndpoint(IMoviesService moviesService)
        {
            _moviesService = moviesService;
            _syncNodeClient = new SyncNodeClient();
            _apiClient = new ApiClient();
        }

        public override async Task HandleAsync(CreateMovieDto request, CancellationToken cancellationToken)
        {
            ThrowIfAnyErrors();

            await _moviesService.Create(request);

            var response = await _syncNodeClient.GetNextNode(new Common.SyncRequestDto
            {
                IpAddress = Environment.GetEnvironmentVariable("CONTAINER_IP"),
                RequestSource = request.SourceIpAddress ?? Environment.GetEnvironmentVariable("CONTAINER_IP")
            });

            var newRequest = new CreateMovieDto
            {
                Title = request.Title,
                Actors = request.Actors,
                ReleaseDate = request.ReleaseDate,
                Budget = request.Budget
            };

            newRequest.SourceIpAddress = request.SourceIpAddress ?? Environment.GetEnvironmentVariable("CONTAINER_IP");

            if (!response.IsLast)
            {
                await _apiClient.CreateMovie(newRequest, response.NextNodeIpAddress);
            }

            await SendOkAsync();
        }
    }
}
