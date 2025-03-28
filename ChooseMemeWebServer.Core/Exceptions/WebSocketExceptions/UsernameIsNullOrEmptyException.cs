using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions
{
    public class UsernameIsNullOrEmptyException : ExpectedException
    {
        public UsernameIsNullOrEmptyException() : base("Username is null or empty")
        {
        }
    }
}
