using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Movies.API.DTOs;
using Movies.API.Services;

namespace Movies.API.Endpoints
{
    [HttpGet("/api/movies/{movieId}")]
    [AllowAnonymous]
    public class GetMovieEndpoint : Endpoint<Guid>
    {
        private readonly IMoviesService _moviesService;

        public GetMovieEndpoint(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        public override async Task HandleAsync(Guid movieId, CancellationToken cancellationToken)
        {
            var result = await _moviesService.GetMovieById(movieId);

            if (result == null)
                AddError("Movie with this Id not found");

            ThrowIfAnyErrors();

            await SendOkAsync(new ViewMovieDto
            {
                Id = result.Id,
                Title = result.Title,
                Budget = result.Budget,
                ReleaseDate = result.ReleaseDate,
                Actors = result.Actors.Split(", ")
            });
        }
    }
}
