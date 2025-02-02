using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfServiceStudentSystem.Models;
using SelfServiceStudentSystem.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection;
using iTextSharp.text.pdf.draw;

namespace SelfServiceStudentSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Homepage()
        {
            return View(); // Kthe faqen homepage.cshtml
        }

        public IActionResult Dashboard()
        {
            var studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to Login if the user is not logged in
            }

            var student = _context.Students
                .Include(s => s.StudyProgram)
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound("Studenti nuk u gjet.");
            }

            var model = new DashboardViewModel
            {
                StudentName = $"{student.FirstName} {student.LastName}",
                Program = student.StudyProgram != null ? student.StudyProgram.ProgramName : "No Program Assigned",
                Status = student.Status == 1 ? "Aktiv" : "Inaktiv"
            };

            return View(model);
        }

        public IActionResult PaySemester()
        {
            var studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var student = _context.Students
                .Include(s => s.StudyProgram)
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }

        [HttpPost]
        public IActionResult ConfirmPayment(int studentId, string programType, string semester, decimal amount)
        {
            var student = _context.Students
                .Include(s => s.StudyProgram)
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound();
            }

            // Ruaj të dhënat në ViewBag
            ViewBag.StudentId = student.Id;
            ViewBag.ProgramType = student.StudyProgram != null ? student.StudyProgram.ProgramName : "Nuk ka program";
            ViewBag.Semester = semester;
            ViewBag.Amount = amount;

            var model = new
            {
                StudentId = student.Id,
                ProgramType = ViewBag.ProgramType,
                Semester = semester,
                Amount = amount,
                StudyProgram = student.StudyProgram
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult ProcessPayment(int studentId, string programType, string semester, decimal amount)
        {
            // Pasi të kryhet pagesa, ridrejto te PaymentSuccess me të dhënat
            return RedirectToAction("PaymentSuccess", new { studentId, programType, semester, amount });
        }





        public IActionResult PaymentSuccess(int studentId, string programType, string semester, decimal amount)
        {
            ViewBag.StudentId = studentId;
            ViewBag.ProgramType = programType;
            ViewBag.Semester = semester;
            ViewBag.Amount = amount;

            return View();
        }



        [HttpGet]
        public IActionResult DownloadPdf(int studentId, string programType, string semester, decimal amount)
        {
            var student = _context.Students
                .Include(s => s.StudyProgram)
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound("Studenti nuk u gjet.");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter.GetInstance(document, ms);
                document.Open();

                // 🖼️ Shto logon
                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Properties", "wwwroot", "images", "university-logo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(500, 100); 
                    logo.Alignment = Element.ALIGN_CENTER;
                    logo.SpacingAfter = 10; 
                    document.Add(logo);


                }

                // 📋 Header
                var titleFont = FontFactory.GetFont("Arial", 20, Font.BOLD, new BaseColor(0, 9, 87)); 
                var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.DARK_GRAY);
                var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                Paragraph header = new Paragraph("South East European University", subTitleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20
                };
                document.Add(header);

                Paragraph title = new Paragraph("Fatura e Pagesës", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20
                };
                document.Add(title);

                // 📊 Tabela me të dhëna
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 90;
                table.SpacingBefore = 10;
                table.SpacingAfter = 20;
                table.DefaultCell.Padding = 8; // Padding për qelizat

                // Header i tabelës
                PdfPCell headerCell = new PdfPCell(new Phrase("Detajet e Pagesës", subTitleFont))
                {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(198, 231, 255), // Ngjyra e kaltër e lehtë
                    Padding = 10
                };
                table.AddCell(headerCell);

                // Të dhënat e studentit
                table.AddCell(new Phrase("ID e Studentit:", bodyFont));
                table.AddCell(new Phrase(student.Id.ToString(), bodyFont));

                table.AddCell(new Phrase("Programi:", bodyFont));
                table.AddCell(new Phrase(student.StudyProgram != null ? student.StudyProgram.ProgramName : "Nuk ka program", bodyFont));

                table.AddCell(new Phrase("Semestri:", bodyFont));
                table.AddCell(new Phrase(!string.IsNullOrEmpty(semester) ? semester : "Semestri 1", bodyFont));

                table.AddCell(new Phrase("Shuma për Pagesë:", bodyFont));
                table.AddCell(new Phrase($"{(amount > 0 ? amount : 0)} EUR", bodyFont));

                document.Add(table);

                // 📞 Footer gjithmonë poshtë
                var footerFont = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.GRAY);
                Paragraph footer = new Paragraph("South East European University\nIlindenska 335, 1200 Tetovo\nTel: +389 44 356 000 | www.seeu.edu.mk", footerFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 350 // Shton hapësirë që footeri të jetë gjithmonë poshtë
                };

                document.Add(footer);

                document.Close();

                byte[] bytes = ms.ToArray();
                return File(bytes, "application/pdf", "FaturaPageses.pdf");
            }
        }








        public IActionResult GetTranscript()
        {
            return View();
        }

        public async Task<IActionResult> Transcript(int studentId)
        {
            var transcripts = await _context.Transcripts
                .Include(t => t.Student)
                .Include(t => t.Subject)
                .Include(t => t.Semester)
                .Where(t => t.StudentId == studentId)
                .ToListAsync();

            return View("Transcript", transcripts);
        }


        [HttpGet]
        public IActionResult DownloadTranscriptPdf(int studentId)
        {
            var transcripts = _context.Transcripts
                .Include(t => t.Student)
                .Include(t => t.Subject)
                .Include(t => t.Semester)
                .Where(t => t.StudentId == studentId)
                .ToList();

            if (!transcripts.Any())
            {
                return NotFound("Nuk u gjetën të dhëna për këtë student.");
            }

            var student = transcripts.FirstOrDefault()?.Student;

            using (var ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter.GetInstance(document, ms);
                document.Open();

                // 🖼️ Logoja
                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Properties", "wwwroot", "images", "university-logo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(500, 100);
                    logo.Alignment = Element.ALIGN_CENTER;
                    document.Add(logo);
                }

                // 📋 Titulli
                var titleFont = FontFactory.GetFont("Arial", 20, Font.BOLD, new BaseColor(0, 9, 87));
                var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
                var boldFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);

                Paragraph title = new Paragraph("Transkripta e Notave", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20
                };
                document.Add(title);

                // 📊 Tabela me të dhënat
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 90;
                table.SpacingBefore = 10;
                table.SpacingAfter = 20;

                table.AddCell(new Phrase("Lënda", boldFont));
                table.AddCell(new Phrase("Semestri", boldFont));
                table.AddCell(new Phrase("Nota", boldFont));
                table.AddCell(new Phrase("Kredite", boldFont));

                foreach (var item in transcripts)
                {
                    table.AddCell(new Phrase(item.Subject?.SubjectName ?? "N/A", bodyFont));
                    table.AddCell(new Phrase(item.Semester?.SemesterNumber.ToString() ?? "N/A", bodyFont));
                    table.AddCell(new Phrase(item.Grade.ToString(), bodyFont));
                    table.AddCell(new Phrase(item.Credits.ToString(), bodyFont));
                }

                document.Add(table);

                // 📞 Footer
                var footerFont = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.GRAY);
                Paragraph footer = new Paragraph("South East European University\nIlindenska 335, 1200 Tetovo\nTel: +389 44 356 000 | www.seeu.edu.mk", footerFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 50
                };
                document.Add(footer);

                document.Close();
                return File(ms.ToArray(), "application/pdf", "Transkripta.pdf");
            }
        }











        public IActionResult GetStudentStatus()
        {
            return View();
        }


        public IActionResult StudentStatus()
        {
            var studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account"); 
            }

            var student = _context.Students
                .Include(s => s.StudyProgram)
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound("Studenti nuk u gjet.");
            }

            var model = new StudentStatusViewModel
            {
                StudentName = $"{student.FirstName} {student.LastName}",
                DateOfBirth = student.DateOfBirth,
                Program = student.StudyProgram != null ? student.StudyProgram.ProgramName : "No Program Assigned",
                Semester = student.Status, // Ose përcakto sipas mënyrës që përdor për semestrin
                AcademicYear = $"{DateTime.Now.Year}/{DateTime.Now.Year + 1}"
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult DownloadStudentStatusPdf(string reason)
        {
            var studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var student = _context.Students
                .Include(s => s.StudyProgram)
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound("Studenti nuk u gjet.");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter.GetInstance(document, ms);
                document.Open();

                // Fontet
                var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var titleFont = new Font(baseFont, 16, Font.BOLD, new BaseColor(0, 9, 87));
                var bodyFont = new Font(baseFont, 11, Font.NORMAL, BaseColor.BLACK);
                var boldFont = new Font(baseFont, 11, Font.BOLD, BaseColor.BLACK);
                var smallFont = new Font(baseFont, 8, Font.NORMAL, BaseColor.BLACK);
                
         
                // Logoja
                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Properties", "wwwroot", "images", "university-logo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(500, 100);
                    logo.Alignment = Element.ALIGN_CENTER;
                    document.Add(logo);
                }

                // Vijë horizontale pas logos
                LineSeparator lineTop = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -2);
                document.Add(new Chunk(lineTop));

                // Teksti ligjor
                Paragraph legalNotice = new Paragraph(
                    "Me kërkesë të studentit, në bazë të nenit 102 të Ligjit për procedurë të përgjithshme administrative.\n" +
                    "At the request of the student, pursuant to Article 102 of the Law on General Administrative Procedure.",
                    smallFont
                )
                {
                    Alignment = Element.ALIGN_LEFT,
                    SpacingAfter = 15
                };
                document.Add(legalNotice);

                // Titulli
                Paragraph title = new Paragraph("Vërtetim | Certificate", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20
                };
                document.Add(title);

                // Të dhënat e studentit
                Paragraph content = new Paragraph
                {
                    Alignment = Element.ALIGN_JUSTIFIED
                };

                content.Add(new Phrase("Universiteti i Evropës Juglindore vërteton se ", bodyFont));
                content.Add(new Phrase($"{student.FirstName} {student.LastName}", boldFont));
                content.Add(new Phrase(", i lindur më ", bodyFont));
                content.Add(new Phrase($"{student.DateOfBirth:dd.MM.yyyy}", boldFont));
                content.Add(new Phrase(", është student i rregullt në vitin akademik ", bodyFont));
                content.Add(new Phrase($"{DateTime.Now.Year}/{DateTime.Now.Year + 1}", boldFont));
                content.Add(new Phrase(", i regjistruar në semestrin ", bodyFont));
                content.Add(new Phrase($"Semestri {student.Status}", boldFont));
                content.Add(new Phrase(", në programin e studimeve ", bodyFont));
                content.Add(new Phrase($"{student.StudyProgram?.ProgramName ?? "Nuk ka program"}.\n\n", boldFont));

                content.Add(new Phrase("South East European University certifies that ", bodyFont));
                content.Add(new Phrase($"{student.FirstName} {student.LastName}", boldFont));
                content.Add(new Phrase(", born on ", bodyFont));
                content.Add(new Phrase($"{student.DateOfBirth:dd.MM.yyyy}", boldFont));
                content.Add(new Phrase(", is a full-time student in the academic year ", bodyFont));
                content.Add(new Phrase($"{DateTime.Now.Year}/{DateTime.Now.Year + 1}", boldFont));
                content.Add(new Phrase(", enrolled in semester ", bodyFont));
                content.Add(new Phrase($"{student.Status}", boldFont));
                content.Add(new Phrase(", in the study program ", bodyFont));
                content.Add(new Phrase($"{student.StudyProgram?.ProgramName ?? "No Program"}.\n\n", boldFont));

                content.Add(new Phrase($"Arsyeja | Reason: ", bodyFont));
                content.Add(new Phrase($"{reason}\n\n", boldFont));

                document.Add(content);

                // Data e Lëshimit
                Paragraph dateInfo = new Paragraph($"Data e Lëshimit: {DateTime.Now:dd.MM.yyyy}", bodyFont)
                {
                    Alignment = Element.ALIGN_RIGHT,
                    SpacingBefore = 20
                };
                document.Add(dateInfo);

                // Vijë horizontale para Këshillit Juridik
                LineSeparator lineBottom = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -2);
                document.Add(new Chunk(lineBottom));

                // Këshilla Juridike
                Paragraph legalAdvice = new Paragraph(
                    "Këshillë Juridike: Kundër kësaj certifikate mund të parashtrohet ankesë tek Shërbimet Studentore brenda 8 ditëve nga dorëzimi i certifikatës.\n" +
                    "Legal Advice: An appeal can be filed against this certificate at the Student Services within 8 days from the date of receipt.",
                    smallFont
                )
                {
                    Alignment = Element.ALIGN_LEFT,
                    SpacingAfter = 10
                };
                document.Add(legalAdvice);

                // Nënshkrimi me tekstin e kërkuar
                Paragraph signLabel = new Paragraph("Shërbime Studentore | Student Services:", boldFont)
                {
                    Alignment = Element.ALIGN_LEFT,
                    SpacingAfter = 5
                };
                document.Add(signLabel);

                string signaturePath = Path.Combine(Directory.GetCurrentDirectory(), "Properties", "wwwroot", "images", "signature.png");

                if (System.IO.File.Exists(signaturePath))
                {
                    iTextSharp.text.Image signature = iTextSharp.text.Image.GetInstance(signaturePath);
                    signature.ScaleToFit(120, 60);
                    signature.Alignment = Element.ALIGN_LEFT;
                    document.Add(signature);
                }

                // 📞 Footer gjithmonë poshtë
                var footerFont = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.GRAY);

                // Llogaritja dinamike për pozicionin e footer-it
                float spaceToBottom = document.BottomMargin - 20; // Shmang hapësirën e tepërt që shkakton faqe të dytë

                Paragraph footer = new Paragraph(
                    "South East European University\nIlindenska 335, 1200 Tetovo\nTel: +389 44 356 000 | www.seeu.edu.mk",
                    footerFont
                )
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = spaceToBottom
                };

                document.Add(footer);


                document.Close();
                byte[] bytes = ms.ToArray();
                return File(bytes, "application/pdf", "Vertetimi_Statusit.pdf");

            }
        }





    }
}
