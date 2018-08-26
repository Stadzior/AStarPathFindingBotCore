namespace AStarPathFindingBotCore.Communication
{
    public class MoveMessage
    {
        public string Type => "Move";
        public int PlayerId { get; set; }
        public string Move { get; set; }
    }
}
