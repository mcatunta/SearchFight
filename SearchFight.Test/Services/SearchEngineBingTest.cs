using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchFight.Services.Impl;
using SearchFight.Services.Interfaces;
using System;

namespace SearchFight.Test.Services
{
    [TestClass]
    public class SearchEngineBingTest : BaseTest
    {
        private ISearchEngine SearchEngine;

        [TestInitialize]
        public void Setup()
        {
            SearchEngine = new SearchEngineBing();
        }

        [TestMethod]
        public void GetAmountOfResultsInSearchErrorWithArguments()
        {
            Throws<ArgumentException>(() =>
            {
                var result = SearchEngine.GetAmountOfResultsInSearch(string.Empty).Result;
            }, "There are not parameters to search.");
        }
    }
}
