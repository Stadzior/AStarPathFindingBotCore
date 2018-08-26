using AStarPathFindingBotCore.Communication;
using AStarPathFindingBotCore.Domain.Interfaces;
using AStarPathFindingBotCore.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using WebSocketSharp;

namespace AStarPathFindingBotCore.Domain.Base
{
    public abstract class LaxmarPlayerBase : ILaxmarPlayer
    {
        #region "Current player state"

        public int Id { get; set; }
        public bool HasFlag { get; set; }
        public bool IsAlive { get; set; }
        public string Name { get; }
        public int MaxMovesPerRound { get; set; }
        public BasePosition BasePosition { get; set; }
        public int ViewRange { get; set; }
        public int MovesLeft { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        #endregion

        public int TimeoutInSeconds { get; } = 5;
        public bool IsConnected { get; set; }
        public JsonSerializerSettings SerializerSettings { get; }
        public WebSocket WebSocket { get; }

        private readonly Dictionary<MoveDirection, string> _directionsDictionary = new Dictionary<MoveDirection, string>
        {
            {MoveDirection.Down, "DOWN"},
            {MoveDirection.Left, "LEFT"},
            {MoveDirection.NoMove, "NO_MOVE"},
            {MoveDirection.Right, "RIGHT"},
            {MoveDirection.Up, "UP"}
        };

        public LaxmarPlayerBase(string name, string webSocketUrl, JsonSerializerSettings serializerSettings = null)
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

            var connectMessage = JsonConvert.SerializeObject(new ConnectMessage { Name = Name }, SerializerSettings);
            WebSocket.Send(connectMessage);

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
                        var connectedMessage = JsonConvert.DeserializeObject<ConnectedMessage>(e.Data);
                        Id = connectedMessage.PlayerId;
                        break;
                    }
                case "MoveRequest":
                    {
                        var moveRequestMessage = JsonConvert.DeserializeObject<MoveRequestMessage>(e.Data);
                        //Do choosing direction
                        MakeMove(MoveDirection.Up);
                        break;
                    }
                default:
                    Console.WriteLine(e.Data);
                    break;
            }
        }

        public bool MakeMove(MoveDirection moveDirection)
        {
            if (!IsConnected)
            {
                Console.WriteLine($"{Name}: I can't move since I'm not in any game yet.");
                return false;
            }

            
            var moveMessage = JsonConvert.SerializeObject(new MoveMessage { PlayerId = Id, Move = _directionsDictionary[moveDirection] }, SerializerSettings);
            WebSocket.Send(moveMessage);
            return true;
            //Console.WriteLine($"{Name}: Trying to join the game: ");
            //for (int i = 1; i <= WebSocket.WaitTime.TotalSeconds; i++)
            //{
            //    if (IsConnected)
            //    {
            //        Console.WriteLine($"{Environment.NewLine}{Name}: Joined the game.");
            //        return true;
            //    }
            //    Thread.Sleep(1000);
            //    Console.Write($"{i}...");
            //}
            //Console.WriteLine($"{Environment.NewLine}{Name}: Could not join the game.");
            //return false;
        }

        public abstract string ChooseDirection();
    }
}
