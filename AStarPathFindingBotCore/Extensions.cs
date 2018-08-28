using System;

namespace AStarPathFindingBotCore
{
    public static class Extensions
    {
        public static int Distance(this (int X, int Y) source, (int X, int Y) target)
            => Math.Abs((target.X - source.X) + (target.Y - source.Y));
    }
}
