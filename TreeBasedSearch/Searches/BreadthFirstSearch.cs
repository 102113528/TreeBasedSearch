using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class BreadthFirstSearch : ISearch
    {
        public string Name { get; } = "Breadth-first search";
        public Environment Environment { get; }

        public BreadthFirstSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            Cell initialCell = Environment.GetCellsByState(CellState.Agent)[0];
            Node currentNode = new Node(initialCell, null);

            Queue<Node> frontier = new Queue<Node>();
            frontier.Enqueue(currentNode);

            List<Cell> visited = new List<Cell>();

            while (frontier.Count > 0)
            {
                currentNode = frontier.Dequeue();

                if (visited.Contains(currentNode.Data)) continue;
                visited.Add(currentNode.Data);

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
                if (aboveCell != null) AddToFrontier(frontier, aboveCell, currentNode);

                Cell leftCell = Environment.GetCellAt(currentNode.Data.X - 1, currentNode.Data.Y);
                if (leftCell != null) AddToFrontier(frontier, leftCell, currentNode);

                Cell belowCell = Environment.GetCellAt(currentNode.Data.X, currentNode.Data.Y + 1);
                if (belowCell != null) AddToFrontier(frontier, belowCell, currentNode);

                Cell rightCell = Environment.GetCellAt(currentNode.Data.X + 1, currentNode.Data.Y);
                if (rightCell != null) AddToFrontier(frontier, rightCell, currentNode);
            }

            return null;
        }

        private void AddToFrontier(Queue<Node> frontier, Cell cell, Node parent)
        {
            Node node = new Node(cell, parent);
            frontier.Enqueue(node);
        }
    }
}