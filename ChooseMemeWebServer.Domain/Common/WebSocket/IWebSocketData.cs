using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public interface IWebSocketData
    {
        public Lobby Lobby { get; set; }

        public Player Player { get; set; }
    }
}
