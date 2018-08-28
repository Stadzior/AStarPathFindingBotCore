using AStarPathFindingBotCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFindingBotCore.Services.Interfaces
{
    public interface IPathFindingService
    {
        List<(int X, int Y)> FindBestPath(Map map, (int X, int Y) startingPoint, (int X, int Y) targetPoint);
    }
}
