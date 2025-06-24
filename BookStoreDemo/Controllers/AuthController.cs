using BookStoreDemo.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetToken()
        {
            JwtToken token = _tokenService.GenerateToken();
            return Ok(token);
        }

        [HttpPost("Validate")]
        public IActionResult Validate([FromBody] string token)
        {
            var isValid = _tokenService.ValidateToken(token);
            if (isValid)
                return Ok(new { Message = "Geçerli token." });
            else
                return Unauthorized(new { Message = "Geçersiz token." });
        }
    }
}
