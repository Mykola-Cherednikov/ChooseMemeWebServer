using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.DTO
{
    public class PresetDTO : IMapWith<Preset>
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public bool IsPrivate { get; set; }

        public void Mapping(AutoMapper.Profile autoMapperProfile)
        {
            autoMapperProfile.CreateMap<Preset, PresetDTO>();
        }
    }
}
