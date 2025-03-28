using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.MediaExceptions
{
    public class MediaNotFoundException : ExpectedException
    {
        public MediaNotFoundException() : base("Media not found exception")
        {
        }
    }
}
