using System.Text.Json;

namespace ChooseMemeWebServer.Core.Common
{
    public class WebSocketData
    {
        public WebSocketData() { }

        public WebSocketData(CommandType commandType)
        {
            string? typeName = Enum.GetName(typeof(CommandType), commandType);

            if (typeName == null)
            {
                throw new ArgumentNullException(typeName);
            }

            CommandTypeName = commandType.ToString();
        }

        public WebSocketData(CommandType commandType, object data)
        {
            string? typeName = Enum.GetName(typeof(CommandType), commandType);

            if (typeName == null)
            {
                throw new ArgumentNullException(typeName);
            }

            CommandTypeName =  typeName;
            Data = JsonSerializer.Serialize(data);
        }

        public string CommandTypeName { get; set; } = string.Empty;

        public string Data { get; set; } = string.Empty;
    }

    public enum CommandType
    {
        CreateLobby,
        PlayerJoin,
        PlayerLeave,
        NewLeader,
        PlayerIsReady
    }
}
