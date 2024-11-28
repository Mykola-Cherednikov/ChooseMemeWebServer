using System.Net.WebSockets;

namespace ChooseMemeWebServer.Models
{
    public class Lobby
    {
        public string Code { get; set; } = string.Empty;

        public List<Player> Players { get; set; } = null!;

        public WebSocket ServerWebSocket { get; set; } = null!;
    }
}
