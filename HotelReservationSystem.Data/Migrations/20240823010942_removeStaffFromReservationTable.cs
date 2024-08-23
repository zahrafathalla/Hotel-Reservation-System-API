using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeStaffFromReservationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservations_staff_StaffId",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_reservations_StaffId",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "reservations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_reservations_StaffId",
                table: "reservations",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_staff_StaffId",
                table: "reservations",
                column: "StaffId",
                principalTable: "staff",
                principalColumn: "Id");
        }
    }
}
