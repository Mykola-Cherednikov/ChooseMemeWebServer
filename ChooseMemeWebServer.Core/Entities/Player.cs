namespace ChooseMemeWebServer.Core.Entities
{
    public class Player
    {
        public string Id { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public Lobby Lobby { get; set; } = null!;

        public bool IsReady { get; set; } = false;

        public bool IsBot { get; set; } = false;

        public bool IsLeader { get; set; } = false;
    }
}
