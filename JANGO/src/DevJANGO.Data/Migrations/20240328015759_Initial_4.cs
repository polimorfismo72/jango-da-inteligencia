using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJANGO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PagamentoPropinas_Propinas_PropinaId",
                table: "PagamentoPropinas");

            migrationBuilder.DropIndex(
                name: "IX_PagamentoPropinas_PropinaId",
                table: "PagamentoPropinas");

            migrationBuilder.DropColumn(
                name: "PropinaId",
                table: "PagamentoPropinas");

            migrationBuilder.AddColumn<Guid>(
                name: "PagamentoPropinaId",
                table: "Propinas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Propinas_PagamentoPropinaId",
                table: "Propinas",
                column: "PagamentoPropinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Propinas_PagamentoPropinas_PagamentoPropinaId",
                table: "Propinas",
                column: "PagamentoPropinaId",
                principalTable: "PagamentoPropinas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propinas_PagamentoPropinas_PagamentoPropinaId",
                table: "Propinas");

            migrationBuilder.DropIndex(
                name: "IX_Propinas_PagamentoPropinaId",
                table: "Propinas");

            migrationBuilder.DropColumn(
                name: "PagamentoPropinaId",
                table: "Propinas");

            migrationBuilder.AddColumn<Guid>(
                name: "PropinaId",
                table: "PagamentoPropinas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoPropinas_PropinaId",
                table: "PagamentoPropinas",
                column: "PropinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PagamentoPropinas_Propinas_PropinaId",
                table: "PagamentoPropinas",
                column: "PropinaId",
                principalTable: "Propinas",
                principalColumn: "Id");
        }
    }
}
