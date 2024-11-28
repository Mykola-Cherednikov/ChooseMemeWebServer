using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace ChooseMemeWebServer
{
    public class ConnectionController : ControllerBase
    {
        [Route("/wsClient")]
        public async Task<IActionResult> ClientConntect(string username, string lobbyCode)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Username is null or empty");
                }

                if (string.IsNullOrEmpty(lobbyCode))
                {
                    return BadRequest("LobbyCode is null or empty");
                }

                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                // TODO: Add Client to lobby connection

                await Listen(webSocket);
                return Ok();
            }
            else
            {
                return BadRequest("Support only WebSocket request");
            }
        }

        [Route("/wsServer")]
        public async Task<IActionResult> ServerConnect()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                // TODO: Add Server lobby creation

                await Listen(webSocket);
                return Ok();
            }
            else
            {
                return BadRequest("Support only WebSocket request");
            }
        }

        private static async Task Listen(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = new WebSocketReceiveResult(0, WebSocketMessageType.Text, true);

            while (!receiveResult.CloseStatus.HasValue)
            {
                receiveResult = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

                // TODO: Add Websocket command handler
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }
    }
}
