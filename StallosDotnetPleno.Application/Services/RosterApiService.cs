using StallosDotnetPleno.Application.Helpers;
using StallosDotnetPleno.Application.Interfaces;
using StallosDotnetPleno.Application.ResultObjects;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Domain.Enums;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace StallosDotnetPleno.Application.Services
{
    public class RosterApiService : IRosterApiService
    {
        private readonly ConfigHelper _configHelper;
        private readonly Dictionary<string, string> _protocolCache;

        public Token _token;

        public RosterApiService(ConfigHelper configHelper)
        {
            _configHelper = configHelper;
            _protocolCache = new Dictionary<string, string>();
            _token = new Token { Key = null, ExpirationDateTime = null };
        }

        public async Task<List<PublicList>> ConsultPersonPublicList(Person person)
        {
            switch (person.RealType.Type)
            {
                case PersonTypeEnum.PF:
                    return await ConsultPFPublicLists(person);
                case PersonTypeEnum.PJ:
                    return await ConsultPJPublicLists(person);
                default:
                    return null;
            }
        }

        private async Task<List<PublicList>> ConsultPFPublicLists(Person person)
        {
            List<PublicList> publicList = new List<PublicList>();

            if (await ConsultList("bolsa-familia", person)) { publicList.Add(new PublicList("bolsa-familia", person)); }
            if (await ConsultList("pep", person)) { publicList.Add(new PublicList("pep", person)); }
            if (await ConsultList("interpol", person)) { publicList.Add(new PublicList("interpol", person)); }

            return publicList;
        }

        private async Task<List<PublicList>> ConsultPJPublicLists(Person person)
        {
            List<PublicList> publicList = new List<PublicList>();

            if (await ConsultList("cepim", person)) { publicList.Add(new PublicList("cepim", person)); }
            if (await ConsultList("ofac", person)) { publicList.Add(new PublicList("ofac", person)); }

            return publicList;
        }

        private async Task<bool> ConsultList(string listName, Person person)
        {
            var token = await GetAccessToken();
            var protocol = await GetProtocol(listName, token);
            var requestUri = await PrepareRequestUri(_configHelper.RosterApiHostname, listName, person);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            request.Headers.Add("accept", "application/json");
            request.Headers.Add("protocolo", protocol);
            request.Headers.Add("Authorization", token);

            try
            {
                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync(); // To get some infos about consult in future

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<string> PrepareRequestUri(string url, string listName, Person person)
        {
            string uri = url + listName + "?nome=" + Uri.EscapeDataString(person.Name);

            if (listName == "bolsa-familia" || listName == "pep" || listName == "interpol")
                uri += "&cpf=" + Uri.EscapeDataString(person.Document);

            return uri;
        }

        private async Task<string> GetAccessToken()
        {
            if (string.IsNullOrEmpty(_token.Key) && _token.ExpirationDateTime == null || DateTime.UtcNow >= _token.ExpirationDateTime)
            {
                _token = await GenerateToken();
            }

            return _token.Key;
        }

        private async Task<Token> GenerateToken()
        {
            var client = new HttpClient();
            var requestUri = _configHelper.RosterOAuthHostname + "token";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            request.Headers.Add("accept", "*/*");

            var byteArray = Encoding.ASCII.GetBytes($"{_configHelper.RosterClientId}:{_configHelper.RosterSecret}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var collection = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "client_credentials"),
                new("scope", "opendata/opendata")
            };

            request.Content = new FormUrlEncodedContent(collection);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResult>(responseBody);

                if (!string.IsNullOrEmpty(tokenResponse.Key))
                {
                    return new Token { Key = tokenResponse.Key, ExpirationDateTime = DateTime.UtcNow.AddSeconds(tokenResponse.ExpirationDateTime) };
                }
                else
                {
                    return new Token { Key = null, ExpirationDateTime = null };
                }
            }
            else
            {
                return new Token { Key = null, ExpirationDateTime = null };
            }
        }

        private async Task<string> GetProtocol(string listName, string token)
        {
            if (!_protocolCache.TryGetValue(listName, out var protocol) || string.IsNullOrEmpty(protocol)) // If not exists in the cache, create a new one
            {
                protocol = await CreateProtocol(listName, token);

                _protocolCache[listName] = protocol; // Add protocol to cache
            }

            return protocol;
        }

        private async Task<string> CreateProtocol(string listName, string token)
        {
            var client = new HttpClient();
            var requestUri = _configHelper.RosterApiHostname + "protocolo";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            request.Headers.Add("accept", "application/json");
            request.Headers.Add("x-api-key", _configHelper.RosterXApi);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var requestBody = new
            {
                Responsavel = "XPTO",
                Origem = 2,
                Consulta = new
                {
                    Nome = "XPTO",
                    Documento = "00000000000"
                },
                Listas = new string[] { listName }
            };

            request.Content = new StringContent(
                JsonConvert.SerializeObject(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var protocolResult = JsonConvert.DeserializeObject<ProtocolResult>(responseBody);

                return string.IsNullOrEmpty(protocolResult.Protocol) ? string.Empty : protocolResult.Protocol;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
