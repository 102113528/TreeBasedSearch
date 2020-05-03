using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class UniformCostSearch : ISearch
    {
        public string Name { get; } = "Uniform-cost search";
        public Environment Environment { get; }

        private readonly Stack<Node> _frontier = new Stack<Node>();
        private readonly List<Cell> _visited = new List<Cell>();

        public UniformCostSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            Node currentNode = new Node(Environment.GetCellsByState(CellState.Agent)[0], null);

            _frontier.Push(currentNode);
            _visited.Add(currentNode.Data);

            while (_frontier.Count > 0)
            {
                currentNode = _frontier.Pop();

                if (currentNode.Data.State == CellState.Goal)
                {
                    List<Cell> result = new List<Cell>();

                    while (currentNode != null)
                    {
                        result.Insert(0, currentNode.Data);
                        currentNode = currentNode.Parent;
                    }

                    return result;
                }

                if (currentNode.Data.State == CellState.Wall) continue;

                List<Node> nodes = GetNeighbouringNodes(currentNode);
                foreach (Node node in nodes) _frontier.Push(node);
            }

            return null;
        }

        private List<Node> GetNeighbouringNodes(Node node)
        {
            List<Node> cells = new List<Node>();

            Cell rightCell = Environment.GetCellAt(node.Data.X + 1, node.Data.Y);
            if (rightCell != null) AddToList(cells, rightCell, node);

            Cell belowCell = Environment.GetCellAt(node.Data.X, node.Data.Y + 1);
            if (belowCell != null) AddToList(cells, belowCell, node);

            Cell leftCell = Environment.GetCellAt(node.Data.X - 1, node.Data.Y);
            if (leftCell != null) AddToList(cells, leftCell, node);

            Cell aboveCell = Environment.GetCellAt(node.Data.X, node.Data.Y - 1);
            if (aboveCell != null) AddToList(cells, aboveCell, node);

            cells.Sort((x, y) => y.Cost.CompareTo(x.Cost));

            return cells;
        }

        private void AddToList(List<Node> list, Cell cell, Node parent)
        {
            if (_visited.Exists(item => item.X == cell.X && item.Y == cell.Y)) return;
            _visited.Add(cell);

            Node node = new Node(cell, parent, parent.Cost + Cell.GetDirectionCost(parent.Data, cell));
            list.Add(node);
        }
    }
}