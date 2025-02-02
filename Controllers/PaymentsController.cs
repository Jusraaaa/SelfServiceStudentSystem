using Microsoft.AspNetCore.Mvc;
using SelfServiceStudentSystem.Data;
using SelfServiceStudentSystem.Models;

namespace SelfServiceStudentSystem.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        // Ky veprim do të shfaqë listën e pagesave
        public IActionResult Index()
        {
            var payments = _context.Payments.ToList(); // Merr listën e pagesave nga database
            return View(payments); // Kthen View me modelin e pagesave
        }

        // Ky veprim do të shfaqë një formë për të shtuar pagesa të reja
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Shfaq formën për krijim
        }

        // Ky veprim ruan pagesën e re në database
        [HttpPost]
        public IActionResult Create(Payment payment)
        {
            if (ModelState.IsValid) // Kontrollon nëse të dhënat janë valide
            {
                payment.PaymentDate = DateTime.Now; // Vendos datën aktuale për pagesën
                _context.Payments.Add(payment); // Shton pagesën në database
                _context.SaveChanges(); // Ruaj ndryshimet
                return RedirectToAction("Index"); // Kthehet në listën e pagesave
            }
            return View(payment); // Nëse ka gabime, shfaq formën me gabimet
        }
    }
}
