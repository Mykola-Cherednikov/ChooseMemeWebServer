using ChooseMemeWebServer.Domain.Models;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface ILobbyService
    {
        public bool TryCreateLobby(WebSocket serverWebSocket);

        public Lobby ConnectToLobby(string code, Player player);


    }
}
