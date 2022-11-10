using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Movies.API.Services;

namespace Movies.API.Endpoints
{
    [HttpDelete("/api/movies/delete/{movieId}")]
    [AllowAnonymous]
    public class DeleteMovieEndpoint : Endpoint<Guid>
    {
        private readonly IMoviesService _moviesService;

        public DeleteMovieEndpoint(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        public override async Task HandleAsync(Guid movieId, CancellationToken cancellationToken)
        {
            var existingMovie = await _moviesService.GetMovieById(movieId);

            if (existingMovie == null)
                AddError("Movie with this Id does not exist.");

            ThrowIfAnyErrors();

            await _moviesService.DeleteMovie(movieId);
            await SendOkAsync();
        }
    }
}
