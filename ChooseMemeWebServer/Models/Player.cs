using System.Net.WebSockets;

namespace ChooseMemeWebServer.Models
{
    public class Player
    {
        public string Username { get; set; } = string.Empty;

        public Lobby Lobby { get; set; } = null!;

        public WebSocket WebSocket { get; set; } = null!;

        public bool IsReady { get; set; } = false;
    }
}
