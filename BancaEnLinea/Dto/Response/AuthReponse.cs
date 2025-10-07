namespace BancaEnLinea.Dto.Response;

public class AuthResponse<T>
{
    public int StatusCode { get; set; }
    public required string Message { get; set; }
    public T? Data { get; set; }
}