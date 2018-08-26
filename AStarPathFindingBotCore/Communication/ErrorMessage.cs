using AStarPathFindingBotCore.Communication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFindingBotCore.Communication
{
    public class ErrorMessage : IMessage
    {
        public string Type => "Error";
        public string Msg { get; set; }
    }
}
