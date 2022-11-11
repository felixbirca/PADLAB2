using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Movies.API.DTOs;
using Movies.API.Services;

namespace Movies.API.Endpoints
{
    [HttpPost("/api/movies/edit")]
    [AllowAnonymous]
    public class EditMovieEndpoint : Endpoint<EditMovieDto>
    {
        private readonly IMoviesService _moviesService;

        public EditMovieEndpoint(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        public override async Task HandleAsync(EditMovieDto request, CancellationToken cancellationToken)
        {
            var existingMovie = await _moviesService.GetMovieById(request.Id);

            if (existingMovie == null)
                AddError("Movie with this Id does not exist.");

            ThrowIfAnyErrors();

            await _moviesService.EditMovie(request);
            await SendOkAsync();
        }
    }
}
