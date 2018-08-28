using AStarPathFindingBotCore.Base;
using AStarPathFindingBotCore.Domain;
using AStarPathFindingBotCore.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AStarPathFindingBotCore
{
    public class ConsolePlayer : LaxmarPlayerBase
    {
        public ConsolePlayer(string name, string webSocketUrl, JsonSerializerSettings serializerSettings = null) : base(name, webSocketUrl, null, serializerSettings)
        {
        }

        public override MoveDirection ChooseDirection(Map map, List<Player> players)
        {
            ConsoleKey hittedKey;
            do
            {
                hittedKey = Console.ReadKey().Key;
            } while (!new[] { ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Spacebar }.Contains(hittedKey));

            switch (hittedKey)
            {
                case ConsoleKey.LeftArrow:
                    return MoveDirection.Left;
                case ConsoleKey.UpArrow:
                    return MoveDirection.Up;
                case ConsoleKey.RightArrow:
                    return MoveDirection.Right;
                case ConsoleKey.DownArrow:
                    return MoveDirection.Down;
                default:
                    return MoveDirection.NoMove;
            }
        }
    }
}
