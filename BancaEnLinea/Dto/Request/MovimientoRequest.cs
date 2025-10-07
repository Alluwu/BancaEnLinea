namespace BancaEnLinea.Dto.Request;

public class MovimientoRequest
{

    public string? NumeroCuenta { get; set; }

    public decimal? Monto { get; set; }

    public string? TipoMovimiento { get; set; }
    
    public string? NumeroCuentaTransferencia { get; set;}

}