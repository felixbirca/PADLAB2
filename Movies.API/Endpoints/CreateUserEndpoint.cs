using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Movies.API.DTOs;
using Movies.API.Services;

namespace Movies.API.Endpoints
{
    [HttpPost("api/users/create")]
    [AllowAnonymous]
    public class CreateUserEndpoint : Endpoint<CreateUserDto>
    {
        private readonly IUsersService _usersService;

        public CreateUserEndpoint(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public override async Task HandleAsync(CreateUserDto req, CancellationToken ct)
        {
            var existingUser =await _usersService.GetUserByName(req.Name);

            if (existingUser == null)
                AddError("User with this name already exists");

            ThrowIfAnyErrors();

            await _usersService.AddUser(req.Name);
        }
    }
}
