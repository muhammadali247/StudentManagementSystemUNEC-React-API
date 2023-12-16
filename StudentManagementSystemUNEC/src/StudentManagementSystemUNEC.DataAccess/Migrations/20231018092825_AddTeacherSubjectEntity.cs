using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystemUNEC.DataAccess.Migrations
{
    public partial class AddTeacherSubjectEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_GroupSubjects_GroupSubjectId",
                table: "TeacherSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_teacherRoles_TeacherRoleId",
                table: "TeacherSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_Teachers_TeacherId",
                table: "TeacherSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSubject",
                table: "TeacherSubject");

            migrationBuilder.RenameTable(
                name: "TeacherSubject",
                newName: "TeacherSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubject_TeacherRoleId",
                table: "TeacherSubjects",
                newName: "IX_TeacherSubjects_TeacherRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubject_TeacherId",
                table: "TeacherSubjects",
                newName: "IX_TeacherSubjects_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubject_GroupSubjectId",
                table: "TeacherSubjects",
                newName: "IX_TeacherSubjects_GroupSubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSubjects",
                table: "TeacherSubjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_GroupSubjects_GroupSubjectId",
                table: "TeacherSubjects",
                column: "GroupSubjectId",
                principalTable: "GroupSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_teacherRoles_TeacherRoleId",
                table: "TeacherSubjects",
                column: "TeacherRoleId",
                principalTable: "teacherRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Teachers_TeacherId",
                table: "TeacherSubjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_GroupSubjects_GroupSubjectId",
                table: "TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_teacherRoles_TeacherRoleId",
                table: "TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Teachers_TeacherId",
                table: "TeacherSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSubjects",
                table: "TeacherSubjects");

            migrationBuilder.RenameTable(
                name: "TeacherSubjects",
                newName: "TeacherSubject");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubjects_TeacherRoleId",
                table: "TeacherSubject",
                newName: "IX_TeacherSubject_TeacherRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubjects_TeacherId",
                table: "TeacherSubject",
                newName: "IX_TeacherSubject_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubjects_GroupSubjectId",
                table: "TeacherSubject",
                newName: "IX_TeacherSubject_GroupSubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSubject",
                table: "TeacherSubject",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_GroupSubjects_GroupSubjectId",
                table: "TeacherSubject",
                column: "GroupSubjectId",
                principalTable: "GroupSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_teacherRoles_TeacherRoleId",
                table: "TeacherSubject",
                column: "TeacherRoleId",
                principalTable: "teacherRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_Teachers_TeacherId",
                table: "TeacherSubject",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
