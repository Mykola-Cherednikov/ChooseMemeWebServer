using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.DTO
{
    public class LobbyDTO : IMapWith<Lobby>
    {
        public string Code { get; set; } = string.Empty;

        public List<PlayerDTO> Players { get; set; } = null!;

        public ServerDTO Server { get; set; } = null!;

        public string Status { get; set; } = string.Empty;

        public List<string> StatusQueue { get; set; } = new();

        public string PresetId { get; set; } = string.Empty;

        public List<PlayerToMediaDTO> PlayerOfferedMedia { get; set; } = new();

        public List<PlayerToMediaDTO> PlayerOfferedMediaHistory { get; set; } = new();

        public void Mapping(AutoMapper.Profile autoMapperProfile)
        {
            autoMapperProfile.CreateMap<Lobby, LobbyDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.StatusQueue, opt => opt.MapFrom(src => src.StatusQueue.Select(sq => sq.ToString()).ToList()));
        }
    }
}
