using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.DTO
{
    public class PlayerToMediaDTO : IMapWith<PlayerToMedia>
    {
        public PlayerDTO Player { get; set; } = null!;

        public MediaDTO Media { get; set; } = null!;

        public int Points { get; set; }

        public void Mapping(AutoMapper.Profile autoMapperProfile)
        {
            autoMapperProfile.CreateMap<PlayerToMedia, PlayerToMediaDTO>();
        }
    }
}
