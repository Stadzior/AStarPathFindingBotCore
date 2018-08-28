using AStarPathFindingBotCore.Domain;
using AStarPathFindingBotCore.Enums;
using AStarPathFindingBotCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AStarPathFindingBotCore.Services
{
    public class AStarPathFindingService : IPathFindingService
    {
        private List<(Node ParentNode, IList<Node> ChildNodes)> _closedList;

        public AStarPathFindingService()
        {
            _closedList = new List<(Node ParentNode, IList<Node> ChildNodes)>();
        }
        
        public List<(int X, int Y)> FindBestPath(Map map, (int X, int Y) startingPoint, (int X, int Y) targetPoint)
        {
            var nodeMap = GenerateNodeMap(map, targetPoint);
            targetPoint = FindClosestVisiblePointToTheTarget(nodeMap, targetPoint);
            var startingNode = nodeMap.SelectMany(x => x).Single(x => x.X == startingPoint.X && x.Y == startingPoint.Y);
            var neighbourNodes = startingNode.GetNeighbours(nodeMap);

            if (neighbourNodes.Any(x => x.X == targetPoint.X && x.Y == targetPoint.Y))
                return new List<(int X, int Y)>
                {
                    targetPoint
                };

            foreach (var node in neighbourNodes)
                node.MoveCostFromStartingPoint = node.MoveCost;

            _closedList.Add((startingNode, neighbourNodes));

            PerformStep(nodeMap, startingNode, targetPoint);

            return BuildPathByTraversingBack(targetPoint);
        }

        private (int X, int Y) FindClosestVisiblePointToTheTarget(List<List<Node>> nodeMap, (int X, int Y) targetPoint)
        {
            var closestVisibleNodeToTheTarget = nodeMap.SelectMany(x => x).Where(x => x.MoveCost > 0).OrderBy(x => (X: x.X, Y: x.Y).Distance(targetPoint)).First();
            var hue = nodeMap.SelectMany(x => x).Where(x => x.MoveCost > 0).Select(x => ((x.X, x.Y), (X: x.X, Y: x.Y).Distance(targetPoint)));
            return (closestVisibleNodeToTheTarget.X, closestVisibleNodeToTheTarget.Y);
        }

        private List<(int X, int Y)> BuildPathByTraversingBack((int X, int Y) targetPoint)
        {
            var path = new List<(int X, int Y)>
            {
                targetPoint
            };

            Node currentNode;
            do
            {
                currentNode = _closedList.Single(x => x.ChildNodes.Any(y => y.X == targetPoint.X && y.Y == targetPoint.Y)).ParentNode;
                path.Add((currentNode.X, currentNode.Y));
            } while (_closedList.Any(x => x.ChildNodes.Contains(currentNode)));

            path.Reverse();
            return path;
        }

        private void PerformStep(List<List<Node>> nodeMap, Node startingNode, (int X, int Y) targetPoint)
        {
            foreach (var parentNode in _closedList.Single(x => x.ParentNode == startingNode).ChildNodes)
            {
                var neighbourNodes = parentNode.GetNeighbours(nodeMap).Where(x => _closedList.All(y => x != y.ParentNode)).ToList();
                var conflictingNodes = neighbourNodes.Where(x => _closedList.SelectMany(y => y.ChildNodes).Contains(x));
                foreach (var conflictingNode in conflictingNodes)
                {
                    if (conflictingNode.MoveCostFromStartingPoint < parentNode.MoveCostFromStartingPoint + conflictingNode.MoveCost)
                        _closedList.Single(x => x.ChildNodes.Contains(conflictingNode)).ChildNodes.Remove(conflictingNode);
                }
                foreach (var neighbourNode in neighbourNodes)
                    neighbourNode.MoveCostFromStartingPoint = parentNode.MoveCostFromStartingPoint + neighbourNode.MoveCost;

                _closedList.Add((parentNode, neighbourNodes));

                if (_closedList.SelectMany(x => x.ChildNodes).Any(x => x.X == targetPoint.X && x.Y == targetPoint.Y))
                    return;
                else
                    PerformStep(nodeMap, parentNode, targetPoint);
            }
        }

        private List<List<Node>> GenerateNodeMap(Map map, (int X, int Y) targetPosition)
        {
            var nodeMap = new List<List<Node>>();
            for (int y = 0; y < map.Height; y++)
            {
                var rowNodes = new List<Node>();
                nodeMap.Add(rowNodes);
                for (int x = 0; x < map.Width; x++)
                {
                    rowNodes.Add(new Node
                    {
                        X = x,
                        Y = y,
                        MoveCost = map.Fields[x][y] > 0 ? map.Fields[x][y] : 0,
                        DistanceFromTarget = targetPosition.Distance((x, y))
                    });
                }
            }
            return nodeMap;
        }
    }
}
