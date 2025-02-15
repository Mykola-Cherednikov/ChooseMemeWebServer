using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public class BasePlayerWebSocketData : IPlayerWebSocketData
    {
        public Lobby Lobby { get; set; } = null!;

        public Player Player { get; set; } = null!;
    }
}
