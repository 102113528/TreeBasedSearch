using System.Collections.Generic;
using System.IO;
using System.Linq;
using TreeBasedSearch.Utilities;

namespace TreeBasedSearch
{
    public class Environment
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        private List<Cell> _cells;

        private Environment() { }

        /// <summary>
        /// Gets the cell at the specified x and y position.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <returns>The cell, or null if no cell is found.</returns>
        public Cell GetCellAt(int x, int y)
        {
            return _cells.FirstOrDefault(cell => cell.X == x && cell.Y == y);
        }

        /// <summary>
        /// Gets a list of cell objects that have the specified cell state type.
        /// </summary>
        /// <param name="state">The cell state to match.</param>
        /// <returns>A list of cells that have the matching cell state.</returns>
        public List<Cell> GetCellsByState(CellState state)
        {
            return _cells.FindAll(cell => cell.State == state);
        }

        /// <summary>
        /// Parses a specified file and creates an environment object based on its contents.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>An environment object based on the contents of the file.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static Environment Parse(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"The file \"{path}\" does not exist!");

            Environment environment = new Environment();
            string[] lines = File.ReadAllLines(path);

            string[] tokens = lines[0].Remove("[", "]").Split(',');
            environment.Rows = int.Parse(tokens[0]);
            environment.Columns = int.Parse(tokens[1]);

            environment._cells = new List<Cell>(environment.Columns * environment.Rows);

            for (int y = 0; y < environment.Rows; y++)
            {
                for (int x = 0; x < environment.Columns; x++)
                {
                    Cell cell = new Cell(x, y, CellState.Empty);
                    environment._cells.Add(cell);
                }
            }

            tokens = lines[1].Remove("(", ")").Split(',');
            int initialAgentX = int.Parse(tokens[0]);
            int initialAgentY = int.Parse(tokens[1]);

            environment.GetCellAt(initialAgentX, initialAgentY).State = CellState.Agent;

            tokens = lines[2].Remove("(", ")").Split('|');

            for (int i = 0; i < tokens.Length; i++)
            {
                string[] split = tokens[i].Split(',');
                int x = int.Parse(split[0]), y = int.Parse(split[1]);

                environment.GetCellAt(x, y).State = CellState.Goal;
            }

            for (int i = 3; i < lines.Length; i++)
            {
                tokens = lines[i].Remove("(", ")").Split(',');

                int left = int.Parse(tokens[0]), top = int.Parse(tokens[1]);
                int right = left + int.Parse(tokens[2]), bottom = top + int.Parse(tokens[3]);

                for (int x = left; x < right; x++)
                {
                    for (int y = top; y < bottom; y++)
                    {
                        environment.GetCellAt(x, y).State = CellState.Wall;
                    }
                }
            }

            return environment;
        }
    }
}