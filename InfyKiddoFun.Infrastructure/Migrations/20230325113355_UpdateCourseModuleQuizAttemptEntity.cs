using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfyKiddoFun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseModuleQuizAttemptEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CourseModuleQuizAttempts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseModuleQuizAttempts_UserId",
                table: "CourseModuleQuizAttempts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModuleQuizAttempts_AppUsers_UserId",
                table: "CourseModuleQuizAttempts",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseModuleQuizAttempts_AppUsers_UserId",
                table: "CourseModuleQuizAttempts");

            migrationBuilder.DropIndex(
                name: "IX_CourseModuleQuizAttempts_UserId",
                table: "CourseModuleQuizAttempts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseModuleQuizAttempts");
        }
    }
}
