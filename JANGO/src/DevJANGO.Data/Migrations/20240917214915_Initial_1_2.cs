using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJANGO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_1_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PagamentoMaticula",
                table: "PagamentoPropinas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagamentoMaticula",
                table: "PagamentoPropinas");
        }
    }
}
