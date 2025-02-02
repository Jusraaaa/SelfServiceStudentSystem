using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfServiceStudentSystem.Migrations
{
    public partial class UpdateTranscriptRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transcript_Semesters_SemesterId",
                table: "Transcript");

            migrationBuilder.DropForeignKey(
                name: "FK_Transcript_Students_StudentId",
                table: "Transcript");

            migrationBuilder.DropForeignKey(
                name: "FK_Transcript_Subjects_SubjectId",
                table: "Transcript");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transcript",
                table: "Transcript");

            migrationBuilder.RenameTable(
                name: "Transcript",
                newName: "Transcripts");

            migrationBuilder.RenameIndex(
                name: "IX_Transcript_SubjectId",
                table: "Transcripts",
                newName: "IX_Transcripts_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Transcript_StudentId",
                table: "Transcripts",
                newName: "IX_Transcripts_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Transcript_SemesterId",
                table: "Transcripts",
                newName: "IX_Transcripts_SemesterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transcripts",
                table: "Transcripts",
                column: "TranscriptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transcripts_Semesters_SemesterId",
                table: "Transcripts",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transcripts_Students_StudentId",
                table: "Transcripts",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transcripts_Subjects_SubjectId",
                table: "Transcripts",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transcripts_Semesters_SemesterId",
                table: "Transcripts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transcripts_Students_StudentId",
                table: "Transcripts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transcripts_Subjects_SubjectId",
                table: "Transcripts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transcripts",
                table: "Transcripts");

            migrationBuilder.RenameTable(
                name: "Transcripts",
                newName: "Transcript");

            migrationBuilder.RenameIndex(
                name: "IX_Transcripts_SubjectId",
                table: "Transcript",
                newName: "IX_Transcript_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Transcripts_StudentId",
                table: "Transcript",
                newName: "IX_Transcript_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Transcripts_SemesterId",
                table: "Transcript",
                newName: "IX_Transcript_SemesterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transcript",
                table: "Transcript",
                column: "TranscriptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transcript_Semesters_SemesterId",
                table: "Transcript",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transcript_Students_StudentId",
                table: "Transcript",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transcript_Subjects_SubjectId",
                table: "Transcript",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
