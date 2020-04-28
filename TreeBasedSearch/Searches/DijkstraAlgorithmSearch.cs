using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class DijkstraAlgorithmSearch : ISearch
    {
        public string Name { get; } = "Dijkstra's algorithm";
        public Environment Environment { get; }

        public DijkstraAlgorithmSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            throw new System.NotImplementedException();
        }
    }
}