using System.Collections.Generic;

namespace AStarPathFindingBotCore.Domain
{
    public class Map
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<List<int>> Fields { get; set; }
    }
}
