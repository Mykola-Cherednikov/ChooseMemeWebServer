﻿// <auto-generated />
using ChooseMemeWebServer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChooseMemeWebServer.Infrastructure.Migrations
{
    [DbContext(typeof(NpgsqlContext))]
    partial class NpgsqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChooseMemeWebServer.Core.Entities.Media", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PresetId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PresetId");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("ChooseMemeWebServer.Core.Entities.Preset", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Presets");
                });

            modelBuilder.Entity("ChooseMemeWebServer.Core.Entities.Question", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("PresetId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PresetId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ChooseMemeWebServer.Core.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChooseMemeWebServer.Core.Entities.Media", b =>
                {
                    b.HasOne("ChooseMemeWebServer.Core.Entities.Preset", "Preset")
                        .WithMany("Media")
                        .HasForeignKey("PresetId");

                    b.Navigation("Preset");
                });

            modelBuilder.Entity("ChooseMemeWebServer.Core.Entities.Preset", b =>
                {
                    b.HasOne("ChooseMemeWebServer.Core.Entities.User", "User")
                        .WithMany("Presets")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChooseMemeWebServer.Core.Entities.Question", b =>
                {
                    b.HasOne("ChooseMemeWebServer.Core.Entities.Preset", "Preset")
                        .WithMany("Questions")
                        .HasForeignKey("PresetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Preset");
                });

            modelBuilder.Entity("ChooseMemeWebServer.Core.Entities.Preset", b =>
                {
                    b.Navigation("Media");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("ChooseMemeWebServer.Core.Entities.User", b =>
                {
                    b.Navigation("Presets");
                });
#pragma warning restore 612, 618
        }
    }
}
