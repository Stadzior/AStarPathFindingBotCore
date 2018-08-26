using AStarPathFindingBotCore.Communication.Interfaces;

namespace AStarPathFindingBotCore.Communication
{
    public class ConnectMessage : IMessage
    {
        public string Type => "Connect";
        public string Name { get; set; }
    }
}
