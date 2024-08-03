using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StallosDotnetPleno.Application.Interfaces;

namespace StallosDotnetPleno.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ITokenService tokenService, ILogger<AuthController> logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("token")]
        [Authorize(AuthenticationSchemes = "Basic")]
        public IActionResult GenerateToken()
        {
            try
            {
                string username = HttpContext.User.Identity!.Name;

                if (username != null && HttpContext.User.Identity.IsAuthenticated)
                {
                    string token = _tokenService.GenerateToken(username);

                    return Ok(new
                    {
                        Success = true,
                        Message = "Token generated successfully.",
                        Token = token
                    });
                }

                return Unauthorized(new
                {
                    Success = false,
                    Message = "Unauthorized ! Please set a username to procede. Or try again later.",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, String.Format("Error generating token for user: {0}", HttpContext.User.Identity!.Name));

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
