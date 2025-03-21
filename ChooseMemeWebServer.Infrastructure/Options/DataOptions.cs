namespace ChooseMemeWebServer.Infrastructure.Options
{
    public class DataOptions
    {
        public const string SectionName = "Data";

        public string PresetsPath { get; set; } = string.Empty;

        public List<string> AllowedExtensions { get; set; } = new List<string>();
    }
}
