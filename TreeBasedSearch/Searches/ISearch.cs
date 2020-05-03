using System.Collections.Generic;

namespace TreeBasedSearch.Searches
{
    public interface ISearch
    {
        /// <summary>
        /// The name of the search algorithm.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The environment to be searched.
        /// </summary>
        Environment Environment { get; }

        /// <summary>
        /// Searches through an environment, calculating the path to move from the initial state to the closest goal state.
        /// </summary>
        /// <returns>A list of cell objects in order from the initial state to the closest goal state.</returns>
        List<Cell> Search();
    }
}