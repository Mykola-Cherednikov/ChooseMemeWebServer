using System.Net.WebSockets;

namespace ChooseMemeWebServer.Domain.Models
{
    public class Player
    {
        public string Id { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public Lobby Lobby { get; set; } = null!;

        public WebSocket WebSocket { get; set; } = null!;

        public bool IsReady { get; set; } = false;

        public bool IsBot { get; set; } = false;
    }
}
