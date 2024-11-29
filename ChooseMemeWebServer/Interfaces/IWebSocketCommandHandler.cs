using ChooseMemeWebServer.Models;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.Interfaces
{
    public interface IWebSocketCommandHandler
    {
        public void Handle(string command, Player player, Lobby lobby);
    }
}
