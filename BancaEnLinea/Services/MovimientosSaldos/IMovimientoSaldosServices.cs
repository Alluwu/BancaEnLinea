using BancaEnLinea.Dto.Request;
using BancaEnLinea.Dto.Response;
using BancaEnLinea.Entities;

namespace BancaEnLinea.Services.MovimientosSaldos;

public interface IMovientoSaldosService
{
    Task<AuthResponse<List<string>>> ListaTipoMovimientoAsync();
    Task<AuthResponse<List<MovimientoSaldos>>> ListaMovimientosAsync(PaginadoRequest request);
    Task<AuthResponse<string>> NuevoMovimientoAsync(MovimientoRequest request);
    Task<AuthResponse<string>> ConsultaSaldosAsync(string numeroCuenta);
}