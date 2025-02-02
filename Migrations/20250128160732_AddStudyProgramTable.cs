using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfServiceStudentSystem.Migrations
{
    public partial class AddStudyProgramTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Semester",
                table: "Students",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    FacultyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentProgramEnrollment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProgramId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProgramEnrollment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentProgramEnrollment_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentProgramEnrollment_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramEnrollment_ProgramId",
                table: "StudentProgramEnrollment",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramEnrollment_StudentId",
                table: "StudentProgramEnrollment",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentProgramEnrollment");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Students",
                newName: "Semester");
        }
    }
}
