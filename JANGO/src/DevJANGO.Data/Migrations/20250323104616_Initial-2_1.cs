using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJANGO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial2_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MinhaSequenciaCodigoAluno");

            migrationBuilder.AlterColumn<int>(
                name: "Codigo",
                table: "AlunoInscritos",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR MinhaSequenciaCodigoAluno",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR MinhaSequencia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "MinhaSequenciaCodigoAluno");

            migrationBuilder.AlterColumn<int>(
                name: "Codigo",
                table: "AlunoInscritos",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR MinhaSequencia",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR MinhaSequenciaCodigoAluno");
        }
    }
}
