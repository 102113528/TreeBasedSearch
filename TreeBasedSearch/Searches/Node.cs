namespace TreeBasedSearch.Searches
{
    public class Node
    {
        public readonly Cell Data;
        public readonly Node Parent;

        public Node(Cell data, Node parent)
        {
            Data = data;
            Parent = parent;
        }
    }
}