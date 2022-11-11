using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Movies.API.Services;

namespace Movies.API.Endpoints
{
    [HttpGet("/api/movies")]
    [AllowAnonymous]
    public class GetAllMoviesEndpoint : EndpointWithoutRequest
    {
        private readonly IMoviesService _moviesService;

        public GetAllMoviesEndpoint(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            await SendAsync(await _moviesService.GetAllMovies());
        }
    }
}
