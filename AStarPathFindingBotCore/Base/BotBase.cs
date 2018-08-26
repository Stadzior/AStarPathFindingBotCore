using AStarPathFindingBotCore.Interfaces;
using AStarPathFindingBotCore.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using WebSocketSharp;

namespace AStarPathFindingBotCore.Base
{
    public abstract class BotBase : IBot
    {
        public int TimeoutInSeconds { get; } = 5;
        public bool IsConnected { get; set; }
        public JsonSerializerSettings SerializerSettings { get; }
        public WebSocket WebSocket { get; }
        public string Name { get; }

        public BotBase(string name, string webSocketUrl, JsonSerializerSettings serializerSettings = null)
        {
            Name = name;
            SerializerSettings = serializerSettings;
            WebSocket = new WebSocket("ws://localhost:8000");
            WebSocket.OnMessage += OnMessageHandler;
        }

        public bool JoinGame()
        {
            if (!WebSocket.ConnectWithTimeout(TimeoutInSeconds))
                return false;

            var serializedConnectMessage = JsonConvert.SerializeObject(new ConnectRequest { Name = Name }, SerializerSettings);
            WebSocket.Send(serializedConnectMessage);

            Console.WriteLine($"{Name}: Trying to join the game: ");
            for (int i = 1; i <= WebSocket.WaitTime.TotalSeconds; i++)
            {
                if (IsConnected)
                {
                    Console.WriteLine($"{Environment.NewLine}{Name}: Joined the game.");
                    return true;
                }
                Thread.Sleep(1000);
                Console.Write($"{i}...");
            }
            Console.WriteLine($"{Environment.NewLine}{Name}: Could not join the game.");
            return false;
        }

        public void ExitGame()
        {
            Console.WriteLine($"{Environment.NewLine}{Name}: Abandoning the game, Bye!");
            WebSocket.Close();
        }

        private void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var responseType = ((JObject)JsonConvert.DeserializeObject(e.Data))["type"];

            switch (responseType.Value<string>())
            {
                case "Connected":
                    {
                        IsConnected = true;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
