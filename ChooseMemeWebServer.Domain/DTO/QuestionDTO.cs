using ChooseMemeWebServer.Application.Common.Mappings;
using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.DTO
{
    public class QuestionDTO : IMapWith<Question>
    {
        public string Id { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public void Mapping(AutoMapper.Profile autoMapperProfile)
        {
            autoMapperProfile.CreateMap<Question, QuestionDTO>();
        }
    }
}
