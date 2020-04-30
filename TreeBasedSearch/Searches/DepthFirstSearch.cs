using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class DepthFirstSearch : ISearch
    {
        public string Name { get; } = "Depth-first search";
        public Environment Environment { get; }

        private Stack<Node> _frontier;
        private List<Cell> _visited;

        public DepthFirstSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            Cell initialCell = Environment.GetCellsByState(CellState.Agent)[0];
            Node currentNode = new Node(initialCell, null);

            _frontier = new Stack<Node>();
            _visited = new List<Cell>();

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

                Cell rightCell = Environment.GetCellAt(currentNode.Data.X + 1, currentNode.Data.Y);
                if (rightCell != null) AddToFrontier(rightCell, currentNode);

                Cell belowCell = Environment.GetCellAt(currentNode.Data.X, currentNode.Data.Y + 1);
                if (belowCell != null) AddToFrontier(belowCell, currentNode);

                Cell leftCell = Environment.GetCellAt(currentNode.Data.X - 1, currentNode.Data.Y);
                if (leftCell != null) AddToFrontier(leftCell, currentNode);

                Cell aboveCell = Environment.GetCellAt(currentNode.Data.X, currentNode.Data.Y - 1);
                if (aboveCell != null) AddToFrontier(aboveCell, currentNode);
            }

            return null;
        }

        private void AddToFrontier(Cell cell, Node parent)
        {
            if (_visited.Exists(item => item.X == cell.X && item.Y == cell.Y)) return;
            _visited.Add(cell);

            Node node = new Node(cell, parent);
            _frontier.Push(node);
        }
    }
}