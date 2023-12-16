using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystemUNEC.DataAccess.Migrations
{
    public partial class changeExamAndExamTypeEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxScore",
                table: "Exams");

            migrationBuilder.AddColumn<byte>(
                name: "maxScore",
                table: "examTypes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddCheckConstraint(
                name: "maxScore",
                table: "examTypes",
                sql: "maxScore <= 100");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "maxScore",
                table: "examTypes");

            migrationBuilder.DropColumn(
                name: "maxScore",
                table: "examTypes");

            migrationBuilder.AddColumn<byte>(
                name: "maxScore",
                table: "Exams",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
