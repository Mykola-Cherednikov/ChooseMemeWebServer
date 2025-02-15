using AutoMapper;
using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.DTO
{
    public class ServerDTO : IMapWith<Server>
    {
        public string Id { get; set; } = null!;

        public void Mapping(Profile autoMapperProfile)
        {
            autoMapperProfile.CreateMap<Server, ServerDTO>();
        }
    }
}
