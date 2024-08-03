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

        public string BasicAuthUsername
        {
            get
            {
                return _configuration["Basic:BasicAuthUsername"];
            }
        }

        public string BasicAuthPassword
        {
            get
            {
                return _configuration["Basic:BasicAuthPassword"];
            }
        }

        public string ApiSecret
        {
            get
            {
                return _configuration["Jwt:ApiSecret"];
            }
        }

        public string RosterOAuthHostname
        {
            get
            {
                return _configuration["AppSettings:RosterOAuthHostname"];
            }
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
