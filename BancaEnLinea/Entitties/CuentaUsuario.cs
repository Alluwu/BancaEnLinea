using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BancaEnLinea.Entities;

[Table("CuentaUsuario", Schema = "banca")]
public class CuentaUsuario
{
    [Key]
    [Column("idCuentaUsuario")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int IdCuentaUsuario { get; set; }
    [Column("idCuenta")]
    public int idCuenta { get; set; }
    [Column("IdCuenta")]
    public Cuenta? Cuenta { get; set; }
    [Column("idUsuario")]
    public string? IdUsuario{ get; set; }
    [ForeignKey("IdUsuario")]
    public IdentityUser? Usuario { get; set; }
    [Column("numeroCuenta")]
    public string? NumeroCuenta { get; set; }
    [Column("Alias")]
    public string? Alias { get; set; }
}