using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJANGO.Data.Migrations
{
    /// <inheritdoc />
    public partial class TbDisciplinaNivelDeEnsino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeDisciplina",
                table: "Disciplinas",
                type: "varchar(45)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)");

            migrationBuilder.AddColumn<Guid>(
                name: "NiveisDeEnsinoId",
                table: "Disciplinas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_NiveisDeEnsinoId",
                table: "Disciplinas",
                column: "NiveisDeEnsinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplinas_NiveisDeEnsinos_NiveisDeEnsinoId",
                table: "Disciplinas",
                column: "NiveisDeEnsinoId",
                principalTable: "NiveisDeEnsinos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplinas_NiveisDeEnsinos_NiveisDeEnsinoId",
                table: "Disciplinas");

            migrationBuilder.DropIndex(
                name: "IX_Disciplinas_NiveisDeEnsinoId",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "NiveisDeEnsinoId",
                table: "Disciplinas");

            migrationBuilder.AlterColumn<string>(
                name: "NomeDisciplina",
                table: "Disciplinas",
                type: "varchar(25)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)");
        }
    }
}
