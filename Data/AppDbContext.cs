using Microsoft.EntityFrameworkCore;
using SelfServiceStudentSystem.Models;
using SelfServiceStudentSystem.Data;  // Shto këtë për të pasur qasje në AppDbContext


namespace SelfServiceStudentSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudyProgram> StudyPrograms { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentEnrollment> StudentEnrollment { get; set; }
        public DbSet<Transcript> Transcripts { get; set; }


    }
}
