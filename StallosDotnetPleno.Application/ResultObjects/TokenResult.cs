using Newtonsoft.Json;

namespace StallosDotnetPleno.Application.ResultObjects
{
    public class TokenResult
    {
        [JsonProperty("access_token")]
        public string Key;
        [JsonProperty("expires_in")]
        public int ExpirationDateTime;
        [JsonProperty("token_type")]
        public string TokenType;
    }
}
