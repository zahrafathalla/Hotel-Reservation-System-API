using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenOfferAndReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_customers_CustomerId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_staff_StaffId",
                table: "users");

            migrationBuilder.DropTable(
                name: "OfferRoms");

            migrationBuilder.DropIndex(
                name: "IX_users_CustomerId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_StaffId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "reservations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "offers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_OfferId",
                table: "reservations",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_offers_OfferId",
                table: "reservations",
                column: "OfferId",
                principalTable: "offers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservations_offers_OfferId",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_reservations_OfferId",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "reservations");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "offers",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "OfferRoms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferRoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferRoms_offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferRoms_rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_CustomerId",
                table: "users",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_users_StaffId",
                table: "users",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRoms_OfferId",
                table: "OfferRoms",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRoms_RoomId",
                table: "OfferRoms",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_customers_CustomerId",
                table: "users",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_staff_StaffId",
                table: "users",
                column: "StaffId",
                principalTable: "staff",
                principalColumn: "Id");
        }
    }
}
