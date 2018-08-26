using AStarPathFindingBotCore.Domain;
using AStarPathFindingBotCore.Enums;
using Newtonsoft.Json;
using WebSocketSharp;

namespace AStarPathFindingBotCore.Interfaces
{
    public interface ILaxmarPlayer
    {
        int Id { get; set; }
        bool HasFlag { get; set; }
        bool IsAlive { get; set; }
        string Name { get; }
        int MaxMovesPerRound { get; set; }
        BasePosition BasePosition { get; set; }
        int ViewRange { get; set; }
        int MovesLeft { get; set; }
        int X { get; set; }
        int Y { get; set; }

        int TimeoutInSeconds { get; }
        bool IsConnected { get; set; }
        JsonSerializerSettings SerializerSettings { get; }
        WebSocket WebSocket { get; }
        bool JoinGame();
        void ExitGame();
        bool MakeMove();
        MoveDirection ChooseDirection();
    }
}
