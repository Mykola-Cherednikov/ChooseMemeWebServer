using MediatR;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreateLobbyWithServer
{
    public class CreateLobbyWithServerCommand : IRequest<CreateLobbyWithServerResponse>
    {
        public WebSocket WebSocket { get; set; } = null!;
    }
}
