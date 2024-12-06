using ChooseMemeWebServer.Domain;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.ImitateWebSocketCommand
{
    public class ImitateWebSocketCommandCommand : IRequest
    {
        public WebSocketData WebSocketData { get; set; } = null!;
    }
}
