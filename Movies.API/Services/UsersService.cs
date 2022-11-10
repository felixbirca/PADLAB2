using Microsoft.EntityFrameworkCore;
using Movies.API.Domain;
using Movies.API.Domain.Entities;

namespace Movies.API.Services
{
    public class UsersService : IUsersService
    {
        private MoviesDbContext _context;

        public UsersService(MoviesDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByName(string name)
        {
            return await _context.Users.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task AddUser(string name)
        {
            await _context.Users.AddAsync(new User { Name = name });
            await _context.SaveChangesAsync();
        }
    }
}
