using BancaEnLinea.Dto.Request;
using BancaEnLinea.Services.MovimientosSaldos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BancaEnLinea.Contollers;

[ApiController]
[Route("movimiento")]
[Authorize]
public class MovimientoController : ControllerBase
{
    private readonly IMovientoSaldosService _movimiento;

    public MovimientoController(IMovientoSaldosService movimiento)
    {
        _movimiento = movimiento;
    }
    [HttpPost("nuevoMovimiento")]
    public async Task<IActionResult> CrearMovimiento([FromBody] MovimientoRequest request)
    {
        var response = await _movimiento.NuevoMovimientoAsync(request);
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet("tipo")]
    public async Task<IActionResult> ListTipo()
    {
        var response = await _movimiento.ListaTipoMovimientoAsync();
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet("listaMovimiento")]
    public async Task<IActionResult> ListaMovimiento([FromQuery] PaginadoRequest request)
    {
        var response = await _movimiento.ListaMovimientosAsync(request);
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet("saldo")]
    public async Task<IActionResult> Saldos([FromQuery] string numeroCuenta)
    {
        var response = await _movimiento.ConsultaSaldosAsync(numeroCuenta);
        return StatusCode(response.StatusCode, response);
    }

}