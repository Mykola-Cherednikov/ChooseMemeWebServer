using ChooseMemeWebServer.Core.Common.Mappings;
using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.DTO
{
    public class LobbyDTO : IMapWith<Lobby>
    {
        public string Code { get; set; } = string.Empty;

        public List<PlayerDTO> Players { get; set; } = null!;

        public void Mapping(AutoMapper.Profile autoMapperProfile)
        {
            autoMapperProfile.CreateMap<Lobby, LobbyDTO>();
        }
    }
}
