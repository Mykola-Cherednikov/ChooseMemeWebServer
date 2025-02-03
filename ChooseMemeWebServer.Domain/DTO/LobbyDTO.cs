using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.DTO
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
