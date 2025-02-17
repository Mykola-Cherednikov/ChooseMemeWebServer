using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IPresetService
    {
        public Task<Preset> CreatePreset(string name);

        public Task<Preset> GetPreset(string id);

        public void DeletePreset(string id);
    }
}
