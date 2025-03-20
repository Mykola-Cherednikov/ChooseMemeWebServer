using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IUserService
    {
        public Task<User> Login(string username, string password);

        public Task<User> Register(string username, string password);
    }
}
