using System.Net.WebSockets;

namespace ChooseMemeWebServer.Domain.Models
{
    public class Player
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Username { get; set; } = string.Empty;

        public Lobby Lobby { get; set; } = null!;

        public bool IsReady { get; set; } = false;

        public bool IsBot { get; set; } = false;

        public bool IsLeader { get; set; } = false;
    }
}
