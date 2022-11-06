using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Movies.API.DTOs;
using Movies.API.Services;

namespace Movies.API.Endpoints
{
    [HttpPost("/api/movies/create")]
    [AllowAnonymous]
    public class CreateMovieEndpoint : Endpoint<CreateMovieDto>
    {
        private readonly IMoviesService _moviesService;

        public CreateMovieEndpoint(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        public override async Task HandleAsync(CreateMovieDto request, CancellationToken cancellationToken)
        {
            ThrowIfAnyErrors();

            await _moviesService.Create(request);
            await SendOkAsync();
        }
    }
}
