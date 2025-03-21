using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace ChooseMemeWebServer.Core.Entities
{
    public class User
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string HashedPassword { get; set; } = string.Empty;

        public List<Preset> Presets { get; set; } = null!;
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(p => p.Username).IsUnique();
        }
    }
}
