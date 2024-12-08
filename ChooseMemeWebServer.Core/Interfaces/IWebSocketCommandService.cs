using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface IWebSocketCommandService
    {
        public void Handle(WebSocketData data, Player player, Lobby lobby);
    }
}
