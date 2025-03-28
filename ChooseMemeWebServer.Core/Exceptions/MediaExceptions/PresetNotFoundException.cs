using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.MediaExceptions
{
    public class PresetNotFoundException : ExpectedException
    {
        public PresetNotFoundException() : base("Preset not found")
        {
        }
    }
}
