using FastEndpoints;
using FluentValidation;

namespace Movies.API.DTOs
{
    public class CreateMovieDto
    {
        public string Title { get; set; } = string.Empty;
        public long Budget { get; set; }
        public DateOnly RleaseDate { get; set; }
        public ICollection<string> Actors { get; set; } = new List<string>();
    }

    public class CreateMovieDtoValidator : Validator<CreateMovieDto>
    {
        public CreateMovieDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required");

            RuleFor(x => x.Budget)
                .NotEmpty()
                .WithMessage("Budget is required");

            RuleFor(x => x.RleaseDate)
                .NotEmpty()
                .WithMessage("Release date is required");
        }
    }
}
