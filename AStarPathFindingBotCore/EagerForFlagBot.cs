using AStarPathFindingBotCore.Base;
using AStarPathFindingBotCore.Domain;
using AStarPathFindingBotCore.Enums;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using AStarPathFindingBotCore.Services.Interfaces;

namespace AStarPathFindingBotCore
{
    /// <summary>
    /// Tactics: Go for the flag without any further analisys, if move cost is greater then moves left then wait. When flag is taken then return to the base.
    /// </summary>
    public class EagerForFlagBot : LaxmarPlayerBase
    {        
        public EagerForFlagBot(string name, string webSocketUrl, IPathFindingService pathFindingService, JsonSerializerSettings serializerSettings = null) : base(name, webSocketUrl, pathFindingService, serializerSettings)
        {
        }

        public override MoveDirection ChooseDirection(Map map, List<Player> players)
        {
            var me = players.Single(x => x.Id == Id);
            var myPosition = (X: me.X, Y: me.Y);
            var targetPosition = me.HasFlag ? (X: me.BasePosition.X, Y: me.BasePosition.Y) : DetermineFlagPosition((map.Width, map.Height), players);
            var (X, Y) = PathFindingService.FindBestPath(map, myPosition, targetPosition).First();

            if (me.MovesLeft < map.Fields[X][Y])
                return MoveDirection.NoMove;

            return X > myPosition.X ? MoveDirection.Right :
                X < myPosition.X ? MoveDirection.Left :
                Y > myPosition.Y ? MoveDirection.Down :
                Y < myPosition.Y ? MoveDirection.Up : MoveDirection.NoMove;
        }

        private (int X, int Y) DetermineFlagPosition((int Width, int Height) mapSize, List<Player> players)
        {
            var playerHoldingTheFlag = players.SingleOrDefault(x => x.HasFlag);
            return playerHoldingTheFlag != null ? (X: playerHoldingTheFlag.X, Y: playerHoldingTheFlag.Y) : (X: (int)Math.Floor((double)mapSize.Width / 2), Y: (int)Math.Floor((double)mapSize.Width / 2));
        }

    }
}
