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

        public Task<Lobby?> PlayerJoinToLobby(string code, Player player);

        public Task<Lobby?> BotJoinToLobby(string code);

        public Task LeaveFromLobby(Player player);
    }
}
