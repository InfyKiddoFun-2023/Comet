using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfyKiddoFun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseEnrollmentAndMinorEdits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Courses",
                newName: "DurationInWeeks");

            migrationBuilder.AddColumn<string>(
                name: "MentorUserId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MentorUserId",
                table: "Courses",
                column: "MentorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AppUsers_MentorUserId",
                table: "Courses",
                column: "MentorUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AppUsers_MentorUserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_MentorUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MentorUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "DurationInWeeks",
                table: "Courses",
                newName: "Duration");
        }
    }
}
