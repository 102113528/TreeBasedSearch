using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class GreedyBestFirstSearch : ISearch
    {
        public string Name { get; } = "Greedy best-first search";
        public Environment Environment { get; }

        public GreedyBestFirstSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            throw new System.NotImplementedException();
        }
    }
}