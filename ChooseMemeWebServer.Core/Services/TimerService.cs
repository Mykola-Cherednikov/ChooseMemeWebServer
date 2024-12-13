using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Core.Interfaces;
using System.Collections.Concurrent;

namespace ChooseMemeWebServer.Core.Services
{
    public class TimerService : ITimerService
    {
        private static readonly ConcurrentDictionary<object, TimerData> activeTimers = new ConcurrentDictionary<object, TimerData>();

        public TimerData? AddTimer(object obj, int milliseconds, Action action, bool isUnremovable = true)
        {
            if (activeTimers.TryGetValue(obj, out TimerData? timerData))
            {
                return RemoveTimer(obj);
            }

            CancellationTokenSource cts = new CancellationTokenSource();

            Task timer = Task.Run(async () =>
            {
                await Task.Delay(milliseconds, cts.Token);
                action.Invoke();
                RemoveTimer(obj);
            });

            timerData = new()
            {
                CancellationTokenSource = cts,
                Task = timer,
                IsUnremovable = isUnremovable,
                Action = action
            };

            activeTimers.TryAdd(obj, timerData);
            return timerData;
        }

        public void ForceTimer(object obj)
        {
            var timerData = RemoveTimer(obj);

            if(timerData == null)
            {
                return;
            }

            timerData.Action.Invoke();
        }

        public TimerData? RemoveTimer(object obj)
        {
            if (!activeTimers.TryRemove(obj, out var timerData))
            {
                return null;
            }

            if (timerData.IsUnremovable)
            {
                return null;
            }

            timerData.CancellationTokenSource.Cancel();   

            return timerData;
        }
    }
}
