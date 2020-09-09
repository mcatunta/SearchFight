using System.Runtime.Serialization;

namespace SearchFight.Services.Models.Bing
{
    [DataContract]
    public class BingResponse
    {
        [DataMember(Name = "webPages")]
        public WebPages WebPages { get; set; }
    }
}
