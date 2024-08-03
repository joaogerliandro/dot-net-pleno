using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using StallosDotnetPleno.Application.Helpers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace StallosDotnetPleno.Api.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ConfigHelper _configHelper;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ConfigHelper configHelper)
            : base(options, logger, encoder, clock)
        {
            _configHelper = configHelper;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            var authHeader = Request.Headers["Authorization"].ToString();

            if (authHeader.StartsWith("Basic "))
            {
                var token = authHeader.Substring("Basic ".Length).Trim();
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                if (username == _configHelper.BasicAuthUsername && password == _configHelper.BasicAuthPassword)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, username) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }

                return AuthenticateResult.Fail("Invalid Credentials");
            }

            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
}
