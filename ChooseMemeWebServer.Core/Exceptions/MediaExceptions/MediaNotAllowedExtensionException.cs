using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.MediaExceptions
{
    public class MediaNotAllowedExtensionException : ExpectedException
    {
        public MediaNotAllowedExtensionException(string path) : base($"Media format is not allowed: {path}")
        {
        }
    }
}
