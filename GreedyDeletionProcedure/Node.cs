namespace GreedyDeletionProcedure;

public class Node
{
    public List<Node> Neighbours = new List<Node>();

    public List<Node> ClosedNeighbourhood = new List<Node>();
    
    public void CreateCN()
    {
        foreach (var node in Neighbours)
        {
            ClosedNeighbourhood.Add(node);
        }
        ClosedNeighbourhood.Add(this);
    }
}