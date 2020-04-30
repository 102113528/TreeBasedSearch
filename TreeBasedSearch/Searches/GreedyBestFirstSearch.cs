using System;
using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class GreedyBestFirstSearch : ISearch
    {
        public string Name { get; } = "Greedy best-first search";
        public Environment Environment { get; }

        private Stack<Node> _frontier;
        private List<Cell> _visited;

        private Cell _goalCell;

        public GreedyBestFirstSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            Cell initialCell = Environment.GetCellsByState(CellState.Agent)[0];
            Node currentNode = new Node(initialCell, null);

            _goalCell = Environment.GetCellsByState(CellState.Goal)[0];

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
                int xDistance = Cell.GetManhattanDistance(x, _goalCell);
                int yDistance = Cell.GetManhattanDistance(y, _goalCell);

                return yDistance.CompareTo(xDistance);
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