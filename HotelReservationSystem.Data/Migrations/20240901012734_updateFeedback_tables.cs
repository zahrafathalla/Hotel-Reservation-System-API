using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateFeedback_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedBacks_staff_StaffId",
                table: "feedBacks");

            migrationBuilder.DropIndex(
                name: "IX_feedBacks_StaffId",
                table: "feedBacks");

            migrationBuilder.DropColumn(
                name: "Reply",
                table: "feedBacks");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "feedBacks");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmitted",
                table: "feedBacks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "FeedbackReply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffResponse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateResponded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeedbackId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackReply_feedBacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "feedBacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedbackReply_staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackReply_FeedbackId",
                table: "FeedbackReply",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackReply_StaffId",
                table: "FeedbackReply",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackReply");

            migrationBuilder.DropColumn(
                name: "DateSubmitted",
                table: "feedBacks");

            migrationBuilder.AddColumn<string>(
                name: "Reply",
                table: "feedBacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "feedBacks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_feedBacks_StaffId",
                table: "feedBacks",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_feedBacks_staff_StaffId",
                table: "feedBacks",
                column: "StaffId",
                principalTable: "staff",
                principalColumn: "Id");
        }
    }
}
