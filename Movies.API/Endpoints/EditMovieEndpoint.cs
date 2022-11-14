using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Movies.API.DTOs;
using Movies.API.Helpers;
using Movies.API.Services;

namespace Movies.API.Endpoints
{
    [HttpPost("/api/movies/edit")]
    [AllowAnonymous]
    public class EditMovieEndpoint : Endpoint<EditMovieDto>
    {
        private readonly IMoviesService _moviesService;
        private SyncNodeClient _syncNodeClient;
        private readonly ApiClient _apiClient;

        public EditMovieEndpoint(IMoviesService moviesService)
        {
            _moviesService = moviesService;
            _syncNodeClient = new SyncNodeClient();
            _apiClient = new ApiClient();
        }

        public override async Task HandleAsync(EditMovieDto request, CancellationToken cancellationToken)
        {
            var existingMovie = await _moviesService.GetMovieById(request.Id);

            if (existingMovie == null)
                AddError("Movie with this Id does not exist.");

            ThrowIfAnyErrors();

            await _moviesService.EditMovie(request);
            var response = await _syncNodeClient.GetNextNode(new Common.SyncRequestDto
            {
                IpAddress = Environment.GetEnvironmentVariable("CONTAINER_IP"),
                RequestSource = request.SourceIpAddress ?? Environment.GetEnvironmentVariable("CONTAINER_IP")
            });

            var newRequest = new EditMovieDto
            {
                Title = request.Title,
                Actors = request.Actors,
                ReleaseDate = request.ReleaseDate,
                Budget = request.Budget
            };

            newRequest.SourceIpAddress = request.SourceIpAddress ?? Environment.GetEnvironmentVariable("CONTAINER_IP");

            if (!response.IsLast)
            {
                await _apiClient.UpdateMovie(newRequest, response.NextNodeIpAddress);
            }

            await SendOkAsync();
        }
    }
}
