using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.ServerJoinLobby
{
    public class ServerJoinLobbyCommand : IRequest
    {
        public Lobby Lobby { get; set; } = null!;
    }
}
