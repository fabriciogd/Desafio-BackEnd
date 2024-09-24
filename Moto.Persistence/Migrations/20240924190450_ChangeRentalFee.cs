using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRentalFee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fee",
                value: 0.2m);

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 15,
                column: "Fee",
                value: 0.4m);

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 30,
                column: "Fee",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 45,
                column: "Fee",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 50,
                column: "Fee",
                value: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 7,
                column: "Fee",
                value: 1.20m);

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 15,
                column: "Fee",
                value: 1.40m);

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 30,
                column: "Fee",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 45,
                column: "Fee",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Plan",
                keyColumn: "Id",
                keyValue: 50,
                column: "Fee",
                value: 1m);
        }
    }
}
