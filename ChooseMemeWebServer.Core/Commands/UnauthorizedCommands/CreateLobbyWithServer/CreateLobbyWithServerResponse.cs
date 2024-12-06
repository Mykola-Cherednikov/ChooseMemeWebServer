using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreateLobbyWithServer
{
    public class CreateLobbyWithServerResponse
    {
        public bool Success { get; set; }

        public Lobby Lobby { get; set; } = null!;

        public LobbyDTO LobbyDTO { get; set; } = null!;
    }
}
