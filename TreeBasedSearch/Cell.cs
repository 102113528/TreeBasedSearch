using System;

namespace TreeBasedSearch
{
    public class Cell
    {
        public readonly int X, Y;
        public CellState State;

        public Cell(int x, int y, CellState state)
        {
            X = x;
            Y = y;
            State = state;
        }

        public static int GetManhattanDistance(Cell current, Cell destination)
        {
            int xDifference = Math.Abs(current.X - destination.X);
            int yDifference = Math.Abs(current.Y - destination.Y);

            return xDifference + yDifference;
        }
    }
}