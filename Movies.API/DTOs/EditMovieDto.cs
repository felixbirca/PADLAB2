﻿using FastEndpoints;
using FluentValidation;

namespace Movies.API.DTOs
{
    public class EditMovieDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public long? Budget { get; set; }
        public DateOnly? RleaseDate { get; set; }
        public ICollection<string>? Actors { get; set; } = new List<string>();
    }

    public class EditMovieDtoValidator : Validator<EditMovieDto>
    {
        public EditMovieDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required");
        }
    }
}
