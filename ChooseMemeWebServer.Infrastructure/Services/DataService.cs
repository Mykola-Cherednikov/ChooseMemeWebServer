using ChooseMemeWebServer.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class DataService(IConfiguration configuration) : IDataService
    {
        private static List<string>? AllowedExtensions;

        public List<string> GetAllowedExtensions()
        {
            if (AllowedExtensions == null)
            {
                AllowedExtensions = new List<string>((configuration.GetSection("AllowedExtensions").Value ?? "").Split(";"));
            }

            return AllowedExtensions;
        }

        public string GetPresetFolderPath()
        {
            return configuration.GetSection("PresetsPath").Value ?? "C:/Presets";
        }
    }
}
