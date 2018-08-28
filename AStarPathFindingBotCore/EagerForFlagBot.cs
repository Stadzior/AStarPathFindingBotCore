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
        private (int X, int Y) _previousPosition;
        private (int X, int Y)? _opponentBasePosition;

        public EagerForFlagBot(string name, string webSocketUrl, IPathFindingService pathFindingService, JsonSerializerSettings serializerSettings = null) : base(name, webSocketUrl, pathFindingService, serializerSettings)
        {
        }

        public override MoveDirection ChooseDirection(Map map, List<Player> players)
        {
            var me = players.Single(x => x.Id == Id);
            var myPosition = (X: me.X, Y: me.Y);
            var opponent = players.SingleOrDefault(x => x.Id != Id);

            var initialFlagPosition = GetInitialFlagPosition((map.Width, map.Height), players);
            var opponentBaseGuess = me.BasePosition.X > initialFlagPosition.X ?
                (me.BasePosition.X - map.Width, me.BasePosition.Y - map.Height) : (me.BasePosition.X + map.Width, me.BasePosition.Y + map.Height);
            _opponentBasePosition = opponent != null ? (opponent.BasePosition.X, opponent.BasePosition.Y) : opponentBaseGuess;

            var targetPosition = me.HasFlag ? (me.BasePosition.X, me.BasePosition.Y) : //Has flag -> go for the base
                (opponent?.HasFlag ?? false) ? (opponent.X, opponent.Y) : //Opponent have flag -> If I can see him, begin chase!
                myPosition.X == initialFlagPosition.X && myPosition.Y == initialFlagPosition.Y ? (_opponentBasePosition.Value.X, _opponentBasePosition.Value.Y) : //If i reached initial flag position and didn't get it -> go for the opponent's base
                initialFlagPosition; // Go for the flag initial position if you don't have any clue what's going on.
            var path = PathFindingService.FindBestPath(map, myPosition, targetPosition);
            var (X, Y) = path.Count > 1 ? path.Skip(1).First() : path.First();

            if (me.MovesLeft < map.Fields[X][Y])
                return MoveDirection.NoMove;

            var chosenDirection = X > myPosition.X ? MoveDirection.Right :
                X < myPosition.X ? MoveDirection.Left :
                Y > myPosition.Y ? MoveDirection.Down :
                Y < myPosition.Y ? MoveDirection.Up : MoveDirection.NoMove;

            if (chosenDirection != MoveDirection.NoMove)
            {
                if (_previousPosition.X == X && _previousPosition.Y == Y)
                    chosenDirection = (MoveDirection) new Random().Next(4);
                _previousPosition = myPosition;
            }
            return chosenDirection;
        }

        private (int X, int Y) GetInitialFlagPosition((int Width, int Height) mapSize, List<Player> players)
         => ((int)Math.Floor((double)mapSize.Width / 2), (int)Math.Floor((double)mapSize.Width / 2));
    }
}