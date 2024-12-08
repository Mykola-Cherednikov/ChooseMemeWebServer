using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface ILobbyService
    {
        public Lobby? GetLobby(string code);

        public List<Lobby> GetLobbies();

        public Lobby CreateLobby();

        public Task<Lobby> CreateLobbyWithServer();

        public Task<Lobby?> PlayerJoinToLobby(string code, Player player);

        public Task<Lobby?> BotJoinToLobby(string code);

        public Task<Lobby> LeaveFromLobby(Player player);
    }
}
