using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core;
using ChooseMemeWebServer.Core.Entities;
using ChooseMemeWebServer.Core.Exceptions.UserExceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;

namespace ChooseMemeWebServer.Application.Services
{
    public class UserService(IDbContext context, IHashService hashService, ITokenService tokenService) : IUserService
    {
        public async Task<string> Login(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (!hashService.VerifyPassword(password, user.HashedPassword))
            {
                throw new UserNotFoundException();
            }

            return tokenService.GenerateToken(user);
        }

        public async Task<string> Register(string username, string password)
        {
            var isUserExists = await context.Users.CountAsync(u => u.Username == username) == 1;

            if (isUserExists)
            {
                throw new UserAlreadyExistsException();
            }

            var user = new User()
            {
                Username = username,
                HashedPassword = hashService.HashPassword(password)
            };

            await context.Users.AddAsync(user);

            await context.SaveChangesAsync();

            return tokenService.GenerateToken(user);
        }
    }
}
