namespace ChooseMemeWebServer.Application.Models
{
    public class Lobby
    {
        public string Code { get; set; } = string.Empty;

        public List<Player> Players { get; set; } = new List<Player>();

        public Server Server { get; set; } = null!;

        public bool IsAllPlayersReady { get => Players.All(p => p.IsReady); }
    }
}
