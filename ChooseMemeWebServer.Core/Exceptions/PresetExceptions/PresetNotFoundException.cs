using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.PresetExceptions
{
    public class PresetNotFoundException : ExpectedException
    {
        public PresetNotFoundException() : base("Preset not found")
        {
        }
    }
}
