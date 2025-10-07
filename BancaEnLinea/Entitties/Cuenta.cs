using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BancaEnLinea.Entities;

[Table("Cuenta", Schema = "banca")]
public class Cuenta
{
    [Key]
    [Column("idCuenta")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int IdCuenta { get; set; }
    [Column("idUsuarioCreacion")]
    public string? IdUsuarioCreacion { get; set; }
    [ForeignKey("IdUsuario")]
    public IdentityUser? Usuario { get; set; }
    [Column("nombreCuenta")]
    public string? NombreCuenta { get; set; }
    [Column("tipoCuenta")]
    public decimal? TipoCuenta { get; set; }
    [Column("nomenclatura")]
    public decimal? Nomenclatura { get; set; }
    [Column("fecha")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

}