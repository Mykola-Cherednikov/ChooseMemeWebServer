using ChooseMemeWebServer.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IMediaService
    {
        public Task<Media> CreateMedia(IFormFile file, string presetId);

        public Task DeleteMedia(string id);
    }
}
