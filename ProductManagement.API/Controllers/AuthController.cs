using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Services;
using ProductManagement.Application.DTOs; // DTO'ları içe aktarın
using ProductManagement.Infrastructure.Services;
using System.Threading.Tasks;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _userService.RegisterUserAsync(model.Username, model.Email, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _userService.LoginUserAsync(model.Username, model.Password);

            if (result.Succeeded)
            {
                // Kullanıcı bilgilerini al
                var user = await _userService.FindByNameAsync(model.Username);

                // JWT token oluştur
                var token = _jwtService.GenerateJwtToken(user.Id, "Admin"); // Rolü dinamik olarak ekleyin

                // JWT token döndür
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}
