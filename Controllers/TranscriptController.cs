using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfServiceStudentSystem.Data;
using SelfServiceStudentSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SelfServiceStudentSystem.Controllers
{
    public class TranscriptController : Controller
    {
        private readonly AppDbContext _context;

        public TranscriptController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int studentId)
        {
            if (studentId <= 0)
            {
                return BadRequest("ID e studentit është e pavlefshme.");
            }

            try
            {
                var transcripts = await _context.Transcripts
                    .Include(t => t.Student)
                    .Include(t => t.Subject)
                    .Include(t => t.Semester)
                    .Where(t => t.StudentId == studentId)
                    .ToListAsync();

                if (transcripts == null || !transcripts.Any())
                {
                    return NotFound("Nuk u gjetën transkripta për këtë student.");
                }

                return View(transcripts);
            }
            catch (Exception ex)
            {
                return Content($"Gabim gjatë marrjes së të dhënave: {ex.Message}");
            }
        }
    }
}
