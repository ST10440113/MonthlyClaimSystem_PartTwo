using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonthlyClaimSystem_PartTwo.Migrations
{
    /// <inheritdoc />
    public partial class newCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lecturer",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LecturerRefID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoursWorked = table.Column<int>(type: "int", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittedBy = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReviewedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactNum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturer", x => x.ClaimId);
                });

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
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimReview_Lecturer_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Lecturer",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coordinator",
                columns: table => new
                {
                    CoordinatorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    LecturerClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinator", x => x.CoordinatorId);
                    table.ForeignKey(
                        name: "FK_Coordinator_Lecturer_LecturerClaimId",
                        column: x => x.LecturerClaimId,
                        principalTable: "Lecturer",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEncrypted = table.Column<bool>(type: "bit", nullable: false),
                    LecturerClaimId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileModel_Lecturer_LecturerClaimId",
                        column: x => x.LecturerClaimId,
                        principalTable: "Lecturer",
                        principalColumn: "ClaimId");
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    LecturerClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.ManagerId);
                    table.ForeignKey(
                        name: "FK_Manager_Lecturer_LecturerClaimId",
                        column: x => x.LecturerClaimId,
                        principalTable: "Lecturer",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimReview_ClaimId",
                table: "ClaimReview",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinator_LecturerClaimId",
                table: "Coordinator",
                column: "LecturerClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_FileModel_LecturerClaimId",
                table: "FileModel",
                column: "LecturerClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_LecturerClaimId",
                table: "Manager",
                column: "LecturerClaimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimReview");

            migrationBuilder.DropTable(
                name: "Coordinator");

            migrationBuilder.DropTable(
                name: "FileModel");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Lecturer");
        }
    }
}
