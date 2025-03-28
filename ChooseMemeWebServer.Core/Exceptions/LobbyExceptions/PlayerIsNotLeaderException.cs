using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.LobbyExceptions
{
    public class PlayerIsNotLeaderException : ExpectedException
    {
        public PlayerIsNotLeaderException() : base("Player is not leader")
        {
        }
    }
}
