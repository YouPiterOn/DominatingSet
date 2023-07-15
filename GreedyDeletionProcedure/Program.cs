using System;

namespace GreedyDeletionProcedure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var myGraph = new Graph(new GraphGenerationOptions()
            {
                Size = 6,
                IsConnected = true,
                MaxConnections = 1,
                LoopAllowed = false
            });
            myGraph.Print();
            myGraph.GetDomSet();
        }
    }
}