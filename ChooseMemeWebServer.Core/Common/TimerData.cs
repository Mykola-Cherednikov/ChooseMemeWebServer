using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Common
{
    public class TimerData
    {
        public CancellationTokenSource CancellationTokenSource { get; set; } = null!;

        public Action Action { get; set; } = null!;

        public Task Task { get; set; } = null!;

        public bool IsRewriting { get; set; }
    }
}
