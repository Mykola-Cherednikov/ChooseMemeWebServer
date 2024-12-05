using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.AddBotToLobby
{
    public class AddBotToLobbyCommand : IRequest<AddBotToLobbyResponse>
    {
        public string Code { get; set; } = string.Empty;
    }
}
