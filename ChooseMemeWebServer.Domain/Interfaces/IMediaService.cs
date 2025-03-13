using ChooseMemeWebServer.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IMediaService
    {
        public Task<List<Media>> GetAllMedia(string presetId);

        public Task<Media> GetOneMedia(string presetId, string mediaId);

        public Task<Media> CreateMedia(IFormFile file, string presetId);

        public Task DeleteMedia(string mediaId);
    }
}
