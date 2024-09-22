using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Moto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PlanWithoutDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Plan_Days",
                table: "Plan");

            migrationBuilder.DeleteData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Days",
                table: "Plan");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPayment",
                table: "Rental",
                type: "numeric(7,5)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,4)",
                oldPrecision: 7,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CostPerDay",
                table: "Plan",
                type: "numeric(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,4)",
                oldPrecision: 5,
                oldScale: 4);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Plan",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.InsertData(
                table: "Plan",
                columns: new[] { "Id", "CostPerDay", "Fee" },
                values: new object[,]
                {
                    { 7, 30m, 1.20m },
                    { 15, 28m, 1.40m },
                    { 30, 22m, 1m },
                    { 45, 20m, 1m },
                    { 50, 18m, 1m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPayment",
                table: "Rental",
                type: "numeric(7,4)",
                precision: 7,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,5)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CostPerDay",
                table: "Plan",
                type: "numeric(5,4)",
                precision: 5,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Plan",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<short>(
                name: "Days",
                table: "Plan",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.InsertData(
                table: "Plan",
                columns: new[] { "Id", "CostPerDay", "Days", "Fee" },
                values: new object[,]
                {
                    { 1, 30m, (short)7, 1.20m },
                    { 2, 28m, (short)15, 1.40m },
                    { 3, 22m, (short)30, 1m },
                    { 4, 20m, (short)45, 1m },
                    { 5, 18m, (short)50, 1m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plan_Days",
                table: "Plan",
                column: "Days",
                unique: true);
        }
    }
}
