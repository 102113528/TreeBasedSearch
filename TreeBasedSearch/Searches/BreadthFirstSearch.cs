using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class BreadthFirstSearch : ISearch
    {
        public string Name { get; } = "Breadth-first search";
        public Environment Environment { get; }

        private Queue<Node> _frontier;
        private List<Cell> _visited;

        public BreadthFirstSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            Cell initialCell = Environment.GetCellsByState(CellState.Agent)[0];
            Node currentNode = new Node(initialCell, null);

            _frontier = new Queue<Node>();
            _frontier.Enqueue(currentNode);

            _visited = new List<Cell>();

            while (_frontier.Count > 0)
            {
                currentNode = _frontier.Dequeue();

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

                Cell aboveCell = Environment.GetCellAt(currentNode.Data.X, currentNode.Data.Y - 1);
                if (aboveCell != null) AddToFrontier(aboveCell, currentNode);

                Cell leftCell = Environment.GetCellAt(currentNode.Data.X - 1, currentNode.Data.Y);
                if (leftCell != null) AddToFrontier(leftCell, currentNode);

                Cell belowCell = Environment.GetCellAt(currentNode.Data.X, currentNode.Data.Y + 1);
                if (belowCell != null) AddToFrontier(belowCell, currentNode);

                Cell rightCell = Environment.GetCellAt(currentNode.Data.X + 1, currentNode.Data.Y);
                if (rightCell != null) AddToFrontier(rightCell, currentNode);
            }

            return null;
        }

        private void AddToFrontier(Cell cell, Node parent)
        {
            if (_visited.Contains(cell)) return;
            _visited.Add(cell);

            Node node = new Node(cell, parent);
            _frontier.Enqueue(node);
        }
    }
}