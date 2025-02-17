using ChooseMemeWebServer.Application.Interfaces;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class DataService : IDataService
    {
        private List<string> AllowedFormats = new List<string>() { "mp4", "jpg", "png" };

        public List<string> GetAllowedFormats()
        {
            return AllowedFormats;
        }

        public string GetPresetFolderPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Presets");
        }
    }
}
