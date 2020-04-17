using System;
using System.Collections.Generic;
using TreeBasedSearch.Searches;

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

            string path = args[0], method = args[1].ToUpper();
            Environment environment = Environment.Parse(path);

            Console.WriteLine("Environment: ");

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

            ISearch search;

            switch (method)
            {
                default:
                    Console.WriteLine("Invalid method!");
                    Console.WriteLine("Methods: DFS, BFS, GBFS, AS, CUS1, CUS2");

                    return;
            }

            List<Cell> solution = search.Search();

            if (solution == null)
            {
                Console.WriteLine("Unable to find a solution!");
                return;
            }

            Console.WriteLine($"Solution (using {search.Name}): ");

            for (int y = 0; y < environment.Rows; y++)
            {
                for (int x = 0; x < environment.Columns; x++)
                {
                    Cell cell = environment.GetCellAt(x, y);

                    if (solution.Contains(cell) && cell.State != CellState.Agent && cell.State != CellState.Goal) Console.Write("+");
                    else if (cell.State == CellState.Agent) Console.Write(">");
                    else if (cell.State == CellState.Goal) Console.Write("*");
                    else if (cell.State == CellState.Wall) Console.Write("#");
                    else Console.Write("-");
                }

                Console.WriteLine();
            }
        }
    }
}