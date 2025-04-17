using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJANGO.Data.Migrations
{
    /// <inheritdoc />
    public partial class TbAreaConhecimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AreaDeConhecimentoId",
                table: "Turmas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("7BE4408E-2027-4C9B-91BC-1CB08AA34BCA"));

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_AreaDeConhecimentoId",
                table: "Turmas",
                column: "AreaDeConhecimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_AreaDeConhecimentos_AreaDeConhecimentoId",
                table: "Turmas",
                column: "AreaDeConhecimentoId",
                principalTable: "AreaDeConhecimentos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_AreaDeConhecimentos_AreaDeConhecimentoId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_AreaDeConhecimentoId",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "AreaDeConhecimentoId",
                table: "Turmas");
        }
    }
}
