using SearchFight.Common.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchFight.Services.Impl
{
    public class SearchEngineBase
    {
        public HttpClient Client;

        public SearchEngineBase()
        {
            Client = new HttpClient();
        }

        public async Task<T> ExecuteClient<T>(string resource)
        {
            var response = await Client.GetAsync(resource);
            if (response.IsSuccessStatusCode)
            {
                return JsonParser.Deserialize<T>(await response.Content.ReadAsStringAsync());
            }
            throw new ApplicationException(string.Format("Problems getting web resource: {0}", resource));
        }
        
        public void ValidateParameters(string searchPhrase)
        {
            if (string.IsNullOrEmpty(searchPhrase))
                throw new ArgumentException("There are not parameters to search.");
        }
    }
}
