namespace AStarPathFindingBotCore.Domain
{
    public class Player
    {
        public int Id { get; set; }
        public bool HasFlag { get; set; }
        public bool IsAlive { get; set; }
        public string Name { get; set; }
        public int MaxMovesPerRound { get; set; }
        public BasePosition BasePosition { get; set; }
        public int ViewRange { get; set; }
        public double MovesLeft { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
