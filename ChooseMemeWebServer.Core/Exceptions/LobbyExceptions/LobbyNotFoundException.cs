namespace ChooseMemeWebServer.Application.Exceptions
{
    public class LobbyNotFoundException : ExpectedException
    {
        public LobbyNotFoundException(string code) : base($"Lobby not found by code: {code}")
        {
        }
    }
}
