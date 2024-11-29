using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface IWebSocketCommandService
    {
        public void Handle(string command, Player player, Lobby lobby);
    }
}
