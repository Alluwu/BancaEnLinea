using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;

namespace BancaEnLinea.Entities;

[Table("MovimientoSaldos", Schema = "banca")]
public class MovimientoSaldos
{
    [Key]
    [Column("idMovimiento")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int IdMomvimiento { get; set; }
    [Column("idCuentaUsuario")]
    public int? IdCuentaUsuario { get; set; }
    [ForeignKey("IdCuentaUsuario")]
    public CuentaUsuario? CuentaUsuario{ get; set; }
    [Column("montoAnterior", TypeName = "decimal(10,2)")]
    public decimal? MontoAnterior { get; set; }
    [Column("monto", TypeName = "decimal(10,2)")]
    public decimal? Monto { get; set; }
    [Column("montoActual", TypeName = "decimal(10,2)")]
    public decimal? MontoActual { get; set; }
    [Column("tipoMovimiento")]
    public string? TipoMovimiento { get; set; }
    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.Now;

}