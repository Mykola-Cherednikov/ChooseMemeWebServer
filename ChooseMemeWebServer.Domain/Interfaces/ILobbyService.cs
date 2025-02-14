using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.LobbyService;
using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface ILobbyService
    {
        public Lobby? GetLobby(string code);

        public List<Lobby> GetLobbies();

        public Lobby CreateLobby();

        public Task AddServerToLobby(Lobby lobby, Server server);

        public Task<Lobby?> AddPlayerToLobby(string code, Player player);

        public Task<Lobby?> AddBotToLobby(string code);

        public Task<Lobby> LeaveFromLobby(LeaveFromLobbyDTO data);

        public Task StartGame(ForceStartGameDTO data);
    }
}
