using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public interface IPlayerWebSocketRequestData
    {
        public Lobby Lobby { get; set; }

        public Player Player { get; set; }
    }
}
