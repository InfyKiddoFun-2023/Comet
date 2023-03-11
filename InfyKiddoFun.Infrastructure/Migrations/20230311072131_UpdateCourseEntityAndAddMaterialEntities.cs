using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfyKiddoFun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseEntityAndAddMaterialEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AppUsers_MentorUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DurationInWeeks",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "SpecificStream",
                table: "Courses",
                newName: "Stream");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "MentorUserId",
                table: "Courses",
                newName: "MentorId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_MentorUserId",
                table: "Courses",
                newName: "IX_Courses_MentorId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CourseMaterial",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaterialType = table.Column<byte>(type: "tinyint", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseMaterial_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseModuleMaterial",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModuleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaterialType = table.Column<byte>(type: "tinyint", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModuleMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseModuleMaterial_CourseModules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "CourseModules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseMaterial_CourseId",
                table: "CourseMaterial",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModuleMaterial_ModuleId",
                table: "CourseModuleMaterial",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AppUsers_MentorId",
                table: "Courses",
                column: "MentorId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AppUsers_MentorId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseMaterial");

            migrationBuilder.DropTable(
                name: "CourseModuleMaterial");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Stream",
                table: "Courses",
                newName: "SpecificStream");

            migrationBuilder.RenameColumn(
                name: "MentorId",
                table: "Courses",
                newName: "MentorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_MentorId",
                table: "Courses",
                newName: "IX_Courses_MentorUserId");

            migrationBuilder.AddColumn<int>(
                name: "DurationInWeeks",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AppUsers_MentorUserId",
                table: "Courses",
                column: "MentorUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }
    }
}
