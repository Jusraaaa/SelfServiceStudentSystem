namespace SelfServiceStudentSystem.Models
{
    public class TranscriptViewModel
    {
        public string StudentName { get; set; }
        public int StudentId { get; set; }
        public string Program { get; set; }
        public List<SubjectGradeViewModel> Subjects { get; set; }
    }

}
