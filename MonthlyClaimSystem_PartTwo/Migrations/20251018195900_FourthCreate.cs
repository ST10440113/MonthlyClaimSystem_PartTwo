using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonthlyClaimSystem_PartTwo.Migrations
{
    /// <inheritdoc />
    public partial class FourthCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewedBy",
                table: "Lecturer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedDate",
                table: "Lecturer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Lecturer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubmittedBy",
                table: "Lecturer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedDate",
                table: "Lecturer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ClaimReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    ReviewerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewerRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Decision = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LecturerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimReview_Lecturer_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturer",
                        principalColumn: "LecturerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimReview_LecturerId",
                table: "ClaimReview",
                column: "LecturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimReview");

            migrationBuilder.DropColumn(
                name: "ReviewedBy",
                table: "Lecturer");

            migrationBuilder.DropColumn(
                name: "ReviewedDate",
                table: "Lecturer");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lecturer");

            migrationBuilder.DropColumn(
                name: "SubmittedBy",
                table: "Lecturer");

            migrationBuilder.DropColumn(
                name: "SubmittedDate",
                table: "Lecturer");
        }
    }
}
