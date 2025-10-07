using BancaEnLinea.Dto.Request;
using BancaEnLinea.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BancaEnLinea.Contollers;
[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IAuthService _auth;

    public UserController(IAuthService auth)
    {
        _auth = auth;
    }
    [HttpPost("registro")]
    public async Task<IActionResult> Registro([FromBody] UsuarioRequest request)
    {
        var response = await _auth.RegistroAsync(request);
        return StatusCode(response.StatusCode, response);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]Login request)
    {
        var response = await _auth.LoginAsync(request);
        return StatusCode(response.StatusCode, response);
    }


}