using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public class BaseWebSocketData : IWebSocketData
    {
        public Lobby Lobby { get; set; } = null!;

        public Player Player { get; set; } = null!;
    }
}
