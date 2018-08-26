using Newtonsoft.Json;
using WebSocketSharp;

namespace AStarPathFindingBotCore.Interfaces
{
    public interface IBot
    {
        int TimeoutInSeconds { get; }
        bool IsConnected { get; set; }
        JsonSerializerSettings SerializerSettings { get; }
        WebSocket WebSocket { get; }
        string Name { get; }
        bool JoinGame();
        void ExitGame();

    }
}
