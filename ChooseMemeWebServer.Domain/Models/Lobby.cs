namespace ChooseMemeWebServer.Domain.Models
{
    public class Lobby
    {
        public string Code { get; set; } = string.Empty;

        public List<Player> Players { get; set; } = null!;

        public Lobby(string code)
        {
            Code = code;
            Players = new List<Player>();
        }

        public void PlayerJoin(Player player)
        {
            Players.Add(player);

            player.Lobby = this;
        }

        public void PlayerLeave(Player player)
        {
            Players.Remove(player);
        }
    }
}
