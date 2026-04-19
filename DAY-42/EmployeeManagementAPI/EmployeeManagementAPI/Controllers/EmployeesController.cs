using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require authentication
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Position = e.Position,
                    Salary = e.Salary,
                    HireDate = e.HireDate,
                    IsActive = e.IsActive
                })
                .ToListAsync();

            return Ok(employees);
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound(new { message = $"Employee with ID {id} not found" });

            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Position = employee.Position,
                Salary = employee.Salary,
                HireDate = employee.HireDate,
                IsActive = employee.IsActive
            };

            return Ok(employeeDto);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] CreateEmployeeDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if email already exists
            if (await _context.Employees.AnyAsync(e => e.Email == createDto.Email))
                return BadRequest(new { message = "Email already exists" });

            var employee = new Employee
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email,
                Position = createDto.Position,
                Salary = createDto.Salary,
                HireDate = DateTime.UtcNow,
                IsActive = true
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Position = employee.Position,
                Salary = employee.Salary,
                HireDate = employee.HireDate,
                IsActive = employee.IsActive
            };

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employeeDto);
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto updateDto)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound(new { message = $"Employee with ID {id} not found" });

            // Check email uniqueness if changed
            if (employee.Email != updateDto.Email &&
                await _context.Employees.AnyAsync(e => e.Email == updateDto.Email))
                return BadRequest(new { message = "Email already exists" });

            employee.FirstName = updateDto.FirstName;
            employee.LastName = updateDto.LastName;
            employee.Email = updateDto.Email;
            employee.Position = updateDto.Position;
            employee.Salary = updateDto.Salary;
            employee.IsActive = updateDto.IsActive;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Employee updated successfully" });
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound(new { message = $"Employee with ID {id} not found" });

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}