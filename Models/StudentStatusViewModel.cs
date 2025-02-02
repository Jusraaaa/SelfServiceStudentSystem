using System;
using System.ComponentModel.DataAnnotations;

namespace SelfServiceStudentSystem.Models
{
    public class StudentStatusViewModel
    {
        [Required]
        public string StudentName { get; set; } = "";

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }  // Ndryshuar nga string në DateTime

        [Required]
        public string Program { get; set; } = "";

        [Required]
        [Range(1, 8, ErrorMessage = "Semestri duhet të jetë midis 1 dhe 8.")]
        public int Semester { get; set; }  // Ndryshuar nga string në int

        [Required]
        public string AcademicYear { get; set; } = "";  // Nëse ruhet si "2023/2024"
    }
}
