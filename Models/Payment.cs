using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfServiceStudentSystem.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }  // Navigation Property

        [Required]
        [Range(1, 8, ErrorMessage = "Semestri duhet të jetë midis 1 dhe 8.")]
        public int Semester { get; set; }  // Ndryshuar nga string në int

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}
