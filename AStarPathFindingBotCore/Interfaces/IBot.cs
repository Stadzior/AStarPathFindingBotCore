using AStarPathFindingBotCore.Enums;

namespace AStarPathFindingBotCore.Interfaces
{
    public interface IBot
    {
        MoveDirection LastMove { get; set; }
        bool WasLastMoveInvalid { get; set; }
    }
}
