using AStarPathFindingBotCore.Communication;
using AStarPathFindingBotCore.Enums;
using AStarPathFindingBotCore.Services;
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
            
            var playerOne = new EagerForFlagBot("Flag_eater", "ws://localhost:8000", new AStarPathFindingService(), settings);
            var playerTwo = new EagerForFlagBot("Base_rusher", "ws://localhost:8000",new AStarPathFindingService(), settings);
            playerOne.JoinGame();
            playerTwo.JoinGame();
            Console.ReadKey(true);
            playerOne.ExitGame();
            playerTwo.ExitGame();
        }
    }
}
