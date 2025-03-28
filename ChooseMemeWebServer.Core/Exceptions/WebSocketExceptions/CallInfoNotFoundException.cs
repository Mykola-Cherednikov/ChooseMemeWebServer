using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions
{
    public class CallInfoNotFoundException : ExpectedException
    {
        public CallInfoNotFoundException(string type) : base($"Call info not found by type: {type}")
        {
        }
    }
}
