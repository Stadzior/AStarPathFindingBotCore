using AStarPathFindingBotCore.Domain.Base;
using AStarPathFindingBotCore.Domain.Interfaces;
using Newtonsoft.Json;

namespace AStarPathFindingBotCore
{
    public class MyBot : LaxmarPlayerBase, IBot
    {
        public MyBot(string name, string webSocketUrl, JsonSerializerSettings serializerSettings = null) : base(name, webSocketUrl, serializerSettings)
        {
        }

        public string ChooseDirection()
        {
            return "LEFT";
        }
    }
}
