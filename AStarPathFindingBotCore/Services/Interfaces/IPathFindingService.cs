using AStarPathFindingBotCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFindingBotCore.Services.Interfaces
{
    public interface IPathFindingService
    {
        List<Node> FindBestPath(List<List<Node>> map, (int X, int Y) startingPoint, (int X, int Y) targetPoint);
    }
}
