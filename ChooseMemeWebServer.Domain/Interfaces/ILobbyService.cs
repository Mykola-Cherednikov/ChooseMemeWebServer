using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.LobbyService.Request;
using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface ILobbyService
    {
        public Lobby? GetLobby(string code);

        public List<Lobby> GetLobbies();

        public Task<Lobby> CreateLobby(string presetId);

        public Task CloseLobby(Lobby lobby, Server server);

        public Task AddServerToLobby(Lobby lobby, Server server);

        public Task<Lobby?> AddPlayerToLobby(string code, Player player);

        public Task<Lobby?> AddBotToLobby(string code);

        public Task<Lobby> LeaveFromLobby(Lobby lobby, Player player);

        public Task StartGame(StartGameRequestDTO data);

        public Task NextStatus(NextStatusRequestDTO data);
    }
}
