using AStarPathFindingBotCore.Base;
using Newtonsoft.Json;

namespace AStarPathFindingBotCore
{
    public class AStarBot : BotBase
    {
        public AStarBot(string name, string webSocketUrl, JsonSerializerSettings serializerSettings = null) : base(name, webSocketUrl, serializerSettings)
        {
        }
    }
}
