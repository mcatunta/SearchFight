using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Services.Interfaces
{
    public interface ISearchEngine
    {
        string Name { get; }
        Task<long> GetAmountOfResultsInSearch(string searchPhrase);
    }
}
