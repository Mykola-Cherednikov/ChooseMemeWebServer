using ChooseMemeWebServer.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public interface IServerWebSocketData
    {
        public Lobby Lobby { get; set; }

        public Server Server { get; set; }
    }
}
