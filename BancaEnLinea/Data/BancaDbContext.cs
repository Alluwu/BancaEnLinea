using BancaEnLinea.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BancaEnLinea.Data;

public class BancaDbContext : IdentityDbContext
{
    public BancaDbContext(DbContextOptions<BancaDbContext> options) : base(options)
    {
    }
    public DbSet<Cuenta> Cuenta { get; set; }
    public DbSet<CuentaUsuario> CuentaUsuario { get; set; }
    public DbSet<MovimientoSaldos> MovimientoSaldos { get; set; }
}