using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public interface ISearch
    {
        string Name { get; }
        Environment Environment { get; }
        List<Cell> Search();
    }
}