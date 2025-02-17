using ChooseMemeWebServer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChooseMemeWebServer.Core
{
    public interface IDbContext
    {
        public DbSet<Media> Medias { get; set; }

        public DbSet<Preset> Presets { get; set; }

        public DbSet<Question> Questions { get; set; }

        public int SaveChanges();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
