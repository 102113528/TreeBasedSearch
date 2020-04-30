using System;
using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class AStarSearch : ISearch
    {
        public string Name { get; } = "A*";
        public Environment Environment { get; }

        private Stack<Node> _frontier;
        private List<Cell> _visited;

        private Cell _initialCell, _goalCell;

        public AStarSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            _initialCell = Environment.GetCellsByState(CellState.Agent)[0];
            _goalCell = Environment.GetCellsByState(CellState.Goal)[0];

            Node currentNode = new Node(_initialCell, null);

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

                List<Node> nodes = GetNeighbouringNodes(currentNode);
                foreach (Node node in nodes) _frontier.Push(node);
            }

            return null;
        }

        private int GetDirectionCost(Cell lastCell, Cell currentCell)
        {
            // 1 - Up, 2 - Left, 3 - Down, 4 - Right

            if (lastCell.Y > currentCell.Y) return 1;
            if (lastCell.X > currentCell.X) return 2;
            if (lastCell.Y < currentCell.Y) return 3;
            if (lastCell.X < currentCell.X) return 4;

            return 0;
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

            cells.Sort((x, y) =>
            {
                int xDistance = x.Cost + Cell.GetManhattanDistance(x.Data, _goalCell);
                int yDistance = y.Cost + Cell.GetManhattanDistance(y.Data, _goalCell);

                return yDistance.CompareTo(xDistance);
            });

            return cells;
        }

        private void AddToList(List<Node> list, Cell cell, Node parent)
        {
            if (_visited.Exists(item => item.X == cell.X && item.Y == cell.Y)) return;
            _visited.Add(cell);

            Node node = new Node(cell, parent, parent.Cost + GetDirectionCost(parent.Data, cell));
            list.Add(node);
        }
    }
}