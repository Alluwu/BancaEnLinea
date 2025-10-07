using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BancaEnLinea.Dto.Request;
using BancaEnLinea.Dto.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace BancaEnLinea.Services.Auth;

public class AuthServices(UserManager<IdentityUser> user, IConfiguration configuration) : IAuthService
{
    private readonly UserManager<IdentityUser> _user = user;
    private readonly IConfiguration _configuration = configuration;

    public async Task<AuthResponse<string>> LoginAsync(Login request)
    {
        var user = await _user.FindByNameAsync(request.nameUser);

        var validPassword = await _user.CheckPasswordAsync(user, request.Password);
        if (!validPassword || user == null)
        {
            return new AuthResponse<string>
            {
                StatusCode = 401,
                Message = "Credenciales invalidas"
            };
        }
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds

        );
        return new  AuthResponse<string>
        {
            StatusCode = 200,
            Message = "Login exitoso",
            Data = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }

    public async Task<AuthResponse<string>> RegistroAsync(UsuarioRequest request)
    {
        if (request.Email == null && request.nameUser == null && request.Password == null)
        {
            return new  AuthResponse<string>
            {
                StatusCode = 400,
                Message = "Por favor ingrese todos los datos solicitados"
            };
        }
        var user = new IdentityUser
        {
            UserName = request.nameUser,
            Email = request.Email
        };
        var result = await _user.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            return new  AuthResponse<string>
            {
                StatusCode = 200,
                Message = "Usuario creado exitosamente"
            };
        }
        var errors = string.Join("; ", result.Errors.Select(e => e.Description));
        return new  AuthResponse<string>
        {
            StatusCode = 400,
            Message = $"No se pudo registrar el usuario{errors}"
        };
    }
}