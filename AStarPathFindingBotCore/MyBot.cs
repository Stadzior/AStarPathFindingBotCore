using AStarPathFindingBotCore.Base;
using AStarPathFindingBotCore.Enums;
using AStarPathFindingBotCore.Interfaces;
using Newtonsoft.Json;
using System;

namespace AStarPathFindingBotCore
{
    public class MyBot : LaxmarPlayerBase, IBot
    {
        public MoveDirection LastMove { get; set; }
        public bool WasLastMoveInvalid { get; set; }

        public MyBot(string name, string webSocketUrl, JsonSerializerSettings serializerSettings = null) : base(name, webSocketUrl, serializerSettings)
        {
        }
        
        public override MoveDirection ChooseDirection()
        {
            return (MoveDirection) new Random().Next(4);
        }
    }
}
