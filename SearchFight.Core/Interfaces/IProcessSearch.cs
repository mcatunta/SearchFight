using SearchFight.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Core.Interfaces
{
    public interface IProcessSearch
    {
        Task<List<ResultSearch>> GetResultOfSearch(List<string> listWords);
        List<BestResult> GetBestResultsInResultSearch(List<ResultSearch> listResultSearch);
        List<string> GetResultOfSearchFight(List<ResultSearch> listResultSearch
            , List<BestResult> listBestResult);
    }
}
