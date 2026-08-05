using KASHOP.BLL.Services;
using KASHOP.DAL.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthunticationService _authunticationService;

        public AccountController(IAuthunticationService authunticationService)
        {
            _authunticationService = authunticationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authunticationService.RegisterAsync(request);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login (LoginRequest request)
        {
            var result = await _authunticationService.LoginAsync(request);
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]ConfirmationRequest request)
        {
            var result = await _authunticationService.ConfirmEmail(request);
            if (result)
            {
                return Ok(new { Message = "Email confirmed successfully." });
            }
            else
            {
                return BadRequest(new { Message = "Email confirmation failed." });
            }
        }
    }
}
