using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.DTO
{
    public class MediaDTO : IMapWith<Media>
    {
        public string Id { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public void Mapping(AutoMapper.Profile autoMapperProfile)
        {
            autoMapperProfile.CreateMap<Media, MediaDTO>();
        }
    }
}
