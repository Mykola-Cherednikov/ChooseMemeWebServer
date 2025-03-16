using ChooseMemeWebServer.Application.Interfaces;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class DataService : IDataService
    {
        private List<string> AllowedFormats = new List<string>() { ".mp4", ".jpg", ".png", ".gif" };

        public List<string> GetAllowedExtensions()
        {
            return AllowedFormats;
        }

        public string GetPresetFolderPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Presets");
        }
    }
}
