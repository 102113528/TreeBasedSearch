using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public class UniformCostSearch : ISearch
    {
        public string Name { get; } = "Uniform-cost search";
        public Environment Environment { get; }

        public UniformCostSearch(Environment environment)
        {
            Environment = environment;
        }

        public List<Cell> Search()
        {
            throw new System.NotImplementedException();
        }
    }
}