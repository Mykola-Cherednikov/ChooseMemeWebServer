using ChooseMemeWebServer.Core;
using ChooseMemeWebServer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChooseMemeWebServer.Infrastructure
{
    public class NpgsqlContext : DbContext, IDbContext
    {
        public DbSet<Media> Medias { get; set; } = null!;

        public DbSet<Preset> Presets { get; set; } = null!;

        public DbSet<Question> Questions { get; set; } = null!;
    }
}
