using Newtonsoft.Json;
using WebSocketSharp;

namespace AStarPathFindingBotCore.Domain.Interfaces
{
    public interface IBot : ILaxmarPlayer
    {
        string ChooseDirection();
    }
}
