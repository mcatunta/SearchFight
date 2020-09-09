using System.Runtime.Serialization;

namespace SearchFight.Services.Models.Bing
{
    [DataContract]
    public class WebPages
    {
        [DataMember(Name = "totalEstimatedMatches")]
        public string TotalEstimatedMatches { get; set; }
    }
}
