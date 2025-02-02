using Microsoft.AspNetCore.Mvc;
using SelfServiceStudentSystem.Models;
using SelfServiceStudentSystem.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace SelfServiceStudentSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // Shfaq faqen e login (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var student = _context.Students
                .Where(s => s.Email.Trim().ToLower() == email.Trim().ToLower())
                .FirstOrDefault();

            if (student == null || student.Password != password)
            {
                ViewBag.ErrorMessage = "Email ose fjalëkalimi gabim!";
                return View();
            }

            // ✅ Kontrollo nëse ID-ja po ruhet saktë në sesion
            HttpContext.Session.SetInt32("StudentId", student.Id);
            HttpContext.Session.SetString("StudentName", $"{student.FirstName} {student.LastName}");

            // Kontroll për debug:
            Console.WriteLine($"Student ID ruajtur në sesion: {student.Id}");

            return RedirectToAction("Dashboard", "Home");
        }




        [HttpPost]
        public IActionResult ValidateLogin(string email, string password)
        {
            var user = _context.Students.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Email ose fjalëkalimi janë të pasakta!";
                return RedirectToAction("Homepage", "Home"); // Redirect back to Homepage if login fails
            }

            // Save session details
            HttpContext.Session.SetInt32("StudentId", user.Id);
            HttpContext.Session.SetString("StudentName", $"{user.FirstName} {user.LastName}");

            // Redirect to Dashboard if login is successful
            return RedirectToAction("Dashboard", "Home");
        }



        // Funksioni për logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();  // Pastron sesionin
            return RedirectToAction("Login", "Account");  // Redirigjo në login
        }
    }
}
