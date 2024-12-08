using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreateLobbyFromWebSocket
{
    public class CreateLobbyFromWebSocketResponse
    {
        public Lobby Lobby { get; set; } = null!;
    }
}
