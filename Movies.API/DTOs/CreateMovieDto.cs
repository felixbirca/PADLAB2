using FastEndpoints;
using FluentValidation;
using Movies.API.Helpers;
using System.Text.Json.Serialization;

namespace Movies.API.DTOs
{
    public class CreateMovieDto : BaseRequest
    {
        public string Title { get; set; } = string.Empty;
        public long Budget { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly ReleaseDate { get; set; }
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

            RuleFor(x => x.ReleaseDate)
                .NotEmpty()
                .WithMessage("Release date is required");
        }
    }
}
