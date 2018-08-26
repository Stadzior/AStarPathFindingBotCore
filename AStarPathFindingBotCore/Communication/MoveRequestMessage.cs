using AStarPathFindingBotCore.Domain;
using AStarPathFindingBotCore.Communication.Interfaces;

namespace AStarPathFindingBotCore.Communication
{
    public class MoveRequestMessage : IMessage
    {
        public string Type => "MoveRequest";
        public string Msg { get; set; }
        public Map Map { get; set; }
        public int Token { get; set; }
    }
}
