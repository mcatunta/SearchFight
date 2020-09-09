using SearchFight.Common.Helpers;
using SearchFight.Services.Interfaces;
using SearchFight.Services.Models.Bing;
using System.Threading.Tasks;

namespace SearchFight.Services.Impl
{
    public class SearchEngineBing : SearchEngineBase, ISearchEngine
    {
        public string Name => "Msn Search";

        private string Url { get; set; }
        private string ApiKey { get; set; }
        private string CustomId { get; set; }

        public async Task<long> GetAmountOfResultsInSearch(string searchPhrase)
        {
            ValidateParameters(searchPhrase);
            GetConfiguration(searchPhrase);
            Client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiKey);
            var response = await ExecuteClient<BingResponse>(Url);
            return long.Parse(response.WebPages.TotalEstimatedMatches);
        }

        private void GetConfiguration(string searchPhrase)
        {
            Url = Configuration.GetConfigValue("Bing.Url");
            ApiKey = Configuration.GetConfigValue("Bing.ApiKey");
            CustomId = Configuration.GetConfigValue("Bing.CustomId");
            Url = Url.Replace("{searchPhrase}", searchPhrase).Replace("{customId}", CustomId);
        }
    }
}
