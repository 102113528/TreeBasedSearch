﻿using System;
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
                case "DFS":
                    search = new DepthFirstSearch(environment);
                    break;
                case "BFS":
                    search = new BreadthFirstSearch(environment);
                    break;
                case "GBFS":
                    search = new GreedyBestFirstSearch(environment);
                    break;
                case "AS":
                    search = new AStarSearch(environment);
                    break;
                case "CUS1":
                    search = new UniformCostSearch(environment);
                    break;
                case "CUS2":
                    search = new BidirectionalAStarSearch(environment);
                    break;
                default:
                    Console.WriteLine("Invalid method!");

                    Console.WriteLine("Methods:");
                    Console.WriteLine("- DFS: Depth-first search");
                    Console.WriteLine("- BFS: Breadth-first search");
                    Console.WriteLine("- GBFS: Greedy best-first search");
                    Console.WriteLine("- AS: A* search");
                    Console.WriteLine("- CUS1: Uniform-cost search");
                    Console.WriteLine("- CUS2: Bidirectional (A*) search");

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

            Cell lastCell = solution[0];

            for (int i = 1; i < solution.Count; i++)
            {
                Cell cell = solution[i];

                if (cell.Y < lastCell.Y) Console.Write("Up");
                else if (cell.X < lastCell.X) Console.Write("Left");
                else if (cell.Y > lastCell.Y) Console.Write("Down");
                else if (cell.X > lastCell.X) Console.Write("Right");

                if (i == solution.Count - 1) Console.WriteLine();
                else Console.Write(", ");

                lastCell = cell;
            }
        }
    }
}