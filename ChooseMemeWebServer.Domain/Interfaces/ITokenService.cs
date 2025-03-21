using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
