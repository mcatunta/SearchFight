using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchFight.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight.Test.Common.Helpers
{
    [TestClass]
    public class ListFunctionsTest
    {
        [TestMethod]
        public void GetWordsToSearchWithNormalWords()
        {
            var words = new List<string>() { ".net", "java" };
            var result = Functions.GetPhraseToSearch(words);
            Assert.AreEqual(words.Count, result.Count);
        }

        [TestMethod]
        public void GetWordsToSearchWithMoreThanOneWordToSearch()
        {
            var words = new List<string>() { ".net", "java", "\"java", "script\"" };
            var result = Functions.GetPhraseToSearch(words);
            Assert.AreEqual(words.Count, result.Count + 1);
        }
    }
}
