using ChooseMemeWebServer.Core;
using ChooseMemeWebServer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChooseMemeWebServer.Infrastructure
{
    public class NpgsqlContext : DbContext, IDbContext
    {
        public NpgsqlContext(DbContextOptions<NpgsqlContext> options) : base(options)
        {
        }

        public DbSet<Media> Medias { get; set; }

        public DbSet<Preset> Presets { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
