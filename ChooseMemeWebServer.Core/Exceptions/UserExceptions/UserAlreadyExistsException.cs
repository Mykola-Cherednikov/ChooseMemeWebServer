using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.UserExceptions
{
    public class UserAlreadyExistsException : ExpectedException
    {
        public UserAlreadyExistsException() : base("User already exists")
        {
        }
    }
}
