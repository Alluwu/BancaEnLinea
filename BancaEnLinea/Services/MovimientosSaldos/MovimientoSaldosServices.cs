using BancaEnLinea.Data;
using BancaEnLinea.Dto;
using BancaEnLinea.Dto.Request;
using BancaEnLinea.Dto.Response;
using BancaEnLinea.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BancaEnLinea.Services.MovimientosSaldos;

public class MovimientoSaldosService(UserManager<IdentityUser> user, BancaDbContext context) : IMovientoSaldosService
{
    private readonly UserManager<IdentityUser> _user = user;
    private readonly BancaDbContext _context = context;

    public async Task<AuthResponse<string>> ConsultaSaldosAsync(string numeroCuenta)
    {
        var saldo = await _context.MovimientoSaldos.OrderByDescending(s => s.Fecha).FirstOrDefaultAsync();
        if (saldo == null)
        {
            return new AuthResponse<string>
            {
                StatusCode = 200,
                Message = $"Su saldo actual es {0}",
            };
        }
        return new AuthResponse<string>
        {
            StatusCode = 200,
            Message = $"Su saldo actual es {saldo.MontoActual}",
        };
    }

    public async Task<AuthResponse<List<MovimientoSaldos>>> ListaMovimientosAsync(PaginadoRequest request)
    {
        try
        {
            var query = _context.MovimientoSaldos.CountAsync();

            var movimientos = await _context.MovimientoSaldos
                .OrderByDescending(m => m.Fecha)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            return new AuthResponse<List<MovimientoSaldos>>
            {
                StatusCode = 200,
                Message = "Movimientos obtenidos exitosamente",
                Data = movimientos
            };

        }
        catch (Exception ex)
        {
            return new AuthResponse<List<MovimientoSaldos>>
            {
                StatusCode = 500,
                Message = $"Error al obtener la lista de movimientos{ex.Message}",
            };
        }
    }

    public Task<AuthResponse<List<string>>> ListaTipoMovimientoAsync()
    {
        var tipoMovimiento = new List<string>();
        foreach (var TipoMovimiento in Enum.GetValues(typeof(TipoMovimiento)))
        {
            tipoMovimiento.Add(tipoMovimiento.ToString());
        }
        var response = new AuthResponse<List<string>>
        {
            StatusCode = 200,
            Message = "Consulta Exitosa",
            Data = tipoMovimiento
        };
        return Task.FromResult(response);
    }

    public async Task<AuthResponse<string>> NuevoMovimientoAsync(MovimientoRequest request)
    {
        try
        {
            decimal? montoFinal = 0;
            decimal? montoAnterior = 0;
            var cuentaUsuario = await _context.CuentaUsuario.FirstOrDefaultAsync(c => c.NumeroCuenta == request.NumeroCuenta);
            if (cuentaUsuario == null)
            {
                return new AuthResponse<string>
                {
                    StatusCode = 400,
                    Message = "La cuenta ingresada no existe",

                };
            }
            var ultimoMovimiento = await _context.MovimientoSaldos.OrderByDescending(c => c.Fecha).FirstOrDefaultAsync();
            if (ultimoMovimiento != null)
            {
                montoAnterior = ultimoMovimiento.MontoActual;

                if (ultimoMovimiento!.MontoActual < request.Monto && (request.TipoMovimiento == "Transaferencia" || request.TipoMovimiento == "Retiro"))
                {

                    return new AuthResponse<string>
                    {
                        StatusCode = 400,
                        Message = "Fondos insuficientes para realizar la transaccion",

                    };
                }
                else if (ultimoMovimiento!.MontoActual > request.Monto && (request.TipoMovimiento == "Transaferencia" || request.TipoMovimiento == "Retiro"))
                {
                    montoFinal = montoAnterior - request.Monto;
                }
                else
                {
                    montoFinal = montoAnterior + request.Monto;
                }
            }
            else
            {
                montoFinal = request.Monto;
                montoAnterior = 0;
            }
            if (request.TipoMovimiento == "Transaferencia")
            {
                var cuentaTransferencia = await _context.CuentaUsuario.FirstOrDefaultAsync(c => c.NumeroCuenta == request.NumeroCuentaTransferencia);
                if (cuentaTransferencia == null)
                {
                    return new AuthResponse<string>
                    {
                        StatusCode = 400,
                        Message = "La cuenta a la que desea transferir no existe",

                    };
                }
            }
            var nuevo = new MovimientoSaldos
            {
                MontoAnterior = montoAnterior,
                Monto = request.Monto,
                MontoActual = montoFinal,
                TipoMovimiento = request.TipoMovimiento,
                IdCuentaUsuario = cuentaUsuario.IdCuentaUsuario,
            };
            await _context.MovimientoSaldos.AddAsync(nuevo);
            await _context.SaveChangesAsync();

            return new AuthResponse<string>
            {
                StatusCode = 200,
                Message = "Movimiento realizado con exito"
            };

        }
        catch (Exception ex)
        {
            return new AuthResponse<string>
            {
                StatusCode = 500,
                Message = $"Error al guardar el movimiento: {ex}"
            };
        }
    }
}