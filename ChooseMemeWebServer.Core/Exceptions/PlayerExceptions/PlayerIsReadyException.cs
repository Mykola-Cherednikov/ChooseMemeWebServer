using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.PlayerExceptions
{
    public class PlayerIsReadyException : ExpectedException
    {
        public PlayerIsReadyException(string username) : base($"Player '{username}' is ready")
        {
        }
    }
}
