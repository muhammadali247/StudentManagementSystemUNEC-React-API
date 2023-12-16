using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystemUNEC.DataAccess.Migrations
{
    public partial class changeFaculty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "studySector",
                table: "Faculties");

            migrationBuilder.AlterColumn<string>(
                name: "facultyCode",
                table: "Faculties",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<string>(
                name: "studySectorCode",
                table: "Faculties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "studySectorName",
                table: "Faculties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "studySectorCode",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "studySectorName",
                table: "Faculties");

            migrationBuilder.AlterColumn<byte>(
                name: "facultyCode",
                table: "Faculties",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte>(
                name: "studySector",
                table: "Faculties",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
