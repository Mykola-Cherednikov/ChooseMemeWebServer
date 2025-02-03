using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.DTO
{
    public class PlayerDTO : IMapWith<Player>
    {
        public string Id { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public bool IsReady { get; set; } = false!;

        public void Mapping(AutoMapper.Profile autoMapperProfile)
        {
            autoMapperProfile.CreateMap<Player, PlayerDTO>();
        }
    }
}
