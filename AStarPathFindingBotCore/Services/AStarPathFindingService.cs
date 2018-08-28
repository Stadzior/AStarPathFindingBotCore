using AStarPathFindingBotCore.Domain;
using AStarPathFindingBotCore.Enums;
using AStarPathFindingBotCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFindingBotCore.Services
{
    public class AStarPathFindingService : IPathFindingService
    {
        public List<Node> FindBestPath(List<List<Node>> map, (int X, int Y) startingPoint, (int X, int Y) targetPoint)
        {
            return new List<Node>();
        }
    }
}
