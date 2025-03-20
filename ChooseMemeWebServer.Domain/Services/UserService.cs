using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core;
using ChooseMemeWebServer.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace ChooseMemeWebServer.Application.Services
{
    public class UserService(IConfiguration configuration, IDbContext context) : IUserService
    {
        public Task<User> Login(string username, string password)
        {
            //Add hash
            throw new NotImplementedException();
        }

        public Task<User> Register(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
