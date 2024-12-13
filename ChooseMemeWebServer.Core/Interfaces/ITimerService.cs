using ChooseMemeWebServer.Core.Common;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface ITimerService
    {
        public TimerData? AddTimer(object obj, int milliseconds, Action action, bool isRewriting = true);

        public void ForceTimer(object obj);

        public TimerData? RemoveTimer(object obj);
    }
}
