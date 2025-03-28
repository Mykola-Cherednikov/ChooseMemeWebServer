using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions
{
    public class LobbyCodeIsNullOrEmptyException : ExpectedException
    {
        public LobbyCodeIsNullOrEmptyException() : base("Lobby code is null or empty")
        {
        }
    }
}
