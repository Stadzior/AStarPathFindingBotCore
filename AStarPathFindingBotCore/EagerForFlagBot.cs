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
            var nextPosition = PathFindingService.FindBestPath(GenerateNodeMap(map, targetPosition), myPosition, targetPosition).First();

            if (me.MovesLeft < nextPosition.MoveCost)
                return MoveDirection.NoMove;

            return nextPosition.X > myPosition.X ? MoveDirection.Right :
                nextPosition.X < myPosition.X ? MoveDirection.Left :
                nextPosition.Y > myPosition.Y ? MoveDirection.Down :
                nextPosition.Y < myPosition.Y ? MoveDirection.Up : MoveDirection.NoMove;
        }

        private (int X, int Y) DetermineFlagPosition((int Width, int Height) mapSize, List<Player> players)
        {
            var playerHoldingTheFlag = players.SingleOrDefault(x => x.HasFlag);
            return playerHoldingTheFlag != null ? (X: playerHoldingTheFlag.X, Y: playerHoldingTheFlag.Y) : (X: (int)Math.Floor((double)mapSize.Width / 2), Y: (int)Math.Floor((double)mapSize.Width / 2));
        }

        private List<List<Node>> GenerateNodeMap(Map map, (int X, int Y) targetPosition)
        {
            var nodeMap = new List<List<Node>>();
            for (int y = 0; y < map.Height; y++)
            {
                var rowNodes = new List<Node>();
                nodeMap.Add(rowNodes);
                for (int x = 0; x < map.Width; x++)
                {
                    rowNodes.Add(new Node
                    {
                        X = x,
                        Y = y,
                        MoveCost = map.Fields[x][y] > 0 ? map.Fields[x][y] : 0,
                        DistanceFromTarget = Math.Abs((targetPosition.X - x) + (targetPosition.Y - y))
                    });
                }
            }
            return nodeMap;
        }
    }
}
