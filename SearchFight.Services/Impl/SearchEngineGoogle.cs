using SearchFight.Common.Helpers;
using SearchFight.Services.Interfaces;
using SearchFight.Services.Models.Google;
using System.Threading.Tasks;

namespace SearchFight.Services.Impl
{
    public class SearchEngineGoogle : SearchEngineBase, ISearchEngine
    {
        public string Name => "Google";
        private string Url { get; set; }
        private string ApiKey { get; set; }
        private string ContextId { get; set; }

        public async Task<long> GetAmountOfResultsInSearch(string searchPhrase)
        {
            ValidateParameters(searchPhrase);
            GetConfiguration(searchPhrase);
            var response = await ExecuteClient<GoogleResponse>(Url);
            return long.Parse(response.SearchInformation.TotalResults);
        }

        public void GetConfiguration(string searchPhrase)
        {            
            Url = Configuration.GetConfigValue("Google.Url");
            ApiKey = Configuration.GetConfigValue("Google.ApiKey");
            ContextId = Configuration.GetConfigValue("Google.ContextId");
            Url = Url.Replace("{ApiKey}", ApiKey).Replace("{ContextId}", ContextId).Replace("{searchPhrase}", searchPhrase);
        }
    }
}
