using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.JoinLobby
{
    public class JoinLobbyCommand : IRequest<JoinLobbyResponse>
    {
        public string LobbyCode { get; set; } = string.Empty;

        public Player Player { get; set; } = null!;
    }
}
