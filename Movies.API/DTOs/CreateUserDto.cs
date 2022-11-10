using FastEndpoints;
using FluentValidation;

namespace Movies.API.DTOs
{
    public class CreateUserDto
    {
        public string Name { get; set; }
    }

    public class CreateUserDtoValidator : Validator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }
}
