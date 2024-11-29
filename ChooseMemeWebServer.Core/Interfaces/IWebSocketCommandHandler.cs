using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface IWebSocketCommandHandler
    {
        public void Handle(string command, Player player, Lobby lobby);
    }
}
