namespace AStarPathFindingBotCore.Domain
{
    public class Node
    {
        public int DistanceFromTarget { get; set; }
        public int MoveCost { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
