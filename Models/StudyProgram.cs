using System.ComponentModel.DataAnnotations;

namespace SelfServiceStudentSystem.Models
{
    public class StudyProgram
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Kodi i programit nuk mund të jetë më shumë se 10 karaktere.")]
        public string ProgramCode { get; set; } = "";

        [Required]
        [MaxLength(100, ErrorMessage = "Emri i programit nuk mund të jetë më shumë se 100 karaktere.")]
        public string ProgramName { get; set; } = "";

        [Required]
        [MaxLength(50, ErrorMessage = "Niveli i studimeve nuk mund të jetë më shumë se 50 karaktere.")]
        public string StudyLevel { get; set; } = "";
    }
}
