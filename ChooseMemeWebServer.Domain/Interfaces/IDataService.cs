namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IDataService
    {
        public List<string> GetAllowedExtensions();

        public string GetPresetFolderPath();
    }
}
