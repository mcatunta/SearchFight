using System.Runtime.Serialization;

namespace SearchFight.Services.Models.Google
{
    [DataContract]
    public class GoogleResponse
    {
        [DataMember(Name = "searchInformation")]
        public SearchInformation SearchInformation { get; set; }
    }
}
