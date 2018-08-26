using AStarPathFindingBotCore.Domain.Base;
using Newtonsoft.Json;

namespace AStarPathFindingBotCore
{
    public class MyBot : LaxmarPlayerBase
    {
        public MyBot(string name, string webSocketUrl, JsonSerializerSettings serializerSettings = null) : base(name, webSocketUrl, serializerSettings)
        {
        }

        public override string ChooseDirection()
        {
            return "UP";
        }
    }
}
