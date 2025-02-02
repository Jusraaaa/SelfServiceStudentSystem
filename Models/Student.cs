using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfServiceStudentSystem.Models
{
    [Table("Students")] // Emri i tabelës në databazë
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("first_name")]
        public string FirstName { get; set; } = "";

        [Required]
        [Column("last_name")]
        public string LastName { get; set; } = "";

        [Required]
        [Column("email")]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Role { get; set; } = "";

        [Required]
        public int Status { get; set; }

        // ✅ **Kjo është lidhja me tabelën `StudyPrograms`**
        [Required]
        [ForeignKey("StudyProgram")]
        public int StudyProgramId { get; set; }
        public StudyProgram StudyProgram { get; set; } // Navigation Property
    }
}
