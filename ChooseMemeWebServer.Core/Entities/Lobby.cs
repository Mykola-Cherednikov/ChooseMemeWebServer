namespace ChooseMemeWebServer.Core.Entities
{
    public class Lobby
    {
        public string Code { get; set; } = string.Empty;

        public List<Player> Players { get; set; } = new List<Player>();

        public GameState State { get; set; } = GameState.WaitingForPlayers;
    }

    public enum GameState
    {
        WaitingForPlayers
    }
}
