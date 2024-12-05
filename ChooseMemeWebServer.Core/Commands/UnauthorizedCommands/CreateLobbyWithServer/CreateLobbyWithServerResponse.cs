using ChooseMemeWebServer.Core.DTO;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreateLobbyWithServer
{
    public class CreateLobbyWithServerResponse
    {
        public bool Success { get; set; }

        public LobbyDTO Lobby { get; set; } = null!;
    }
}
