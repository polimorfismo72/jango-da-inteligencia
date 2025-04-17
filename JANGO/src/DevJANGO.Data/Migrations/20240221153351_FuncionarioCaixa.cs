using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJANGO.Data.Migrations
{
    /// <inheritdoc />
    public partial class FuncionarioCaixa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FuncionarioCaixaId",
                table: "AlunoMatriculados",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FuncionarioCaixaId",
                table: "AlunoInscritos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AlunoMatriculados_FuncionarioCaixaId",
                table: "AlunoMatriculados",
                column: "FuncionarioCaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoInscritos_FuncionarioCaixaId",
                table: "AlunoInscritos",
                column: "FuncionarioCaixaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoInscritos_FuncionarioCaixas_FuncionarioCaixaId",
                table: "AlunoInscritos",
                column: "FuncionarioCaixaId",
                principalTable: "FuncionarioCaixas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoMatriculados_FuncionarioCaixas_FuncionarioCaixaId",
                table: "AlunoMatriculados",
                column: "FuncionarioCaixaId",
                principalTable: "FuncionarioCaixas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlunoInscritos_FuncionarioCaixas_FuncionarioCaixaId",
                table: "AlunoInscritos");

            migrationBuilder.DropForeignKey(
                name: "FK_AlunoMatriculados_FuncionarioCaixas_FuncionarioCaixaId",
                table: "AlunoMatriculados");

            migrationBuilder.DropIndex(
                name: "IX_AlunoMatriculados_FuncionarioCaixaId",
                table: "AlunoMatriculados");

            migrationBuilder.DropIndex(
                name: "IX_AlunoInscritos_FuncionarioCaixaId",
                table: "AlunoInscritos");

            migrationBuilder.DropColumn(
                name: "FuncionarioCaixaId",
                table: "AlunoMatriculados");

            migrationBuilder.DropColumn(
                name: "FuncionarioCaixaId",
                table: "AlunoInscritos");
        }
    }
}
