using ChooseMemeWebServer.Domain.Models;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface ILobbyService
    {
        public Lobby? GetLobby(string code);

        public List<Lobby> GetLobbies();

        public Lobby CreateLobby();

        public Lobby CreateLobbyWithServer(WebSocket serverWebSocket);

        public bool TryPlayerJoinToLobby(string code, Player player, out Lobby? lobby);

        public bool TryBotJoinToLobby(string code, out Lobby? lobby);
    }
}
