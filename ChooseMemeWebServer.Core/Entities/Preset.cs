﻿using System.ComponentModel.DataAnnotations;

namespace ChooseMemeWebServer.Core.Entities
{
    public class Preset
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<Question> Questions { get; set; } = null!;

        public List<Media> Media { get; set; } = null!;
    }
}
