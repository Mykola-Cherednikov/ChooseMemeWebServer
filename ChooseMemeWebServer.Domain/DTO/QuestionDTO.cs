using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Application.Models;
using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.DTO
{
    public class QuestionDTO : IMapWith<Question>
    {
        public string Text { get; set; } = string.Empty;

        public void Mapping(AutoMapper.Profile autoMapperProfile)
        {
            autoMapperProfile.CreateMap<Question, QuestionDTO>();
        }
    }
}
