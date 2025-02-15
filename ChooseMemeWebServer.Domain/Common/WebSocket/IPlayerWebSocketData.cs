using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public interface IPlayerWebSocketData
    {
        public Lobby Lobby { get; set; }

        public Player Player { get; set; }
    }
}
