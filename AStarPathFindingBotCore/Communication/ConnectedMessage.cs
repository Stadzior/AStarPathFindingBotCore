using AStarPathFindingBotCore.Communication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFindingBotCore.Communication
{
    public class ConnectedMessage : IMessage
    {
        public string Type => "Connected";
        public string Msg { get; set; }
        public int PlayerId { get; set; }
    }
}
