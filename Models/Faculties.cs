using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfServiceStudentSystem.Models
{
    [Table("Faculties")] // Përcakton emrin e tabelës në databazë
    public class Faculty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Emri i fakultetit nuk mund të jetë më shumë se 100 karaktere.")]
        public string Name { get; set; } = "";

        // Navigation Property - Një fakultet ka shumë programe studimore
        public List<StudyProgram> StudyPrograms { get; set; } = new List<StudyProgram>();
    }
}
