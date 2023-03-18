using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfyKiddoFun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCourseMaterialEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseMaterial");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseMaterial",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialType = table.Column<byte>(type: "tinyint", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_CourseMaterial_CourseId",
                table: "CourseMaterial",
                column: "CourseId");
        }
    }
}
