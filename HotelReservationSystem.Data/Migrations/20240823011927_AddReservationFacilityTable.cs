using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationFacilityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationFacility",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    FacilityId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationFacility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationFacility_facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationFacility_reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFacility_FacilityId",
                table: "ReservationFacility",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFacility_ReservationId",
                table: "ReservationFacility",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationFacility");
        }
    }
}
