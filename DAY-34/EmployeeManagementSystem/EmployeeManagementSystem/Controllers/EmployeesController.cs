using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Data;
using System;
using System.Linq;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Dependency Injection
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees - Main view with all LINQ features
        public async Task<IActionResult> Index(
            string searchName,
            string departmentFilter,
            string sortOrder,
            DateTime? fromDate,
            DateTime? toDate)
        {
            // Start with all employees - LINQ Query
            var employees = _context.Employees.AsQueryable();

            // 1️⃣ SEARCH by name using LINQ
            if (!string.IsNullOrEmpty(searchName))
            {
                employees = employees.Where(e => e.Name.Contains(searchName));
                ViewBag.SearchName = searchName;
            }

            // 2️⃣ FILTER by department using LINQ
            if (!string.IsNullOrEmpty(departmentFilter))
            {
                employees = employees.Where(e => e.Department == departmentFilter);
                ViewBag.DepartmentFilter = departmentFilter;
            }

            // 3️⃣ FILTER by date range using LINQ
            if (fromDate.HasValue)
            {
                employees = employees.Where(e => e.HireDate >= fromDate.Value);
                ViewBag.FromDate = fromDate.Value.ToString("yyyy-MM-dd");
            }
            if (toDate.HasValue)
            {
                employees = employees.Where(e => e.HireDate <= toDate.Value);
                ViewBag.ToDate = toDate.Value.ToString("yyyy-MM-dd");
            }

            // 4️⃣ SORT by salary using LINQ
            ViewBag.SalarySortParam = string.IsNullOrEmpty(sortOrder) ? "salary_desc" : "";
            ViewBag.CurrentSort = sortOrder;

            switch (sortOrder)
            {
                case "salary_desc":
                    employees = employees.OrderByDescending(e => e.Salary);
                    break;
                default:
                    employees = employees.OrderBy(e => e.Salary);
                    break;
            }

            // Get unique departments for filter dropdown
            var departments = await _context.Employees
                .Select(e => e.Department)
                .Distinct()
                .ToListAsync();
            ViewBag.Departments = departments;

            var employeeList = await employees.ToListAsync();

            return View(employeeList);
        }

        // GET: Employees/Statistics - LINQ Aggregation
        public async Task<IActionResult> Statistics()
        {
            var employees = await _context.Employees.ToListAsync();

            // 5️⃣ GROUP by department and calculate aggregates using LINQ
            var departmentGroups = employees
                .GroupBy(e => e.Department)
                .Select(g => new DepartmentGroup
                {
                    Department = g.Key,
                    EmployeeCount = g.Count(),
                    AverageSalary = g.Average(e => e.Salary),
                    Employees = g.ToList()
                })
                .ToList();

            // Calculate overall statistics using LINQ
            var statistics = new StatisticsViewModel
            {
                TotalEmployees = employees.Count(),
                OverallAverageSalary = employees.Any() ? employees.Average(e => e.Salary) : 0,
                HighestPaidEmployee = employees.OrderByDescending(e => e.Salary).FirstOrDefault(),
                DepartmentGroups = departmentGroups
            };

            return View(statistics);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Department,Salary,JobStatus,HireDate,PhoneNumber,Address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Employee created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Department,Salary,JobStatus,HireDate,PhoneNumber,Address")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Employee updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Employee deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}