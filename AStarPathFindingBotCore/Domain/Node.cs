namespace AStarPathFindingBotCore.Domain
{
    public class Node
    {
        public int DistanceFromFlag { get; set; }
        public int DistanceFromOwnBase { get; }
        public int DistanceFromOpponentBase { get; }
        public int DistanceFromOpponent { get; set; }
        public int MoveCost { get; set; }
        public int X { get; }
        public int Y { get; }
    }
}
