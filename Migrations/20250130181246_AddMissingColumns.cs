using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfServiceStudentSystem.Migrations
{
    public partial class AddMissingColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentProgramEnrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Programs",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Program",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Programs");

            migrationBuilder.RenameTable(
                name: "Programs",
                newName: "Program");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Students",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Students",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Students",
                newName: "first_name");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "StudyProgramId",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ServiceType",
                table: "Services",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Semester",
                table: "Payments",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "Program",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "ProgramCode",
                table: "Program",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProgramName",
                table: "Program",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudyLevel",
                table: "Program",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Program",
                table: "Program",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudyProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    SemesterNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_Program_StudyProgramId",
                        column: x => x.StudyProgramId,
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubjectCode = table.Column<string>(type: "TEXT", nullable: false),
                    SubjectName = table.Column<string>(type: "TEXT", nullable: false),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentEnrollment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudyProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    SemesterId = table.Column<int>(type: "INTEGER", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEnrollment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEnrollment_Program_StudyProgramId",
                        column: x => x.StudyProgramId,
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEnrollment_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEnrollment_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Students_StudyProgramId",
                table: "Students",
                column: "StudyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentId",
                table: "Payments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_FacultyId",
                table: "Program",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_StudyProgramId",
                table: "Semesters",
                column: "StudyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollment_SemesterId",
                table: "StudentEnrollment",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollment_StudentId",
                table: "StudentEnrollment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollment_StudyProgramId",
                table: "StudentEnrollment",
                column: "StudyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_StudentId",
                table: "Transcripts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_SubjectId",
                table: "Transcripts",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Students_StudentId",
                table: "Payments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Program_Faculties_FacultyId",
                table: "Program",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Program_StudyProgramId",
                table: "Students",
                column: "StudyProgramId",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Students_StudentId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Program_Faculties_FacultyId",
                table: "Program");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Program_StudyProgramId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "StudentEnrollment");

            migrationBuilder.DropTable(
                name: "Transcripts");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudyProgramId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Payments_StudentId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Program",
                table: "Program");

            migrationBuilder.DropIndex(
                name: "IX_Program_FacultyId",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "StudyProgramId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProgramCode",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "ProgramName",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "StudyLevel",
                table: "Program");

            migrationBuilder.RenameTable(
                name: "Program",
                newName: "Programs");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Students",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Students",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Students",
                newName: "FirstName");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Students",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Program",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceType",
                table: "Services",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Semester",
                table: "Payments",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "Programs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Programs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Programs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Programs",
                table: "Programs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StudentProgramEnrollment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false)
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
    }
}
