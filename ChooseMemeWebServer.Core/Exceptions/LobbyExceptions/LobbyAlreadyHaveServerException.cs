using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.LobbyExceptions
{
    public class LobbyAlreadyHaveServerException : ExpectedException
    {
        public LobbyAlreadyHaveServerException() : base("Lobby already have server")
        {
        }
    }
}
