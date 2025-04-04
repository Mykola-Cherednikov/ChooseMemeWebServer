﻿using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core;
using ChooseMemeWebServer.Core.Entities;
using ChooseMemeWebServer.Core.Exceptions.MediaExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ChooseMemeWebServer.Application.Services
{
    public class MediaService(IDbContext context, IDataService dataService) : IMediaService
    {
        public async Task<Media> CreateMedia(IFormFile file, string presetId)
        {
            var extension = Path.GetExtension(file.FileName);

            if (!dataService.GetAllowedExtensions().Contains(extension))
            {
                throw new MediaNotAllowedExtensionException(file.FileName);
            }

            if (file.Length / 1_048_576 > 10) // Move to config
            {
                throw new MediaFileSizeException(file.Length);
            }

            var fileName = CleanFileName(Path.GetFileNameWithoutExtension(file.FileName), extension);

            var filePath = Path.Combine(dataService.GetPresetFolderPath(), presetId, fileName);

            if (Path.Exists(filePath))
            {
                throw new MediaAlreadyExistsException();
            }

            var preset = await context.Presets.FirstOrDefaultAsync(p => p.Id == presetId);

            if (preset == null)
            {
                throw new PresetNotFoundException();
            }

            var media = new Media()
            {
                Id = Guid.NewGuid().ToString(),
                FileName = fileName,
                Preset = preset
            };

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            using (var savedFile = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(savedFile);
            }

            await context.Medias.AddAsync(media);

            await context.SaveChangesAsync();

            return media;
        }

        private string CleanFileName(string fileName, string extension)
        {
            char[] allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._-".ToCharArray();
            string cleanedFileName = new string(fileName.Where(c => allowedChars.Contains(c)).ToArray());
            cleanedFileName = cleanedFileName.Substring(0, Math.Min(40, cleanedFileName.Length)) + extension;

            return cleanedFileName;
        }

        public async Task DeleteMedia(string mediaId)
        {
            var media = await context.Medias.Include(m => m.Preset).FirstOrDefaultAsync(m => m.Id == mediaId);

            if (media == null)
            {
                throw new MediaNotFoundException();
            }

            File.Delete(Path.Combine(dataService.GetPresetFolderPath(), media.Preset.Id, media.FileName));

            context.Medias.Remove(media);

            await context.SaveChangesAsync();
        }

        public async Task<List<Media>> GetAllMedia(string presetId)
        {
            var preset = await context.Presets.Include(p => p.Media).FirstOrDefaultAsync(p => p.Id == presetId);

            if (preset == null)
            {
                throw new PresetNotFoundException();
            }

            return preset.Media;
        }

        public async Task<Media> GetOneMedia(string mediaId)
        {
            var media = await context.Medias.FirstOrDefaultAsync(m => m.Id == mediaId);

            if (media == null)
            {
                throw new MediaNotFoundException();
            }

            return media;
        }
    }
}
