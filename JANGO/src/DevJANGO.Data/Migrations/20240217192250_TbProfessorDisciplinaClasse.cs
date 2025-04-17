using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJANGO.Data.Migrations
{
    /// <inheritdoc />
    public partial class TbProfessorDisciplinaClasse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeClasse",
                table: "ProfessorDisciplinaClasses",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeDisciplina",
                table: "ProfessorDisciplinaClasses",
                type: "varchar(45)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeClasse",
                table: "ProfessorDisciplinaClasses");

            migrationBuilder.DropColumn(
                name: "NomeDisciplina",
                table: "ProfessorDisciplinaClasses");
        }
    }
}
