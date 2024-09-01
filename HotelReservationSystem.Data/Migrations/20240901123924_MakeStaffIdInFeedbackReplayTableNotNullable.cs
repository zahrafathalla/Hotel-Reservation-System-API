using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeStaffIdInFeedbackReplayTableNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackReply_staff_StaffId",
                table: "FeedbackReply");

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "FeedbackReply",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackReply_staff_StaffId",
                table: "FeedbackReply",
                column: "StaffId",
                principalTable: "staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackReply_staff_StaffId",
                table: "FeedbackReply");

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "FeedbackReply",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackReply_staff_StaffId",
                table: "FeedbackReply",
                column: "StaffId",
                principalTable: "staff",
                principalColumn: "Id");
        }
    }
}
