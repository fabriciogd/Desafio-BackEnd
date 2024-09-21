using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Moto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Courier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_Placa",
                table: "Motorcycles");

            migrationBuilder.CreateTable(
                name: "Courier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Identificador = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CNPJ = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    NumeroCNH = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    TipoCNH = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    ImagemCNH = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courier", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_Placa",
                table: "Motorcycles",
                column: "Placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courier_CNPJ",
                table: "Courier",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courier_NumeroCNH",
                table: "Courier",
                column: "NumeroCNH",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courier");

            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_Placa",
                table: "Motorcycles");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_Placa",
                table: "Motorcycles",
                column: "Placa");
        }
    }
}
