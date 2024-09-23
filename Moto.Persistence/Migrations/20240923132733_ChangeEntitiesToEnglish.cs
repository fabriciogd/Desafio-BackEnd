using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEntitiesToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "Motorcycle");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Courier");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "Courier");

            migrationBuilder.RenameColumn(
                name: "Placa",
                table: "Motorcycle",
                newName: "LicensePlate");

            migrationBuilder.RenameColumn(
                name: "Modelo",
                table: "Motorcycle",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "Ano",
                table: "Motorcycle",
                newName: "Year");

            migrationBuilder.RenameIndex(
                name: "IX_Motorcycle_Placa",
                table: "Motorcycle",
                newName: "IX_Motorcycle_LicensePlate");

            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Courier",
                newName: "Cnpj");

            migrationBuilder.RenameColumn(
                name: "TipoCNH",
                table: "Courier",
                newName: "DrivingLicenseType");

            migrationBuilder.RenameColumn(
                name: "NumeroCNH",
                table: "Courier",
                newName: "DrivingLicense");

            migrationBuilder.RenameColumn(
                name: "ImagemCNH",
                table: "Courier",
                newName: "DrivingLicenseImagePath");

            migrationBuilder.RenameIndex(
                name: "IX_Courier_CNPJ",
                table: "Courier",
                newName: "IX_Courier_Cnpj");

            migrationBuilder.RenameIndex(
                name: "IX_Courier_NumeroCNH",
                table: "Courier",
                newName: "IX_Courier_DrivingLicense");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Rental",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedEndDate",
                table: "Rental",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Rental",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OccuredOn",
                table: "Event",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Courier",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Courier");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Motorcycle",
                newName: "Ano");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Motorcycle",
                newName: "Modelo");

            migrationBuilder.RenameColumn(
                name: "LicensePlate",
                table: "Motorcycle",
                newName: "Placa");

            migrationBuilder.RenameIndex(
                name: "IX_Motorcycle_LicensePlate",
                table: "Motorcycle",
                newName: "IX_Motorcycle_Placa");

            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "Courier",
                newName: "CNPJ");

            migrationBuilder.RenameColumn(
                name: "DrivingLicenseType",
                table: "Courier",
                newName: "TipoCNH");

            migrationBuilder.RenameColumn(
                name: "DrivingLicenseImagePath",
                table: "Courier",
                newName: "ImagemCNH");

            migrationBuilder.RenameColumn(
                name: "DrivingLicense",
                table: "Courier",
                newName: "NumeroCNH");

            migrationBuilder.RenameIndex(
                name: "IX_Courier_Cnpj",
                table: "Courier",
                newName: "IX_Courier_CNPJ");

            migrationBuilder.RenameIndex(
                name: "IX_Courier_DrivingLicense",
                table: "Courier",
                newName: "IX_Courier_NumeroCNH");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Rental",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedEndDate",
                table: "Rental",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Rental",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "Motorcycle",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OccuredOn",
                table: "Event",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Courier",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "Courier",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
