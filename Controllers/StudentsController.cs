using Microsoft.AspNetCore.Mvc;
using SelfServiceStudentSystem.Data;
using SelfServiceStudentSystem.Models;
using System;
using System.Linq;

namespace SelfServiceStudentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _context.Students.ToList();
            if (students == null || !students.Any())
            {
                return NotFound("Nuk ka asnjë student në sistem.");
            }
            return Ok(students);
        }

        // POST: api/Students
        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Studenti nuk mund të jetë null.");
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            // CreatedAtAction për të kthyer statusin e suksesit dhe linkun për të marrë studentin
            return CreatedAtAction(nameof(GetStudents), new { id = student.Id }, student);
        }

        // DELETE: api/Students/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound("Studenti nuk u gjet.");
            }

            _context.Students.Remove(student);
            _context.SaveChanges();

            return Ok($"Studenti me ID {id} u fshi me sukses.");
        }

        // PUT: api/Students/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound("Studenti nuk u gjet.");
            }

            // Përditësojmë të dhënat
            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.Email = updatedStudent.Email;
            student.Password = updatedStudent.Password;

            _context.SaveChanges();

            return Ok(student);
        }
    }
}
