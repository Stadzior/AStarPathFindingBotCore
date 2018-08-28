using AStarPathFindingBotCore.Domain;
using AStarPathFindingBotCore.Enums;
using AStarPathFindingBotCore.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebSocketSharp;

namespace AStarPathFindingBotCore.Interfaces
{
    public interface ILaxmarPlayer
    {
        int Id { get; set; }
        string Name { get; }
        int TimeoutInSeconds { get; }
        bool IsConnected { get; set; }
        JsonSerializerSettings SerializerSettings { get; }
        WebSocket WebSocket { get; }
        bool JoinGame();
        void ExitGame();
        bool MakeMove(MoveDirection moveDirection);
        MoveDirection ChooseDirection(Map map, List<Player> players);
    }
}
