using AStarPathFindingBotCore.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using WebSocketSharp;

namespace AStarPathFindingBotCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ws = new WebSocket("ws://localhost:8000"))
            {

                ws.OnMessage += (sender, e) =>
                    Console.WriteLine("Server says: " + e.Data);

                ws.Connect();
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var serializedConnectMessage = JsonConvert.SerializeObject(new ConnectMessage { Name = "Kamil" }, settings);
                ws.Send(serializedConnectMessage);
                
                Console.ReadKey(true);
            }
        }
    }
}
