using AStarPathFindingBotCore.Domain;
using AStarPathFindingBotCore.Communication.Interfaces;
using System.Collections.Generic;

namespace AStarPathFindingBotCore.Communication
{
    public class MoveRequestMessage : IMessage
    {
        public string Type => "MoveRequest";
        public string Msg { get; set; }
        public Map Map { get; set; }
        public List<Player> Players { get; set; }
        public int Token { get; set; }
    }
}
