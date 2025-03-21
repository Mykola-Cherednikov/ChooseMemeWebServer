using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IUserService
    {
        public Task<string> Login(string username, string password);

        public Task<string> Register(string username, string password);
    }
}
