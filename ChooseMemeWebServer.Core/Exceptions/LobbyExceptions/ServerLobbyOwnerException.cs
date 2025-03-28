using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.LobbyExceptions
{
    public class ServerLobbyOwnerException : ExpectedException
    {
        public ServerLobbyOwnerException() : base("Server is not owner of this lobby")
        {
        }
    }
}
