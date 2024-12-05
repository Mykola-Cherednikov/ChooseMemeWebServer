using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.PlayerJoinLobby
{
    public class PlayerJoinLobbyResponse
    {
        public bool IsSuccess { get; set; }

        public Lobby Lobby { get; set; } = null!;
    }
}
