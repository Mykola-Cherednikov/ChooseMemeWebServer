using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.LobbyService;
using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface ILobbyService
    {
        public Lobby? GetLobby(string code);

        public List<LobbyDTO> GetLobbies();

        public Lobby CreateLobby();

        public Task<Lobby> ServerJoinToLobby(Lobby lobby);

        public Task<Lobby?> AddPlayerToLobby(string code, Player player);

        public Task<Lobby?> AddBotToLobby(string code);

        public Task<Lobby> LeaveFromLobby(LeaveFromLobbyDTO data);

        public Task ForceStartGame(ForceStartGameDTO data);
    }
}
