using BancaEnLinea.Dto.Request;
using BancaEnLinea.Dto.Response;
using Microsoft.AspNetCore.Identity;

namespace BancaEnLinea.Services.Auth;

public interface IAuthService
{
    Task< AuthResponse<string>> RegistroAsync(UsuarioRequest request);
    Task< AuthResponse<string>> LoginAsync(Login request);
}