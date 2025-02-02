using Microsoft.AspNetCore.Mvc;
using SelfServiceStudentSystem.Data;
using SelfServiceStudentSystem.Models;
using System.Linq;

namespace SelfServiceStudentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public IActionResult GetServices()
        {
            var services = _context.Services.ToList();
            return Ok(services);
        }

        // POST: api/Services
        [HttpPost]
        public IActionResult AddService([FromBody] Service service)
        {
            if (service == null)
            {
                return BadRequest("Shërbimi nuk mund të jetë null.");
            }

            _context.Services.Add(service);
            _context.SaveChanges();

            return Ok(service);
        }

        // DELETE: api/Services/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound("Shërbimi nuk u gjet.");
            }

            _context.Services.Remove(service);
            _context.SaveChanges();

            return Ok($"Shërbimi me ID {id} u fshi me sukses.");
        }

        // GET (Simulimi i DELETE): api/Services/DeleteTest/{id}
        [HttpGet("DeleteTest/{id}")]
        public IActionResult DeleteTest(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound("Shërbimi nuk u gjet.");
            }

            _context.Services.Remove(service);
            _context.SaveChanges();

            return Ok($"Shërbimi me ID {id} u fshi me sukses.");
        }


        // PUT: api/Services/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateService(int id, [FromBody] Service updatedService)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound("Shërbimi nuk u gjet.");
            }

            service.ServiceType = updatedService.ServiceType;
            service.Description = updatedService.Description;

            _context.SaveChanges();

            return Ok(service);
        }
    }
}
