using StallosDotnetPleno.Application.Helpers;
using StallosDotnetPleno.Application.Interfaces;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Domain.Enums;

namespace StallosDotnetPleno.Application.Services.RosterServices
{
    public class RosterApiService : IRosterApiService
    {
        private readonly ConfigHelper _configHelper;

        public RosterApiService(ConfigHelper configHelper)
        {
            _configHelper = configHelper;
        }

        public async Task<ICollection<PublicList>> ConsultPersonPublicList(Person person)
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

        private async Task<ICollection<PublicList>> ConsultPFPublicLists(Person person)
        {
            ICollection<PublicList> publicList = null;

            if (await ConsultList("bolsa-familia", person)) { publicList.Add(new PublicList("bolsa-familia", person)); }
            if (await ConsultList("pep", person)) { publicList.Add(new PublicList("pep", person)); }
            if (await ConsultList("interpol", person)) { publicList.Add(new PublicList("interpol", person)); }

            return publicList;
        }

        private async Task<ICollection<PublicList>> ConsultPJPublicLists(Person person)
        {
            ICollection<PublicList> publicList = null;

            if (await ConsultList("cepim", person)) { publicList.Add(new PublicList("cepim", person)); }
            if (await ConsultList("ofac", person)) { publicList.Add(new PublicList("ofac", person)); }

            return publicList;
        }

        private async Task<bool> ConsultList(string listName, Person person)
        {
            var protocol = "protocol"; // Get Protocol
            var token = "token"; // Get Token

            var client = new HttpClient();
            var requestUri = PrepareRequestUri(_configHelper.RosterApiHostname, listName, person);
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

        private static string PrepareRequestUri(string url, string listName, Person person)
        {
            string uri = url + listName + "?nome=" + Uri.EscapeDataString(person.Name);

            if (listName == "bolsa-familia" || listName == "pep" || listName == "interpol")
                uri += "&cpf=" + person.Document;

            return uri;
        }
    }
}
