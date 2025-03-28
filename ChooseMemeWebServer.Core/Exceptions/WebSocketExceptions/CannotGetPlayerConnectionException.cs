using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions
{
    public class CannotGetPlayerConnectionException : ExpectedException
    {
        public CannotGetPlayerConnectionException(string username) : base($"Cannot get connetion with player: {username}")
        {
        }
    }
}
