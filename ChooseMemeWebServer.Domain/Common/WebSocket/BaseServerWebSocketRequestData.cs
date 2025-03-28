using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public class BaseServerWebSocketRequestData : IServerWebSocketRequestData
    {
        public Lobby Lobby { get; set; } = null!;

        public Server Server { get; set; } = null!;
    }
}
