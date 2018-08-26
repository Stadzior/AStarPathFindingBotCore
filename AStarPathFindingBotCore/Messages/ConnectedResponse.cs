using AStarPathFindingBotCore.Messages.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFindingBotCore.Messages
{
    public class ConnectedResponse : IMessage
    {
        public string Type => "Connected";
        public string Msg { get; set; }
        public int PlayerId { get; set; }
    }
}
