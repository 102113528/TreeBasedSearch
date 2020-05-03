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

        /// <summary>
        /// Calculates the cost to move from one cell to another.
        /// </summary>
        /// <param name="current">The current cell.</param>
        /// <param name="next">The next cell.</param>
        /// <returns>The cost to move.</returns>
        public static int GetDirectionCost(Cell current, Cell next)
        {
            if (current.Y > next.Y) return 1; // Up
            if (current.X > next.X) return 2; // Left
            if (current.Y < next.Y) return 3; // Down
            if (current.X < next.X) return 4; // Right

            // They didn't move, so return zero.
            return 0;
        }

        /// <summary>
        /// Calculates the Manhattan distance between two cells.
        /// </summary>
        /// <param name="left">The first cell.</param>
        /// <param name="right">The second cell.</param>
        /// <returns>The Manhattan distance.</returns>
        public static int GetManhattanDistance(Cell left, Cell right)
        {
            int xDifference = Math.Abs(left.X - right.X);
            int yDifference = Math.Abs(left.Y - right.Y);

            return xDifference + yDifference;
        }
    }
}