using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions
{
    public class InstanceNotFoundException : ExpectedException
    {
        public InstanceNotFoundException(string className) : base($"Instance not found: {className}")
        {
        }
    }
}
