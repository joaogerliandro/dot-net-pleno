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
            switch(person.RealType.Type)
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

            // Logics

            return publicList;
        }

        private async Task<ICollection<PublicList>> ConsultPJPublicLists(Person person)
        {
            ICollection<PublicList> publicList = null;

            // Logics

            return publicList;
        }
    }
}
