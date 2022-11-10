using Movies.API.Domain.Entities;

namespace Movies.API.Services
{
    public interface IUsersService
    {
        Task AddUser(string name);
        Task<User?> GetUserByName(string name);
    }
}