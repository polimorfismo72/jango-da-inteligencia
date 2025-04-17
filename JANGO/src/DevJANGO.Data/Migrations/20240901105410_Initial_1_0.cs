using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJANGO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_1_0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroDeTransacaoDePagamento",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "NumeroDeTransacaoDePagamento",
                table: "PagamentoPropinas");

            migrationBuilder.DropColumn(
                name: "NumeroDeTransacaoDePagamento",
                table: "PagamentoMultas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroDeTransacaoDePagamento",
                table: "Pedidos",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumeroDeTransacaoDePagamento",
                table: "PagamentoPropinas",
                type: "varchar(29)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumeroDeTransacaoDePagamento",
                table: "PagamentoMultas",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");
        }
    }
}
