using AStarPathFindingBotCore.Communication;
using AStarPathFindingBotCore.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace AStarPathFindingBotCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var playerOne = new MyBot("Kamil", "ws://localhost:8000", settings);
            var playerTwo = new MyBot("Michał", "ws://localhost:8000", settings);
            playerOne.JoinGame();
            playerTwo.JoinGame();
            Console.ReadKey(true);
            playerOne.ExitGame();
            playerTwo.ExitGame();
        }
    }
}
