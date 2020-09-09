using System.Collections.Generic;

namespace SearchFight.Common.Helpers
{
    public static class Functions
    {
        public static List<string> GetPhraseToSearch(List<string> wordsToSearch)
        {
            var listPhrase = new List<string>();
            var specialPhrase = string.Empty;
            foreach (var word in wordsToSearch)
            {
                if (string.IsNullOrEmpty(specialPhrase))
                {
                    if (word.Contains("\"") && word.Substring(word.Length - 2, 1) != "\"")
                        specialPhrase += word.Replace("\"", "");
                    else
                        listPhrase.Add(word);
                }
                else
                {
                    specialPhrase += " " + word.Replace("\"", "");
                    if (word.Contains("\""))
                    {
                        listPhrase.Add(specialPhrase);
                        specialPhrase = string.Empty;
                    }
                }
            }
            return listPhrase;
        }
    }
}
