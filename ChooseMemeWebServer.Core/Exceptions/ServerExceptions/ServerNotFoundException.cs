using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.ServerExceptions
{
    public class ServerNotFoundException : ExpectedException
    {
        public ServerNotFoundException() : base("Server not found")
        {
        }
    }
}
