using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MotorcyclePlaca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpectedEndData",
                table: "Rental",
                newName: "ExpectedEndDate");

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Motorcycle",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpectedEndDate",
                table: "Rental",
                newName: "ExpectedEndData");

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Motorcycle",
                type: "character varying(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8);
        }
    }
}
