using ChooseMemeWebServer.Core.DTO;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.GetLobbies
{
    public class GetLobbiesResponse
    {
        public List<LobbyDTO> Lobbies { get; set; } = null!;
    }
}
