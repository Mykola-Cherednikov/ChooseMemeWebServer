namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IHelperService
    {
        public void Shuffle<T>(IList<T> list);
    }
}
