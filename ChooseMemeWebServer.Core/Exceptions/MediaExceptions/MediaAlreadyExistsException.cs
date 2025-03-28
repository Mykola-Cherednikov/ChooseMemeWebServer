using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.MediaExceptions
{
    public class MediaAlreadyExistsException : ExpectedException
    {
        public MediaAlreadyExistsException() : base($"Media with this name already exists")
        {
        }
    }
}
