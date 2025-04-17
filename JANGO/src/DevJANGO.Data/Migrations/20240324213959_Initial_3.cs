using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJANGO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PagamentoPropinas_Meses_MesId",
                table: "PagamentoPropinas");

            migrationBuilder.DropTable(
                name: "PagamentoPropinaItems");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Propinas");

            migrationBuilder.RenameColumn(
                name: "MesId",
                table: "PagamentoPropinas",
                newName: "PropinaId");

            migrationBuilder.RenameIndex(
                name: "IX_PagamentoPropinas_MesId",
                table: "PagamentoPropinas",
                newName: "IX_PagamentoPropinas_PropinaId");

            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Propinas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "PagamentoPropinas",
                type: "varchar(29)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "FuncionarioCaixaId",
                table: "PagamentoPropinas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "NumeroDeMeses",
                table: "PagamentoPropinas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoPropina",
                table: "PagamentoPropinas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "AnoLetivo",
                table: "AlunoInscritos",
                type: "varchar(9)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoPropinas_FuncionarioCaixaId",
                table: "PagamentoPropinas",
                column: "FuncionarioCaixaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PagamentoPropinas_FuncionarioCaixas_FuncionarioCaixaId",
                table: "PagamentoPropinas",
                column: "FuncionarioCaixaId",
                principalTable: "FuncionarioCaixas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PagamentoPropinas_Propinas_PropinaId",
                table: "PagamentoPropinas",
                column: "PropinaId",
                principalTable: "Propinas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PagamentoPropinas_FuncionarioCaixas_FuncionarioCaixaId",
                table: "PagamentoPropinas");

            migrationBuilder.DropForeignKey(
                name: "FK_PagamentoPropinas_Propinas_PropinaId",
                table: "PagamentoPropinas");

            migrationBuilder.DropIndex(
                name: "IX_PagamentoPropinas_FuncionarioCaixaId",
                table: "PagamentoPropinas");

            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Propinas");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "PagamentoPropinas");

            migrationBuilder.DropColumn(
                name: "FuncionarioCaixaId",
                table: "PagamentoPropinas");

            migrationBuilder.DropColumn(
                name: "NumeroDeMeses",
                table: "PagamentoPropinas");

            migrationBuilder.DropColumn(
                name: "PrecoPropina",
                table: "PagamentoPropinas");

            migrationBuilder.DropColumn(
                name: "AnoLetivo",
                table: "AlunoInscritos");

            migrationBuilder.RenameColumn(
                name: "PropinaId",
                table: "PagamentoPropinas",
                newName: "MesId");

            migrationBuilder.RenameIndex(
                name: "IX_PagamentoPropinas_PropinaId",
                table: "PagamentoPropinas",
                newName: "IX_PagamentoPropinas_MesId");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Propinas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PagamentoPropinaItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuncionarioCaixaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PagamentoPropinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomePropina = table.Column<string>(type: "varchar(29)", nullable: false),
                    NumeroDeMeses = table.Column<int>(type: "int", nullable: false),
                    PrecoPropina = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentoPropinaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentoPropinaItems_FuncionarioCaixas_FuncionarioCaixaId",
                        column: x => x.FuncionarioCaixaId,
                        principalTable: "FuncionarioCaixas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PagamentoPropinaItems_PagamentoPropinas_PagamentoPropinaId",
                        column: x => x.PagamentoPropinaId,
                        principalTable: "PagamentoPropinas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PagamentoPropinaItems_Propinas_PropinaId",
                        column: x => x.PropinaId,
                        principalTable: "Propinas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoPropinaItems_FuncionarioCaixaId",
                table: "PagamentoPropinaItems",
                column: "FuncionarioCaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoPropinaItems_PagamentoPropinaId",
                table: "PagamentoPropinaItems",
                column: "PagamentoPropinaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoPropinaItems_PropinaId",
                table: "PagamentoPropinaItems",
                column: "PropinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PagamentoPropinas_Meses_MesId",
                table: "PagamentoPropinas",
                column: "MesId",
                principalTable: "Meses",
                principalColumn: "Id");
        }
    }
}
