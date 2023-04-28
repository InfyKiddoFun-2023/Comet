using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfyKiddoFun.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseModuleQuizAttemptAndCourseModuleQuizQuestionAttemptEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseModuleQuizAttempts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseModuleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QuizId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    TotalCorrectAnswers = table.Column<int>(type: "int", nullable: false),
                    TotalWrongAnswers = table.Column<int>(type: "int", nullable: false),
                    TotalUnanswered = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    TotalMarks = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    IsPassed = table.Column<bool>(type: "bit", nullable: false),
                    QuizTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassPercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModuleQuizAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseModuleQuizAttempts_CourseModuleQuizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "CourseModuleQuizzes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseModuleQuizAttempts_CourseModules_CourseModuleId",
                        column: x => x.CourseModuleId,
                        principalTable: "CourseModules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseModuleQuizQuestionAttempt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    CourseModuleQuizAttemptId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModuleQuizQuestionAttempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseModuleQuizQuestionAttempt_CourseModuleQuizAttempts_CourseModuleQuizAttemptId",
                        column: x => x.CourseModuleQuizAttemptId,
                        principalTable: "CourseModuleQuizAttempts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseModuleQuizQuestionAttempt_CourseModuleQuizQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "CourseModuleQuizQuestion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseModuleQuizAttempts_CourseModuleId",
                table: "CourseModuleQuizAttempts",
                column: "CourseModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModuleQuizAttempts_QuizId",
                table: "CourseModuleQuizAttempts",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModuleQuizQuestionAttempt_CourseModuleQuizAttemptId",
                table: "CourseModuleQuizQuestionAttempt",
                column: "CourseModuleQuizAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModuleQuizQuestionAttempt_QuestionId",
                table: "CourseModuleQuizQuestionAttempt",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseModuleQuizQuestionAttempt");

            migrationBuilder.DropTable(
                name: "CourseModuleQuizAttempts");
        }
    }
}
