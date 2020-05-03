namespace TreeBasedSearch.Searches
{
    public class Node
    {
        /// <summary>
        /// The data this node stores (a cell object).
        /// </summary>
        public readonly Cell Data;

        /// <summary>
        /// The parent node.
        /// </summary>
        public readonly Node Parent;

        /// <summary>
        /// The cost to move from the parent node to this node.
        /// Only used for informed search algorithms (A*, Bidirectional (A*), and Uniform-cost).
        /// </summary>
        public readonly int Cost;

        public Node(Cell data, Node parent, int cost = 0)
        {
            Data = data;
            Parent = parent;
            Cost = cost;
        }
    }
}