using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Moto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Plan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Days = table.Column<short>(type: "smallint", nullable: false),
                    CostPerDay = table.Column<decimal>(type: "numeric(7,5)", precision: 7, scale: 5, nullable: false),
                    Fee = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plan");
        }
    }
}
