using System.ComponentModel.DataAnnotations;

namespace ChooseMemeWebServer.Core.Entities
{
    public class Media
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public Preset Preset { get; set; } = null!;
    }
}
