﻿using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core;
using ChooseMemeWebServer.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ChooseMemeWebServer.Application.Services
{
    public class MediaService(IDbContext context, IDataService dataService) : IMediaService
    {
        public async Task<Media> CreateMedia(IFormFile file, string presetId)
        {
            if (!dataService.GetAllowedFormats().Contains(Path.GetExtension(file.FileName)))
            {
                return null;
            }

            if (file.Length / 1_048_576 > 10) // Move to config
            {
                return null;
            }

            var fileName = file.FileName.Substring(0, Math.Min(40, file.FileName.Length));

            var filePath = Path.Combine(dataService.GetPresetFolderPath(), presetId, fileName);

            if (Path.Exists(filePath))
            {
                return null;
            }

            var preset = await context.Presets.FirstOrDefaultAsync(p => p.Id == presetId);

            if (preset == null)
            {
                return null;
            }

            var media = new Media()
            {
                Id = Guid.NewGuid().ToString(),
                FileName = fileName,
                Preset = preset
            };

            using (var savedFile = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(savedFile);
            }

            await context.Medias.AddAsync(media);

            await context.SaveChangesAsync();

            return media;
        }

        public async Task DeleteMedia(string id)
        {
            var media = await context.Medias.Include(m => m.Preset).FirstOrDefaultAsync(m => m.Id == id);

            if (media == null)
            {
                return;
            }

            File.Delete(Path.Combine(dataService.GetPresetFolderPath(), media.Preset.Id, media.FileName));

            context.Medias.Remove(media);

            await context.SaveChangesAsync();
        }
    }
}
