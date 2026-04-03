using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student/Index - List all students
        public async Task<IActionResult> Index(string searchString)
        {
            var students = from s in _context.Students
                           select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString) ||
                                              s.StudentId.ToString().Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(await students.ToListAsync());
        }

        // GET: Student/Details/5 - View student details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create - Show create form
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create - Add new student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Phone,Course,AdmissionDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existingStudent = await _context.Students
                    .FirstOrDefaultAsync(s => s.Email == student.Email);

                if (existingStudent != null)
                {
                    ModelState.AddModelError("Email", "This email is already registered");
                    return View(student);
                }

                _context.Add(student);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Student added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5 - Show edit form
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5 - Update student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,Name,Email,Phone,Course,AdmissionDate")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if email exists for another student
                    var existingStudent = await _context.Students
                        .FirstOrDefaultAsync(s => s.Email == student.Email && s.StudentId != student.StudentId);

                    if (existingStudent != null)
                    {
                        ModelState.AddModelError("Email", "This email is already registered to another student");
                        return View(student);
                    }

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Student updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Delete/5 - Show delete confirmation
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5 - Delete student
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Student deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}