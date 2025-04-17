using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJANGO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_1_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumPautaDaEscolaOrigem",
                table: "AlunoInscritos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumPautaDaEscolaOrigem",
                table: "AlunoInscritos",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");
        }
    }
}
