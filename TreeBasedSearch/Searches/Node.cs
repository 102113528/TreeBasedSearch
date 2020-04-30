namespace TreeBasedSearch.Searches
{
    public class Node
    {
        public readonly Cell Data;
        public readonly Node Parent;
        public readonly int Cost;

        public Node(Cell data, Node parent, int cost = 0)
        {
            Data = data;
            Parent = parent;
            Cost = cost;
        }
    }
}