using ChooseMemeWebServer.Core.DTO;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.GetLobby
{
    public class GetLobbyResponse
    {
        public LobbyDTO Lobby { get; set; } = null!;
    }
}
