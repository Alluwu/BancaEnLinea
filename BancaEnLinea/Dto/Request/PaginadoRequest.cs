namespace BancaEnLinea.Dto.Request;

public class PaginadoRequest
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 25;

    public string? searchText { get; set; } = null;
    
}