using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.MediaExceptions
{
    public class MediaFileSizeException : ExpectedException
    {
        public MediaFileSizeException(long size) : base($"Media file size: {size} is above threshold")
        {
        }
    }
}
