using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfServiceStudentSystem.Models
{
    public class Semester
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("StudyProgram")]
        public int StudyProgramId { get; set; }

        public int SemesterNumber { get; set; }
        public int Year { get; set; }

        // Navigation Property
        public StudyProgram StudyProgram { get; set; }
    }
}
