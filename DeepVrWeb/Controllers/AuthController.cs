using DeepVrWeb.DTO;
using DeepVrWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeepVrWeb.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
    {
        try
        {
            var response = await _authService.Login(request);
            return Ok(response);
        }
        catch (DeepVrLibrary.Exceptions.AuthenticationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] AuthRequest request)
    {
        try
        {
            var user = await _authService.Register(request);
            return Ok(new { message = "Пользователь успешно зарегистрирован", userId = user?.Id });
        }
        catch (DeepVrLibrary.Exceptions.AuthenticationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}