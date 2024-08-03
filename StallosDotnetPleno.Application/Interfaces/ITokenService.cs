using System.Security.Claims;

namespace StallosDotnetPleno.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(string username);
    }
}
