using ChooseMemeWebServer.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public class BaseServerWebSocketRequestData : IServerWebSocketRequestData
    {
        public Lobby Lobby { get; set; } = null!;

        public Server Server { get; set; } = null!;
    }
}
