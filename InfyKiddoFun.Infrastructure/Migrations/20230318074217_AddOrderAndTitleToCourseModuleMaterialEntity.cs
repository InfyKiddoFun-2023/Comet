using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfyKiddoFun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAndTitleToCourseModuleMaterialEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseModuleMaterial_CourseModules_ModuleId",
                table: "CourseModuleMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseModuleQuiz_CourseModules_ModuleId",
                table: "CourseModuleQuiz");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseModuleQuizQuestion_CourseModuleQuiz_QuizId",
                table: "CourseModuleQuizQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseModuleQuiz",
                table: "CourseModuleQuiz");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseModuleMaterial",
                table: "CourseModuleMaterial");

            migrationBuilder.RenameTable(
                name: "CourseModuleQuiz",
                newName: "CourseModuleQuizzes");

            migrationBuilder.RenameTable(
                name: "CourseModuleMaterial",
                newName: "CourseModuleMaterials");

            migrationBuilder.RenameIndex(
                name: "IX_CourseModuleQuiz_ModuleId",
                table: "CourseModuleQuizzes",
                newName: "IX_CourseModuleQuizzes_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseModuleMaterial_ModuleId",
                table: "CourseModuleMaterials",
                newName: "IX_CourseModuleMaterials_ModuleId");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CourseModuleMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "CourseModuleMaterials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseModuleQuizzes",
                table: "CourseModuleQuizzes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseModuleMaterials",
                table: "CourseModuleMaterials",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModuleMaterials_CourseModules_ModuleId",
                table: "CourseModuleMaterials",
                column: "ModuleId",
                principalTable: "CourseModules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModuleQuizQuestion_CourseModuleQuizzes_QuizId",
                table: "CourseModuleQuizQuestion",
                column: "QuizId",
                principalTable: "CourseModuleQuizzes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModuleQuizzes_CourseModules_ModuleId",
                table: "CourseModuleQuizzes",
                column: "ModuleId",
                principalTable: "CourseModules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseModuleMaterials_CourseModules_ModuleId",
                table: "CourseModuleMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseModuleQuizQuestion_CourseModuleQuizzes_QuizId",
                table: "CourseModuleQuizQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseModuleQuizzes_CourseModules_ModuleId",
                table: "CourseModuleQuizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseModuleQuizzes",
                table: "CourseModuleQuizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseModuleMaterials",
                table: "CourseModuleMaterials");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CourseModuleMaterials");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "CourseModuleMaterials");

            migrationBuilder.RenameTable(
                name: "CourseModuleQuizzes",
                newName: "CourseModuleQuiz");

            migrationBuilder.RenameTable(
                name: "CourseModuleMaterials",
                newName: "CourseModuleMaterial");

            migrationBuilder.RenameIndex(
                name: "IX_CourseModuleQuizzes_ModuleId",
                table: "CourseModuleQuiz",
                newName: "IX_CourseModuleQuiz_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseModuleMaterials_ModuleId",
                table: "CourseModuleMaterial",
                newName: "IX_CourseModuleMaterial_ModuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseModuleQuiz",
                table: "CourseModuleQuiz",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseModuleMaterial",
                table: "CourseModuleMaterial",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModuleMaterial_CourseModules_ModuleId",
                table: "CourseModuleMaterial",
                column: "ModuleId",
                principalTable: "CourseModules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModuleQuiz_CourseModules_ModuleId",
                table: "CourseModuleQuiz",
                column: "ModuleId",
                principalTable: "CourseModules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModuleQuizQuestion_CourseModuleQuiz_QuizId",
                table: "CourseModuleQuizQuestion",
                column: "QuizId",
                principalTable: "CourseModuleQuiz",
                principalColumn: "Id");
        }
    }
}
