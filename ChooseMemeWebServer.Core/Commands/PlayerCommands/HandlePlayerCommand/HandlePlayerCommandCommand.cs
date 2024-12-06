using ChooseMemeWebServer.Core.Commands.PlayerCommands;

namespace ChooseMemeWebServer.Core.Commands.PlayerCommands.HandlePlayerCommand
{
    public class HandlePlayerCommandCommand : PlayerBaseCommand
    {
        public string WebSocketData { get; set; } = string.Empty;
    }
}
