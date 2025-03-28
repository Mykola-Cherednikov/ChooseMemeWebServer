using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.PlayerExceptions
{
    public class PlayerNotFoundException : ExpectedException
    {
        public PlayerNotFoundException() : base("Player not found")
        {
        }
    }
}
