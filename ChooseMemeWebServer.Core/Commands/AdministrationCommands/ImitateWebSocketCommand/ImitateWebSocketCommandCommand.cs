using ChooseMemeWebServer.Core.Common;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.ImitateWebSocketCommand
{
    public class ImitateWebSocketCommandCommand : IRequest
    {
        public WebSocketData WebSocketData { get; set; } = null!;

        public string PlayerId { get; set; } = string.Empty;
    }
}
