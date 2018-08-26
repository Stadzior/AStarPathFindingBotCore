using AStarPathFindingBotCore.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace AStarPathFindingBotCore
{
    public class PathFindingBot
    {
        public JsonSerializerSettings SerializerSettings { get; }
        public PathFindingBot(JsonSerializerSettings serializerSettings = null)
        {
            SerializerSettings = serializerSettings;
        }

        public bool JoinGame(WebSocket webSocket, string name)
        {
            var serializedConnectMessage = JsonConvert.SerializeObject(new ConnectMessage { Name = name }, SerializerSettings);
            webSocket.Send(serializedConnectMessage);

        }
    }
}
