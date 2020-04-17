using System;

namespace TreeBasedSearch
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Invalid number of arguments!");
                Console.WriteLine("Syntax: search <path> <method>");

                return;
            }

            string path = args[0];
            Environment environment = Environment.Parse(path);

            for (int y = 0; y < environment.Rows; y++)
            {
                for (int x = 0; x < environment.Columns; x++)
                {
                    CellState cellState = environment.GetCellAt(x, y).State;

                    if (cellState == CellState.Agent) Console.Write(">");
                    else if (cellState == CellState.Goal) Console.Write("*");
                    else if (cellState == CellState.Wall) Console.Write("#");
                    else Console.Write("-");
                }

                Console.WriteLine();
            }
        }
    }
}