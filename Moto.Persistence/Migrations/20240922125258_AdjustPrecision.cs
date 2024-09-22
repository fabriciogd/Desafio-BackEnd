using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPayment",
                table: "Rental",
                type: "numeric(7,2)",
                precision: 7,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,5)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Fee",
                table: "Plan",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,4)",
                oldPrecision: 5,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "CostPerDay",
                table: "Plan",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,5)",
                oldPrecision: 7,
                oldScale: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPayment",
                table: "Rental",
                type: "numeric(7,5)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Fee",
                table: "Plan",
                type: "numeric(5,4)",
                precision: 5,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "CostPerDay",
                table: "Plan",
                type: "numeric(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);
        }
    }
}
