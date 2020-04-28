using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class AStarSearch : ISearch
    {
        public string Name { get; } = "A*";
        public Environment Environment { get; }

        public AStarSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            throw new System.NotImplementedException();
        }
    }
}