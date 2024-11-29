using ChooseMemeWebServer.Models;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.Interfaces
{
    public interface ILobbyService
    {
        public bool TryCreateLobby(WebSocket serverWebSocket);

        public Lobby ConnectToLobby(string code, Player player);


    }
}
