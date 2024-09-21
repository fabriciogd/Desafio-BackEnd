using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Moto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Motorcycles",
                table: "Motorcycles");

            migrationBuilder.RenameTable(
                name: "Motorcycles",
                newName: "Motorcycle");

            migrationBuilder.RenameIndex(
                name: "IX_Motorcycles_Placa",
                table: "Motorcycle",
                newName: "IX_Motorcycle_Placa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motorcycle",
                table: "Motorcycle",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Rental",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    CourierId = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleId = table.Column<int>(type: "integer", nullable: false),
                    PlanId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpectedEndData = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TotalPayment = table.Column<decimal>(type: "numeric(7,5)", precision: 7, scale: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rental_Courier_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Courier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rental_Motorcycle_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rental_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rental_CourierId",
                table: "Rental",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_MotorcycleId",
                table: "Rental",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_PlanId",
                table: "Rental",
                column: "PlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Motorcycle",
                table: "Motorcycle");

            migrationBuilder.RenameTable(
                name: "Motorcycle",
                newName: "Motorcycles");

            migrationBuilder.RenameIndex(
                name: "IX_Motorcycle_Placa",
                table: "Motorcycles",
                newName: "IX_Motorcycles_Placa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motorcycles",
                table: "Motorcycles",
                column: "Id");
        }
    }
}
