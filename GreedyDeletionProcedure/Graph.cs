using System.Runtime.InteropServices;

namespace GreedyDeletionProcedure;

public class Graph
{
    public Node[] Nodes;

    private int[,] _connections;

    public Graph(GraphGenerationOptions options)
    {
        Nodes = new Node[options.Size];
        _connections = new int[options.Size,options.Size];
        
        if (options.MaxConnections <= 0) options.MaxConnections = 1;
        
        for (int i = 0; i < options.Size; i++)
        {
            _connections[i, i] = new Random().Next(Convert.ToInt32(options.LoopAllowed)+1);
            Nodes[i] = new Node();
        }

        for (int i = 0; i < options.Size; i++)
        {
            for (int j = 0; j < i; j++)
            {
                _connections[i, j] = new Random().Next(options.MaxConnections+1);
                _connections[j, i] = _connections[i, j];
            }
        }

        for (int i = 0; i < options.Size; i++)
        {
            for (int j = 0; j < options.Size; j++)
            {
                if (_connections[i, j] > 0)
                {
                    Nodes[i].Neighbours.Add(Nodes[j]);
                }
            }
            Nodes[i].CreateCN();
        }
    }

    public void Print()
    {
        for (int i = 0; i < Math.Sqrt(_connections.Length); i++)
        {
            for (int j = 0; j < Math.Sqrt(_connections.Length); j++)
            {
                Console.Write(_connections[i,j] + " ");
            }
            Console.WriteLine();
        } 
    }

    public void GetDomSet()
    {
        List<Node> dominatingSet = Nodes.ToList();
        while (true)
        {
            var gdpValues = new Dictionary<Node, int>();

            foreach (var node in dominatingSet)
            {
                var dominantsNum = new List<int>();
                foreach (var neighbour in node.ClosedNeighbourhood)
                {
                    var num = 0;
                    foreach (var toCheck in neighbour.ClosedNeighbourhood)
                        if (dominatingSet.Contains(toCheck) && toCheck != node) 
                            num++;
                    dominantsNum.Add(num);
                }
                gdpValues.Add(node, dominantsNum.Min());
            }
            if(gdpValues.All(x => x.Value == 0)) break;
            var pair = gdpValues.First(x => x.Value == gdpValues.Max(y => y.Value));
            dominatingSet.Remove(pair.Key);
        }

        foreach (var node in dominatingSet)
        {
            Console.Write((Array.FindIndex(Nodes, x => x == node)+1) + " : ");
            foreach (var neighbour in node.Neighbours)
            {
                Console.Write((Array.FindIndex(Nodes, x => x == neighbour)+1) + " ");
            }
            Console.WriteLine();
        }
    }
}