using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfServiceStudentSystem.Models
{
    [Table("StudentEnrollment")] // Përcakton lidhjen me tabelën StudentEnrollment në databazë
    public class StudentEnrollment
    {
        [Key]
        public int Id { get; set; } // ID unik

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }  // Navigation Property

        [Required]
        [ForeignKey("StudyProgram")]
        public int StudyProgramId { get; set; }
        public StudyProgram StudyProgram { get; set; }  // Navigation Property

        [Required]
        [ForeignKey("Semester")]
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }  // Navigation Property

        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now; // Data e regjistrimit
    }
}
