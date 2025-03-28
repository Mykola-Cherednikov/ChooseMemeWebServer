using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.LobbyExceptions
{
    public class NextLobbyStatusNotFoundException : ExpectedException
    {
        public NextLobbyStatusNotFoundException(
            string statusName) : base($"Next lobby status '{statusName}' does not exists")
        {
        }
    }
}
