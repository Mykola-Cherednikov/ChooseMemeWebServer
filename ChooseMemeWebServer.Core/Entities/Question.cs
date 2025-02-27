using System.ComponentModel.DataAnnotations;

namespace ChooseMemeWebServer.Core.Entities
{
    public class Question
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public Preset Preset { get; set; } = null!;
    }
}
