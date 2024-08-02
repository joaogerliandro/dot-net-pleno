using System.Text.Json.Serialization;

namespace StallosDotnetPleno.Application.ResultObjects
{
    public class TokenResult
    {
        [JsonPropertyName("access_token")]
        public string Key;
        [JsonPropertyName("expires_in")]
        public long ExpirationDateTime;
        [JsonPropertyName("token_type")]
        public string TokenType;
    }
}
