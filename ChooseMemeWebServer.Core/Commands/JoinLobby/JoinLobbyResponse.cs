using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Commands.JoinLobby
{
    public class JoinLobbyResponse
    {
        public Lobby Lobby { get; set; } = null!;
    }
}
