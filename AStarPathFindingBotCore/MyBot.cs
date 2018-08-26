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
        public MoveDirection LastMove { get; private set; }
        public bool WasLastMoveInvalid { get; private set; }
        public List<List<Node>> NodeMap { get; private set; }

        public MyBot(string name, string webSocketUrl, JsonSerializerSettings serializerSettings = null) : base(name, webSocketUrl, serializerSettings)
        {
        }

        public override MoveDirection ChooseDirection(Map map, List<Player> players)
        {
            //UpdateMap(map);
            return (MoveDirection) new Random().Next(4);
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
