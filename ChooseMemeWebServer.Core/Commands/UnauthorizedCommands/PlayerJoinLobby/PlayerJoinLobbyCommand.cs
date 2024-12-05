using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.PlayerJoinLobby
{
    public class PlayerJoinLobbyCommand : IRequest<PlayerJoinLobbyResponse>
    {
        public string LobbyCode { get; set; } = string.Empty;

        public Player Player { get; set; } = null!;
    }
}
