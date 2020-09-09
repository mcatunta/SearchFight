using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchFight.Core.Impl;
using SearchFight.Core.Interfaces;
using SearchFight.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchFight.Test.Core
{
    [TestClass]
    public class ProcessSearchTest : BaseTest
    {
        private IProcessSearch ProcessSearch;

        [TestInitialize]
        public void Setup()
        {
            ProcessSearch = new ProcessSearch();
        }

        [TestMethod]
        public void GetResultOfSearchErrorWithArguments()
        {
            Throws<ArgumentException>(() =>
            {
                var result = ProcessSearch.GetResultOfSearch(null).Result;
            }, "There are not words to search.");
        }

        [TestMethod]
        public void GetBestResultsInResultSearchErrorWithArguments()
        {
            Throws<ArgumentException>(() =>
            {
                ProcessSearch.GetBestResultsInResultSearch(null);
            }, "There are not results in the search to get best results.");
        }

        [TestMethod]
        public void GetBestResultsInResultSearchSuccess()
        {
            var listResult = new List<ResultSearch>()
            {
                new ResultSearch() { PhraseSearch = ".net", SearchEngine = "Google", AmountResults = 4450000000L },
                new ResultSearch() { PhraseSearch = ".net", SearchEngine = "Msn Search", AmountResults = 12354420L },
                new ResultSearch() { PhraseSearch = "java", SearchEngine = "Google", AmountResults = 966000000L },
                new ResultSearch() { PhraseSearch = "java", SearchEngine = "Msn Search", AmountResults = 94381485L }
            };
            var result = ProcessSearch.GetBestResultsInResultSearch(listResult);
            Assert.AreEqual(result.FirstOrDefault(p => p.SearchEngine == "Google").PhraseSearch, ".net");
            Assert.AreEqual(result.FirstOrDefault(p => p.SearchEngine == "Msn Search").PhraseSearch, "java");
        }

        [TestMethod]
        public void GetResultOfSearchFightErrorWithArgumentsInSearchResult()
        {
            Throws<ArgumentException>(() =>
            {
                ProcessSearch.GetResultOfSearchFight(null, null);
            }, "There are not results in the search to get results in fight search.");
        }

        [TestMethod]
        public void GetResultOfSearchFightErrorWithArgumentsInBestResult()
        {
            var listResult = new List<ResultSearch>()
            {
                new ResultSearch() { PhraseSearch = ".net", SearchEngine = "Google", AmountResults = 4450000000L },
            };
            Throws<ArgumentException>(() =>
            {
                ProcessSearch.GetResultOfSearchFight(listResult, null);
            }, "There are not best results to get results in fight search.");
        }

        [TestMethod]
        public void GetResultOfSearchFightSuccess()
        {
            var listResult = new List<ResultSearch>()
            {
                new ResultSearch() { PhraseSearch = ".net", SearchEngine = "Google", AmountResults = 4450000000L },
                new ResultSearch() { PhraseSearch = ".net", SearchEngine = "Msn Search", AmountResults = 12354420L },
                new ResultSearch() { PhraseSearch = "java", SearchEngine = "Google", AmountResults = 966000000L },
                new ResultSearch() { PhraseSearch = "java", SearchEngine = "Msn Search", AmountResults = 94381485L }
            };
            var bestResult = new List<BestResult>()
            {
                new BestResult() { SearchEngine = "Google", PhraseSearch = ".net"},
                new BestResult() { SearchEngine = "Msn Search", PhraseSearch = "java"}
            };
            var result = ProcessSearch.GetResultOfSearchFight(listResult, bestResult);

            Assert.AreEqual(result.Count, 5);
            Assert.IsTrue(result.FirstOrDefault(p => p.Contains("Total")).Contains(".net"));
        }
    }
}
