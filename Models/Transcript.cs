using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfServiceStudentSystem.Models
{
    [Table("Transcripts")]
    public class Transcript
    {
        [Key]
        public int TranscriptId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }  // Lidhja me studentin

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }  // Lidhja me lëndën

        [ForeignKey("Semester")]
        public int SemesterId { get; set; }
        public virtual Semester Semester { get; set; }  // Lidhja me semestrin

        public int Grade { get; set; }
        public int Credits { get; set; }
    }
}
