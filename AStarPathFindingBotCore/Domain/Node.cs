using System;
using System.Collections.Generic;
using System.Linq;

namespace AStarPathFindingBotCore.Domain
{
    public class Node
    {
        public int DistanceFromTarget { get; set; }
        public int MoveCost { get; set; }
        public int MoveCostFromStartingPoint { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public List<Node> GetNeighbours(List<List<Node>> nodeMap)
        {
            return nodeMap.SelectMany(x => x)
                .Where(x => (x.X == X && Math.Abs(x.Y - Y) == 1) || (x.Y == Y && Math.Abs(x.X - X) == 1))
                .OrderByDescending(x => x.MoveCost + x.DistanceFromTarget)
                .ToList();
        }
    }
}
