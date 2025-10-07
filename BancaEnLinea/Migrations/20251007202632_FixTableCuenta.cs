using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancaEnLinea.Migrations
{
    /// <inheritdoc />
    public partial class FixTableCuenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuentaUsuario_Cuenta_CuentaIdCuenta",
                schema: "banca",
                table: "CuentaUsuario");

            migrationBuilder.RenameColumn(
                name: "CuentaIdCuenta",
                schema: "banca",
                table: "CuentaUsuario",
                newName: "IdCuenta");

            migrationBuilder.RenameIndex(
                name: "IX_CuentaUsuario_CuentaIdCuenta",
                schema: "banca",
                table: "CuentaUsuario",
                newName: "IX_CuentaUsuario_IdCuenta");

            migrationBuilder.AddForeignKey(
                name: "FK_CuentaUsuario_Cuenta_IdCuenta",
                schema: "banca",
                table: "CuentaUsuario",
                column: "IdCuenta",
                principalSchema: "banca",
                principalTable: "Cuenta",
                principalColumn: "idCuenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuentaUsuario_Cuenta_IdCuenta",
                schema: "banca",
                table: "CuentaUsuario");

            migrationBuilder.RenameColumn(
                name: "IdCuenta",
                schema: "banca",
                table: "CuentaUsuario",
                newName: "CuentaIdCuenta");

            migrationBuilder.RenameIndex(
                name: "IX_CuentaUsuario_IdCuenta",
                schema: "banca",
                table: "CuentaUsuario",
                newName: "IX_CuentaUsuario_CuentaIdCuenta");

            migrationBuilder.AddForeignKey(
                name: "FK_CuentaUsuario_Cuenta_CuentaIdCuenta",
                schema: "banca",
                table: "CuentaUsuario",
                column: "CuentaIdCuenta",
                principalSchema: "banca",
                principalTable: "Cuenta",
                principalColumn: "idCuenta");
        }
    }
}
