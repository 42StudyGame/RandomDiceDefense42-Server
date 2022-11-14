using Newtonsoft.Json;

namespace Randomdice_API.Models
{
    public class MatchedResult
    {
        public bool isMatched { get; set; }
        [JsonProperty]
        public string ip { get; set; }
        [JsonProperty]
        public string port { get; set; }
    }
}
