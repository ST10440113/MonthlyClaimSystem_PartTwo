using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonthlyClaimSystem_PartTwo.Migrations
{
    /// <inheritdoc />
    public partial class eigthCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "Lecturer",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "Lecturer",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");
        }
    }
}
