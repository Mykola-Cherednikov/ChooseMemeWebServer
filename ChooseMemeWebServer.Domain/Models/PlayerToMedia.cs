using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Models
{
    public class PlayerToMedia
    {
        public Player Player { get; set; } = null!;

        public Media Media { get; set; } = null!;

        public int Points { get; set; }
    }
}
