using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class DepthFirstSearch : ISearch
    {
        public string Name { get; } = "Depth-first search";
        public Environment Environment { get; }

        public DepthFirstSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            throw new System.NotImplementedException();
        }
    }
}