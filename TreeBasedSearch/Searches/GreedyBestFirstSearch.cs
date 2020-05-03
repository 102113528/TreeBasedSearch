using System;
using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class GreedyBestFirstSearch : ISearch
    {
        public string Name { get; } = "Greedy best-first search";
        public Environment Environment { get; }

        private readonly Stack<Node> _frontier = new Stack<Node>();
        private readonly List<Cell> _visited = new List<Cell>();

        private List<Cell> _goalCells;

        public GreedyBestFirstSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            Node currentNode = new Node(Environment.GetCellsByState(CellState.Agent)[0], null);

            _frontier.Push(currentNode);
            _visited.Add(currentNode.Data);

            _goalCells = Environment.GetCellsByState(CellState.Goal);

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

                List<Cell> cells = GetNeighbouringCells(currentNode);
                foreach (Cell cell in cells) AddToFrontier(cell, currentNode);
            }

            return null;
        }

        private List<Cell> GetNeighbouringCells(Node node)
        {
            List<Cell> cells = new List<Cell>();

            Cell rightCell = Environment.GetCellAt(node.Data.X + 1, node.Data.Y);
            if (rightCell != null) cells.Add(rightCell);

            Cell belowCell = Environment.GetCellAt(node.Data.X, node.Data.Y + 1);
            if (belowCell != null) cells.Add(belowCell);

            Cell leftCell = Environment.GetCellAt(node.Data.X - 1, node.Data.Y);
            if (leftCell != null) cells.Add(leftCell);

            Cell aboveCell = Environment.GetCellAt(node.Data.X, node.Data.Y - 1);
            if (aboveCell != null) cells.Add(aboveCell);

            cells.Sort((x, y) =>
            {
                int xBestDistance = Cell.GetManhattanDistance(x, _goalCells[0]);
                int yBestDistance = Cell.GetManhattanDistance(y, _goalCells[0]);

                for (int i = 1; i < _goalCells.Count; i++)
                {
                    int xDistance = Cell.GetManhattanDistance(x, _goalCells[i]);
                    int yDistance = Cell.GetManhattanDistance(y, _goalCells[i]);

                    if (xDistance < xBestDistance) xBestDistance = xDistance;
                    if (yDistance < yBestDistance) yBestDistance = yDistance;
                }

                return yBestDistance.CompareTo(xBestDistance);
            });

            return cells;
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