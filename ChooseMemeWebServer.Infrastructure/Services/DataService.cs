using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class DataService(IOptions<DataOptions> dataOptions) : IDataService
    {
        public List<string> GetAllowedExtensions()
        {
            return dataOptions.Value.AllowedExtensions;
        }

        public string GetPresetFolderPath()
        {
            return dataOptions.Value.PresetsPath ?? "C:/Presets";
        }
    }
}
