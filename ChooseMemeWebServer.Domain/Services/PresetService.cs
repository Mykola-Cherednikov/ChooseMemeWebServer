using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core;
using ChooseMemeWebServer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChooseMemeWebServer.Application.Services
{
    public class PresetService(IDbContext context) : IPresetService
    {
        public async Task<Preset> CreatePreset(string name)
        {
            var preset = new Preset()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name
            };

            await context.Presets.AddAsync(preset);

            await context.SaveChangesAsync();

            return preset;
        }

        public async Task<Preset> GetPreset(string id)
        {
            var preset = await context.Presets.FirstOrDefaultAsync(p => p.Id == id);

            if (preset == null)
            {
                throw new Exception();
            }

            return preset;
        }

        public void DeletePreset(string id)
        {
            throw new NotImplementedException();
        }
    }
}
