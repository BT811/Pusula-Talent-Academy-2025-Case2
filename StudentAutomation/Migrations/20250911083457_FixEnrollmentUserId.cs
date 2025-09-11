using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentAutomation.Migrations
{
    /// <inheritdoc />
    public partial class FixEnrollmentUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Courses_CourseId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Grades",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Grades",
                newName: "EnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                newName: "IX_Grades_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_CourseId",
                table: "Grades",
                newName: "IX_Grades_EnrollmentId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Enrollments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                newName: "IX_Enrollments_UserId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Attendances",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                newName: "IX_Attendances_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_UserId",
                table: "Attendances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Users_UserId",
                table: "Enrollments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentId",
                table: "Grades",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Users_UserId",
                table: "Grades",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Users_UserId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_TeacherId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_UserId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Users_UserId",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Grades",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "EnrollmentId",
                table: "Grades",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_UserId",
                table: "Grades",
                newName: "IX_Grades_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_EnrollmentId",
                table: "Grades",
                newName: "IX_Grades_CourseId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Enrollments",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Attendances",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_UserId",
                table: "Attendances",
                newName: "IX_Attendances_StudentId");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Courses_CourseId",
                table: "Grades",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
