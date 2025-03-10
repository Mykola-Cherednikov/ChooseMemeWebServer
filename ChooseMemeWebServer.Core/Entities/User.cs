using System.ComponentModel.DataAnnotations;

namespace ChooseMemeWebServer.Core.Entities
{
    public class User
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string HashPassword { get; set; } = string.Empty;

        public List<Preset> Presets { get; set; } = null!;
    }
}
