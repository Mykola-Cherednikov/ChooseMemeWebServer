namespace ChooseMemeWebServer.Application.Models
{
    public class Server
    {
        public string Id { get; set; } = string.Empty;

        public Lobby Lobby { get; set; } = null;
    }
}
