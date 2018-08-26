using AStarPathFindingBotCore.Messages.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFindingBotCore.Messages
{
    public class ConnectRequest : IMessage
    {
        public string Type => "Connect";
        public string Name { get; set; }
    }
}
