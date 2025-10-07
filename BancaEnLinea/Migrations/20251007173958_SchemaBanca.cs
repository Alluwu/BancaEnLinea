using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BancaEnLinea.Migrations
{
    /// <inheritdoc />
    public partial class SchemaBanca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "banca");

            migrationBuilder.CreateTable(
                name: "Cuenta",
                schema: "banca",
                columns: table => new
                {
                    idCuenta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUsuarioCreacion = table.Column<string>(type: "text", nullable: true),
                    IdUsuario = table.Column<string>(type: "text", nullable: true),
                    nombreCuenta = table.Column<string>(type: "text", nullable: true),
                    tipoCuenta = table.Column<decimal>(type: "numeric", nullable: true),
                    nomenclatura = table.Column<decimal>(type: "numeric", nullable: true),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.idCuenta);
                    table.ForeignKey(
                        name: "FK_Cuenta_AspNetUsers_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CuentaUsuario",
                schema: "banca",
                columns: table => new
                {
                    idCuentaUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idCuenta = table.Column<int>(type: "integer", nullable: false),
                    CuentaIdCuenta = table.Column<int>(type: "integer", nullable: true),
                    idUsuario = table.Column<string>(type: "text", nullable: true),
                    numeroCuenta = table.Column<string>(type: "text", nullable: true),
                    Alias = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaUsuario", x => x.idCuentaUsuario);
                    table.ForeignKey(
                        name: "FK_CuentaUsuario_AspNetUsers_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CuentaUsuario_Cuenta_CuentaIdCuenta",
                        column: x => x.CuentaIdCuenta,
                        principalSchema: "banca",
                        principalTable: "Cuenta",
                        principalColumn: "idCuenta");
                });

            migrationBuilder.CreateTable(
                name: "MovimientoSaldos",
                schema: "banca",
                columns: table => new
                {
                    idMovimiento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idCuentaUsuario = table.Column<int>(type: "integer", nullable: true),
                    montoAnterior = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    monto = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    montoActual = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    tipoMovimiento = table.Column<string>(type: "text", nullable: true),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoSaldos", x => x.idMovimiento);
                    table.ForeignKey(
                        name: "FK_MovimientoSaldos_CuentaUsuario_idCuentaUsuario",
                        column: x => x.idCuentaUsuario,
                        principalSchema: "banca",
                        principalTable: "CuentaUsuario",
                        principalColumn: "idCuentaUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_IdUsuario",
                schema: "banca",
                table: "Cuenta",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaUsuario_CuentaIdCuenta",
                schema: "banca",
                table: "CuentaUsuario",
                column: "CuentaIdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaUsuario_idUsuario",
                schema: "banca",
                table: "CuentaUsuario",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoSaldos_idCuentaUsuario",
                schema: "banca",
                table: "MovimientoSaldos",
                column: "idCuentaUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientoSaldos",
                schema: "banca");

            migrationBuilder.DropTable(
                name: "CuentaUsuario",
                schema: "banca");

            migrationBuilder.DropTable(
                name: "Cuenta",
                schema: "banca");
        }
    }
}
