using System;
using System.Threading;
using WebSocketSharp;

namespace AStarPathFindingBotCore
{
    public static class WebSocketExtensions
    {
        public static void ConnectWithTimeout(this WebSocket webSocket, int timeoutInSeconds)
        {
            webSocket.WaitTime = TimeSpan.FromSeconds(timeoutInSeconds);
            webSocket.Connect();
            Console.WriteLine("Trying to reach the server: ");
            for (int i = 1; i <= webSocket.WaitTime.TotalSeconds; i++)
            {
                if (webSocket.IsAlive)
                {
                    Console.WriteLine("Server reached.");
                    return;
                }
                Thread.Sleep(1000);
                Console.Write($"{i}...");
            }
            Console.WriteLine($"{Environment.NewLine}Server is unreachable.");
        }
    }
}
