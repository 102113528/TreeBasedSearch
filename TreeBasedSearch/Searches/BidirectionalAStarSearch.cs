using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeBasedSearch.Searches
{
    public class BidirectionalAStarSearch : ISearch
    {
        public string Name { get; } = "Bidirectional (A*) search";
        public Environment Environment { get; }

        private Stack<Node> _forwardFrontier, _backwardFrontier;
        private List<Cell> _forwardVisited, _backwardVisited;

        private Cell _initialCell, _goalCell;

        public BidirectionalAStarSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            _forwardFrontier = new Stack<Node>();
            _forwardVisited = new List<Cell>();

            _initialCell = Environment.GetCellsByState(CellState.Agent)[0];
            Node forwardNode = new Node(_initialCell, null);

            _forwardFrontier.Push(forwardNode);
            _forwardVisited.Add(forwardNode.Data);

            _backwardFrontier = new Stack<Node>();
            _backwardVisited = new List<Cell>();

            _goalCell = Environment.GetCellsByState(CellState.Goal)[0];
            Node backwardNode = new Node(_goalCell, null);

            _backwardFrontier.Push(backwardNode);
            _backwardVisited.Add(backwardNode.Data);

            while (_forwardFrontier.Count > 0 && _backwardFrontier.Count > 0)
            {
                forwardNode = _forwardFrontier.Pop();
                backwardNode = _backwardFrontier.Pop();

                if (forwardNode.Data.State != CellState.Wall) AStarSearch(_forwardFrontier, _forwardVisited, _goalCell, forwardNode);
                if (backwardNode.Data.State != CellState.Wall) AStarSearch(_backwardFrontier, _backwardVisited, _initialCell, backwardNode);

                foreach (Cell forwardCell in _forwardVisited)
                {
                    foreach (Cell backwardCell in _backwardVisited)
                    {
                        if (forwardCell.X == backwardCell.X && forwardCell.Y == backwardCell.Y)
                        {
                            List<Cell> result = new List<Cell>();

                            forwardNode = _forwardFrontier.Pop();

                            while (forwardNode != null)
                            {
                                result.Insert(0, forwardNode.Data);
                                forwardNode = forwardNode.Parent;
                            }

                            Cell last = result.Last();
                            backwardNode = _backwardFrontier.Pop();
                            if (backwardNode.Data.X == last.X && backwardNode.Data.Y == last.Y) backwardNode = backwardNode.Parent;

                            while (backwardNode != null)
                            {
                                result.Add(backwardNode.Data);
                                backwardNode = backwardNode.Parent;
                            }

                            return result;
                        }
                    }
                }
            }

            return null;
        }

        private void AStarSearch(Stack<Node> frontier, List<Cell> visited, Cell destinationCell, Node currentNode)
        {
            List<Node> nodes = GetNeighbouringNodes(visited, destinationCell, currentNode);
            foreach (Node node in nodes) frontier.Push(node);
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

        private List<Node> GetNeighbouringNodes(List<Cell> visited, Cell destination, Node node)
        {
            List<Node> cells = new List<Node>();

            Cell rightCell = Environment.GetCellAt(node.Data.X + 1, node.Data.Y);
            if (rightCell != null) AddToList(visited, cells, rightCell, node);

            Cell belowCell = Environment.GetCellAt(node.Data.X, node.Data.Y + 1);
            if (belowCell != null) AddToList(visited, cells, belowCell, node);

            Cell leftCell = Environment.GetCellAt(node.Data.X - 1, node.Data.Y);
            if (leftCell != null) AddToList(visited, cells, leftCell, node);

            Cell aboveCell = Environment.GetCellAt(node.Data.X, node.Data.Y - 1);
            if (aboveCell != null) AddToList(visited, cells, aboveCell, node);

            cells.Sort((x, y) =>
            {
                int xDistance = x.Cost + Cell.GetManhattanDistance(x.Data, destination);
                int yDistance = y.Cost + Cell.GetManhattanDistance(y.Data, destination);

                return yDistance.CompareTo(xDistance);
            });

            return cells;
        }

        private void AddToList(List<Cell> visited, List<Node> list, Cell cell, Node parent)
        {
            if (visited.Exists(item => item.X == cell.X && item.Y == cell.Y)) return;
            visited.Add(cell);

            Node node = new Node(cell, parent, parent.Cost + GetDirectionCost(parent.Data, cell));
            list.Add(node);
        }
    }
}