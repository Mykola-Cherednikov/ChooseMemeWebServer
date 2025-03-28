using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions
{
    public class CannotGetServerConnectionException : ExpectedException
    {
        public CannotGetServerConnectionException() : base("Cannot connect to server")
        {
        }
    }
}
