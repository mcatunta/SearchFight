using SearchFight.Common.Helpers;
using SearchFight.Core.Interfaces;
using SearchFight.Core.Models;
using SearchFight.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchFight.Core.Impl
{
    public class ProcessSearch : IProcessSearch
    {
        private List<ISearchEngine> ListSearchEngine;

        public ProcessSearch()
        {
            LoadSearchEngines();
        }

        private void LoadSearchEngines()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(p => p.FullName.StartsWith("SearchFight"));
            ListSearchEngine = assemblies.SelectMany(p => p.GetTypes())
                .Where(p => p.GetInterface(typeof(ISearchEngine).ToString()) != null)
                .Select(p => Activator.CreateInstance(p) as ISearchEngine).ToList();
        }

        public async Task<List<ResultSearch>> GetResultOfSearch(List<string> listWords)
        {
            if (listWords == null || listWords.Count == 0)
                throw new ArgumentException("There are not words to search.");
            var listResultSearch = new List<ResultSearch>();
            var listPhrases = Functions.GetPhraseToSearch(listWords);
            foreach(var searchEngine in ListSearchEngine)
            {
                foreach(var phrase in listPhrases)
                {
                    var resultSearch = new ResultSearch()
                    {
                        PhraseSearch = phrase,
                        SearchEngine = searchEngine.Name,
                        AmountResults =
                            await searchEngine.GetAmountOfResultsInSearch(phrase.Replace(" ", "+"))
                    };
                    listResultSearch.Add(resultSearch);
                }
            }
            return listResultSearch;
        }

        public List<BestResult> GetBestResultsInResultSearch(List<ResultSearch> listResultSearch)
        {
            if (listResultSearch == null || listResultSearch.Count == 0)
                throw new ArgumentException("There are not results in the search to get best results.");
            var listBestResult = new List<BestResult>();
            foreach(var searchEngine in ListSearchEngine)
            {
                var listResultBySearchEngine = listResultSearch.Where(p => p.SearchEngine == searchEngine.Name).ToList();
                var maxValue = listResultBySearchEngine.Max(p => p.AmountResults);
                var bestResult = new BestResult()
                {
                    SearchEngine = searchEngine.Name,
                    PhraseSearch = listResultBySearchEngine.FirstOrDefault(p => p.AmountResults == maxValue).PhraseSearch
                };
                listBestResult.Add(bestResult);
            }
            return listBestResult;
        }

        public List<string> GetResultOfSearchFight(List<ResultSearch> listResultSearch
            , List<BestResult> listBestResult)
        {
            if (listResultSearch == null || listResultSearch.Count == 0)
                throw new ArgumentException("There are not results in the search to get results in fight search.");
            if (listBestResult == null || listBestResult.Count == 0)
                throw new ArgumentException("There are not best results to get results in fight search.");
            var listResult = new List<string>();
            var listPhrases = listResultSearch.GroupBy(p => p.PhraseSearch).Select(p => p.Key).ToList();
            foreach(var phrase in listPhrases)
            {
                var result = string.Format("{0}:",phrase);
                foreach(var searchEngine in ListSearchEngine)
                {
                    result += string.Format(" {0}: {1}", searchEngine.Name, listResultSearch
                        .FirstOrDefault(p => p.PhraseSearch == phrase && p.SearchEngine == searchEngine.Name)
                        .AmountResults);
                }
                listResult.Add(result);
            }
            foreach(var bestResult in listBestResult)
            {
                listResult.Add(string.Format("{0} winner: {1}", bestResult.SearchEngine, bestResult.PhraseSearch));
            }
            var maxValue = listResultSearch.Max(p => p.AmountResults);
            listResult.Add(string.Format("Total winner: {0}",
                listResultSearch.FirstOrDefault(p => p.AmountResults == maxValue).PhraseSearch)); 
            return listResult;
        }
    }
}
