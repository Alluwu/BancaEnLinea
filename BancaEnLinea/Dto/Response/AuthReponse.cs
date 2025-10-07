namespace BancaEnLinea.Dto.Response;

public class AuthResponse
{
    public int StatusCode { get; set; }
    public required string Message { get; set; }
    public object? Data { get; set; }
}