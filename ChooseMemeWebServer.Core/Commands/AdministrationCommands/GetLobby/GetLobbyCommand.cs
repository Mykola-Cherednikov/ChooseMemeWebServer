using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.GetLobby
{
    public class GetLobbyCommand : IRequest<GetLobbyResponse>
    {
        public string Code { get; set; } = string.Empty;
    }
}
