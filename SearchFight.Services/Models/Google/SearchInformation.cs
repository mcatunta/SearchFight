using System.Runtime.Serialization;

namespace SearchFight.Services.Models.Google
{
    [DataContract]
    public class SearchInformation
    {
        [DataMember(Name = "totalResults")]
        public string TotalResults { get; set; }
    }
}
