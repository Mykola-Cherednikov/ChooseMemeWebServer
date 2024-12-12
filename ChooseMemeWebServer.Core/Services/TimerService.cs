using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Core.Interfaces;

namespace ChooseMemeWebServer.Core.Services
{
    public class TimerService : ITimerService
    {
        private static readonly Dictionary<object, TimerData> activeTimers = new Dictionary<object, TimerData>();

        public void AddTimer(object obj, int milliseconds, Action action, bool isRewriting = true)
        {
            if (activeTimers.ContainsKey(obj))
            {
                if (!isRewriting)
                {
                    return;
                }

                RemoveTimer(obj);
            }

            CancellationTokenSource cts = new CancellationTokenSource();

            Task timer = Task.Run(async () =>
            {
                await Task.Delay(milliseconds, cts.Token);
                action.Invoke();
                RemoveTimer(obj);
            });

            TimerData timerData = new()
            {
                CancellationTokenSource = cts,
                Task = timer,
                IsRewriting = isRewriting,
                Action = action
            };

            activeTimers.Add(obj, timerData);
        }

        public void ForceTimer()
        {

        }

        public void RemoveTimer(object obj)
        {
            if (!activeTimers.TryGetValue(obj, out TimerData? timerData))
            {
                return;
            }

            timerData.CancellationTokenSource.Cancel();
            activeTimers.Remove(obj);
        }
    }
}
