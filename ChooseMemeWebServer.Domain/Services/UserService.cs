using ChooseMemeWebServer.Application.DTO.UserService.Request;
using ChooseMemeWebServer.Application.DTO.UserService.Response;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core;
using ChooseMemeWebServer.Core.Entities;
using ChooseMemeWebServer.Core.Exceptions.UserExceptions;
using Microsoft.EntityFrameworkCore;

namespace ChooseMemeWebServer.Application.Services
{
    public class UserService(IDbContext context, IHashService hashService, ITokenService tokenService) : IUserService
    {
        public async Task<UserResponseDTO> Login(UserRequestDTO data)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == data.Username);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (!hashService.VerifyPassword(data.Password, user.HashedPassword))
            {
                throw new UserNotFoundException();
            }

            return new UserResponseDTO() { Token = tokenService.GenerateToken(user) };
        }

        public async Task<UserResponseDTO> Register(UserRequestDTO data)
        {
            var isUserExists = await context.Users.CountAsync(u => u.Username == data.Username) == 1;

            if (isUserExists)
            {
                throw new UserAlreadyExistsException();
            }

            var user = new User()
            {
                Username = data.Username,
                HashedPassword = hashService.HashPassword(data.Password)
            };

            await context.Users.AddAsync(user);

            await context.SaveChangesAsync();

            return new UserResponseDTO() { Token = tokenService.GenerateToken(user) };
        }
    }
}
