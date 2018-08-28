using AStarPathFindingBotCore.Base;
using AStarPathFindingBotCore.Domain;
using AStarPathFindingBotCore.Enums;
using AStarPathFindingBotCore.Interfaces;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AStarPathFindingBotCore
{
    public class MyBot : LaxmarPlayerBase
    {
        public List<List<Node>> NodeMap { get; private set; }
        public (int X, int Y)? FlagPosition { get; set; }
        public (int X, int Y) OpponentPosition { get; set; }
        public (int X, int Y) OpponentBasePosition { get; set; }
        public (int X, int Y) MyPosition { get; set; }
        public (int X, int Y) BasePosition { get; set; }
        
        public MyBot(string name, string webSocketUrl, JsonSerializerSettings serializerSettings = null) : base(name, webSocketUrl, serializerSettings)
        {
        }

        public override MoveDirection ChooseDirection(Map map, List<Player> players)
        {
            FlagPosition = DetermineFlagPosition(map, players);

            return AStarDetermineNextStep();
        }

        private (int X, int Y) DetermineFlagPosition(Map map, List<Player> players)
        {
            var playerHoldingTheFlag = players.SingleOrDefault(x => x.HasFlag);
            return playerHoldingTheFlag != null ? (X: playerHoldingTheFlag.X, Y: playerHoldingTheFlag.Y) :
                FlagPosition ?? (X: (int)Math.Floor((double)map.Width / 2), Y: (int)Math.Floor((double)map.Width / 2));
        }

        private MoveDirection AStarDetermineNextStep()
        {
            var rand = new Random().Next(2);
            if (rand == 1)
                rand = 2;
            //UpdateMap(map);
            return (MoveDirection)rand;
        }

        private void UpdateMap(Map map)
        {
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    //if ()
                }
            }
        }
    }
}
