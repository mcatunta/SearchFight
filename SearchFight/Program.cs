using SearchFight.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchFight
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("There are not words to search. Please try again.");
                return;
            }
            var listResult = SearchCore.GetResultOfSearch(args.ToList());
            if (listResult.Count == 0)
            {
                Console.WriteLine("We have problems in the search. Please contact technical support.");
                return;
            }
            foreach(var result in listResult)
            {
                Console.WriteLine(result);
            }
        }
    }
}
