using AStarPathFindingBotCore.Messages;
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
        private const int TIMEOUT_IN_SECONDS = 5;
        static void Main(string[] args)
        {
            using (var webSocket = new WebSocket("ws://localhost:8000"))
            {
                webSocket.OnMessage += (sender, e) =>
                    Console.WriteLine("Server says: " + e.Data);

                webSocket.ConnectWithTimeout(TIMEOUT_IN_SECONDS);
                
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var bot = new PathFindingBot(settings);
                bot.JoinGame(webSocket, "Kamil");

                Console.ReadKey(true);
            }
        }
    }
}
