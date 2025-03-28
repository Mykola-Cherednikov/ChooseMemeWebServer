using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.UserExceptions
{
    public class UserNotFoundException : ExpectedException
    {
        public UserNotFoundException() : base("User not found")
        {
        }
    }
}
