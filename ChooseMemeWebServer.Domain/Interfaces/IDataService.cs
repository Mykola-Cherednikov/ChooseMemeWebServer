namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IDataService
    {
        public List<string> GetAllowedFormats();

        public string GetPresetFolderPath();
    }
}
