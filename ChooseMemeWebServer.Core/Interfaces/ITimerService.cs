namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface ITimerService
    {
        public void AddTimer(object obj, int milliseconds, Action action, bool isRewriting = true);

        public void RemoveTimer(object obj);
    }
}
