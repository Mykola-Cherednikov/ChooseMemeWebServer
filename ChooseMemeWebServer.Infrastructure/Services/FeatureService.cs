using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core;
using Microsoft.EntityFrameworkCore;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class FeatureService(IDbContext context, IDataService dataService) : IFeatureService
    {
        public async Task ClearUnusedMedia()
        {
            int batchSize = 1000;
            int page = 0;

            while (true)
            {
                var medias = await context.Medias
                    .Include(m => m.Preset)
                    .OrderBy(m => m.Id)
                    .Skip(page * batchSize)
                    .Take(batchSize)
                    .ToListAsync();

                if (!medias.Any())
                    break;

                var unusedMedia = medias
                    .Where(media => !File.Exists(Path.Combine(dataService.GetPresetFolderPath(), media.Preset.Id, media.FileName)))
                    .ToList();

                if (unusedMedia.Any())
                {
                    context.Medias.RemoveRange(unusedMedia);
                    await context.SaveChangesAsync();
                }

                page++;
            }
        }
    }
}
