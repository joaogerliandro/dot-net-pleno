using System.Text.Json.Serialization;

namespace StallosDotnetPleno.Application.ResultObjects
{
    public class ProtocolResult
    {
        [JsonPropertyName("Protocolo")]
        public string Protocol { get; set; }
        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }
}
