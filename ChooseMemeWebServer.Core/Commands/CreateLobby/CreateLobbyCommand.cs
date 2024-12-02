using MediatR;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.Core.Commands.CreateLobby
{
    public class CreateLobbyCommand : IRequest<CreateLobbyResponse>
    {
        public WebSocket WebSocket { get; set; } = null!;
    }
}
