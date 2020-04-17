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
    }
}