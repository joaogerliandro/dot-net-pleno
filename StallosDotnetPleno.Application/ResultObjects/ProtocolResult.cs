using Newtonsoft.Json;

namespace StallosDotnetPleno.Application.ResultObjects
{
    public class ProtocolResult
    {
        [JsonProperty("Protocolo")]
        public string Protocol { get; set; }
    }
}
