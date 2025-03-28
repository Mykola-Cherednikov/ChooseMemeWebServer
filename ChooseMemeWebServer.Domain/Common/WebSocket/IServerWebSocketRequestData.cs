using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public interface IServerWebSocketRequestData
    {
        public Lobby Lobby { get; set; }

        public Server Server { get; set; }
    }
}
