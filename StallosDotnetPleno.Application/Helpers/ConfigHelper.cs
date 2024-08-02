using Microsoft.Extensions.Configuration;

namespace StallosDotnetPleno.Application.Helpers
{
    public class ConfigHelper
    {
        private readonly IConfiguration _configuration;

        public ConfigHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string RosterApiHostname
        {
            get
            {
                return _configuration["AppSettings:RosterApiHostname"];
            }
        }

        public string RosterClientId
        {
            get
            {
                return _configuration["AppSettings:RosterClientId"];
            }
        }

        public string RosterSecret
        {
            get
            {
                return _configuration["AppSettings:RosterSecret"];
            }
        }

        public string RosterXApi
        {
            get
            {
                return _configuration["AppSettings:RosterXApi"];
            }
        }
    }
}
