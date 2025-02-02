using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfServiceStudentSystem.Migrations
{
    public partial class AddTranscriptTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Program_Faculties_FacultyId",
                table: "Program");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Program_StudyProgramId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollment_Program_StudyProgramId",
                table: "StudentEnrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Program_StudyProgramId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Transcripts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Program",
                table: "Program");

            migrationBuilder.RenameTable(
                name: "Program",
                newName: "StudyPrograms");

            migrationBuilder.RenameIndex(
                name: "IX_Program_FacultyId",
                table: "StudyPrograms",
                newName: "IX_StudyPrograms_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyPrograms",
                table: "StudyPrograms",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Transcript",
                columns: table => new
                {
                    TranscriptId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    SemesterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Grade = table.Column<int>(type: "INTEGER", nullable: false),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transcript", x => x.TranscriptId);
                    table.ForeignKey(
                        name: "FK_Transcript_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transcript_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transcript_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transcript_SemesterId",
                table: "Transcript",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcript_StudentId",
                table: "Transcript",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcript_SubjectId",
                table: "Transcript",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_StudyPrograms_StudyProgramId",
                table: "Semesters",
                column: "StudyProgramId",
                principalTable: "StudyPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollment_StudyPrograms_StudyProgramId",
                table: "StudentEnrollment",
                column: "StudyProgramId",
                principalTable: "StudyPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudyPrograms_StudyProgramId",
                table: "Students",
                column: "StudyProgramId",
                principalTable: "StudyPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPrograms_Faculties_FacultyId",
                table: "StudyPrograms",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_StudyPrograms_StudyProgramId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollment_StudyPrograms_StudyProgramId",
                table: "StudentEnrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudyPrograms_StudyProgramId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyPrograms_Faculties_FacultyId",
                table: "StudyPrograms");

            migrationBuilder.DropTable(
                name: "Transcript");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyPrograms",
                table: "StudyPrograms");

            migrationBuilder.RenameTable(
                name: "StudyPrograms",
                newName: "Program");

            migrationBuilder.RenameIndex(
                name: "IX_StudyPrograms_FacultyId",
                table: "Program",
                newName: "IX_Program_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Program",
                table: "Program",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Transcripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    Grade = table.Column<int>(type: "INTEGER", nullable: false),
                    Semester = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transcripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transcripts_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transcripts_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_StudentId",
                table: "Transcripts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_SubjectId",
                table: "Transcripts",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Program_Faculties_FacultyId",
                table: "Program",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Program_StudyProgramId",
                table: "Semesters",
                column: "StudyProgramId",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollment_Program_StudyProgramId",
                table: "StudentEnrollment",
                column: "StudyProgramId",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Program_StudyProgramId",
                table: "Students",
                column: "StudyProgramId",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
